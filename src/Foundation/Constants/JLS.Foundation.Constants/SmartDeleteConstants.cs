using Sitecore.Data;

namespace JLS.Foundation.Constants
{
    public struct SmartDeleteConstants
    {
        public struct FieldIds
        {
            public static ID DeleteRequested = new ID("{F5949EE6-1900-4F2A-B764-DE7041A533FA}");
        }

        public struct Views
        {
            public const string BlankViewPath = "~/Areas/ExperienceEditor/Views/Blank.cshtml";
        }

        public struct Rendering
        {
            public const string DatasourceTemplateFieldId = "{1A7C85E5-DC0B-490D-9187-BB1DBCB4C72F}";
        }
    }
}