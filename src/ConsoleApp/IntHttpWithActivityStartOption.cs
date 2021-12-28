using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class IntHttpWithActivityStartOption
    {
        private const string RequestUri = "http://localhost:5170/api/circle/areaOf/4";
        IDependency dependency;
        ILogger<IntHttpWithActivityStartOption> logger;
        public IntHttpWithActivityStartOption(IDependency dep, ILogger<IntHttpWithActivityStartOption> logger)
        {
            this.dependency = dep;
            this.logger = logger;
        }
        async internal Task Execute()
        {
            using (new Activity(nameof(IntHttpWithActivityStartOption)).Start())
            {
                logger.LogInformation($"Calling the locally hosted API using {RequestUri}. Correct the port number as that is env specific");
                string result = await new HttpClient().GetStringAsync(RequestUri);
                logger.LogInformation($"Area of circle with r=4: {result}");
            }
        }
    }
}