using System.Web;
using System.Web.Optimization;

namespace WebSpiderDocs
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;   //enable CDN support

            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/shared-js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
			//bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/bundles/shared-css"));

			/*
			 * web base spider docs
			 */
			bundles.Add(new Bundle("~/bundles/js/webapp.js").Include(
				"~/scripts/vendors/jquery.3.3.1.min.js",

				"~/scripts/angular.js",
				"~/scripts/angular-ui/ui-bootstrap-tpls.js",
				"~/scripts/angular-route.js",
				"~/scripts/angular-cookies.js",
				"~/scripts/moment.js",
				"~/scripts/angular-moment.js",
				"~/scripts/ngStorage.js",
				"~/scripts/loading-bar.js",
				// "~/scripts/custom/ng-chkeditor.js",
				"~/scripts/vendors/customDropDown.js",

				"~/scripts/lodash.js",

				"~/scripts/vendors/ckeditor4/ckeditor.js",
				"~/scripts/app/notify.js",
				"~/scripts/app/modules/ngContextMenu.js",
				"~/scripts/app/directives/ng-ckeditor.js",
				"~/scripts/app/directives/ng-multicombo.js",

				"~/scripts/app/modules/angular-sidebarjs.min.js",
				"~/scripts/app/modules/app.js",
				"~/scripts/app/config/constant.js",

				"~/scripts/app/directives/attribute-directive.js",
				"~/scripts/app/services/spiderdocsService.js",
				"~/scripts/app/services/comService.js",
				"~/scripts/app/services/modalService.js",
				"~/scripts/app/spiderdocs-conf.js",
				"~/scripts/app/controllers/forms/form-import-controller.js",
				"~/scripts/app/controllers/forms/form-input-controller.js",
				"~/scripts/app/controllers/forms/form-email-controller.js",
				"~/scripts/app/controllers/forms/form-doc-details-controller.js",
				"~/scripts/app/controllers/forms/form-confimation-controller.js",
				"~/scripts/app/controllers/includes/inc-database-menus-controller.js"
			));

			bundles.Add(new StyleBundle("~/bundles/css/webapp.css").Include(
				"~/content/app/css/custom.min.css"
			));

			// workspace top
			bundles.Add(new Bundle("~/bundles/js/webapp/workspace-index.js").Include(
				"~/scripts/app/services/tagService.js",

				"~/scripts/app/directives/tree-directive.js",

				"~/scripts/app/controllers/workspace-controller.js",
				"~/scripts/app/controllers/forms/form-folder-tree-controller.js",
				"~/scripts/app/controllers/forms/form-property-controller.js",
				"~/scripts/app/controllers/forms/form-review-controller.js",
				"~/scripts/app/controllers/forms/form-new-view-controller.js",
				"~/scripts/app/taggen.js",
				"~/scripts/app/controllers/forms/form-save-new-controller.js",
				"~/scripts/app/controllers/includes/inc-checkin-ver-controller.js",
				"~/scripts/app/controllers/includes/inc-checkin-new-controller.js",
				"~/scripts/app/controllers/forms/form-search-documents-controller.js"
				

			));

			bundles.Add(new Bundle("~/bundles/js/webapp/workspace-local.js").Include(
							"~/scripts/app/services/tagService.js",

							"~/scripts/app/directives/tree-directive.js",

							"~/scripts/app/controllers/workspace-local-controller.js",
							"~/scripts/app/controllers/forms/form-reason-controller.js",
							"~/scripts/app/controllers/forms/form-save-new-controller.js",
							"~/scripts/app/controllers/includes/inc-checkin-ver-controller.js",
							"~/scripts/app/controllers/includes/inc-checkin-new-controller.js",
							"~/scripts/app/controllers/forms/form-search-documents-controller.js",
							"~/scripts/app/controllers/forms/form-folder-tree-controller.js"
			));
			bundles.Add(new Bundle("~/bundles/js/webapp/registration-users").Include(
				"~/scripts/app/controllers/registration-users-controller.js",
				"~/scripts/app/controllers/forms/form-new-user-controller.js"
			));

			bundles.Add(new Bundle("~/bundles/js/webapp/registration-attributes").Include(
				"~/scripts/app/controllers/registration-attributes-controller.js",
				"~/scripts/app/controllers/forms/form-save-attributes-controller.js",
				"~/scripts/app/directives/tree-directive.js",
				"~/scripts/app/controllers/forms/form-folder-tree-controller.js"

			));

			bundles.Add(new Bundle("~/bundles/js/webapp/registration-group").Include(
				"~/scripts/app/controllers/registration-group-controller.js",
				"~/scripts/app/controllers/forms/form-save-group-controller.js"
			));

			bundles.Add(new Bundle("~/bundles/js/webapp/permission-group").Include(
				"~/scripts/app/controllers/permission-group-controller.js"
				// "~/scripts/app/controllers/forms/form-save-permission-group-controller.js"
			));

			bundles.Add(new Bundle("~/bundles/js/webapp/permission-folder").Include(
				"~/scripts/app/controllers/permission-folder-controller.js",
				"~/scripts/app/directives/tree-directive.js",
				"~/scripts/app/controllers/forms/form-folder-tree-controller.js"

			));

			bundles.Add(new Bundle("~/bundles/js/webapp/registration-documenttype").Include(
				"~/scripts/app/controllers/registration-documenttype-controller.js",
				"~/scripts/app/controllers/forms/form-save-documenttype-controller.js"
			));



			/* 
				
				Options 

			*/
			bundles.Add(new Bundle("~/bundles/js/webapp/options-preference").Include(
				"~/scripts/app/controllers/options-preference-controller.js"
			));




			/*
            Report Builder
            */
			bundles.Add(new StyleBundle("~/bundles/css/report-builder").Include(
                                                                "~/content/css/site.css",
                                                                "~/content/css/bootstrap.css",
                                                                "~/content/css/select2.min.css",
                                                                "~/content/css/jsgrid-theme.min.css",
                                                                "~/content/css/jsgrid.min.css",
                                                                "~/content/css/popmenu.css",
                                                                "~/content/css/jquery-ui.css"
                                                                ));

            bundles.Add(new Bundle("~/bundles/js/report-builder").Include(
                "~/Scripts/custom/jquery-1.10.2.js",
                "~/Scripts/custom/jquery-ui.js",
                "~/Scripts/custom/bootstrap.min.js",
                "~/Scripts/custom/jsgrid.min.js",
                "~/Scripts/custom/spin.js",
                "~/Scripts/custom/q.min.js",
                "~/Scripts/custom/jquery-ajax-blob-arraybuffer.js",
                "~/Scripts/custom/Blob.js",
                "~/Scripts/custom/FileSaver.js",
                "~/Scripts/custom/http.js",
                "~/Scripts/custom/lodash.js",
                "~/Scripts/custom/sweetalert.js",
                "~/Scripts/custom/globalVar.js",
                "~/Scripts/custom/ReportManagement.js"
            ));

             System.Web.Optimization.BundleTable.EnableOptimizations= false;
        }
    }
}
