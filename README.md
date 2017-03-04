# hackathon2017

## Smart Delete

![Smart Delete](https://raw.githubusercontent.com/Justice-League-Of-Sitecore/hackathon2017/develop/readme-logo.jpg)

Smart delete is a module for Sitecore 8.x that provides additional functionality around the default Sitecore item deletion behavior. 

```diff
- Smart Delete was developed over a weekend for Sitecore Hackathon 2017
- Be aware that not all code can be considered production ready until
- more testing is completed and the module is used across multiple versions
- of Sitecore. We believe the concepts and code for this module are solid
- and can be used as a base for your own production implementation with 
- proper testing and customized to your organization's governance needs.
```

[![License](https://img.shields.io/badge/license-MIT%20License-brightgreen.svg)](https://opensource.org/licenses/MIT)

Smart delete provides governance around item deletion in Sitecore. When content is created or edited, a good Sitecore implementation will use workflow to help govern what content is live and when that content is published to the live site. Item deletion, however, does not go through workflow and can be published out at any time after the item is deleted from master. 

Smart delete allows content authors to delete a content item by simply checking a box so that the deletion of content goes through a workflow just like content creation and editing.

![Request Delete](https://raw.githubusercontent.com/Justice-League-Of-Sitecore/hackathon2017/develop/readme-RequestDelete.png)

A workflow action will move any item marked for deletion to a special deletion workflow provided by the Smart Delete module. In addition to the better governance of content, the deletion workflow also automatically serializes the deleted content for easy restore and logs to a custom log file for easy auditing.

![Deletion Workflow](https://raw.githubusercontent.com/Justice-League-Of-Sitecore/hackathon2017/develop/readme-deletionworkflow.png)

If you have any questions, comments, or suggegstions with the Smart Delete module, please report them in the Issue Tracker. Also feel free to reach out to the authors on Twitter or Sitecore Slack.

Enjoy!

| [![Eric Stafford](https://avatars2.githubusercontent.com/u/9593511?v=3&s=220)](https://github.com/5up3rman) | [![Bill Cacy](https://pbs.twimg.com/profile_images/718146990484271104/IV--ElH_.jpg)](https://github.com/BillCacy) | [![Scott Stocker](https://avatars0.githubusercontent.com/u/22794?v=3&s=220)](https://github.com/sestocker) |
---|---|---
| Eric Stafford | Bill Cacy | Scott Stocker |
| Senior Sitecore Developer | Lead Sitecore Developer | Sitecore Architect |

---

### Instructions

- Download the package from the Github downloads section
- Install the package. This will write the necessary content items to Sitecore as well as the default compiled dll's and view files. You can optionally take the source and make modifications (for example, the workflow actions).
  - Base template (ItemBase) for "request delete" checkbox
  - the SmartDelete workflow
  - the workflow action you will include in your workflows (that moves the item to the deletion workflow)
- Next, you'll want to setup template inheritance from the module's ItemBase so that the "request delete" checkbox appears everywhere. If all of your templates already inherit from a custom base, just add Smart Delete's ItemBase and you will be set. (We'd recommend against modifying Standard Template). The easiest way to do this is by logging into Sitecore then navigating to http://{sitecore-instance}/sitecore/shell/applications/SmartDelete/SmartDelete.aspx. This utility page will automatically assign the correct inhertiance based on the templates you select.
- Finally, you need to add "Move to Deletion Workflow Action" in all of your workflows. Anywhere will do but we suggest you add it to the final workflow step to maintain your governance structure. Ideally, this action happens *before* an autopublish. When this actions runs, the item will be moved in the Deletion Workflow where the final delete (recycle) happens in the approved step of the workflow.

