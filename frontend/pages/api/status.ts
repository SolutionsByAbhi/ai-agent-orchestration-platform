import  type  {  NextApiRequest,  NextApiResponse  }  from "next";

export  default  async  function  handler(req:  NextApiRequest,  res:  NextApiResponse)  {
    const  {  id  }  =  req.query;

    const  response  =  await  fetch(`${process.env.ORCHESTRATOR_URL}/api/status/${id}`);
    const  json  =  await  response.json();

    res.status(200).json(json);
}
