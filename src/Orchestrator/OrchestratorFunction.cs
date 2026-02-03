using  System.Net;
using  Microsoft.Azure.Functions.Worker;
using  Microsoft.Azure.Functions.Worker.Http;
using  Microsoft.DurableTask;
using  Microsoft.DurableTask.Client;
using  Orchestrator.Models;
using  Shared;
using  System.Text.Json;

namespace  Orchestrator;

public  class  OrchestratorFunction
{
        [Function("OrchestrateWorkflow")]
        public  async  Task<string>  RunOrchestrator(
                [OrchestrationTrigger]  TaskOrchestrationContext  context)
        {
                var  input  =  context.GetInput<string>()  ??  "";
                var  workflowId  =  context.InstanceId;

                //  1.  Ask  Planner  to  break  down  the  goal
                var  plannerTask  =  new  AgentTask
                {
                        WorkflowId  =  workflowId,
                        AgentType  =  AgentType.Planner.ToString(),
                        Payload  =  input
                };

                var  plannerResult  =  await  context.CallActivityAsync<AgentResult>(
                        "DispatchPlannerTask",  plannerTask);

                //  2.  Use  planner  output  to  create  research  tasks
                var  researchTasks  =  await  context.CallActivityAsync<List<AgentTask>>(
                        "CreateResearchTasks",  plannerResult);

                var  researchResults  =  new  List<AgentResult>();
                foreach  (var  task  in  researchTasks)
                {
                        var  result  =  await  context.CallActivityAsync<AgentResult>(
                                "DispatchResearchTask",  task);
                        researchResults.Add(result);
                }

                //  3.  Summarize
                var  summary  =  await  context.CallActivityAsync<AgentResult>(
                        "DispatchSummarizerTask",
                        new  AgentTask
                        {
                                WorkflowId  =  workflowId,
                                AgentType  =  AgentType.Summarizer.ToString(),
                                Payload  =  JsonSerializer.Serialize(researchResults)
                        });

                //  4.  Final  executor  step
                var  final  =  await  context.CallActivityAsync<AgentResult>(
                        "DispatchExecutorTask",
                        new  AgentTask
                        {
                                WorkflowId  =  workflowId,
                                AgentType  =  AgentType.Executor.ToString(),
                                Payload  =  summary.Result
                        });

                return  final.Result;
        }

        [Function("StartOrchestration")]
        public  async  Task<HttpResponseData>  Start(
                [HttpTrigger(AuthorizationLevel.Function,  "post",  Route  =  "start-workflow")]  HttpRequestData  req,
                [DurableClient]  DurableTaskClient  client)
        {
                var  body  =  await  new  StreamReader(req.Body).ReadToEndAsync();
                var  json  =  JsonDocument.Parse(body);
                var  goal  =  json.RootElement.GetProperty("goal").GetString()  ??  "";

                var  instanceId  =  await  client.ScheduleNewOrchestrationInstanceAsync(
                        "OrchestrateWorkflow",  goal);

                var  response  =  req.CreateResponse(HttpStatusCode.Accepted);
                await  response.WriteStringAsync($"Started  workflow:  {instanceId}");
                return  response;
        }
}
