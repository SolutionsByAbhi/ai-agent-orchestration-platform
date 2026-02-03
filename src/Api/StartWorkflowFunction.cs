using  System.Net;
using  Microsoft.Azure.Functions.Worker;
using  Microsoft.Azure.Functions.Worker.Http;
using  Microsoft.DurableTask.Client;

namespace  Api;

public  class  StartWorkflowFunction
{
        [Function("StartWorkflowApi")]
        public  async  Task<HttpResponseData>  Run(
                [HttpTrigger(AuthorizationLevel.Function,  "post",  Route  =  "start-workflow")]  HttpRequestData  req,
                [DurableClient]  DurableTaskClient  client)
        {
                var  body  =  await  new  StreamReader(req.Body).ReadToEndAsync();
                var  instanceId  =  await  client.ScheduleNewOrchestrationInstanceAsync(
                        "OrchestrateWorkflow",  body);

                var  response  =  req.CreateResponse(HttpStatusCode.Accepted);
                await  response.WriteStringAsync($"Started  workflow:  {instanceId}");
                return  response;
        }
}
