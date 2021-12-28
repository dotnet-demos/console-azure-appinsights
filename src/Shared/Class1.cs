using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace Shared
{
    public class CloudRoleNameTelemetryInitializer : ITelemetryInitializer
    {
        private readonly string roleName;

        public CloudRoleNameTelemetryInitializer(string roleName)
        {
            this.roleName = roleName ?? throw new ArgumentNullException(nameof(roleName));
        }
        public void Initialize(ITelemetry telemetry)
        {
            if (string.IsNullOrEmpty(telemetry?.Context?.Cloud?.RoleName))
            {
                //set custom role name here
                telemetry.Context.Cloud.RoleName = this.roleName;
            }
        }
    }
}