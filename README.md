# AzureADSecuredWebApiCallInvoker
This is test utility to invoke Web Api service call , which is secured through Azure active directory authentication. 
It supports two types of authentication :
  a.) Certificate based authentication (X509 Certificate)
  b.) Azure AD secret key based authentication.
  
  Currently in this code it always look for the certificate for which Store Location is CurrentUser.
