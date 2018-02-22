using System.Web;
using System.Web.Optimization;
using System.Collections.Generic;

namespace UniversityWeb
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            //BundleTable.EnableOptimizations = true;
            /****************DEFAULT BUNDLE CONFIGURATION THAT CAME WITH ASP.NET MVC************/
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
            //            "~/Scripts/jquery-ui-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.unobtrusive*",
            //            "~/Scripts/jquery.validate*"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            //bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
            //            "~/Content/themes/base/jquery.ui.core.css",
            //            "~/Content/themes/base/jquery.ui.resizable.css",
            //            "~/Content/themes/base/jquery.ui.selectable.css",
            //            "~/Content/themes/base/jquery.ui.accordion.css",
            //            "~/Content/themes/base/jquery.ui.autocomplete.css",
            //            "~/Content/themes/base/jquery.ui.button.css",
            //            "~/Content/themes/base/jquery.ui.dialog.css",
            //            "~/Content/themes/base/jquery.ui.slider.css",
            //            "~/Content/themes/base/jquery.ui.tabs.css",
            //            "~/Content/themes/base/jquery.ui.datepicker.css",
            //            "~/Content/themes/base/jquery.ui.progressbar.css",
            //            "~/Content/themes/base/jquery.ui.theme.css"));

            /****************DEFAULT BUNDLE CONFIGURATION THAT CAME WITH ASP.NET MVC************/

            ///***************BASE BUNDLES****************/
            List<string> baseScripts = new List<string> {
                "~/Format/js/ua.utilities.js",
                "~/Format/js/libs/datejs.cultureinfodefault-debug.js",
                "~/Format/js/ua.localize.js",
                "~/Format/js/ua.appdata.js",
                "~/Format/js/libs/datejs.core-debug.js",
                "~/Format/js/libs/datejs.parser-debug.js",
                "~/Format/js/libs/datejs.sugarpak-debug.js",
                "~/Format/js/ua.utilities.formatting.js",
                "~/Format/js/ua.app.js",
                "~/Format/js/ua.utilities.js",
                "~/Format/js/libs/jquery-ui-1.11.4.custom.js", 
                "~/Format/js/libs/jquery.simplemodal.custom.js",
                "~/Format/js/libs/jquery.qtip.js",
                "~/Format/js/libs/jquery.validate.js",
                "~/Format/js/libs/jquery.transit.js",
                "~/Format/js/libs/jquery.dotdotdot.js",
                "~/Format/js/libs/inView.js",
                "~/Format/js/libs/jquery.validate.unobtrusive.custom.js",
                "~/Format/js/ua.validation.js",
                "~/Format/js/libs/handlebars.js",
                "~/Format/js/libs/spin.js",
                "~/Format/js/libs/jquery.uniform.js",
                "~/Format/js/libs/jquery.infieldlabel.custom.js",
                "~/Format/js/libs/bootstrap-alert.js",
                /*"~/Format/js/libs/jquery.history.js",*/
                "~/Format/js/common/ua.header.js",
                "~/Format/js/common/ua.footer.js",
                "~/Format/js/ui/ua.loader.js",
                "~/Format/js/libs/typeahead.custom.js",
                "~/Format/js/ui/ua.autocomplete.js",
                "~/Format/js/ui/ua.dropdown.js",
                "~/Format/js/ui/ua.uniform.js",
                "~/Format/js/ui/ua.tooltip.js",
                "~/Format/js/ui/ua.datepicker.js",
                "~/Format/js/ui/ua.infieldlabel.js",
                "~/Format/js/ui/ua.stepper.js",
                "~/Format/js/ui/ua.toggler.js",
                "~/Format/js/ui/ua.modal.js",
                "~/Format/js/common/ua.airportlookup.js",
                "~/Format/js/common/ua.rulesengine.js",
                 "~/Format/js/common/ua.sessiontimeout.js",
                "~/Format/js/common/ua.beta.js"
            };

            //CORE BUNDLE
            List<string> coreScripts = new List<string>();
            coreScripts.Add("~/Format/js/libs/jquery-2.1.4.js");
            coreScripts.Add("~/Format/js/libs/lodash.js");

            /* Add Angular JS */
            bundles.Add(new ScriptBundle("~/bundles/js/angular").Include(
               "~/Format/js/libs/angular-1.2.8/angular.js",
                "~/Format/js/libs/angular-1.2.8/angular-route.js",
                "~/Format/js/libs/angular-1.2.8/angular-ui-router.js",              
               "~/Format/js/libs/angular-1.2.8/angular-resource.js",
               "~/Format/js/libs/angular-1.2.8/angular-animate.js"
              
           ));
           // bundles.Add(new ScriptBundle("~/bundles/js/BootStrap").Include(
           //    "~/Format/js/UniversityBootStrap.js"
           //));
            bundles.Add(new ScriptBundle("~/bundles/js/BootStrap").Include(
                 "~/Format/js/UniversityHome/University.Home.js",
                 "~/Format/js/Account/University.Account.js",
                 "~/Format/js/Account/University.Account.Controllers.js",
                 "~/Format/js/UniversityBootStrap.js"               

           ));
           // bundles.Add(new ScriptBundle("~/bundles/js/universityhome").Include(
           //    "~/Format/js/UniversityHome/UniversityHome.js"
           //));
           // bundles.Add(new ScriptBundle("~/bundles/js/account").Include(
           //    "~/Format/js/UniversityHome/Account.js"
           //));

            coreScripts.AddRange(baseScripts);

            bundles.Add(new ScriptBundle("~/bundles/js/core").Include(coreScripts.ToArray()));

            //COMPATABILITY CORE BUNDLE
            List<string> compatabilityCoreScripts = new List<string>();
            compatabilityCoreScripts.Add("~/Format/js/libs/jquery-1.11.3.js");
            compatabilityCoreScripts.Add("~/Format/js/libs/lodash.compat.js");
            compatabilityCoreScripts.AddRange(baseScripts);

            bundles.Add(new ScriptBundle("~/bundles/js/compatCore").Include(compatabilityCoreScripts.ToArray()));



            bundles.Add(new ScriptBundle("~/bundles/js/modernizr").Include(
                "~/Format/js/libs/modernizr-*"
            ));
            bundles.Add(new StyleBundle("~/bundles/css").Include("~/Format/css/site.css"));
        }
    }
}