using System;
using System.Windows.Forms;

namespace AADSecuredWebApiCallInvoker
{
    public partial class ApiTestUtility : Form
    {
        public ApiTestUtility()
        {
            InitializeComponent();
        }

        private void Invoke_Click(object sender, EventArgs e)
        {
            ServiceInvokerFacade facade = new ServiceInvokerFacade();
            var response =  facade.TestServiceRequestUrl();
            //var result = response.Result;
            displaypanel.Text =
                $"Response Code - {response.StatusCode} ," + Environment.NewLine + Environment.NewLine+ 
                $"Response Status - {response.IsSuccessStatusCode} , " + Environment.NewLine + Environment.NewLine +
                $"Response Content - { response.Content.ReadAsStringAsync().Result}" ;
        }
    }
}
