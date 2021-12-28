using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class NestedWithActivityOption
    {
        IDependency dependency;
        ILogger<NestedWithActivityOption> logger;
        public NestedWithActivityOption(IDependency dep, ILogger<NestedWithActivityOption> logger)
        {
            this.dependency = dep;
            this.logger = logger;
        }
        async internal Task Execute()
        {
            using (new Activity(nameof(NestedWithActivityOption)).Start())
            {
                double valueOfPi = 0;
                using (new Activity("Inner Activity 1 - Get Value Of Pi ").Start())
                {
                    string result = await new HttpClient().GetStringAsync("https://uploadbeta.com/api/pi/?cached&n=10");
                    valueOfPi = double.Parse(result.Trim('"'));
                    logger.LogInformation($"Value of pi from API: {result}");
                }
                using (new Activity("Inner Activity 2 - Area of circle with r=2 ").Start())
                {
                    logger.LogInformation($"Area of circle with r=2: {valueOfPi * (2 ^ 2)}");
                }
            }
        }
    }
}