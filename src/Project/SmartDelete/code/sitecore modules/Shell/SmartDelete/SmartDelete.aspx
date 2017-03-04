<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SmartDelete.aspx.cs" Inherits="JLS.SmartDelete.Website.sitecore_modules.Shell.SmartDelete.SmartDelete" %>

<!DOCTYPE html>
<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Smart Delete</title>
    <link rel="Stylesheet" type="text/css" href="/sitecore/shell/themes/standard/default/WebFramework.css" />
    <link rel="Stylesheet" type="text/css" href="./default.css" />
</head>
<body>
    <form id="form1" runat="server" class="wf-container">
        <div class="wf-content">
            <h1>Smart Delete - Powered by Justice League of Sitecore
            </h1>
            <p class="wf-subtitle">Update templates to include Smart Delete functionality.</p>

            <div class="wf-configsection">
                <h2><span>Select templates</span></h2>
                <p>This process will add the necessary template to the inheritance chain. Existing inheritance options will be unaltered. We are simply adding an additional item to the list.</p>

                <p>List or tree select of templates goes here</p>



                <p>
                    <input id="updateTemplates" name="updateTemplates" type="submit" value="Update Selected Templates" />
                </p>
            </div>


            <div class="wf-configsection">
                <h2><span>Remember Eric's mom loves you!!</span></h2>
            </div>
        </div>
    </form>
</body>
</html>
