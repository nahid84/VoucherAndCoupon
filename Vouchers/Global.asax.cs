using System.Web.Http;

namespace Dominos.OLO.Vouchers
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            UnityConfig.RegisterComponents();
        }
    }
}
