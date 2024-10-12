# Console Utils 🛠️

RepoPilot incorporates a variety of **Console Utils** to improve the user experience by adding helpful shortcuts, enhancing output visibility, and offering advanced functionality within the command-line interface. 
These utilities are designed to streamline everyday tasks, making RepoPilot a powerful and intuitive tool for developers working in a Git-based environment. The following section outlines the key utilities available in RepoPilot’s 
console and how they are integrated into the design.

1. **Enhanced Command-Line Navigation**

    RepoPilot offers enhanced navigation capabilities to make it easier for users to move through directories, files, and command history within the shell.

    **Directory Shortcuts**

    Navigating between directories is one of the most common tasks in a console environment.
    RepoPilot provides utilities that simplify this process, allowing users to quickly access frequently used directories or project locations.
    - **Predefined Shortcuts**: RepoPilot includes shortcuts like ``` cdp ``` (change to project directory) or ``` home ```, which automatically navigate to pre-configured project or home directories;
    - **Bookmarking**: Users can bookmark specific directories with custom names for fast access. For instance, ``` bookmark src ``` can save the ``` /src ``` folder location, and ``` goto src ``` instantly navigates to it;
    - **Directory History Navigation**: RepoPilot maintains a history of recently visited directories, allowing users to easily move back and forth between them with commands like ``` prev ``` and ``` next ```.

    **Command History Search**

    RepoPilot improves the typical command history feature found in standard shells by integrating advanced search capabilities:
    - **Fuzzy Search**: Users can quickly find and execute previously run commands by typing part of the command and navigating through search results with minimal typing. For example, typing pull will show a list of previously executed ``` git pull ``` commands.
    - **History Autocompletion**: The console autocompletes based on past inputs, reducing the need to retype frequently used commands.
    
2. **Customizable Output Formatting**

    RepoPilot enhances how command outputs are presented to improve readability and allow users to focus on the most relevant information.

    **Color-Coded Output**

    To improve visibility, RepoPilot uses color-coded outputs for different types of information.
    This helps users quickly identify the status of commands or locate important details within large blocks of output.
    - **Success Messages**: Outputs from successful operations (like ``` git commit ``` or ``` git push ```) are displayed in green;
    - **Error Messages**: Errors are highlighted in red, making it clear when an operation has failed;
    - **Warnings**: Yellow text is used for warnings or potential issues that need attention but don’t block the operation.
   
    **Output Filtering**

    For commands that produce large amounts of data (like ``` git log ``` or ``` git diff ```), RepoPilot allows users to filter the output:
    - **Keyword Filtering**: Users can filter outputs by keywords (e.g., ``` git log | filter <keyword> ```), displaying only relevant lines;
    - **Date-Based Filtering**: RepoPilot allows for date-based filtering, especially useful for logs or history viewing (``` git log --since <date> ```).

    **Pagination and Truncation**

    When displaying long outputs, RepoPilot can automatically paginate or truncate the results, making it easier to navigate through large blocks of information.
    Commands like ``` git diff ``` or ``` git log ``` benefit from this feature by reducing screen clutter.
    - **Auto-Pagination**: When a command generates a lot of output, RepoPilot automatically paginates the results, allowing users to scroll through the output one screen at a time;
    - **Collapse Long Outputs**: For commands with extensive output (e.g., long file diffs), RepoPilot offers the option to collapse sections and only expand them on demand.
  
3. **Auto-Suggestions and Intelligent Command Assistance**

    RepoPilot is designed to be proactive, offering intelligent suggestions and assistance based on user behavior.
    These features reduce typing errors and guide users toward efficient workflows.

    **Command Suggestions**

    RepoPilot’s console provides real-time suggestions as users type commands, offering completion options based on the context of the current workflow.
    - **Git Command Suggestions**: RepoPilot suggests related Git commands based on what users are typing, reducing the cognitive load of remembering complex syntax.
      Example:
      ```html
        User types: git ch
        Suggestion: git checkout <branch_name>
      ```
    - **Alias Suggestions**: When a user frequently types long commands, RepoPilot suggests creating an alias to streamline future use.
      For example, after repeated use of ``` git pull origin main ```, the system may suggest creating an alias ``` alias gpm='git pull origin main' ```.

    **Error Correction Suggestions**

    When a user inputs a command with a minor mistake, RepoPilot provides potential corrections.
    - **Command Typo Detection**: RepoPilot identifies typos in commands and offers alternatives, reducing errors from simple typing mistakes.
      Example:
      ```html
        User types: git checout feature-branch
        Suggestion: Did you mean `git checkout feature-branch`?
      ```

    **Inline Documentation**

    For lesser-known or complex Git commands, RepoPilot offers inline documentation or usage hints directly within the console.
    For example, if a user starts typing ``` git rebase ```, RepoPilot could provide a brief summary of what the command does and key options.
   
4. **File and Project Utilities**

   RepoPilot extends beyond Git-specific operations by offering file and project management utilities, making it easier for developers to manage files and directories without leaving the console.
   
   **File Preview and Quick Access**

   RepoPilot provides file preview capabilities, allowing users to quickly inspect files or changes without having to open them in a separate editor.
   - **Quick File Preview**: Users can preview the contents of a file (e.g., ``` cat file.txt ```) directly within the console using ``` preview <filename> ```.
     RepoPilot integrates this preview feature into commands like ``` git diff ```, enabling users to view changes inline;
   - **Quick File Access**: RepoPilot allows for faster access to files by offering filename autocompletion based on the project structure, reducing the need to navigate through directories manually.

   **File Change Tracking**
   
   RepoPilot integrates with Git to track changes within files and directories in real-time, helping users stay aware of the current state of their workspace.
   - **Real-Time Change Detection**: The console highlights files that have been modified since the last commit, ensuring users are aware of unsaved or uncommitted work.
   
5. **Session Management and Configuration Persistence**

   RepoPilot ensures that users can maintain their environment across sessions, improving long-term usability and customization.

   **Session Persistence**

   RepoPilot remembers the user’s session state, including current directory, open files, and command history, ensuring a seamless experience across terminal restarts.
   - **Resuming Sessions**: Upon starting RepoPilot, users can pick up where they left off, with the console restoring the last working directory and command context.
  
   **Custom Configuration Files**

   RepoPilot allows users to save configuration preferences and customizations, such as aliases, color schemes, or directory bookmarks, in their shell configuration files (``` .bashrc ```, ```.zshrc ```).
   - **Configurable Settings**: Users can modify key settings like default Git branch names, frequently accessed repositories, or alias groups to personalize the console experience;
   - **Export and Import Configurations**: RepoPilot supports exporting configuration settings to share between machines or team members, ensuring consistency in the development environment.
   
6. **Notification and Alert System**

   RepoPilot integrates a notification and alert system to keep users informed about important events or changes during their workflow.

   **Background Task Alerts**

   For operations that take time (such as long-running Git operations or builds), RepoPilot notifies users when the task is completed in the background.
   - **Task Completion Alerts**: Notifications pop up when a background operation finishes, reducing the need for users to manually check the status.

   **Reminders and Warnings**

   RepoPilot offers optional reminders or warnings for actions that may have been overlooked, such as uncommitted changes or unstaged files before a push.
   - **Reminder for Uncommitted Changes**: If a user attempts to push changes without committing, RepoPilot reminds them to commit first.
     
     Example:
     ```html
      [Warning] You have uncommitted changes. Would you like to commit them before pushing? (yes/no)
     ```
