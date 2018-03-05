Cake.AppCenter
===========

[Cake](https://cakebuild.net/) add-in that facilitates communication with Microsoft’s [AppCenter API](https://docs.microsoft.com/en-us/appcenter/).

## Getting Started

1. Clone repo
2. Update `API_TOKEN` and `OWNER_NAME` string constants within ApiTests.cs with actual values
2. Build solution
3. Reference Cake.AppCenter.dll in your build.cake script
```cake
#reference "localtools/Cake.AppCenter.dll"
```

## Supports

1. Retrieving [list](https://openapi.appcenter.ms/#/account/apps_list) of apps (`GET /v0.1/apps`)
2. [Create](https://openapi.appcenter.ms/#/account/apps_create) new app (`POST /v0.1/apps`)
3. [Deleting](https://openapi.appcenter.ms/#/account/apps_delete) app (`DELETE /v0.1/apps/{owner_name}/{app_name}`)

## References
* Cake.Core v0.26.1
* Humanizer v2.2.0
* NewtonSharp v10.0.3
* RestSharp v106.0.1

## Sample

To return a list of apps, call `GetApps` in your build.cake script:
```csharp
Task("GetApps")
  .Does(() =>
{

  (bool success, List<Cake.AppCenter.Response.AppResponse> appResponse) result = GetApps("YOUR_API_TOKEN");

  if (result.success)
  {
    foreach (var app in result.appResponse)
    {
      Information(app.Name);
    }
  }
  else {
    Warning("Unable to retrieve apps.");
  }
});
```
