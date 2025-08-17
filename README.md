📝 Task Tracker Console App<br />
A simple C# console application for managing your personal tasks with CRUD operations and status tracking.

🚀 Features<br />
Add new tasks with a name
Update task names
Delete tasks
Mark tasks as todo, in-progress, or done
List all tasks or filter by status
Command-based text interface

🧑‍💻 How to Use<br />
When the app starts, you’ll be prompted to enter a command.

📚 List of Commands<br />
add "Task name"	Adds a new task (default status: todo)
update <id> "New name"	Updates task name by ID
delete <id>	Deletes a task by ID
mark-in-progress <id>	Sets task status to in-progress
mark-done <id>	Sets task status to done

💡 Examples<br />
list	Lists all tasks
list todo	Lists tasks with status todo
list in-progress	Lists tasks with status in-progress
list done	Lists tasks with status done
help	Shows available commands and examples
exit	Closes the application

🧱 Structure<br />
MyTask: Represents a task object (ID, Name, Status)
MyTaskService: In-memory task manager with CRUD methods
ExtractQuotedText(): Helper to parse quoted text for task names