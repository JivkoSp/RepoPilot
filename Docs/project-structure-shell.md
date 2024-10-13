# Shell Project Structure 🧩

The **Shell Project Structure** of RepoPilot defines the organization of core functionalities, managers, services, and utilities that collectively create a robust and extensible shell environment. 
This modular architecture ensures that each component is responsible for a specific aspect of the application, promoting maintainability, scalability, and ease of development.

1. **Overview**

    RepoPilot’s architecture is divided into four main directories:
    - **Core**: Contains fundamental modules that handle alias management, file system operations, and the main shell loop;
    - **Managers**: Houses modules responsible for managing higher-level tasks such as local repositories;
    - **Services**: Includes modules that interact with external services like GitHub and manage user interactions;
    - **Utils**: Provides utility functions and command executors that support core and manager modules.

    This structured approach ensures clear separation of concerns, making the project easier to navigate, extend, and maintain.
   
2. **Core Components**

    The **Core** directory encompasses the essential modules that form the backbone of RepoPilot’s shell environment. These modules manage aliases, handle file system operations, and control the main shell loop.

    **/RepoPilot/Core/AliasManager**

    The **AliasManager** handles the creation, modification, listing, and removal of command aliases.
    It allows users to define shortcuts for frequently used commands, enhancing productivity and reducing repetitive typing.

    Responsibilities:
    - **HandleAliasCommand**: Manages alias operations based on user input, including listing current aliases, adding new ones, and removing existing aliases;
    - **Alias Storage**: Maintains an in-memory dictionary of aliases that can be persisted across sessions by integrating with shell configuration files.

    **Key Method**:
    ```html
      public static void HandleAliasCommand(ref Dictionary<string, string> aliases, string[] args)
    ```

    **/RepoPilot/Core/FileSystemManager**

    The **FileSystemManager** is responsible for managing file system operations within the shell.
    It provides functionalities such as listing directory contents, changing directories, creating directories, and inspecting files.

    Responsibilities:
    - **ListDirectoryContents**: Lists files and directories with enhanced formatting and color-coded output;
    - **ChangeDirectory**: Changes the current working directory based on user input;
    - **MakeDirectory**: Creates new directories, supporting nested directory creation;
    - **LookCommand**: Displays detailed information about specified files, including size, permissions, and last accessed time.

    **Key Methods**:
     ```html
      public static void ListDirectoryContents(string currentDirectory, string[] args)
      public static void ChangeDirectory(ref string currentDirectory, string[] args)
      public static void MakeDirectory(string currentDirectory, string[] args)
      public static void LookCommand(string fileName)
     ```

    **/RepoPilot/Core/Shell**

    The **Shell** class serves as the main loop for the command-line interface.
    It processes user input, executes commands, manages command history, and coordinates interactions between different modules.

    Responsibilities:
    - **Run**: Initiates the shell loop, handling user input and command execution;
    - **RunPilotCommandAsync**: Executes custom pilot commands for managing repositories and interactions;
    - **LoadCommandHistory & SaveCommandHistory**: Manage the persistence of command history across sessions.

    **Key Methods**:
    ```html
      public async Task Run()
      public async Task RunPilotCommandAsync(string arguments)
      private void LoadCommandHistory()
      private void SaveCommandHistory()
      private void RunExternalCommand(string command, string arguments)
    ```
    
3. **Managers**

    The **Managers** directory contains modules that handle specific high-level tasks, such as managing local Git repositories.
    These components encapsulate complex logic and coordinate between core modules and services.

    **/RepoPilot/Managers/LocalRepositoryManager**

    The **LocalRepositoryManager** manages local Git repositories, facilitating operations like repository creation, branch management, and commit handling.

    Responsibilities:
    - **CreateLocalRepository**: Initializes a new local Git repository;
    - **ManageBranches**: Provides interactive branch management options, including creating, listing, switching, merging, and deleting branches;
    - **ManageCommits**: Handles commit-related functionalities, such as viewing commit history and amending commits.

    **Key Methods**:
    ```html
      public static void CreateLocalRepository(string? localPath, string? repoName)
      public static async Task ManageBranches(GitHubClient client)
      public static void ManageCommits()
    ```
    
4. **Services**

    The **Services** directory includes modules that interact with external services and handle specific operations, such as GitHub integration and user interactions within the shell.

    **/RepoPilot/Services/GitHubService**

    The **GitHubService** interfaces with GitHub using the Octokit library, enabling operations like authentication, repository creation, and remote branch management.

    Responsibilities:
    - **GetCurrentBranchName**: Retrieves the current Git branch name;
    - **GetStoredGitHubToken**: Retrieves the stored GitHub token from a file;
    - **SetGitHubCredentials**: Prompts users to enter and store their GitHub credentials;
    - **CreateRepository**: Creates a new repository on GitHub and optionally initializes it locally.

    **Key Methods**:
    ```html
      public static string GetCurrentBranchName(string repoPath)
      public static string GetStoredGitHubToken()
      public static async Task SetGitHubCredentials()
      public static async Task CreateRepository(string token)
    ```

    **/RepoPilot/Services/UserInteraction**

    The **UserInteraction** service manages all user prompts and displays help messages, ensuring clear and consistent communication within the shell.

    Responsibilities:
    - **Prompt**: Displays messages and captures user input;
    - **DisplayHelp**: Shows available commands and their descriptions;
    - **DisplayPilotHelp**: Shows pilot-specific commands;
    - **DisplayBranchesHelp & DisplayCommitsHelp**: Show detailed options for branch and commit management.

    **Key Methods**:
     ```html
        public static string? Prompt(string message)
        public static void DisplayHelp()
        public static void DisplayPilotHelp()
        public static void DisplayBranchesHelp()
        public static void DisplayCommitsHelp()
     ```
    
5. **Utilities**

    The **Utils** directory contains helper modules that provide general-purpose functionality, such as executing Git commands and managing the shell’s console output.
    These utilities support the core components and managers by offering reusable logic that simplifies complex operations.

    **/RepoPilot/Utils/ConsoleUtils**

    The **ConsoleUtils** module enhances the console experience by providing utilities for setting the console font, printing banners, and handling input with tab completion and command history.

    Responsibilities:
    - **SetConsoleFont**: Customizes the console font;
    - **PrintBanner**: Displays an ASCII art banner on startup;
    - **ReadLineWithTabCompletion**: Reads user input with support for tab completion and command history;
    - **GetTabCompletionOptions**: Provides options for tab completion based on current input;
    - **GetAllCommands**: Returns a list of available commands for auto-completion;
    - **ClearCurrentLine**: Clears the current line in the console for re-rendering input.

    **Key Methods**:
    ```html
      public static void SetConsoleFont(string fontName, short fontSize)
      public static void PrintBanner()
      public static string ReadLineWithTabCompletion()
      public static List<string> GetTabCompletionOptions(string currentInput)
      private static List<string> GetAllCommands()
      private static void ClearCurrentLine(int length)
    ```
    
    **/RepoPilot/Utils/GitCommandExecutor**

    The **GitCommandExecutor** module abstracts the execution of Git commands, ensuring that all Git operations are handled consistently and providing formatted outputs.

    Responsibilities:
    - **ExecuteGitCommand**: Executes specified Git commands within a given working directory;
    - **CreateBranch, SwitchBranch, MergeBranches, ListLocalBranches, ListRemoteBranches, DeleteRemoteBranch**: Specific Git operations;
    - **ViewCommitHistory, AmendLastCommit, CreateSignedCommit**: Manage commit-related functionalities.

    **Key Methods**:
    ```html
      public static void ExecuteGitCommand(string command, string workingDirectory = "")
      public static void CreateBranch(string branchName)
      public static void SwitchBranch(string branchName)
      public static void MergeBranches(string branchName)
      public static void ListLocalBranches()
      public static async Task ListRemoteBranches(GitHubClient client)
      public static async Task DeleteRemoteBranch(GitHubClient client, string branchName)
      public static void ViewCommitHistory()
      public static void AmendLastCommit()
      public static void CreateSignedCommit()
    ```
    
6. **Interactions and Workflow**

    The **Shell** component orchestrates interactions between Core components, Managers, Services, and Utilities to provide a cohesive command-line experience.

    **Workflow Example: Creating an Alias**
    - **User Input**: The user types ``` alias gs='git status' ``` in the shell;
    - **Shell Processing**: The ``` Shell ``` class parses the command and delegates it to the AliasManager;
    - **Alias Handling**: The ``` AliasManager ``` updates the in-memory alias dictionary and persists it by suggesting saving to the shell configuration file;
    - **Command Execution**: When the user later types ``` gs ```, the ``` Shell ``` recognizes it as an alias and expands it to ``` git status ```, executing it via the ``` GitCommandExecutor ```;
    - **Feedback**: Output from the command is formatted and displayed using ``` ConsoleUtils ```.

    **Workflow Example: Managing Branches**
    - **User Input**: The user types ``` pilot manage branches ```;
    - **Shell Processing**: The ``` Shell ``` class delegates the command to the ``` LocalRepositoryManager ```;
    - **Branch Management**: The ``` LocalRepositoryManager ``` interacts with the ``` GitCommandExecutor ``` and ``` GitHubService ``` to perform branch operations;
    - **Feedback**: ``` UserInteraction ``` provides prompts and displays results using ``` ConsoleUtils ```.
