# Command History 🕒

RepoPilot improves command-line usability with an efficient Command History feature. This feature allows users to review, reuse, and search previously executed commands, making repetitive tasks quicker and reducing the need to retype complex commands. 
By leveraging Command History, developers can enhance their workflow and minimize mistakes caused by re-entering commands manually.

1. **Overview of Command History**

    The Command History feature in RepoPilot keeps a record of all commands executed during a session, allowing users to scroll through previous commands, search for specific entries, and rerun them with minimal effort.
    This section explains how to navigate, access, and benefit from RepoPilot’s enhanced command recall functionality.
   
2. **Accessing Command History**

    RepoPilot integrates with your terminal’s default history mechanism but extends it with more advanced features like interactive searching, command editing, and history persistence.
    You can access and scroll through the history using the **Up** and **Down** arrow keys.
   
    Basic Commands for History Navigation:
    - **Up Arrow**: Scroll to the previous command in the history;
    - **Down Arrow**: Scroll to the next command in the history;
    - **Ctrl + R**: Activate reverse search to find a specific command by typing a substring.
   
3. **Searching Command History**

    RepoPilot supports an interactive search of your command history. This is particularly useful for finding long or complex commands that were used earlier in the session or even in previous sessions.
    
    Reverse Search:
    To search through your command history, press **Ctrl + R** and start typing the keywords or part of the command you want to retrieve. RepoPilot will immediately display matching commands, allowing you to scroll through them interactively.

    Example:
    ```html
      (reverse-i-search)`docker': docker run -d --name my_container nginx
    ```
    You can then press Enter to execute the highlighted command or continue searching by typing more characters.
    
    Features:
    - **Fuzzy Matching**: You don't need to remember the exact command; typing partial keywords will retrieve the relevant results;
    - **Instant Recall**: Once a match is found, press Enter to rerun the command directly.
   
4. **Reusing Commands**

    Once you have navigated to a previously executed command using the **Up** arrow or **Ctrl + R**, you can rerun the command as-is by pressing Enter, or edit it if needed before execution.

    Example Use Case:
    If you need to rerun a lengthy or complex command like:
    ```html
      git push origin feature/advanced-search
    ```
    You can simply recall it from history using **Up Arrow** or **Ctrl + R** and press **Enter** to execute.
  
5. **Modifying Previous Commands**

    Often, you'll need to rerun a previous command with slight modifications. After recalling a command from history, you can freely edit it before execution. 
    This is especially helpful when you want to change parameters, file paths, or flags.

    Example:
    After recalling a command like:
    ```html
     docker run -d --name my_container nginx
    ```
    You can modify it to run a different container:
    ```html
     docker run -d --name another_container alpine
    ```
    This saves time while ensuring flexibility in managing recurring tasks.
  
6. **History Persistence Across Sessions**

     RepoPilot maintains a persistent command history that carries over between terminal sessions. Even if you close your terminal or reboot your machine, your previous commands will still be available for retrieval. 
     This is particularly useful for long-running projects where you may need to refer back to commands used days or weeks ago.

     Features:
     - **Cross-Session History**: Commands from previous sessions are preserved;
     - **Consistent Workflow**: Continuity in your workflow, even after restarting the terminal.
   
7. **Clearing Command History**

     In cases where you want to clear the history to maintain privacy or reduce clutter, RepoPilot provides commands to wipe or trim the command history.

     Clear Current Session History:
     ```html
      history -c
     ```
     Clear Entire Persistent History:
     ```html
     rm ~/.bash_history
     ```
     This will clear all stored history across sessions. However, you can selectively delete certain entries or sessions if needed.
   
8. **Command History Best Practices**

     While Command History can greatly improve your workflow, here are a few tips to get the most out of this feature:

     8.1 **Using Reverse Search for Long Commands**

      When dealing with complex commands, such as those involving multiple options or long file paths, use **Ctrl + R** to search for key parts of the command instead of retyping them.

     8.2 Editing Recalled Commands

      When rerunning previous commands, consider editing parts of the command before execution. For example, changing file names or adjusting options without having to type the entire command again.

     8.3 Clearing Sensitive Commands

      For security reasons, consider clearing commands that involve sensitive information (like passwords in the command line) from your history.

     8.4 Leverage Persistent History
  
      RepoPilot’s persistent history allows you to avoid manually recording frequently used commands. You can rely on the tool to store and retrieve them across sessions.
   
9. **Command History with Git and Docker**

    RepoPilot’s command history is particularly valuable when working with Git and Docker, as it can recall intricate or repetitive commands, such as:

    Git Example:
    ```html
     git rebase -i HEAD~5
    ```
    If you often use Git commands with complex flags or branching, having them readily available from your history can save time.

    Docker Example:
    ```html
     docker build -t my_image:latest .
    ```
    For container management tasks that require multiple steps or parameters, recalling and modifying a previous command streamlines Docker workflows.
