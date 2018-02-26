Cake.AppCenter
===========

Cake add-in that facilitates communication with Microsoft’s AppCenter API.

## Getting Started

1. Clone repo
2. Update `API_TOKEN` and `OWNER_NAME` string constants within ApiTests.cs with actual values
2. Build solution
3. Reference Cake.AppCenter.dll in your build.cake script
```cake
#reference "localtools/Cake.AppCenter.dll"
```

## Supports

1. Retrieving list of apps (`GET /v0.1/apps`)
2. Adding new app (`POST /v0.1/apps`)
3. Deleting app (`DELETE /v0.1/apps/{owner_name}/{app_name}`)

## Sample

To return a list of apps, in your build.cake script:
```csharp
var result = Cake.AppCenter.GetApps("API_TOKEN");
```
