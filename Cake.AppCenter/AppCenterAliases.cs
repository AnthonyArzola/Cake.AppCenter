using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

using Cake.AppCenter.Response;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.Diagnostics;

namespace Cake.AppCenter
{
    /// <summary>
    /// Extends Cake functionality to support interaction with AppCenter.
    /// </summary>
    [CakeAliasCategory("AppCenter")]
    public static class AppCenterAliases
    {
        public static string ENV_VAR_API_TOKEN = "APPCENTER_API_TOKEN";

        /// <summary>
        /// Create new app.
        /// </summary>
        /// <param name="context">Cake context.</param>
        /// <param name="apiToken">AppCenter API token.</param>
        /// <param name="settings">App settings object.</param>
        /// <returns>Tuple with success flag and app response object if applicable.</returns>
        [CakeMethodAlias]
        public static async Task<(bool success, AppResponse response)> CreateApp(this ICakeContext context, string apiToken, AppSettings settings)
        {
            var token = apiToken ?? context.Environment.GetEnvironmentVariable(ENV_VAR_API_TOKEN);

            // Guard against missing required properties
            if (string.IsNullOrEmpty(token))
                throw new NoNullAllowedException($"AppCenter API token is required. Pass in or set at {ENV_VAR_API_TOKEN}");
            if (string.IsNullOrEmpty(settings.Name))
                throw new NoNullAllowedException("App name is required.");
            if (string.IsNullOrEmpty(settings.DisplayName))
                throw new NoNullAllowedException("App display name is required.");

            var appCenterClient = new AppCenterClientApi(token);
            var result = await appCenterClient.CreateApp(settings);

            if (!result.success)
                context.Log.Error("Error creating app.");

            return result;
        }

        /// <summary>
        /// Returns a list of apps.
        /// </summary>
        /// <param name="context">Cake context.</param>
        /// <param name="apiToken">AppCenter API token.</param>
        /// <returns>Tuple with success flag and list of apps if applicable.</returns>
        [CakeMethodAlias]
        public static async Task<(bool success, List<AppResponse> response)> GetApps(this ICakeContext context, string apiToken)
        {
            var token = apiToken ?? context.Environment.GetEnvironmentVariable(ENV_VAR_API_TOKEN);

            // Guard against missing required properties
            if (string.IsNullOrEmpty(token))
                throw new NoNullAllowedException($"AppCenter API token is required. Pass in or set at {ENV_VAR_API_TOKEN}");

            var appCenterClient = new AppCenterClientApi(token);
            var result = await appCenterClient.GetApps();

            if (!result.success)
                context.Log.Error("Error retrieving apps.");

            return result;
        }

        [CakeMethodAlias]
        public static async Task<bool> DeleteApp(this ICakeContext context, string apiToken, string appName, string ownerName)
        {
            var token = apiToken ?? context.Environment.GetEnvironmentVariable(ENV_VAR_API_TOKEN);

            // Guard against missing required properties
            if (string.IsNullOrEmpty(token))
                throw new NoNullAllowedException($"AppCenter API token is required. Pass in or set at {ENV_VAR_API_TOKEN}");
            if (string.IsNullOrEmpty(appName) || string.IsNullOrEmpty(ownerName))
                throw new NoNullAllowedException("App and Owner name are required.");

            var appCenterClient = new AppCenterClientApi(token);
            var result = await appCenterClient.DeleteApp(appName, ownerName);

            if (!result)
                context.Log.Error($"Error deleting '{appName}'");

            return result;
        }
    }
}