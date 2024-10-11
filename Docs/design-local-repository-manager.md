# Local Repository Manager 📂

The **Local Repository Manager** in RepoPilot is a core feature designed to streamline the management of Git repositories on your local machine. 
It provides users with powerful tools to efficiently navigate, manage, and interact with multiple repositories in a unified interface. 
Whether you are working on a single repository or handling multiple projects, this feature ensures that your local repositories are always easy to locate, access, and manipulate.

1. **Overview of Local Repository Manager**

  Managing multiple repositories locally can quickly become cumbersome, especially when switching between projects or managing various Git states. 
  The **Local Repository Manager** in RepoPilot solves this by providing a centralized interface for handling all your local repositories.

  Key Features:
  - **Repository Discovery**: Automatically detects repositories in predefined or user-specified directories;
  - **Status Overview**: Provides a clear overview of the state of all repositories (e.g., clean, modified, or uncommitted changes);
  - **Quick Navigation**: Enables rapid switching between repositories without manual file system navigation;
  - **Repository Operations**: Executes common Git operations (e.g., pull, push, fetch, merge) across multiple repositories from a single interface.
  
2. **Setting Up the Local Repository Manager**

  RepoPilot allows easy configuration for detecting and managing repositories on your local machine. 
  During setup, you can specify the directories where your repositories are stored, or let RepoPilot automatically scan your file system for Git repositories.

  Setup Steps:
  
  1. **Specify Repository Directories**: You can either allow RepoPilot to automatically detect repositories in common folders (e.g., ``` ~/Project ```s, ``` ~/Workspace ```) or manually specify directories;
  2. **Initialization**: Once the directories are set, RepoPilot will initialize and display a list of all the detected repositories;
  3. **Repository Caching**: RepoPilot will cache the discovered repositories for quick access, reducing the need for future scans.
  
  Example Configuration:
  ```html
    repomanager --add-directory ~/Projects
  ```
  
3. **Managing Repositories**

  Once configured, the Local Repository Manager provides an organized, centralized view of all your repositories. 
  You can interact with repositories individually or perform bulk operations.
  
  Key Actions:
  - **View Repository Status**: Displays whether the repository is up-to-date, has uncommitted changes, or is in a detached head state;
  - **Quick Access to Repositories**: Easily navigate to a repository’s root directory or open it in your favorite editor;
  - **Batch Operations**: Perform Git operations (e.g., pull, push, fetch) on multiple repositories simultaneously to keep them up-to-date.

  Example Commands:
  - **List Repositories**:
    ```html
      repomanager list
    ```
    This command lists all repositories along with their current status.
  - **Check Status of All Repositories**:
    ```html
      repomanager status
    ```
    This command provides a summary of all repositories, indicating any uncommitted changes or if they are behind the remote.
    
4. **Performing Git Operations**

  The Local Repository Manager enables you to carry out Git commands directly from its interface. This includes individual or batch operations on one or more repositories. 
  The goal is to simplify common Git workflows, whether you're syncing your repositories or resolving merge conflicts.
  
  Supported Git Operations:
  - **Pull**: Update the local branch by pulling the latest changes from the remote repository;
  - **Push**: Push your local commits to the remote branch;
  - **Fetch**: Fetch the latest changes without merging them into your current branch;
  - **Merge**: Merge changes from one branch to another directly through RepoPilot’s interface;
  - **Commit**: Commit staged changes across one or more repositories.

  Example Commands:
  - **Pull All Repositories**:
    ```html
      repomanager pull
    ```
    This command pulls the latest changes from all remote branches.
    
  - **Fetch Changes for a Specific Repository**:
    ```html
      repomanager fetch <repository_name>
    ```
    Fetches changes only for the specified repository.
    
5. **Repository Status Overview**
  
  RepoPilot's Local Repository Manager provides a visual overview of the status of each repository. 
  This makes it easy to identify which repositories require attention or actions.

  Status Indicators:
  - **Clean**: The repository is up-to-date and contains no uncommitted changes;
  - **Modified**: Local changes exist that have not been committed;
  - **Ahead/Behind**: The repository is ahead of or behind the remote branch by a certain number of commits;
  - **Untracked Files**: New files have been added to the repository that are not yet tracked by Git.

  Example Output:
  ```html
    repomanager status
  ```
  Sample output might look like:
  ```html
    Repository: my_project    | Status: Clean
    Repository: repo2         | Status: Behind by 2 commits
    Repository: repo3         | Status: Modified, 3 files uncommitted
  ```
  
6. **Batch Processing and Automation**

  For users managing large numbers of repositories, the ability to perform batch operations is a significant productivity boost. 
  RepoPilot supports batch Git operations, enabling you to update or manage all repositories in one go.

  Batch Commands:
  - **Pull All**: Pull updates from the remote for all repositories;
  - **Push All**: Push local commits across all repositories to the remote;
  - **Fetch All**: Fetch updates from the remote without merging them.

  Example Command:
  ```html
    repomanager pull --all
  ```
  This command pulls the latest changes from all repositories at once.
  
7. **Alias Support for Repositories**

  The Local Repository Manager also allows users to create aliases for repositories, enabling faster access without needing to specify long directory paths.

  Adding an Alias:
  ```html
    repomanager alias add <repository_name> <alias_name>
  ```
  This command creates an alias for easier reference. For example:
  ```html
    repomanager alias add ~/Projects/my_project myproj
  ```
  Now you can use myproj as a shortcut to refer to the repository.
  
8. **Best Practices for Local Repository Management**

   - **Keep Repositories Organized**: Use RepoPilot to group and manage repositories based on your workflow or project structure, keeping everything organized in one place;
   - **Batch Update Regularly**: Regularly update your repositories in batch to stay in sync with remote changes;
   - **Use Aliases for Speed**: Create aliases for frequently accessed repositories to streamline navigation and execution of commands;
   - **Monitor Status**: Regularly check the status overview to ensure no repositories are lagging behind or have uncommitted changes.
