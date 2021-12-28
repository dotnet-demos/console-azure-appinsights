using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class MakeHttpCallWithActivitySourceOption
    {
        IDependency dependency;
        ILogger<MakeHttpCallWithActivitySourceOption> logger;
        static ActivitySource option2Activity = new ActivitySource(nameof(ConsoleApp));
        public MakeHttpCallWithActivitySourceOption(IDependency dep, ILogger<MakeHttpCallWithActivitySourceOption> logger)
        {
            this.dependency = dep;
            this.logger = logger;
        }
        async internal Task Execute()
        {
            using (option2Activity.StartActivity(nameof(MakeHttpCallWithActivitySourceOption)))
            {
                string result = await new HttpClient().GetStringAsync("https://uploadbeta.com/api/pi/?cached&n=10");
                logger.LogInformation($"Value of pi from API: {result}");
            }
        }
    }
}