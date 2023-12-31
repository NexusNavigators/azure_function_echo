using System;
using System.IO;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace azure_function_echo
{
    public class logInput
    {
        private readonly ILogger _logger;

        public logInput(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<logInput>();
        }

        [Function("logInput")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req
        ) {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            string body = await new StreamReader(req.Body).ReadToEndAsync();
            _logger.LogInformation(body);

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Welcome to Azure Functions!");

            return response;
        }
    }
}
