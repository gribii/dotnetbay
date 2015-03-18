using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DotNetBay.Core.Execution;
using DotNetBay.Data.EF;
using DotNetBay.WebApp.App_Start;

namespace DotNetBay.WebApp
{
    public class MvcApplication : HttpApplication
    {
        public static IAuctionRunner AuctionRunner { get; private set; }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AutoMapperConfig.Configure();

            var mainRepository = new EFMainRepository();
            mainRepository.SaveChanges();

            AuctionRunner = new AuctionRunner(mainRepository);
            AuctionRunner.Start();
        }
    }
}