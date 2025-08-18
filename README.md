Project URL: https://roadmap.sh/projects/task-tracker<br />
📝 Task Tracker Console App<br />
A simple C# console application for managing your personal tasks with CRUD operations and status tracking.

🚀 Features<br />
Add new tasks with a name<br />
Update task names<br />
Delete tasks<br />
Mark tasks as todo, in-progress, or done<br />
List all tasks or filter by status<br />
Command-based text interface<br />

🧑‍💻 How to Use<br />
When the app starts, you’ll be prompted to enter a command.

📚 List of Commands<br />
add "Task name"	Adds a new task (default status: todo)<br />
update <id> "New name"	Updates task name by ID<br />
delete <id>	Deletes a task by ID<br />
mark-in-progress <id>	Sets task status to in-progress<br />
mark-done <id>	Sets task status to done<br />

💡 Examples<br />
list	Lists all tasks<br />
list todo	Lists tasks with status todo<br />
list in-progress	Lists tasks with status in-progress<br />
list done	Lists tasks with status done<br />
help	Shows available commands and examples<br />
exit	Closes the application<br />

🧱 Structure<br />
MyTask: Represents a task object (ID, Name, Status)<br />
MyTaskService: In-memory task manager with CRUD methods<br />
ExtractQuotedText(): Helper to parse quoted text for task names<br />
