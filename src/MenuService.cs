using EasyConsole;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.Extensions.Logging;

namespace ConsoleApp
{
    internal class MenuService : BackgroundService
    {
        Option1 Option1;
        Option2 Option2;
        ILogger<MenuService> logger;
        MakeHttpCallWithActivityOption makeHttpCallOptionWithActivityOption;
        MakeHttpCallWithActivityAndTelemetryClientOption makeHttpCallWithActivityAndTelemetryClientOption;
        public MenuService(
            ILogger<MenuService> logger,
            Option1 opt1,
            Option2 opt2, 
            MakeHttpCallWithActivityOption makeHttpCallOptionWithActivityOption,
            MakeHttpCallWithActivityAndTelemetryClientOption makeHttpCallWithActivityAndTelemetryClientOption)
        {
            Option1 = opt1;
            Option2 = opt2;
            this.makeHttpCallOptionWithActivityOption = makeHttpCallOptionWithActivityOption;
            this.makeHttpCallWithActivityAndTelemetryClientOption = makeHttpCallWithActivityAndTelemetryClientOption;
            this.logger = logger;
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                var menu = new Menu()
                    .Add("Menu option 1", async (token) => await Option1.Execute())
                    .Add("Menu option 2 - Throws exception", async (token) => await Option2.Execute())
                    .Add("Menu option 3 - HttpRequest with Activity", async (token) => await makeHttpCallOptionWithActivityOption.Execute())
                    .Add("Menu option 3 - HttpRequest with Activity and telemetry client", async (token) => await makeHttpCallWithActivityAndTelemetryClientOption.Execute())
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