using JLS.Foundation.Constants;
using Sitecore.Data.Items;
using Sitecore.SecurityModel;
using Sitecore.Workflows.Simple;
using System;

namespace JLS.Foundation.Workflow.Actions
{
    public class MoveWorkflowAction
    {
        public void Process(WorkflowPipelineArgs args)
        {
            var item = args.DataItem;

            // If the item Is Approved to be deleted and the Delete Requested Checkbox is checked... youre good to go
            if (item != null && item[SmartDeleteConstants.FieldIds.DeleteRequested].Equals("1"))
            {
                args.CommentFields.Add($"{item.ID.ToShortID()}-{DateTime.Now.Ticks}", $"{Sitecore.Context.User.LocalName} moved the item: {item.Name} to the deletion workflow.");

                MoveItemToWorkflow(item);
            }
        }

        private void MoveItemToWorkflow(Item item)
        {
            using (new SecurityDisabler())
            {
                item.Editing.BeginEdit();
                item[Sitecore.FieldIDs.Workflow] = SmartDeleteConstants.TemplateIds.DeletionWorkflow.ToString();
                item[Sitecore.FieldIDs.WorkflowState] = SmartDeleteConstants.TemplateIds.DeletionWorkflowState.ToString();
                item.Editing.EndEdit();
            }
        }
    }
}
