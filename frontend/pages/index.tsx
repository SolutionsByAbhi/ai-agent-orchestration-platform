import  {  useState  }  from  "react";
import  {  startWorkflow  } from  "../lib/api";
import  WorkflowCard  from  "../components/WorkflowCard";

export  default  function  Home()  {
    const  [goal,  setGoal]  =  useState("");
    const  [workflowId,  setWorkflowId]  =  useState("");

    const  handleStart  =  async  ()  =>  {
        const  id  =  await  startWorkflow(goal);
        setWorkflowId(id);
    };

    return  (
        <div  className="container">
            <h1>AI  Agent  Console</h1>

            <textarea
                placeholder="Enter  your  goal..."
                value={goal}
                onChange={(e)  =>  setGoal(e.target.value)}
            />

            <button  onClick={handleStart}>Start  Workflow</button>

            {workflowId  &&  <WorkflowCard  id={workflowId}  />}
        </div>
    );
}

