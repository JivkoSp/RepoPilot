# Technologies 🛠️

RepoPilot leverages a combination of modern and reliable technologies to offer a smooth and efficient experience for managing Git repositories and integrating with GitHub.
Below is an overview of the key technologies used in RepoPilot:
1. **C# and .NET**
   - **Primary Language**: RepoPilot is built using C#;
   - **.NET Core**: The application runs on .NET, a cross-platform, open-source framework that provides a comprehensive development platform for building modern applications.
     This allows RepoPilot to run on multiple operating systems, including Windows, Linux, and macOS, offering flexibility for diverse development environments.
2. **Git**
   - **Version Control System**: Git is the core version control system used by RepoPilot for managing code repositories. RepoPilot abstracts many of Git’s essential functionalities, such as branch management,
     commit history, and repository creation, into simple commands that users can interact with through the CLI;
   - **Git Commands**: Underneath the hood, RepoPilot relies on native Git commands executed through shell processes. This allows it to perform tasks such as creating branches, merging, viewing commit history, and initializing repositories.
3. **GitHub API**
   - **Integration with GitHub**: RepoPilot integrates directly with GitHub using the official ``` GitHub API ```. This enables seamless management of GitHub repositories, branches, and commits from within the tool, providing users with the ability to:
       - **Create repositories**
       - **Manage branches on GitHub**
       - **Delete remote branches**
   - **Authentication**: OAuth tokens are used for secure authentication with GitHub. RepoPilot handles the process of token storage and retrieval, allowing users to authenticate their GitHub accounts and interact with their repositories securely.
4. **LibGit2Sharp** (Future Integration)
   - **Planned for Future Releases**: LibGit2Sharp, a .NET wrapper around the libgit2 library, provides a programmatic interface for Git operations within .NET applications.
     This technology will potentially enhance RepoPilot by replacing native Git shell command execution with fully managed Git operations in C#, improving performance and reducing external dependencies.
5. **Command-Line Interface** (CLI)
   - **Custom Shell Environment**: RepoPilot includes a custom-built shell environment, designed to offer an intuitive and user-friendly interface for interacting with both local file systems and Git/GitHub commands.
     Users can navigate directories, execute commands, and manage their repositories all within this interactive CLI;
   - **Command History**: The CLI also supports command history, so users can recall and reuse previous commands easily.
6. **Process Management**
   - **System.Diagnostics**: RepoPilot uses the ``` System.Diagnostics ``` library to run external Git commands as subprocesses. This allows for asynchronous execution and interaction with the native Git installation,
     ensuring full Git functionality while maintaining control over input/output streams;
   - **Error Handling**: Comprehensive error handling is implemented to capture any errors from the subprocesses and provide detailed feedback to users.
7. **File System Management**
   - **Directory and File Operations**: RepoPilot includes a module for managing file and directory operations, powered by standard .NET libraries like ``` System.IO ```.
     This allows users to list files, change directories, and manage repositories at the file system level directly from the CLI.
8. **JSON for Configuration**
   - **Token Storage**: User credentials and GitHub OAuth tokens are securely stored using JSON-based configuration files.
     This enables persistent storage and retrieval of user data across sessions while maintaining simplicity in reading/writing structured data.
9. **Asynchronous Programming**
   - **Async/Await**: RepoPilot uses asynchronous programming extensively, especially for operations that interact with external systems like GitHub.
     This ensures that long-running tasks, such as fetching remote branches or creating repositories, do not block the main execution thread, offering a more responsive user experience.
10. **Dependency Injection** (Planned Feature)
   - **Future Enhancement**: RepoPilot’s architecture is being developed with scalability in mind, and future versions may incorporate Dependency Injection (DI) for better testability, modularity, and maintainability.
     DI will allow for cleaner separation between different services like GitHub interaction, file system management, and user interaction modules.
