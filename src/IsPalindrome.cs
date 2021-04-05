using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
namespace StringTools
{
    public class IsPalindrome
    {
        public async Task<APIGatewayProxyResponse> Handler(APIGatewayProxyRequest lambdaEvent, ILambdaContext context)
        {
            context.Logger.LogLine($"Body {lambdaEvent.Body}.");
            var source = JsonConvert.DeserializeObject<Root>(lambdaEvent.Body).Source;
            context.Logger.LogLine($"Source {source}.");

            var reverseArray = source.ToCharArray();
            Array.Reverse(reverseArray);

            var reverse = new string(reverseArray);
            context.Logger.LogLine($"Source {reverse}.");

            var isPalindrome = source == reverse;
        
            return new APIGatewayProxyResponse
            {
                Body = JsonConvert.SerializeObject(new Response() { result = isPalindrome ? $"{source} is a Palindrome" : $"{source} is not a Palindrome" }),
                StatusCode = 200
            };

        }
    }

    public class Root
    {
        [JsonProperty("source")]
        public string Source { get; set; }
    }

    public class Response
    {
        [JsonProperty("result")]
        public string result { get; set; }
    }
}
