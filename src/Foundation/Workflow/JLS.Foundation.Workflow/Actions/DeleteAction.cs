﻿using JLS.Foundation.Constants;
using JLS.Foundation.Logging;
using Sitecore.Configuration;
using Sitecore.Data.Items;
using Sitecore.Workflows;
using Sitecore.Workflows.Simple;
using System;

namespace JLS.Foundation.Workflow.Actions
{
    public class DeleteAction
    {
        public void Process(WorkflowPipelineArgs args)
        {
            var item = args.DataItem;

            // If the item Is Approved to be deleted and the Delete Requested Checkbox is checked... youre good to go
            if (item != null && IsApproved(item) && item[SmartDeleteConstants.FieldIds.DeleteRequested].Equals("1"))
            {
                args.CommentFields.Add($"{item.ID.ToShortID()}-{DateTime.Now.Ticks}", $"{Sitecore.Context.User.LocalName} approved the deletion of the item: {item.Name}.");

                if (!Serialization.SerializationManager.SerializeItem(item))
                {
                    SmartDeleteLogger.Error("JLS.FOUNDATION.WORKFLOW: An error occurred serializing item during delete workflow.");
                    return;
                }

                if (Settings.RecycleBinActive)
                    item.Recycle();
                else
                    item.Delete();
            }
        }

        private bool IsApproved(Item item)
        {
            var context = new WorkflowContext(Sitecore.Context.Data);
            return context.IsApproved(item);
        }
    }
}