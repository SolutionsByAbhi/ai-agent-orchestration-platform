
 #  🤖  **AI Agent  Orchestration  Platform**   
 ###  *A  production‑grade, event‑driven,  multi‑agent  AI  system built  on  Azure  Functions, Durable  Workflows,  and  Azure OpenAI  —  with  a real‑time  Agent  Console  UI.*

 This  repository  implements a  **cloud‑native,  multi‑agent  orchestration platform**  designed  for  complex reasoning,  research,  summarization,  and task  execution.    
It  mirrors  the  architectural patterns  used  inside  **Microsoft AI  Foundry**,  **Azure  AI**, and  **Copilot  engineering  teams** —  combining  **agentic  AI**, **event‑driven  workflows**,  and  a **Next.js  visualization  console**.
 
It’s  a  **full  platform** that  demonstrates:
 
 - Distributed  agent  execution   
 -  Durable  orchestration   
 -  Queue‑based task  routing    
-  Azure  OpenAI‑powered  reasoning   
 -  Real‑time workflow  visualization    
-  Production‑ready  IaC  and CI/CD    

 ---
 
 # 🌟  **Key  Features**
 
##  🔹  **Multi‑Agent  Architecture**
Four  specialized  agents,  each with  tailored  system  prompts:

 -  **PlannerAgent**  — breaks  down  user  goals into  atomic  tasks   
 -  **ResearchAgent**  — gathers  structured  findings   
 -  **SummarizerAgent**  — synthesizes  insights    
-  **ExecutorAgent**  —  produces polished  final  output   
 
 Each  agent runs  as  an  **independent Azure  Function  App**,  enabling horizontal  scaling  and  isolation.

 ---
 
 ## 🔹  **Durable  Orchestration**
 The **Orchestrator**  uses  Azure  Durable Functions  to:
 
 - Start  workflows    
-  Dispatch  tasks  to agents    
 - Wait  for  results   
 -  Aggregate  outputs   
 -  Produce final  deliverables    

 This  enables  **long‑running, stateful  AI  workflows**  with full  reliability.
 
 ---

 ##  🔹  **Event‑Driven Communication**
 Agents  communicate  through:

 -  **Azure  Storage Queues**    
 - **Event  Grid  notifications**   
 -  **Durable  Functions activities**    
 
This  architecture  supports  **parallelism**, **fault  tolerance**,  and  **loose coupling**.
 
 ---
 
##  🔹  **Azure  OpenAI Integration**
 Each  agent  uses Azure  OpenAI  with  **role‑specific prompts**:
 
 -  Planning   
 -  Research   
 -  Summarization   
 -  Execution   
 
 Prompts are  stored  in  version‑controlled files  for  transparency  and iteration.
 
 ---
 
##  🔹  **Agent  Console (Next.js  Frontend)**
 A  beautiful, real‑time  UI  for:
 
-  Starting  workflows   
 -  Viewing  agent progress    
 - Inspecting  planner  tasks   
 -  Viewing  research findings    
 - Reading  summaries    
-  Seeing  final  output   
 
 This console  makes  the  system feel  like  a  **Copilot debugging  dashboard**.
 
 ---

 ##  🔹  **Infrastructure‑as‑Code (Bicep)**
 The  `infra/`  folder deploys:
 
 -  Function Apps    
 - Storage  Queues    
-  Event  Grid   
 -  Azure  OpenAI configuration    
 - App  settings    
-  Networking  (optional)   
 
 Everything  is reproducible  and  cloud‑ready.
 
---
 
 ##  🔹 **CI/CD  with  GitHub  Actions**
Automated  workflows  for:
 
-  Build  &  test   
 -  Deploy Functions    
 - Validate  Bicep  templates   
 
 This  ensures a  **production‑grade  engineering  workflow**.

 ---
 
 # 🧱  **Repository  Structure**
 
```
 ai-agent-orchestration-platform/
 ├──  infra/                                      #  Azure  infrastructure (Bicep)
 ├──  src/
 │     ├──  Orchestrator/                   #  Durable orchestrator
 │     ├──  Agents/                              #  Planner, Researcher,  Summarizer,  Executor
 │     ├──  Api/                                   # Public  API  entrypoint
 │     └──  Shared/                             #  Shared  utilities
 ├── frontend/                                  # Next.js  Agent  Console
 ├── tests/                                       #  Automated tests
 └──  .github/workflows/                 #  CI/CD  pipelines
 ```

 This  structure  mirrors Microsoft’s  internal  engineering  patterns for  multi‑service  AI  systems.

 ---
 
 # 🧠  **How  It  Works**

 ##  1️⃣  User submits  a  goal   
 Through  the  API or  the  Agent  Console UI.
 
 ##  2️⃣ PlannerAgent  breaks  the  goal into  tasks    
Stored  as  structured  JSON.

 ##  3️⃣  ResearchAgent executes  each  task   
 Findings  are  collected independently.
 
 ##  4️⃣ SummarizerAgent  synthesizes  insights   
 Produces  a  crisp, human‑friendly  summary.
 
 ## 5️⃣  ExecutorAgent  generates  final output    
 A polished  deliverable  tailored  to the  user’s  intent.
 
##  6️⃣  Agent  Console visualizes  everything    
Real‑time  updates  show:
 
-  Current  agent   
 -  Task  list   
 -  Research findings    
 - Summary    
 - Final  output    

 ---
 
 # 🚀  **Getting  Started**
 
##  1.  Deploy  infrastructure

 ```bash
 cd  infra
az  deployment  sub  create \
     --location westeurope  \
    --template-file  main.bicep  \
    --parameters  environmentName=dev
 ```

 ##  2.  Configure Function  Apps    
Copy  `appsettings.sample.json`  into  each Function  App’s  configuration.
 
##  3.  Run  backend locally
 
 ```bash
 cd src/Orchestrator
 func  start
 ```

 ##  4.  Run frontend  locally
 
 ```bash
cd  frontend
 npm  install
npm  run  dev
 ```

 ##  5.  Start a  workflow    
From  the  UI  or via  API:
 
 ```bash
curl  -X  POST  https://<api>/api/start-workflow \
     -H "Content-Type:  application/json"  \
    -d  '{  "goal": "Create  a  3-day  Azure AI  learning  plan"  }'
```
 
 ---
 
#  🎨  **Agent  Console Preview**
 
 The  UI includes:
 
 -  Workflow timeline    
 - Agent  progress  indicators   
 -  Planner  task list    
 - Research  result  viewer   
 -  Summary  panel   
 -  Final output  panel    

 It  feels  like a  **Copilot  engineering  dashboard**.

 ---
 
 # 🔐  **Security  &  Governance**

 This  platform  follows Microsoft  best  practices:
 
-  No  secrets  in code    
 - Managed  identities  supported   
 -  Azure  OpenAI keys  stored  in  Key Vault    
 - Principle  of  least  privilege   
 -  Isolated Function  Apps    

 ---
 
 # 🎯  **Why  This  Project Stands  Out**
 
 This repository  demonstrates:
 
 - Agentic  AI  design   
 -  Distributed  systems thinking    
 - Durable  orchestration    
-  Azure  OpenAI  mastery   
 -  Cloud‑native engineering    
 - Frontend  +  backend  integration   
 -  IaC +  CI/CD  discipline   
 
