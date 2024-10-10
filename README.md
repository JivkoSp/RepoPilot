## RepoPilot Documentation 📘

Welcome to the official documentation for RepoPilot, a powerful command-line tool designed to enhance your workflow by making Git and GitHub repository management easier, faster, and more intuitive. Whether you're a developer, a DevOps engineer, 
or a project manager, RepoPilot offers a simple yet comprehensive solution for handling complex version control tasks.

This documentation will guide you through the features and capabilities of RepoPilot, explain its design decisions, and show you how to effectively use the tool in your daily development or operational processes. 
Whether you're setting up a new repository, managing branches, or committing changes, RepoPilot simplifies Git's complexity and integrates seamlessly with GitHub.

### RepoPilot's key features include:

- **User-friendly Interface**: A streamlined command structure to easily manage repositories and branches;
- **GitHub Integration**: Automatically interact with GitHub for repository creation and branch management;
- **Enhanced Workflow**: Features like command history, aliases, and shell-like environment to boost productivity;
  
In the following sections, you'll find detailed explanations about RepoPilot’s purpose, design decisions, structure, and usage.

* **Intro** 📜
    - [Purpose](Docs/intro-purpose.md)
    - [Capabilities](Docs/intro-capabilities.md)
    - [Overall design](Docs/intro-design.md)
    - [Technologies](Docs/intro-technologies.md)
* **Design Decisions** 🧩
    - [Shell Design](Docs/design-decision-shell.md)
    - [Git Command Integration](Docs/design-decision-authentication-and-authorization.md)
    - [GitHub Integration](Docs/design-decision-reverse-proxy.md)
    - [FileSystem Operations](Docs/design-decision-messaging.md)
    - [Alias Management](Docs/design-decision-containerization.md)
    - [Tab Completion](Docs/design-decision-fault-handling.md)
    - [Command History](Docs/design-decision-data-storage.md)
* **Design** 🛠️
    - [Local Repository Manager](Docs/design-search-service.md)
    - [Shell](Docs/design-user-service.md)
    - [FileSystem Manager](Docs/design-topicarticle-service.md)
    - [Git Command Executor](Docs/design-privatehistory-service.md)
    - [Alias Manager](Docs/design-logcollection-service.md)
    - [GitHub Service](Docs/design-logcollection-service.md)
    - [User Interaction](Docs/design-logcollection-service.md)
    - [Console Utils](Docs/design-logcollection-service.md)
* **Project Structure** 📂
    - [Local Repository Manager Project Structure](Docs/project-structure-user-service.md)
    - [Shell Project Structure](Docs/project-structure-topic-article-service.md)
    - [FileSystem Manager Project Structure](Docs/project-structure-private-history-service.md)
    - [GitHub Service Project Structure](Docs/project-structure-public-history-service.md)
* **Usage** 🚀
   - [How to Run RepoPilot](Docs/usage-how-to-run.md)
   - [Shell Commands](Docs/usage-how-to-run.md)
   - [Managing Repositories](Docs/usage-how-to-run.md)
   - [GitHub Integration](Docs/usage-how-to-run.md)
