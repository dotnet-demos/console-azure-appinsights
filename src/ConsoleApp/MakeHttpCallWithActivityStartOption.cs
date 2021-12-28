using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class MakeHttpCallWithActivityStartOption
    {
        IDependency dependency;
        ILogger<MakeHttpCallWithActivityStartOption> logger;
        public MakeHttpCallWithActivityStartOption(IDependency dep, ILogger<MakeHttpCallWithActivityStartOption> logger)
        {
            this.dependency = dep;
            this.logger = logger;
        }
        async internal Task Execute()
        {
            using (new Activity(nameof(MakeHttpCallWithActivityStartOption)).Start())
            {
                string result = await new HttpClient().GetStringAsync("https://uploadbeta.com/api/pi/?cached&n=10");
                logger.LogInformation($"Value of pi from API: {result}");
            }
        }
    }
}