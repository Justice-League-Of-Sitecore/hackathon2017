using Sitecore.Mvc.Pipelines.Response.GetRenderer;
using Sitecore.Mvc.Presentation;

namespace JLS.Foundation.SmartDelete.ExperienceEditor.Pipelines.GetRenderer
{
    // If the rendering item has been requested to be deleted, hide the rendering.
    public class GetControllerRenderer : Sitecore.Mvc.Pipelines.Response.GetRenderer.GetControllerRenderer
    {
        public override void Process(GetRendererArgs args)
        {
            var rendering = args.Rendering;
            var database = Sitecore.Context.Database;

            if (!database.Name.Equals("master") || rendering == null)
                return;

            // If Rendering Item has the Delete Requested Field and it is checked, return the blank view.
            if (rendering.Item != null && rendering.Item[SmartDeleteConstants.FieldIds.DeleteRequested].Equals("1"))
                args.Result = this.GetRenderer(rendering, args);

            base.Process(args);
        }

        // Return the blank view
        protected override Renderer GetRenderer(Rendering rendering, GetRendererArgs args)
        {
            return new ViewRenderer
            {
                ViewPath = SmartDeleteConstants.Views.BlankViewPath,
                Rendering = rendering
            };
        }
    }
}