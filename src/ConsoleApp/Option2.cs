using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class Option2
    {
        IDependency dependency;
        ILogger<Option2> logger;
        static ActivitySource option2Activity = new ActivitySource(nameof(ConsoleApp));
        public Option2(IDependency dep, ILogger<Option2> logger)
        {
            dependency = dep;
            this.logger = logger;
        }
        async internal Task Execute()
        {
            using (option2Activity.StartActivity(nameof(Option2)))
            {
                using (logger.BeginScope($"Scope {nameof(Option2)}"))
                {
                    logger.LogTrace($"Start");
                    await Task.Delay(1);
                    logger.LogInformation($"Value from {nameof(IDependency)} is '{ dependency.Foo()}'");
                    logger.LogInformation("going to throw exception");
                    throw new Exception("Test exception to simulate");
                }
            }
        }
    }
            }