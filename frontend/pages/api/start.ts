import  type  {  NextApiRequest,  NextApiResponse  }  from  "next";

export  default  async  function  handler(req:  NextApiRequest,  res:  NextApiResponse)  {
    const  {  goal  }  =  req.body;

    const  response  =  await  fetch(process.env.ORCHESTRATOR_URL  +  "/api/start-workflow",  {
        method:  "POST",
        body:  JSON.stringify({  goal  }),
        headers:  {  "Content-Type":  "application/json"  },
    });

    const  text  =  await  response.text();
    const  id  =  text.replace("Started  workflow:  ",  "");

    res.status(200).json({  workflowId:  id  });
}
