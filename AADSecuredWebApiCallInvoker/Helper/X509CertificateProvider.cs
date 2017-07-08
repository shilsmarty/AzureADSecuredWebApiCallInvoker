using System;
using System.Security.Cryptography.X509Certificates;

namespace AADSecuredWebApiCallInvoker.Helper
{
    public class X509CertificateProvider
    {
            private readonly StoreName _certStoreName;
            private readonly StoreLocation _certStoreLocation;

            public X509CertificateProvider(StoreName certStoreName, StoreLocation certStoreLocation)
            {
                _certStoreName = certStoreName;
                _certStoreLocation = certStoreLocation;
            }

            public X509Certificate2 GetCertificateByThumbprint(string thumbprint)
            {
                var certStore = new X509Store(_certStoreName, _certStoreLocation);

                try
                {
                    certStore.Open(OpenFlags.ReadOnly);
                    var certResult = certStore.Certificates.Find(X509FindType.FindByThumbprint, thumbprint,
                        true);

                    if (certResult?.Count == 0)
                    {
                        throw new ArgumentException(
                            string.Format("Certificate with thumbprint {0} not found in StoreName: {1}, StoreLocation:{2}",
                                thumbprint, _certStoreName, _certStoreLocation), nameof(thumbprint));
                    }

                    return certResult[0];
                }
                finally
                {
                    certStore.Close();
                }
            }
        }
    }
