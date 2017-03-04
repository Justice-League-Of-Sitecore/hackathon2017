using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace JLS.SmartDelete.Website.sitecore_modules.Shell.SmartDelete
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

            ShowTree(templateRootItem, Tree);
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

        private void ShowTree(Item item, HtmlGenericControl cell)
        {
            var cell1 = new HtmlGenericControl("div");
            cell1.Attributes["class"] = "Scroller";
            cell.Controls.Add(cell1);

            var list = new CheckBoxList();
            var descendents = item.Axes.GetDescendants();
            //loop through, exclude branches/system, create checkbox for each template
            foreach (var d in descendents)
            {
                if (d.Paths.Path.ToLower().Contains("sitecore/templates/branches")
                    || d.Paths.Path.ToLower().Contains("sitecore/templates/system")
                    || d.Paths.Path.ToLower().Contains("sitecore/templates/sample")
                    || d.Paths.Path.ToLower().Contains("sitecore/templates/list manager")
                    || d.Paths.Path.ToLower().Contains("__standard values")) continue;

                var listItem = new ListItem();
                var iconImage = ThemeManager.GetIconImage(d, 16, 16, "absmiddle", "0px 2px 0px 0px");
                listItem.Text = $"{iconImage} {d.Paths.Path}";
                listItem.Value = d.ID.ToString();
                list.Items.Add(listItem);
            }

            cell.Controls.Add(list);
        }

        /// <summary>
        /// Grabs selected items and adds the new inheritance item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void updateTemplates_Click(object sender, EventArgs e)
        {

        }
    }
}