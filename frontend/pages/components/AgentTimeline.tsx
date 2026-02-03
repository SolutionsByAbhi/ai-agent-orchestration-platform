export  default  function  AgentTimeline({  status  })  {
    const  steps  =  [
        "Planner",
        "Researcher",
        "Summarizer",
        "Executor"
    ];

    return  (
        <div  className="timeline">
            {steps.map((step)  =>  (
                <div  key={step}  className={`step  ${status.current  ===  step  ?  "active"  :  ""}`}>
                    {step}
                </div>
            ))}
        </div>
    );
}
