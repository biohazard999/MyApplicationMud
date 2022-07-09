using Javax.Net.Ssl;

using Xamarin.Android.Net;

namespace MyApplicationMud.Platforms.Android;

public sealed class CustomAndroidMessageHandler : AndroidMessageHandler
{
    protected override IHostnameVerifier GetSSLHostnameVerifier(HttpsURLConnection connection)
        => new CustomHostnameVerifier();

    private sealed class CustomHostnameVerifier : Java.Lang.Object, IHostnameVerifier
    {
        //HttpsURLConnection.DefaultHostnameVerifier.Verify(hostname, session)
        // || (hostname == "10.0.2.2" && session.PeerPrincipal.Name == "CN=localhost");
        //TODO: Verfiy hostname
        public bool Verify(string? hostname, ISSLSession? session)
            => true;
    }
}
