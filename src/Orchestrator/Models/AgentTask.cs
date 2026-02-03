namespace  Orchestrator.Models;

public  class  AgentTask
{
        public  string  WorkflowId  {  get;  set;  }  =  default!;
        public  string  TaskId  {  get;  set;  }  =  Guid.NewGuid().ToString();
        public  string  AgentType  {  get;  set;  }  =  default!;
        public  string  Payload  {  get;  set;  }  =  default!;
}
