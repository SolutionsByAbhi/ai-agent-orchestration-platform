using  Azure;
using  Azure.AI.OpenAI;

namespace  Shared;

public  static  class  OpenAIClientFactory
{
        public  static  OpenAIClient  CreateFromEnvironment()
        {
                var  endpoint  =  Environment.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT")
                                            ??  throw  new  InvalidOperationException("AZURE_OPENAI_ENDPOINT  not  set");
                var  key  =  Environment.GetEnvironmentVariable("AZURE_OPENAI_KEY")
                                    ??  throw  new  InvalidOperationException("AZURE_OPENAI_KEY  not  set");

                return  new  OpenAIClient(new  Uri(endpoint),  new  AzureKeyCredential(key));
        }

        public  static  string  GetDeploymentName()
        {
                return  Environment.GetEnvironmentVariable("AZURE_OPENAI_DEPLOYMENT")
                              ??  throw  new  InvalidOperationException("AZURE_OPENAI_DEPLOYMENT  not  set");
        }
}
