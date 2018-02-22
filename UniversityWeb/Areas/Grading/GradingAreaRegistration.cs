using System.Web.Mvc;

namespace UniversityWeb.Areas.Grading
{
    public class GradingAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Grading";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Grading_default",
                "Grading/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
