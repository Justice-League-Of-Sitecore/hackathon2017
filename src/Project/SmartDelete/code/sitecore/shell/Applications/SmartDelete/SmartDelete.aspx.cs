using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using JLS.Foundation.Constants;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;

namespace JLS.SmartDelete.Website.sitecore.shell.Applications.SmartDelete
{
    public partial class SmartDelete : Page
    {
        protected HtmlGenericControl Tree;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckSecurity())
                return;
            var masterDb = Factory.GetDatabase("master");
            var templateRootItem = masterDb.GetItem("/sitecore/templates");
            //Sanity check, just in case
            if (templateRootItem == null)
            {
                var noResults = new HtmlGenericControl("div");
                var noResultsText = new LiteralControl { Text = "No templates found." };
                noResults.Controls.Add(noResultsText);
                Tree.Controls.Add(noResults);
                return;
            }

            ShowTree(templateRootItem, templateList);
        }


        /// <summary>
        /// Checks the security for the current context user.
        /// </summary>
        /// 
        /// <returns>True if allowed, otherwise false</returns>
        private bool CheckSecurity()
        {
            var user = Sitecore.Context.User;
            if (user != null && user.IsAdministrator)
                return true;
            var site = Sitecore.Context.Site;
            var url = site != null ? site.LoginPage : "";
            if (url.Length > 0)
                Response.Redirect(url, true);
            return false;
        }

        private void ShowTree(Item item, ListControl chkBoxList)
        {
            var descendents = item.Axes.GetDescendants();
            //loop through, exclude branches/system, create checkbox for each template
            foreach (Item d in descendents)
            {
                if (d.Paths.Path.ToLower().Contains("sitecore/templates/branches")
                    || d.Paths.Path.ToLower().Contains("sitecore/templates/system")
                    || d.Paths.Path.ToLower().Contains("sitecore/templates/sample")
                    || d.Paths.Path.ToLower().Contains("sitecore/templates/list manager")
                    || d.Paths.Path.ToLower().Contains("__standard values")
                    || d.Paths.Path.ToLower().Contains("/smart delete")) continue;

                //only add templates
                if (d.TemplateID == new Sitecore.Data.ID("{AB86861A-6030-46C5-B394-E8F99E8B87DB}"))
                {
                    var listItem = new ListItem();
                    var iconImage = ThemeManager.GetIconImage(d, 16, 16, "absmiddle", "0px 2px 0px 0px");
                    listItem.Text = $"{iconImage} {d.Paths.Path}";
                    listItem.Value = d.ID.ToString();
                    chkBoxList.Items.Add(listItem);
                }
            }
        }

        /// <summary>
        /// Grabs selected items and adds the new inheritance item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void updateTemplates_Click(object sender, EventArgs e)
        {
            var selectedValues = templateList.Items.Cast<ListItem>().Where(li => li.Selected).Select(li => li.Value).ToList();
            var masterDb = Factory.GetDatabase("master");
            foreach (var selectedItem in selectedValues)
            {
                var item = masterDb.GetItem(new ID(selectedItem));
                if (item == null) continue;
                var existingTemplates = item.Fields["__Base template"].Value;
                if (!existingTemplates.Contains(SmartDeleteConstants.TemplateIds.ItemBase.ToString()))
                {
                    item.Editing.BeginEdit();
                    item.Fields["__Base template"].Value = $"{existingTemplates}|{SmartDeleteConstants.TemplateIds.ItemBase}";
                    item.Editing.EndEdit();
                }
            }
        }
    }
}