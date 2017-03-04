using Sitecore;
using Sitecore.Configuration;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.SecurityModel;
using Sitecore.Web.UI.Sheer;

namespace JLS.Foundation.SmartDelete.Shell.Pipelines
{
    public class DeleteItems : Sitecore.Shell.Framework.Pipelines.DeleteItems
    {
        /// <summary>
        ///     Override the Confirm delete.
        /// </summary>
        /// <param name="args"></param>
        public override void Confirm(ClientPipelineArgs args)
        {
            var database = Factory.GetDatabase(args.Parameters["database"]);
            var item = database?.GetItem(args.Parameters["items"], LanguageManager.GetLanguage(args.Parameters["language"]));

            if (item == null || item.Fields[SmartDeleteConstants.FieldIds.DeleteRequested] != null || item.Paths.IsContentItem)
            {
                if (args.Aborted)
                    return;
            }

            base.Confirm(args);
        }

        public void RequestItemDeletion(ClientPipelineArgs args, Item item)
        {
            if (!HasDeleteRights(item))
            {
                SheerResponse.Alert($"Sorry you do not have permission to delete the item: \"{item.DisplayName}\".");
                args.AbortPipeline();
                return;
            }

            if (item.HasChildren)
            {
                SheerResponse.Alert($"Sorry, you cannot delete the item: {item.DisplayName}. Please delete the sub-items first.");
                args.AbortPipeline();
                return;
            }

            if (item[SmartDeleteConstants.FieldIds.DeleteRequested].Equals("0"))
            {
                //using (new SecurityDisabler())
                //{
                //    item.Editing.BeginEdit();
                    item[SmartDeleteConstants.FieldIds.DeleteRequested] = "1";
                //    item.Editing.EndEdit();
                //}

                // Alert the Content Author that the request to delete the item has been submitted for review... 
                // by the Workflow Master Approver
                SheerResponse.Alert($"The request to delete \"{item.DisplayName}\" has been submitted for review.");
                args.AbortPipeline();
            }
        }

        private bool HasDeleteRights(Item item)
        {
            return Context.IsAdministrator || item.Access.CanDelete();
        }
    }
}