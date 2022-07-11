using System;
using System.Linq;

using IdentityModel.OidcClient;

using Microsoft.AspNetCore.Components.Authorization;

namespace MyApplicationMud.Services;

public record MauiRedirectToLoginHandler(
    OidcClient OidcClient,
    IState<LoginState> LoginState,
    IDispatcher Dispatcher
) : IRedirectToLoginHandler
{
    public async Task Redirect(string? returnUrl = null)
    {
        if (LoginState.Value.IsAuthenticated)
        {
            return;
        }

        var result = await OidcClient.LoginAsync(new LoginRequest());

#if WINDOWS //Bring window to front on windows after logon
        if (Application.Current is not null)
        {
            var currentWindow = Application.Current.Windows[0].Handler.PlatformView;
            var nativeWindowHandle = WinRT.Interop.WindowNative.GetWindowHandle(currentWindow);
            var win32WindowsId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(nativeWindowHandle);
            var winuiAppWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(win32WindowsId);
            if (winuiAppWindow.Presenter is Microsoft.UI.Windowing.OverlappedPresenter p)
            {
                p.IsAlwaysOnTop = true;
                p.IsAlwaysOnTop = false;
            }
        }
#endif

        if (result.IsError)
        {
            //TODO: Error handling
            Dispatcher.DispatchUserLoggedOut();
            return;
        }

        var user = User.FromClaimsPrincipal(result.User);

        if (result.User?.Identity?.IsAuthenticated == true)
        {
            await SecureStorage.SetAsync(OidcConsts.AccessTokenKeyName, result.AccessToken);
            await SecureStorage.SetAsync(OidcConsts.RefreshTokenKeyName, result.RefreshToken);
            Dispatcher.DispatchUserLoggedIn(user);
            return;
        }

        Dispatcher.DispatchUserLoggedOut();
        return;
    }
}
