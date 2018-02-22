using System.Web.Mvc;

namespace UniversityWeb.Areas.UniversityHome
{
    public class UniversityHomeAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "UniversityHome";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            //context.MapRoute(
            //    "Home_default",
            //    "Home/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional },
            //    new[] { "UniversityWeb.Areas.Home.Controllers" }
            //);
        }
    }
}
