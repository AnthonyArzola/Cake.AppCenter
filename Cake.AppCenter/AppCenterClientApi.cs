using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

using Cake.AppCenter.Response;
using RestSharp;
using Newtonsoft.Json;

namespace Cake.AppCenter
{
    public class AppCenterClientApi
    {
        private const string API_BASE_URL = "https://api.appcenter.ms/v0.1";

        private const string HEADER_API_TOKEN = "X-API-Token";
        private const string HEADER_CONTENT_TYPE = "Content-Type";
        private const string HEADER_ACCEPT = "Accept";

        private const string CONTENT_JSON = "application/json";

        private readonly string API_TOKEN;
        private readonly IRestClient _restClient;

        public AppCenterClientApi(string apiToken)
        {
            _restClient = new RestClient(API_BASE_URL);
            API_TOKEN = apiToken;
        }

        /// <summary>
        /// Creates a new app and returns it to the caller.
        /// </summary>
        /// <returns>Returns tuple. First </returns>
        public async Task<(bool success, AppResponse response)> CreateApp(AppSettings settings)
        {
            var request = new RestRequest("/apps", Method.POST);

            request.AddHeader(HEADER_API_TOKEN, API_TOKEN);
            request.AddHeader(HEADER_CONTENT_TYPE, CONTENT_JSON);
            request.AddHeader(HEADER_ACCEPT, CONTENT_JSON);
            request.RequestFormat = DataFormat.Json;

            var json = JsonConvert.SerializeObject(settings);
            request.AddParameter(CONTENT_JSON, json, ParameterType.RequestBody);

            (bool success, AppResponse response) result;

            try
            {
                var response = await _restClient.ExecuteTaskAsync(request);

                if (response.IsSuccessful && response.StatusCode == HttpStatusCode.Created)
                {
                    result = (true, JsonConvert.DeserializeObject<AppResponse>(response.Content));
                }
                else
                {
                    // Attempt extracting error message
                    var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);
                    Console.WriteLine(
                        $"Unable to create app. Error is: '{errorResponse.Error.Message}', code {errorResponse.Error.Code}");

                    result = (false, null);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Unable to create app. Exception is: {exception.Message}");
                result = (false, null);
            }

            return result;
        }

        /// <summary>
        /// Retrieves a list of available apps.
        /// </summary>
        /// <returns>List of apps.</returns>
        public async Task<(bool success, List<AppResponse> response)> GetApps()
        {
            var request = new RestRequest("/apps", Method.GET);

            request.AddHeader(HEADER_API_TOKEN, API_TOKEN);

            (bool success, List<AppResponse> response) result;

            try
            {
                var response = await _restClient.ExecuteTaskAsync(request);

                if (response.IsSuccessful && response.StatusCode == HttpStatusCode.OK)
                {
                    result = (true, JsonConvert.DeserializeObject<List<AppResponse>>(response.Content));
                }
                else
                {
                    // Attempt extracting error message
                    var errorResponse = JsonConvert.DeserializeObject<ErrorDetails>(response.Content);
                    Console.WriteLine(
                        $"Unable to retrieve apps. Error is: '{errorResponse.Message}', code {errorResponse.Code}");

                    result = (false, null);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Unable to retrieve apps. Exception is: {exception.Message}");
                result = (false, null);
            }

            return result;
        }

        /// <summary>
        /// Deletes an app.
        /// </summary>
        /// <param name="appName">Name of the app.</param>
        /// <param name="ownerName">Name of th owner.</param>
        /// <returns>Boolean indicating if request was successful.</returns>
        public async Task<bool> DeleteApp(string appName, string ownerName)
        {
            var request = new RestRequest($"/apps/{ownerName}/{appName}", Method.DELETE);

            request.AddHeader(HEADER_API_TOKEN, API_TOKEN);

            var result = false;

            try
            {
                var response = await _restClient.ExecuteTaskAsync(request);
                if (response.IsSuccessful && response.StatusCode == HttpStatusCode.NoContent)
                {
                    result = true;
                }
                else
                {
                    // Attempt extracting error message
                    var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);
                    Console.WriteLine($"Unable to delete app. Error is: '{errorResponse.Error.Message}', code {errorResponse.Error.Code}");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Unable to delete app. Exception is: {exception.Message}");
            }

            return result;
        }

    }
}