using System;
using Microsoft.Identity.Client;

namespace ipcam
{
    public static class Constants
    {
        public static string Tenant = "familienpoulsen.onmicrosoft.com";
        public static string ClientID = "e74b4a34-3f28-4a7f-abe3-885be13ef14e";
        public static string[] Scopes = { "email" };
        public static string SignUpSignInPolicy = "B2C_1_EmailSignup";
        public static string ResetPasswordPolicy = "<INSERT_ADB2C_POLICY_NAME_HERE>";
        public static string AuthorityBase = $"https://login.microsoftonline.com/tfp/{Tenant}/";
        public static string Authority = $"{AuthorityBase}{SignUpSignInPolicy}";
        public static UIParent UiParent = null;
        public static string RedirectUri = $"msal{Tenant}://auth";
    }
}
