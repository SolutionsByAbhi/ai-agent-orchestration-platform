#  🤖  AI  Agent  Orchestration  Platform  (Azure  +  Azure  OpenAI)

A  production-style  **multi-agent  orchestration  platform**  built  on:

-  **Azure  Functions**  (isolated  agents  +  orchestrator)
-  **Durable  Functions**  (long-running  workflows)
-  **Azure  Storage  Queues**  (task  routing)
-  **Event  Grid**  (agent-to-agent  signaling)
-  **Azure  OpenAI**  (reasoning,  planning,  summarization)

This  repo  demonstrates  how  to  build  **agentic  AI  systems**  the  way  a  Microsoft  AI  Foundry  or  Azure  AI  team  would:    
event-driven,  observable,  modular,  and  cloud-native.

---

##  🧠  Agent  architecture

Agents:

-  **PlannerAgent**  –  breaks  a  user  goal  into  structured  tasks
-  **ResearchAgent**  –  calls  external  APIs  /  tools  and  gathers  context
-  **SummarizerAgent**  –  condenses  results  into  human-friendly  output
-  **ExecutorAgent**  –  produces  final  actionable  output  (e.g.,  plan,  email,  report)

The  **Orchestrator**:

-  Accepts  a  user  request
-  Calls  Planner  →  dispatches  tasks  to  agents
-  Waits  for  results  via  queues/events
-  Aggregates  and  returns  final  response

---

##  🏗️  Tech  stack

-  Azure  Functions  (.NET  8  isolated)
-  Durable  Functions
-  Azure  Storage  Queues
-  Azure  Event  Grid
-  Azure  OpenAI
-  Bicep  for  infra
-  GitHub  Actions  for  CI/CD

---

##  🚀  Quick  start

1.  **Deploy  infra**

```bash
cd  infra
az  deployment  sub  create  \
    --location  westeurope  \
    --template-file  main.bicep  \
    --parameters  \
        environmentName=dev  \
        location=westeurope
