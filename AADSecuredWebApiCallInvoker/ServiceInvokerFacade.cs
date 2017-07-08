using System.Net.Http;
using AADSecuredWebApiCallInvoker.Helper;

namespace AADSecuredWebApiCallInvoker
{
    public class ServiceInvokerFacade : DaemonClientSetup
    {
        public HttpResponseMessage TestServiceRequestUrl()
        {
            string dummyInputParemeter = "DummyInputparameter";
            SetUpClient(Constants.CredentialType.Certificate);
            ServiceInvoker.HttpClient = DaemonClientSetup.HttpClient;
            return new ServiceInvoker().GetResponse(dummyInputParemeter);
        }
    }
}
