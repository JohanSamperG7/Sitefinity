using System;
using System.Web;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Localization;

namespace SitefinityWebApp
{
    public class Global : HttpApplication
    {
        private void Bootstrapper_Bootstrapped(object sender, EventArgs e)
        {
            Res.RegisterResource<ResourcesLbl.LabelTest>();
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            Bootstrapper.Bootstrapped += this.Bootstrapper_Bootstrapped;
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}