# GitHub Service 🧩

The **GitHub Service** is a critical component that handles interactions between the local environment and GitHub via the GitHub API. 
This service manages the creation and manipulation of GitHub repositories, branch management, and user authentication, ensuring seamless integration between the user’s local development environment and their GitHub account.

**Key Responsibilities**

1. **GitHub Authentication**:
    - The service handles user authentication through personal access tokens (PAT). This is a secure way for users to authenticate themselves when performing GitHub operations, such as creating repositories or managing branches remotely;
    - The token is securely stored in a local file (``` github_token.txt ```) after being validated, ensuring it can be reused in future sessions without re-entering credentials.
    - **Authentication Process**:
        - Users provide their GitHub token via the console;
        - The token is validated by attempting to retrieve the current user's GitHub account information;
        - If authentication is successful, the token is stored for future use. If authentication fails, an error message is displayed.
2. **Repository Creation and Management**:
    - The service provides functionality to create new GitHub repositories from the local environment.
    - **Repository Creation**:
      - Users are prompted to enter details such as the repository name, description, and visibility (public or private);
      - After creating the repository on GitHub, users are given the option to create a local clone of the repository as well;
      - If the user opts to initialize the repository locally, the service works with the Local Repository Manager to set up a new local Git repository in the specified path.
    - **Repository Management Features**:
      - Users can create a repository with default settings (name, description, visibility) and instantly connect it with their local development environment;
      - The service also handles any errors encountered during repository creation, such as naming conflicts or permission issues.
3. **Branch Management**:
    - The **GitHub Service** integrates with the GitCommandExecutor to handle branch-related tasks for both local and remote repositories;
    - **Local Branch Operations**:
      - Users can create and switch between local branches using Git commands.
    - **Remote Branch Operations**:
      - The service allows users to list all remote branches associated with a repository and provides the functionality to delete branches from the remote repository via the GitHub API;
      - **Branch Deletion**: Users can delete remote branches by entering the branch name and confirming the operation;
      - **Listing Remote Branches**: The service retrieves and displays all branches from the specified repository using the GitHub API.
4. **Token Storage and Security**:
    - The service securely manages GitHub personal access tokens by storing them locally in the ``` github_token.txt ``` file after successful authentication.
      This allows the token to be reused in future sessions without requiring re-entry, improving user experience while maintaining security;
    - If no token is found or the stored token is invalid, the user is prompted to enter a valid token to proceed with GitHub operations.
5. **Console-Driven Workflow**:
    - The **GitHub Service** is fully integrated with the command-line interface (CLI) provided by the project.
      Users can interact with the service directly from the console, making it convenient to perform GitHub operations within the same environment as their local Git commands;
    - **Interactive Prompts**: Users are guided through the process of creating repositories, entering tokens, and managing branches with step-by-step console prompts.
      This provides a user-friendly experience, especially for those less familiar with GitHub’s API or Git commands.
6. **Integration with Other Components**:
    - The **GitHub Service** works closely with other project components, including:
      - **Local Repository Manager**: For initializing and managing local Git repositories after creating a GitHub repository;
      - **GitCommandExecutor**: For executing Git commands that interact with both local and remote repositories, such as branch creation, switching, and merging;
      - **UserInteraction**: For prompting users with questions and options during operations like repository creation and branch management.
7. **Error Handling**:
    - The service includes comprehensive error handling for operations such as authentication failures, repository creation issues, and branch management errors.
      If an operation fails, detailed error messages are logged to help users troubleshoot issues quickly;
    - For example, during repository creation, the service ensures the repository name is valid and available on GitHub, and during branch deletion, it confirms the branch exists before attempting to remove it.

**Console Interaction Flow**
- **Authentication**: When first interacting with GitHub, the user is prompted to provide their GitHub personal access token. If the token is valid, it is saved locally,
  enabling the user to perform further GitHub-related operations without needing to re-authenticate in subsequent sessions;
- **Repository Creation**: The service prompts the user for repository details (name, description, visibility) and handles the creation process, including the option to initialize the repository locally;
- **Branch Operations**: Users can list, create, switch, and delete branches either locally or remotely. The service manages interactions with the GitHub API to retrieve remote branch information and delete branches as needed.
