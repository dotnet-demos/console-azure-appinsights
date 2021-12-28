using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class MakeHttpCallWithActivitySourceAndTelemetryClientOption
    {
        IDependency dependency;
        ILogger<MakeHttpCallWithActivitySourceAndTelemetryClientOption> logger;
        TelemetryClient tc;
        static ActivitySource option2Activity = new ActivitySource(nameof(ConsoleApp));

        public MakeHttpCallWithActivitySourceAndTelemetryClientOption(IDependency dep,
                                                                TelemetryClient tc,
                                                                ILogger<MakeHttpCallWithActivitySourceAndTelemetryClientOption> logger)
        {
            this.dependency = dep;
            this.logger = logger;
            this.tc = tc;
        }
        async internal Task Execute()
        {
            using (Activity activity = option2Activity.StartActivity(nameof(MakeHttpCallWithActivitySourceAndTelemetryClientOption)))
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