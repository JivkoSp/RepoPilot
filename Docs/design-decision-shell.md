# Shell Design 🛠️

The custom shell in RepoPilot is a core component that provides an interactive interface for users to manage Git repositories and perform file system operations. 
The shell is designed with simplicity and flexibility in mind, allowing users to execute commands seamlessly within a single environment. Below is a breakdown of the design principles and key features of the RepoPilot shell:

1. **Command-Line Interface** (CLI)
   - RepoPilot offers a **text-based CLI** that allows users to interact with the local system and Git repositories. This CLI serves as the primary method for users to execute various commands, making it an intuitive and developer-friendly tool;
   - Users can navigate between directories, manage Git repositories, and execute external commands all from within this single environment.
2. **Command Parsing**
   - The shell is designed to parse user inputs and distinguish between commands and arguments. For example, commands like ``` ls ```, ``` cd ```, and ``` git ``` are recognized, and their respective arguments are processed to carry out the intended operation;
   - Aliases: The shell supports user-defined aliases, allowing users to create shortcuts for frequently used commands. The ``` alias ``` command allows for adding, listing, and removing aliases dynamically.
3. **Command History**
   - The shell includes a command history feature, which logs all executed commands to a file. This allows users to recall and reuse previous commands easily, enhancing the efficiency of working with the shell;
   - Command history is saved in a file (``` commandHistory.txt ```), which is loaded at startup and updated upon exiting the shell.
4. **Built-in Git Functionality**
   - RepoPilot’s shell natively supports Git operations, such as creating branches, switching branches, merging, and viewing commit history. Users can perform all major Git-related tasks directly from the shell without needing to switch between environments;
   - These Git commands are executed using native Git processes, which ensure compatibility with the user’s existing Git installation. For example, commands like ``` git status ```, ``` git branch ```, and ``` git merge ``` are run by invoking Git through the shell.
5. **External Command Execution**
   - The shell can run **external commands** using the system’s native processes. When a command such as ``` git ```, ``` python ```, or any other system-level executable is invoked, the shell spawns a new process to run the command and captures its output.
   - Standard Output and Error Handling: Both standard output and standard error streams from external commands are captured and displayed to the user in real time, providing clear feedback on the results of the command execution.
6. **Directory Management**
   - RepoPilot includes a set of commands for **navigating the file system** and managing directories. These commands include:
       - ``` ls ``` – Lists the contents of the current directory;
       - ``` cd ``` – Changes the working directory;
       - ``` mkdir ``` – Creates a new directory;
       - ``` look ``` – Provides detailed information about a file, such as size, last access time, and whether it's executable.
   - Abbreviated Paths: When displaying the current directory, the shell abbreviates long paths, making them more readable while still maintaining context for the user.
7. **Asynchronous GitHub Integration**
   - The shell provides integration with GitHub through the GitHub API. Users can perform remote repository operations, such as creating repositories and managing branches, directly from the shell;
   - Asynchronous execution is used for operations that interact with GitHub, ensuring that the shell remains responsive even when performing long-running tasks like fetching remote branches or creating repositories.
8. **User-Friendly Prompts and Feedback**
   - RepoPilot’s shell offers clear and user-friendly prompts. For example, when switching branches, merging, or creating a new branch, the shell will prompt the user for input, making the interaction intuitive and straightforward;
   - Color-coded output: The shell uses color coding to differentiate between different types of information.
     For example:
       - Directory listings (``` [DIR] ```) are highlighted in **yellow**;
       - Files (``` [FILE] ```) are highlighted in **cyan**;
       - Errors are displayed in **red**, making it easy to spot issues in command execution.
9. **Help and Documentation**
    - The shell provides built-in help commands, such as ``` help ``` and ``` pilot ```, to assist users in navigating available commands and GitHub integration. These commands display usage instructions and brief descriptions of various features;
    - Pilot Help: Specific to RepoPilot, the ``` pilot ``` command provides detailed help on managing GitHub repositories, including repository creation, branch management, and commit handling.
10. **Customization and Extensibility**
    - The shell is designed with extensibility in mind, allowing developers to add new commands or modify existing ones without disrupting the core functionality;
    - Future enhancements could include integration with more third-party tools, additional Git commands, or extended support for shell scripting and automation.
