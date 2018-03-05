using System.Threading.Tasks;

using Cake.AppCenter.Enums;
using Cake.AppCenter.Response;

using Xunit;
using Xunit.Abstractions;

namespace Cake.AppCenter.Tests
{
    public class ApiTests
    {
        //TODO: Replace with your API token and owner name
        private const string API_TOKEN = "";
        private const string OWNER_NAME = "";
        private readonly ITestOutputHelper OutputHelper;

        public ApiTests(ITestOutputHelper output)
        {
            OutputHelper = output;
        }

        [Fact]
        public void Test_GetApps_Valid_Api_Token()
        {
            var client = new AppCenterClientApi(API_TOKEN);
            var result = client.GetApps();

            Assert.True(result.success);

            if (result.success)
            {
                foreach (var appResponse in result.response)
                {
                    OutputHelper.WriteLine($"App '{appResponse.Name}' ({appResponse.OS}) - owned by {appResponse.Owner.Name}");
                }
            }
        }

        [Fact]
        public void Test_GetApps_Invalid_Api_Token()
        {
            var client = new AppCenterClientApi(API_TOKEN + "a");
            var (success, response) = client.GetApps();

            Assert.True(!success);
        }

        [Fact]
        public void Test_CreateApp_With_Invalid_Fields()
        {
            var client = new AppCenterClientApi(API_TOKEN);

            var appSettings = new AppSettings()
            {
                Name = "Space in the name",
                DisplayName = "TestApp Display",
                Description = "TestApp Desciption",
                OS = OperatingSystem.Windows.ToString(),
                Platform = Platform.UWP.ToString()
            };

            var (success, response) = client.CreateApp(appSettings);

            Assert.False(success);
        }

        [Fact]
        public void Test_CreateApp_With_Valid_Fields()
        {
            var client = new AppCenterClientApi(API_TOKEN);

            var appSettings = new AppSettings()
            {
                Name = "TestAppName",
                DisplayName = "TestAppDisplay",
                Description = "TestAppDesciption",
                OS = OperatingSystem.Windows.ToString(),
                Platform = Platform.UWP.ToString()
            };

            var createAppTask = new Task<(bool, AppResponse)>(() => client.CreateApp(appSettings));
            createAppTask.Start();

            Task<bool> deleteAppTask = createAppTask.ContinueWith<bool>(r =>
            {
                Assert.True(r.Result.Item1);
                Assert.True(!string.IsNullOrEmpty(r.Result.Item2.AppSecret));

                return client.DeleteApp(appSettings.Name, OWNER_NAME);
            });

            Assert.True(deleteAppTask.Result);
        }
       
    }
}