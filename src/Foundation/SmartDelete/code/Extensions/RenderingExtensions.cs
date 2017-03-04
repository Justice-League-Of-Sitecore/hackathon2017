using System;
using System.Xml.Linq;
using JLS.Foundation.Constants;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Globalization;
using Sitecore.Layouts;
using Sitecore.Mvc.Extensions;

namespace JLS.Foundation.SmartDelete.Extensions
{
    public static class RenderingExtensions
    {
        #region bool

        /// <summary>
        /// Checks if the Datasource Exists
        /// </summary>
        /// <param name="database"></param>
        /// <param name="datasource"></param>
        /// <returns></returns>
        public static bool DatasourceExists(Database database, string datasource)
        {
            return database?.GetItem(datasource) != null;
        }

        /// <summary>
        /// Checks the Rendering to see if it Requires a Datasource.
        /// </summary>
        /// <param name="renderingItem"></param>
        /// <returns></returns>
        public static bool RequiresDatasource(RenderingItem renderingItem)
        {
            return !string.IsNullOrEmpty(renderingItem.InnerItem[SmartDeleteConstants.Rendering.DatasourceTemplateFieldId]);
        }

        private static bool IsXmlBasedRendering(this Sitecore.Mvc.Presentation.Rendering rendering)
        {
            return !rendering.Properties["RenderingXml"].IsWhiteSpaceOrNull();
        }

        #endregion bool

        #region Rendering 

        public static RenderingReference GetRenderingReference(this Sitecore.Mvc.Presentation.Rendering rendering, Language language, Database database)
        {
            if (!rendering.IsXmlBasedRendering())
                return null;

            var text = rendering.Properties["RenderingXml"];

            XElement element;

            try
            {
                element = XElement.Parse(text);
            }
            catch (Exception ex)
            {
                Log.Error($"Failed to parse rendering xml definition for rendering '{rendering.RenderingItemPath}'", ex, rendering.GetType());
                return null;
            }

            return new RenderingReference(element.ToXmlNode(), language, database);
        }

        #endregion Rendering
    }
}