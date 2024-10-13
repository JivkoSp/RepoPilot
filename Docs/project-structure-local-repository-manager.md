# Local Repository Manager 🧩

The **Local Repository Manager** is responsible for handling all Git-related operations within local repositories. It allows users to seamlessly manage their Git repositories from both a local and remote perspective. 
The following functionalities are provided by the Local Repository Manager:

Key Responsibilities
- **Create and Initialize Local Repositories**:
    - The local repository manager provides the ability to create a new Git repository in a user-specified local directory. It initializes the repository with Git configuration and optionally links it to a remote repository.
    - Example:
        - Users can choose to create a local repository after creating a new repository on GitHub, or they can initialize a standalone local repository.
- **Manage Branches**:
    - Branch management is a key feature of the local repository manager. Users can create, switch, list, and delete branches.
    - **Features include**:
        - Creating new branches;
        - Switching between local branches;
        - Listing all branches available in the local repository;
        - Merging branches in the local repository;
        - Listing and managing remote branches (via GitHub API integration).
- **Commit Management**:
    - Users can manage commits directly within the local repository. This includes creating commits, amending the latest commit, and creating signed commits;
    - **Commit Features include**:
        - Viewing the commit history with a graph representation of the repository’s history;
        - Amending the latest commit, allowing users to modify the most recent commit without creating a new one;
        - Creating signed commits, adding cryptographic authenticity to each commit.
- **Execute Custom Git Commands**:
    - The local repository manager supports custom Git command execution. This enables users to run any valid Git command from within the local repository environment;
    - The output of the command is displayed directly in the console, making it easy for users to see the result of their operations.

**Console Integration**

The local repository manager integrates with the ConsoleUtils module to provide an interactive command-line interface. 
The CLI includes features such as:
- **Tab Completion**: Users can quickly complete Git commands or file paths using the tab key;
- **Command History**: Previous commands are saved, allowing users to navigate through the command history with the arrow keys;
- **Real-time Feedback**: Git operations, such as commits, branch changes, and merges, provide immediate feedback via the console.

**GitHub Integration**

The local repository manager integrates with GitHub via the **Octokit** library. This allows the local manager to interact with remote GitHub repositories for actions such as:
- Creating new repositories on GitHub;
- Listing and managing remote branches;
- Deleting remote branches.

By leveraging this GitHub integration, the Local Repository Manager allows users to work seamlessly between local and remote repositories.
