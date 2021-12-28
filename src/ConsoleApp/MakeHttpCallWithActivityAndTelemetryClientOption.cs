using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class MakeHttpCallWithActivityAndTelemetryClientOption
    {
        IDependency dependency;
        ILogger<MakeHttpCallWithActivityAndTelemetryClientOption> logger;
        TelemetryClient tc;

        public MakeHttpCallWithActivityAndTelemetryClientOption(IDependency dep,
                                                                TelemetryClient tc,
                                                                ILogger<MakeHttpCallWithActivityAndTelemetryClientOption> logger)
        {
            this.dependency = dep;
            this.logger = logger;
            this.tc = tc;
        }
        async internal Task Execute()
        {
            using (Activity activity = new Activity(nameof(MakeHttpCallWithActivityAndTelemetryClientOption)).Start())
            {
                
                using (tc.StartOperation<RequestTelemetry>(activity))
                {
                    string result = await new HttpClient().GetStringAsync("https://uploadbeta.com/api/pi/?cached&n=10");
                    logger.LogInformation($"Value of pi from API: {result}");
                }
            }
        }
    }
}