using System.ComponentModel;

namespace Cake.AppCenter.Enums
{
    public enum Platform
    {
        Java = 0,
        [Description("Objective-C-Swift")]
        ObjectiveC_Swift,
        UWP,
        Cordova,
        [Description("React-Native")]
        ReactNative,
        Xamarin
    }

    public enum OperatingSystem
    {
        Android = 0,
        iOS,
        macOS,
        Tizen,
        tvOS,
        Windows
    }

    public enum ErrorCode
    {
        BadRequest = 0,
        Conflict,
        NotAcceptable,
        NotFound,
        InternalServerError,
        Unauthorized,
        TooManyRequests
    }

    public enum Origin
    {
        [Description("mobile-center")]
        mobile_center=0,
        hockeyapp,
        codepush
    }

    public enum OwnerType
    {
        Org = 0,
        User
    }

    public enum MemberType
    {
        Manager = 0,
        Developer,
        Viewer,
        Tester
    }

}