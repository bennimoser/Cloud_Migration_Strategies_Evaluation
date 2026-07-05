using System.Data.Entity;
using System.Web.Http;
using ERP.Data;
using ERP.Data.Migrations;

namespace ERP.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<ErpContext, Configuration>());

            using (var ctx = new ErpContext())
            {
                ctx.Database.Initialize(force: false);
            }

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
