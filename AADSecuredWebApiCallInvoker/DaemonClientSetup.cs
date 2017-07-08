using System;
using System.Configuration;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using AADSecuredWebApiCallInvoker.Helper;

namespace AADSecuredWebApiCallInvoker
{
    public abstract class DaemonClientSetup
    {
        //
        // The Client ID is used by the application to uniquely identify itself to Azure AD.
        // The App Key is a credential used by the application to authenticate to Azure AD.
        // The Tenant is the name of the Azure AD tenant in which this application is registered.
        // The AAD Instance is the instance of Azure, for example public Azure or Azure China.
        // The Authority is the sign-in URL of the tenant.
        //
        private static readonly string AadInstance = ConfigurationManager.AppSettings["ida:AADInstance"];
        private static readonly string Tenant = ConfigurationManager.AppSettings["ida:Tenant"];
        private static readonly string ClientId = ConfigurationManager.AppSettings["ida:ClientId"];
        private static readonly string AppKey = ConfigurationManager.AppSettings["ida:AppKey"];
        private static readonly string CertificateThumbPrint = ConfigurationManager.AppSettings["ClientThumbprint"];
        static readonly string Authority = String.Format(CultureInfo.InvariantCulture, AadInstance, Tenant);

        //
        // To authenticate to the service, the client needs to know the service's App ID URI.
        // To contact the service we need it's URL as well.
        //
        private static readonly string TodoListResourceId = ConfigurationManager.AppSettings["TargetServiceResourceId"];


        public static HttpClient HttpClient = new HttpClient();
        private static AuthenticationContext _authContext = null;

        public HttpResponseMessage Response = null;

        private ClientAssertionCertificate GetCertificateReference()
        {
            var certProvider = new X509CertificateProvider(StoreName.My, StoreLocation.CurrentUser);
            var certX509 = certProvider.GetCertificateByThumbprint(CertificateThumbPrint);
            ClientAssertionCertificate cer = new ClientAssertionCertificate(ClientId, certX509);
            return cer;
        }

        public void SetUpClient(Constants.CredentialType type)
        {
            _authContext = new AuthenticationContext(Authority);
            //
            // Get an access token from Azure AD using client credentials.
            // If the attempt to get a token fails because the server is unavailable, retry twice after 3 seconds each.
            //
            AuthenticationResult result = null;
            int retryCount = 0;
            bool retry;
            do
            {
                retry = false;
                try
                {
                    if (type == Constants.CredentialType.Certificate)
                    {
                        var certAssertion = GetCertificateReference();
                        result = _authContext.AcquireTokenAsync(TodoListResourceId, certAssertion).Result;
                        // ADAL includes an in memory cache, so this call will only send a message to the server if the cached token is expired.
                    }
                    else
                    {
                        var certAssertion = new ClientCredential(ClientId, AppKey);
                        result = _authContext.AcquireTokenAsync(TodoListResourceId, certAssertion).Result;
                    }
                    // ADAL includes an in memory cache, so this call will only send a message to the server if the cached token is expired.
                }
                catch (AdalException ex)
                {
                    if (ex.ErrorCode == "temporarily_unavailable")
                    {
                        retry = true;
                        retryCount++;
                        Thread.Sleep(3000);
                    }
                }
            } while ((retry == true) && (retryCount < 3));


            HttpClient.DefaultRequestHeaders.Accept.Clear();
            HttpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
                );
            // Add the access token to the authorization header of the request.
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
        }
    }
}
