using System.Text.RegularExpressions;
using JLS.Foundation.SmartDelete.Extensions;
using Sitecore;
using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Mvc.Pipelines.Response.GetRenderer;
using Sitecore.Mvc.Presentation;

namespace JLS.Foundation.SmartDelete.ExperienceEditor.Pipelines.GetRenderer
{
    public class GetViewRenderer : Sitecore.Mvc.Pipelines.Response.GetRenderer.GetViewRenderer
    {
        public override void Process(GetRendererArgs args)
        {
            if (args.Result != null)
                return;

            args.Result = GetRenderer(args.Rendering, args);
        }

        protected override Renderer GetRenderer(Rendering rendering, GetRendererArgs args)
        {
            var path = GetPath(Context.Database, rendering, GetViewPath(rendering, args));

            if (string.IsNullOrWhiteSpace(path))
                return null;

            // Find the Experience Editor specific view
            var eePath = Regex.Replace(path, @"^(.*)\.cshtml$", "$1_EE.cshtml");
            path = System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(eePath)) &&
                   Context.PageMode.IsExperienceEditorEditing
                ? eePath : path;

            return new ViewRenderer
            {
                ViewPath = path,
                Rendering = rendering
            };
        }

        /// <summary>
        /// If the Rendering expects a Datasource and it is Null or Empty, return the Blank View.
        /// </summary>
        /// <param name="database"></param>
        /// <param name="rendering"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private string GetPath(Database database, Rendering rendering, string path)
        {
            if (!RenderingExtensions.RequiresDatasource(rendering.RenderingItem) ||
                RenderingExtensions.DatasourceExists(database, rendering.DataSource))
                return path;

            return SmartDeleteConstants.Views.BlankViewPath;
        }
    }
}