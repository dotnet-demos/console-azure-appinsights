
class Calculator
{
    private const string RequestUri = "https://uploadbeta.com/api/pi/?cached&n=3";
    ILogger<Calculator> logger;
    public Calculator(ILogger<Calculator> logger) =>
        this.logger = logger;

    internal async Task<double> AreaOfCircle(int radius)
    {
        string result = await new HttpClient().GetStringAsync(RequestUri);
        double valueOfPi = double.Parse(result.Trim('"'))/100;
        logger.LogInformation($"Got the value of pi as {valueOfPi} from {RequestUri}");
        return valueOfPi * radius * radius;
    }
}