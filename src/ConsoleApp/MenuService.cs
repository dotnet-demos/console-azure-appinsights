using EasyConsole;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace ConsoleApp
{
    internal class MenuService : BackgroundService
    {
        Option1 Option1;
        Option2 Option2;
        ILogger<MenuService> logger;
        MakeHttpCallWithActivityOption makeHttpCallOptionWithActivityOption;
        MakeHttpCallWithActivityStartOption makeHttpCallOptionWithActivityStartOption;
        NestedWithActivityOption nestedWithActivityOption;
        MakeHttpCallWithActivityAndTelemetryClientOption makeHttpCallWithActivityAndTelemetryClientOption;
        IntHttpWithActivityStartOption  intHttpWithActivityStartOption;
        public MenuService(
            ILogger<MenuService> logger,
            Option1 opt1,
            Option2 opt2, 
            MakeHttpCallWithActivityOption makeHttpCallOptionWithActivityOption,
            MakeHttpCallWithActivityStartOption makeHttpCallOptionWithActivityStartOption,

            NestedWithActivityOption nestedWithActivityOption,
            MakeHttpCallWithActivityAndTelemetryClientOption makeHttpCallWithActivityAndTelemetryClientOption,
            IntHttpWithActivityStartOption intHttpWithActivityStartOption
            )
        {
            Option1 = opt1;
            Option2 = opt2;
            this.nestedWithActivityOption = nestedWithActivityOption;
            this.makeHttpCallOptionWithActivityStartOption=makeHttpCallOptionWithActivityStartOption;
            this.makeHttpCallOptionWithActivityOption = makeHttpCallOptionWithActivityOption;
            this.makeHttpCallWithActivityAndTelemetryClientOption = makeHttpCallWithActivityAndTelemetryClientOption;

            this.intHttpWithActivityStartOption = intHttpWithActivityStartOption;
            this.logger = logger;
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Activity.DefaultIdFormat = ActivityIdFormat.W3C;
            try
            {
                var menu = new Menu()
                    .Add("Menu option 1", async (token) => await Option1.Execute())
                    .Add("Throws exception", async (token) => await Option2.Execute())
                    .Add("HttpRequest with Activity", async (token) => await makeHttpCallOptionWithActivityOption.Execute())
                    .Add("HttpRequest with Activity.Start", async (token) => await makeHttpCallOptionWithActivityStartOption.Execute())
                    .Add("Nested with Activity.Start", async (token) => await nestedWithActivityOption.Execute())
                    .Add("HttpRequest with Activity and telemetry client", async (token) => await makeHttpCallWithActivityAndTelemetryClientOption.Execute())

                    .Add("IntHttp with Activity.Start", async (token) => await intHttpWithActivityStartOption.Execute())

                    .AddSync("Exit", () => Environment.Exit(0));
                await menu.Display(CancellationToken.None);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception occured in an option. Continuing..");
            }
            await base.StartAsync(stoppingToken);
        }
    }
}