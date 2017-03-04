# hackathon2017

## Smart Delete

![Smart Delete](https://raw.githubusercontent.com/Justice-League-Of-Sitecore/hackathon2017/develop/readme-logo.jpg)

Smart delete is a module for Sitecore 8.x that provides additional functionality around the default Sitecore item deletion behavior. 

```diff
- Smart Delete was developed over a weekend for Sitecore Hackathon 2017
- Be aware that not all code can be considered production ready until
- more testing is completed and the module is used across multiple versions
- of Sitecore. We beleive the concepts and code for this module are solid
- and can be used as a base for your own production implementation with 
- proper testing and customized to your organization's governance needs.
```

[![License](https://img.shields.io/badge/license-MIT%20License-brightgreen.svg)](https://opensource.org/licenses/MIT)

Smart delete provides governance around item deletion in Sitecore. When content is created or edited, a good Sitecore implementation will use workflow to help govern what content is live and when that content is published to the live site. Item deletion, however, does not go through workflow and can be published out at any time after the item is deleted from master. 

Smart delete allows content authors to delete a content item by simply checking a box so that the deletion of content goes through a workflow just like content creation and editing.

[screenshot]

A workflow action will move any item marked for deletion to a special deletion workflow provided by the Smart Delete module. In addition to the better governance of content, the deletion workflow also automatically serializes the deleted content for easy restore and logs to a custom log file for easy auditing.

[screenshot]

If you have any questions, comments, or suggegstions with the Smart Delete module, please report them in the Issue Tracker. Also feel free to reach out to the authors on Twitter or Sitecore Slack.

Enjoy!

| [![Eric Stafford](https://avatars2.githubusercontent.com/u/9593511?v=3&s=220)](https://github.com/5up3rman) | [![Bill Cacy](https://avatars3.githubusercontent.com/u/4210256?v=3&s=220)](https://github.com/BillCacy) | [![Scott Stocker](https://avatars0.githubusercontent.com/u/22794?v=3&s=220)](https://github.com/sestocker) |
---|---|---
| Eric Stafford | Bill Cacy | Scott Stocker |
| Senior Sitecore Developer | Lead Sitecore Developer | Sitecore Architect |

---

### Technical Details

