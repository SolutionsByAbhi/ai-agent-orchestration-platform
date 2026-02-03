using  Azure.AI.OpenAI;
using  Microsoft.Azure.Functions.Worker;
using  Microsoft.Extensions.Logging;
using  Orchestrator.Models;
using  Shared;
using  System.Text.Json;

namespace  PlannerAgent;

public  class  PlannerFunction
{
        private  readonly  OpenAIClient  _client;
        private  readonly  string  _deployment;

        public  PlannerFunction()
        {
                _client  =  OpenAIClientFactory.CreateFromEnvironment();
                _deployment  =  OpenAIClientFactory.GetDeploymentName();
        }

        [Function("PlannerAgent")]
        public  async  Task<AgentResult>  Run(
                [ActivityTrigger]  AgentTask  task,
                FunctionContext  context)
        {
                var  logger  =  context.GetLogger("PlannerAgent");
                logger.LogInformation("PlannerAgent  received  goal:  {Goal}",  task.Payload);

                var  chat  =  new  ChatCompletionsOptions
                {
                        Messages  =
                        {
                                new  ChatMessage(ChatRole.System,
                                        "You  are  a  planning  agent.  Break  the  user's  goal  into  3-5  concrete  research  tasks  in  JSON  array  form."),
                                new  ChatMessage(ChatRole.User,  task.Payload)
                        }
                };

                var  response  =  await  _client.GetChatCompletionsAsync(_deployment,  chat);
                var  content  =  response.Value.Choices[0].Message.Content;

                return  new  AgentResult
                {
                        WorkflowId  =  task.WorkflowId,
                        TaskId  =  task.TaskId,
                        AgentType  =  task.AgentType,
                        Result  =  content
                };
        }
}
