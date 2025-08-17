// declare variables needed
string? userInput = "";
string command;
string[] validCommands = { "list", "list done", "list in-progress", "list todo", "add", "update", "delete", "mark-in-progress", "mark-done", "exit" };

Console.WriteLine("Welcome to your task tracker! Enter 'help' to list all valid commands or 'exit' to exit the application:");

do
{
    try
    {
        userInput = Console.ReadLine()?.ToLower().Trim();
        if (string.IsNullOrEmpty(userInput))
        {
            continue;
        }

        int spaceIndex = userInput.IndexOf(' ');
        command = spaceIndex >= 0 ? userInput.Substring(0, spaceIndex) : userInput;

        if (command == "help")
        {
            Console.WriteLine("Valid Commands: list, list done, list in-progress, list todo, add, update, delete, mark-in-progress, mark-done, exit");
            Console.WriteLine();
            Console.WriteLine("Examples:");
            Console.WriteLine("# Adding a new task");
            Console.WriteLine("add 'Buy groceries'");
            Console.WriteLine();
            Console.WriteLine("# Updating and deleting tasks");
            Console.WriteLine("update 1 'Buy groceries and cook dinner'");
            Console.WriteLine("delete 1");
            Console.WriteLine();
            Console.WriteLine("# Marking a task as in progress or done");
            Console.WriteLine("mark-in-progress 1");
            Console.WriteLine("mark-done 1");
            Console.WriteLine();
            Console.WriteLine("# Listing all tasks");
            Console.WriteLine("list");
            Console.WriteLine();
            Console.WriteLine("# Listing tasks by status");
            Console.WriteLine("list todo");
            Console.WriteLine("list in-progress");
            Console.WriteLine("list done");
        }
        else if (userInput == "list done" || userInput == "list todo" || userInput == "list in-progress")
        {
            var tasks = MyTaskService.GetAll();
            string statusType = userInput.Substring(spaceIndex + 1).ToLower().Trim();
            var filteredTasks = tasks
                .Where(task => task.Status.ToLower() == statusType)
                .ToList();

            foreach (var task in filteredTasks)
            {
                Console.WriteLine($"{task.Id} {task.Name} - {task.Status}");
            }

        }
        else if (command == "list")
        {
            var tasks = MyTaskService.GetAll();
            foreach (var task in tasks)
            {
                Console.WriteLine($"{task.Id} {task.Name} - {task.Status}");
            }
        }
        else if (command == "add")
        {
            string? taskName = ExtractQuotedText(userInput);

            if (string.IsNullOrWhiteSpace(taskName))
            {
                Console.WriteLine("Task name cannot be empty.");
                continue;
            }

            MyTask newTask = new MyTask { Name = taskName, Status = "todo" };
            int taskId = MyTaskService.Add(newTask);
            Console.WriteLine($"Task added successfully (ID: {taskId})");
        }
        else if (new[] { "delete", "update", "mark-in-progress", "mark-done" }.Contains(command))
        {
            try
            {
                int taskId = int.Parse(userInput.Substring(spaceIndex + 1).Trim());
                if (taskId <= 0)
                {
                    Console.WriteLine("Task ID must be a positive number");
                }

                var existingTask = MyTaskService.Get(taskId);

                if (existingTask == null)
                {
                    Console.WriteLine($"Task with ID {taskId} not found.");
                    continue;
                }

                if (command == "update")
                {
                    string? newTaskName = ExtractQuotedText(userInput);

                    if (string.IsNullOrWhiteSpace(newTaskName))
                    {
                        Console.WriteLine("Task name cannot be empty.");
                        continue;
                    }

                    MyTask updatedTask = new MyTask { Id = existingTask.Id, Name = newTaskName, Status = existingTask.Status };
                    MyTaskService.Update(updatedTask);
                }
                else if (command == "delete")
                {
                    MyTaskService.Delete(taskId);
                }
                else
                {
                    string updatedStatus = command == "mark-in-progress" ? "in-progress" : "done";
                    MyTask updatedTask = new MyTask { Id = existingTask.Id, Name = existingTask.Name, Status = updatedStatus };
                    MyTaskService.Update(updatedTask);
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid task ID. Please enter a number");
                continue;
            }
        }
        else
        {
            Console.WriteLine($"Invalid command. Type 'help' to see the list of valid commands.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Something went wrong: {ex.Message}");
    }
} while (userInput != "exit");

// Helper method that extracts a task name enclosed in quotes
static string? ExtractQuotedText(string input)
{
    int firstQuoteIndex = input.IndexOf('"');
    int lastQuoteIndex = input.LastIndexOf('"');

    if (firstQuoteIndex == -1 || lastQuoteIndex == -1)
    {
        throw new ArgumentException("Missing or malformed quotes in task name.");
    }

    return input.Substring(firstQuoteIndex + 1, lastQuoteIndex - firstQuoteIndex - 1).Trim();
}

// MyTask class
public class MyTask
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Status { get; set; }
}

/** Task Manager with CRUD methods
GetAll() - returns all tasks
Get(id) - returns a task by ID
Add(myTask) - adds a task and assigns a unique ID
Delete(id) - removes a task by ID
Update(myTask) - updates the task name of an existing task in the list
**/
public static class MyTaskService
{
    static List<MyTask> MyTasks { get; }
    static int nextId = 2;
    static MyTaskService()
    {
        MyTasks = new List<MyTask>
        {
            new MyTask {Id = 1, Name = "Buy groceries", Status = "todo"}
        };
    }



    // Get all tasks
    public static List<MyTask> GetAll() => MyTasks;

    // Returns a single task by ID, or null if not found
    public static MyTask? Get(int id) => MyTasks.FirstOrDefault(myTask => myTask.Id == id);

    // Add new task
    public static int Add(MyTask myTask)
    {
        myTask.Id = nextId++;
        MyTasks.Add(myTask);
        return myTask.Id;
    }

    // Delete a single task by ID
    public static void Delete(int id)
    {
        var myTask = Get(id);
        if (myTask is null)
        {
            return;
        }

        MyTasks.Remove(myTask);
    }

    // Update a single task
    public static void Update(MyTask myTask)
    {
        int index = MyTasks.FindIndex(t => t.Id == myTask.Id);
        if (index == -1)
        {
            return;
        }

        MyTasks[index] = myTask;
    }
}