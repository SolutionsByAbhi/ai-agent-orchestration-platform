import  {  useRouter  }  from  "next/router";
import  {  useEffect,  useState  }  from  "react";
import  {  getWorkflowStatus  }  from  "../../lib/api";
import  AgentTimeline  from  "../../components/AgentTimeline";
import  TaskList  from  "../../components/TaskList";
import  ResearchResults  from  "../../components/ResearchResults";
import  SummaryBox  from  "../../components/SummaryBox";
import  FinalOutput  from  "../../components/FinalOutput";

export  default  function  WorkflowPage()  {
    const  router  =  useRouter();
    const  {  id  }  =  router.query;

    const  [status,  setStatus]  =  useState<any>(null);

    useEffect(()  =>  {
        if  (!id)  return;

       const  interval  =  setInterval(async  ()  =>  {
            const  s  =  await  getWorkflowStatus(id  as  string);
            setStatus(s);
        },  1500);

        return  ()  =>  clearInterval(interval);
    },  [id]);

    if  (!status)  return  <div>Loading...</div>;

    return  (
        <div  className="workflow-container">
            <h2>Workflow:  {id}</h2>

            <AgentTimeline  status={status}  />
            <TaskList  tasks={status.tasks}  />
            <ResearchResults  results={status.research}  />
            <SummaryBox  summary={status.summary}  />
            <FinalOutput  output={status.final}  />
        </div>
    );
}
