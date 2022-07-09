using Android.App;
using Android.Content;
using Android.Content.PM;

namespace MyApplicationMud.Platforms.Android;

[Activity(NoHistory = true, LaunchMode = LaunchMode.SingleTop, Exported = true)]
[IntentFilter(new[] { Intent.ActionView },
              Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
              DataScheme = callbackScheme)]
public class AuthenticatorCallbackActivity : WebAuthenticatorCallbackActivity
{
    private const string callbackScheme = "my-application-mud";
}
