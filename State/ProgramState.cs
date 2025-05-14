using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace Task_Stack.State
{
    // State of program.
    // Corresponds to 0-1 program instances.
    // Corresponds to 0-1 save files.
    public class ProgramState : ChangeNotifier
    {
        private static ProgramState Singleton = null;

        #region Fields
        private List<TaskTree> _allTrees;
        // A read-only view on AllTrees.
        public ICollection<TaskTree> AllTrees
        {
            get => new ReadOnlyCollection<TaskTree>(this._allTrees);
        }

        public bool UnsavedChanges { get; private set; } = true;
        internal string SaveFile { get; private set; }
        #endregion Fields
        #region Constructors + Saving
        // Create a blank new ProgramState().
        internal ProgramState()
        {
            this._allTrees = new List<TaskTree>();
            this.SaveFile = GenerateDefaultSaveFilePath();

            // Implement this.UnsavedChanges
            this.OnDataChange(() => UnsavedChanges = true);

            // Make sure we only have one instance per program instance
            Debug.Assert(ProgramState.Singleton == null);
            ProgramState.Singleton = this;
        }

        // From a save data string.
        private static readonly ImmutableHashSet<string> saveDataFields =
            ImmutableHashSet.Create("type", "version", "task_trees");
        private static bool CheckDataFields(Dictionary<string, string> dict) // should return True if valid Dictionary
        {
            // check value of "type"
            if (dict["type"] != "ProgramState") return false;
            // check Keys are same as saveDataFields
            if (!saveDataFields.SetEquals(new HashSet<string>(dict.Keys))) return false;
            // check Values are all not null
            if (dict.Values.Any(value => value is null)) return false;
            // check non-string values to be valid
            try
            {
                // check "task_trees" is serialized list
                List<string> taskTrees_strings = State_Serialization.UnserializeList(dict["task_trees"]);
                // check each TaskTree is serialized dict
                foreach (string taskTree_str in taskTrees_strings)
                {
                    Dictionary<string, string> taskTree_dict = State_Serialization.UnserializeDictionary(taskTree_str);
                }
            }
            catch (State_Serialization.SyntaxException)
            {
                return false;
            }
            // all checks passed
            return true;
        }
        internal ProgramState(string saveData) : this()
        {
            Dictionary<string, string> dict = State_Serialization.UnserializeDictionary(saveData);

            // confirm dict schema; otherwise throw
            bool validDict = CheckDataFields(dict);
            if (!validDict)
            {
                throw new Exception("Not a valid serialized ProgramState string:" + dict.ToString());
            }

            // build any TaskTrees
            List<string> generated_trees_serialized = State_Serialization.UnserializeList(dict["task_trees"]);
            List<TaskTree> generated_children = generated_trees_serialized.ConvertAll(str => new TaskTree(str));
            generated_children.ForEach(taskTree => this.AddTree(taskTree, notifyChangeListeners: false));
        }
        // Create save data for a parent Task/TaskTree to use in its own serialization.
        internal string ToSaveData()
        {
            // define dict schema + save this task's metadata
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("type", "ProgramState");
            dict.Add("version", ConfigurationManager.AppSettings["appVersion"]);
            dict.Add("task_trees", null);

            // add task trees
            List<string> serialized_taskTrees = this._allTrees.ConvertAll(tree => tree.ToSaveData());
            dict["task_trees"] = State_Serialization.SerializeList(serialized_taskTrees);

            // verify we did this right
            Debug.Assert(CheckDataFields(dict));

            return State_Serialization.SerializeDictionary(dict);
        }
        
        // Restore a ProgramState from a file path.
        // If file path is null or an empty string ( i.e. new Uri("") ), a default application data file location is used.
        // A relative path is converted to a qualified path.
        internal ProgramState(Uri saveFile) : this(ReadFileAsString(saveFile != null && saveFile.OriginalString.Length > 0
                                                                        ? QualifyPath(saveFile)
                                                                        : GenerateDefaultSaveFilePath()))
        {
            this.SaveFile = saveFile != null && saveFile.OriginalString.Length > 0
                            ? QualifyPath(saveFile)
                            : GenerateDefaultSaveFilePath();
            this.UnsavedChanges = false;
        }

        // Save this ProgramState to a file path.
        // Save() does not notify listeners.
        public void Save()
        {
            string saveData = this.ToSaveData();
            Console.WriteLine($"Writing ProgramState to absolute filepath: {this.SaveFile}");
            WriteStringToFile(str: saveData, absolutePath: this.SaveFile);
            this.UnsavedChanges = false;
        }
        #endregion Constructors + Saving
        #region Public data methods
        public void AddTree(TaskTree tree, bool notifyChangeListeners = true)
        {
            this._allTrees.Add(tree);
            tree.OnDataChange(this.TriggerDataChange);
            if (notifyChangeListeners) this.TriggerDataChange();
        }
        public void RemoveTree(TaskTree tree)
        {
            this._allTrees.Remove(tree);
            tree.Dispose();
            this.TriggerDataChange();
        }

        public void Dispose()
        {
            this.ClearChangeListeners();
            foreach (TaskTree child in AllTrees)
            {
                child.Dispose();
            }
        }

        public override string ToString()
        {
            string result = "";

            List<string> treeStrings = this.AllTrees.Select(tree => tree.ToString()).ToList();
            // Each tree string should be indented
            treeStrings = treeStrings.Select(treeString => treeString.Replace(Environment.NewLine, Environment.NewLine + "    ")).ToList();
            // Each tree string needs to end in a newline as well
            treeStrings = treeStrings.Select(treeString => treeString + Environment.NewLine).ToList();
            // Combine the strings, separating each tree with "newline + ===== + newline"
            string treeSeparator = $"{Environment.NewLine}====={Environment.NewLine}";
            result += string.Join(treeSeparator, treeStrings);

            // Add data about the entire program state
            result += $"{Environment.NewLine}=====-----{Environment.NewLine}";
            string savedChanges = this.UnsavedChanges ? "Unsaved changes" : "No unsaved changes";
            result += $"ProgramState [ {savedChanges} ]{Environment.NewLine}";
            result += $"# of trees: {this.AllTrees.Count()}{Environment.NewLine}";
            result += $"Save file: {this.SaveFile}{Environment.NewLine}";

            return result;
        }

        // Is there a load file at filepath?
        // If filepath == null, use default save file path.
        public static bool LoadFileAvailable(Uri filepath = null)
        {
            string filepath_str = (filepath == null)
                                    ? GenerateDefaultSaveFilePath()
                                    : QualifyPath(filepath);
            try
            {
                ReadFileAsString(filepath_str);
            }
            catch (Exception e) when (  e is DirectoryNotFoundException ||
                                        e is FileNotFoundException      ||
                                        e is IsolatedStorageException   ||
                                        e is IOException                    )
            {
                return false;
            }

            return true;
        }

        #endregion Public data methods
        #region Private helper methods
        private static readonly string defaultFileName = "taskstack_data.json";
        private static string GenerateDefaultSaveFilePath()
        {
            using (IsolatedStorageFile fileStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly, null, null))
            {
                // Create in the root of the fileStore.
                fileStore.CreateFile(defaultFileName);
            }
            return "###" + defaultFileName; // ### indicates that we are using IsolatedStorageFile, not a path
        }
        private static string QualifyPath(Uri relativePath)
        {
            string path = relativePath.AbsolutePath;
            if (!relativePath.IsFile)
            {
                path = Path.Combine(new[] { path, defaultFileName });
            }
            return path;
        }
        private static string ReadFileAsString(string absolutePath)
        {
            if (absolutePath.StartsWith("###"))
            {
                absolutePath = absolutePath.Substring(3);
                using (IsolatedStorageFile fileStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly, null, null))
                {
                    IsolatedStorageFileStream fileStream = fileStore.OpenFile(absolutePath, FileMode.Open);
                    using ( StreamReader reader = new StreamReader(fileStream) )
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            else return File.ReadAllText(absolutePath);
        }
        private static void WriteStringToFile(string str, string absolutePath)
        {
            if (absolutePath.StartsWith("###"))
            {
                absolutePath = absolutePath.Substring(3);
                using (IsolatedStorageFile fileStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly, null, null))
                {
                    IsolatedStorageFileStream fileStream = fileStore.OpenFile(absolutePath, FileMode.Create);
                    using (StreamWriter writer = new StreamWriter(fileStream))
                    {
                        writer.Write(str);
                        return;
                    }
                }
            }
            else File.WriteAllText(absolutePath, str);
        }
        #endregion Private helper methods
    }

    public class TaskTree : ChangeNotifier
    {
        #region Fields
        public string Name
        {
            get => this.RootTask.Name;
            set { this.RootTask.Name = value; }
        }
        // Readonly so that we can be sure we only need to register this.DataChangeOccured() with the root task
        // RootTask can still be modified internally; only the object reference cannot be changed.
        public readonly Task RootTask;
        #endregion Fields
        #region Constructors + Saving
        // Create a blank TaskTree.
        // Shares a name with rootTask.
        public TaskTree(Task rootTask)
        {
            if (rootTask.Parent != null) throw new Exception("Root task must have null parent.");
            rootTask.OnDataChange(this.TriggerDataChange);
            this.RootTask = rootTask;
        }

        // From save data.
        private static readonly ImmutableHashSet<string> saveDataFields =
            ImmutableHashSet.Create("type", "root_task");
        private static bool CheckDataFields(Dictionary<string, string> dict) // should return True if valid Dictionary
        {
            // check value of "type"
            if (dict["type"] != "TaskTree") return false;
            // check Keys are same as saveDataFields
            if (!saveDataFields.SetEquals(new HashSet<string>(dict.Keys))) return false;
            // check Values are all not null
            if (dict.Values.Any(value => value is null)) return false;
            // check non-string values to be valid
            try
            {
                Dictionary<string, string> rootTask_dict = State_Serialization.UnserializeDictionary(dict["root_task"]);
            }
            catch (State_Serialization.SyntaxException)
            {
                return false;
            }
            // all checks passed
            return true;
        }
        internal TaskTree(string saveData)
        {
            Dictionary<string, string> dict = State_Serialization.UnserializeDictionary(saveData);

            // confirm dict schema; otherwise throw
            bool validDict = CheckDataFields(dict);
            if (!validDict)
            {
                throw new Exception("Not a valid serialized TaskTree string:" + dict.ToString());
            }

            // build root node
            Task rootTask = new Task(saveData: dict["root_task"]);
            rootTask.OnDataChange(this.TriggerDataChange);
            this.RootTask = rootTask;
        }
        // Create save data for a parent Task/TaskTree to use in its own serialization.
        internal string ToSaveData()
        {
            // define dict schema + save this task's metadata
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("type", "TaskTree");
            dict.Add("root_task", this.RootTask.ToSaveData());

            // verify we did this right
            Debug.Assert(CheckDataFields(dict));

            return State_Serialization.SerializeDictionary(dict);
        }
        #endregion Constructors + Saving
        #region UI metadata
        // For UI display purposes, get the width + height of the tree including + under this node.
        public int SubtreeWidth
        {
            get => this.RootTask.SubtreeWidth;
        }
        public int SubtreeHeight
        {
            get => this.RootTask.SubtreeHeight;
        }
        #endregion UI metadata
        #region Public data methods
        public void ForEach(Action<Task, int[][]> action)
        {
            this.RootTask.ForEach(action);
        }

        public override string ToString()
        {
            string result = this.RootTask.ToString() + Environment.NewLine;
            int treeWidth = result.Split(new[] { Environment.NewLine }, StringSplitOptions.None)[0].Length;
            result += new string('-', treeWidth) + Environment.NewLine;

            result += $"Tree name: {this.Name}";

            return result;
        }

        public void Dispose()
        {
            this.ClearChangeListeners();
            this.RootTask.Dispose();
        }
        #endregion Public data methods
    }
    public class Task : ChangeNotifier
    {
        #region Fields
        private string _name;
        public string Name
        {
            get => this._name;
            set
            {
                this._name = value;
                this.TriggerDataChange();
            }
        }
        private string _description;
        public string Description
        {
            get => this._description;
            set
            {
                this._description = value;
                this.TriggerDataChange();
            }
        }
        // Whether this task (and its children) should appear greyed out.
        private bool _done;
        public bool Done
        {
            get => this._done;
            set
            {
                this._done = value;
                switch (value)
                {
                    // When marking a task as done, mark its children done as well
                    case (true):
                        this._children.ForEach(
                            // Will propogate recursively
                            (childTask) => { childTask.Done = true; }
                        );
                        break;
                    // When marking a task as not done again, mark its parents not done as well
                    case (false):
                        if (!this.IsRootNode) this._parent.Done = true;
                        break;
                }
                this.TriggerDataChange();
            }
        }
        private Task _parent;
        public Task Parent
        {
            get => this._parent;
            private set
            {
                this._parent = value;
                this._parent.TriggerDataChange();
            }
        }
        private List<Task> _children;
        public ReadOnlyCollection<Task> Children
        {
            get => new ReadOnlyCollection<Task>(this._children);
        }
        #endregion Fields
        #region UI metadata
        // Width + height of the subtree including + under this node.
        public int SubtreeWidth
        {
            get => _children.Count;
        }
        public int SubtreeHeight
        {
            get
            {
                int thisNodeHeight = 1;
                if (Children.Count == 0) return thisNodeHeight;

                int maxChildHeight = 0;
                this._children.ForEach(
                    (childTask) => { maxChildHeight = Math.Max(maxChildHeight, childTask.SubtreeHeight); }
                );
                return thisNodeHeight + maxChildHeight;
            }
        }
        // Returns true if this is the root node.
        public bool IsRootNode
        {
            get
            {
                return this._parent == null;
            }
        }
        #endregion UI metadata
        #region Constructors + saving
        // Create a new blank Task.
        internal Task(string name, string description = "", bool done = false, Task parentTask = null)
        {
            this._name = name;
            this._description = description;
            this._done = done;
            this._parent = parentTask;

            this._children = new List<Task>();
        }

        // From save data.
        private static readonly ImmutableHashSet<string> saveDataFields =
            ImmutableHashSet.Create("type", "name", "description", "done", "children");
        private static bool CheckDataFields(Dictionary<string, string> dict) // should return True if valid Dictionary
        {
            // check value of "type"
            if (dict["type"] != "Task") return false;
            // check Keys are same as saveDataFields
            if (!saveDataFields.SetEquals(new HashSet<string>(dict.Keys))) return false;
            // check Values are all not null
            if (dict.Values.Any(value => value is null)) return false;
            // check non-string values to be valid
            try
            {
                // check "children" is serialized list
                List<string> children_strings = State_Serialization.UnserializeList(dict["children"]);
                // check each child is serialized dict
                foreach (string child_str in children_strings)
                {
                    Dictionary<string, string> child_dict = State_Serialization.UnserializeDictionary(child_str);
                }
                // check "done" is serialized bool
                State_Serialization.UnserializeBool(dict["done"]);
            }
            catch (State_Serialization.SyntaxException)
            {
                return false;
            }
            // all checks passed
            return true;
        }
        internal Task(string saveData)
        {
            Dictionary<string, string> dict = State_Serialization.UnserializeDictionary(saveData);

            // confirm dict schema; otherwise throw
            bool validDict = CheckDataFields(dict);
            if (!validDict)
            {
                throw new Exception("Not a valid serialized Task string:" + dict.ToString());
            }

            // build this Task
            this._name = dict["name"];
            this._description = dict["description"];
            this._done = State_Serialization.UnserializeBool(dict["done"]);
            this._children = new List<Task>();

            // build any children
            List<string> generated_children_serialized = State_Serialization.UnserializeList(dict["children"]);
            List<Task> generated_children = generated_children_serialized.ConvertAll(str => new Task(str));
            generated_children.ForEach(child => this.AddChild(child, notifyChangeListeners: false));
        }
        // Create save data for a parent Task/TaskTree to use in its own serialization.
        internal string ToSaveData()
        {
            // define dict schema + save this task's metadata
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("type", "Task");
            dict.Add("name", this._name);
            dict.Add("description", this._description);
            dict.Add("done", State_Serialization.SerializeBool(this._done));
            dict.Add("children", null);

            // convert children
            List<string> serializedChildren = this._children.ConvertAll(
                (Task child) => child.ToSaveData()
            );
            dict["children"] = State_Serialization.SerializeList(serializedChildren);

            // verify we did this right
            Debug.Assert(CheckDataFields(dict));

            return State_Serialization.SerializeDictionary(dict);
        }
        #endregion Constructors + saving
        #region Public data methods

        // Add a child node
        // The parent of childTask (i.e. this Task) calls its DataChangeOccured() in its createChild().
        //  - notifyChangeListeners
        public void AddChild(Task childTask, bool notifyChangeListeners = true)
        {
            childTask._parent = this; // do not use .Parent, as this would call TriggerDataChange() again.
            this._children.Add(childTask);
            childTask.OnDataChange(this.TriggerDataChange);
            if (notifyChangeListeners) this.TriggerDataChange();
        }

        // Remove this node (and its children if present)
        public void Remove()
        {
            // We should not be removing the root node
            Debug.Assert(this._parent != null);

            // Remove recursively starting with children.
            foreach (Task childTask in this._children)
            {
                childTask.Remove();
            }

            // Remove this task from parent list
            this._parent._children.Remove(this);

            // Notify listeners of this event
            this.TriggerDataChange();
            // line above should imply line below
            // this._parent.TriggerDataChange();

            this.Dispose();
        }

        // Call on root node only.
        // Task - corresponding task object for this invocation.
        // int[][] -    level 1: length == levels above root node. (root node has length 1)
        //              level 2: length 2. [index of ancestor node at this level, total nodes at this level] (root node: [0, 1])
        public void ForEach(Action<Task, int[][]> action)
        {
            Debug.Assert(this._parent == null);
            List<int[]> ancestry = new List<int[]>{ new[] { 0, 1 } };
            this.ForEach(action, ancestry);
        }
        // ancestry should reflect info for and including [ this ]
        private void ForEach(Action<Task, int[][]> action, List<int[]> ancestry)
        {
            action.Invoke(this, ancestry.ToArray());

            for (int i = 0; i < this._children.Count; i++)
            {
                Task childTask = this._children[i];
                
                List<int[]> childAncestry = new List<int[]>(ancestry);
                childAncestry.Append(new[] {i, this._children.Count});

                childTask.ForEach(action, childAncestry);
            }
        }

        // Return a multi-line string showing data of this task and its children.
        // String contains deliminating borders internally but not externally (far left or right).
        public override string ToString()
        {
            // Get the string for each of its children
            List<string> childrenToString = this._children.ConvertAll(
                (Task child) => child.ToString()
            );

            // Combine the strings for each of its children
            // * each child's string as array of lines
            List<string[]> childrenAsLines = childrenToString.ConvertAll(
                (string childString) => childString.Split(new[] { Environment.NewLine }, StringSplitOptions.None)
            );
            // * each line within a child should be equal width
            Debug.Assert(
                childrenAsLines.All(
                    (string[] currChildLines) => currChildLines.All(
                        (string line) =>
                            line.Length == currChildLines[0].Length // if childLines is empty, childLines[0] should not be referenced due to childLines.All
                                                                // (also, if there is a child, it has lines)
                    )
                )
            );
            // * combine strings (align at last line)
            int childrenMaxHeight = childrenAsLines.Select(
                (string[] childLines) => childLines.Length
            ).DefaultIfEmpty(0).Max();
            string[] childrenMergedLines = new string[childrenMaxHeight]; // This task's string, as an array split at each newline... minus the lines for this specific task.
            for (int i = 0; i < childrenMaxHeight; i++) // i represents # of line rows from BOTTOM (i.e. end)
            {
                // Transpose our children into currRow, marking empty values (due to jagged lengths inside children) with null
                string[] currRow = new string[this._children.Count];
                for (int j = 0; j < currRow.Length; j++) // For each child, add the child's text in this row
                {
                    string[] currChildLines = childrenAsLines[j];
                    string currLine = i < currChildLines.Length // If the current child's string has enough lines to reach row i from the bottom
                                        ? currChildLines[currChildLines.Length - 1 - i] // Get the current line text (line [i] from the bottom)
                                        : null; // Filler value - represents when this row is higher than this child's highest row
                }

                // Combine, inserting " | " between inner borders
                const string firstSeparator = "";
                const string borderSeparator = " | ";
                const string nonborderSeparator = "   ";
                StringBuilder currRowConcat = new StringBuilder();
                for (int j = 0; j < currRow.Length;  j++)
                {
                    string [] currChild = childrenAsLines[j];
                    int currChildLineWidth = currChild[0].Length;
                    string currChildCurrText = currRow[j];
                    // inner border occurs when at least one side of the border is not empty (null)
                    string separator =  j == 0
                                        ? firstSeparator
                                        : currChildCurrText == null && currRow[j - 1] == null
                                            ? borderSeparator
                                            : nonborderSeparator;
                    currRowConcat.Append( separator );
                    // if this child's text is a null string, it still needs to be padded so it is a consistent length within its column
                    if ( currChildCurrText == null )
                    { 
                        currChildCurrText = new string(' ', currChildLineWidth);
                    }
                    currRowConcat.Append(currChildCurrText);
                }

                // set this row's string in childrenCombinedLines, at index [length - 1 - i]
                childrenMergedLines[childrenMergedLines.Length - 1 - i] = currRowConcat.ToString();
            }

            // Generate the string for this task
            int finalStringWidth = this._children.Count > 0
                                    ? childrenMergedLines[0].Length
                                    : 7; // String width for top-level tasks
            Func<int, string, string> atSize = (length, str) =>
            {
                if (str.Length > length)
                {
                    // truncate
                    return str.Substring(0, length - 2) + "..";
                }
                else if (str.Length < length)
                {
                    // pad
                    return str + new string(' ', length - str.Length);
                }
                else
                {
                    // correct size
                    return str;
                }
            };
            List<string> currTaskLines = new List<string>();
            currTaskLines.Add(atSize(finalStringWidth, this.Name));
            currTaskLines.Add(atSize(finalStringWidth, this.Description));
            currTaskLines.Add(atSize(finalStringWidth, this.Done ? "Done" : "Not done"));

            // Place the string for this task below children
            string[] allMergedLines = new string[childrenMergedLines.Length + currTaskLines.Count];
            for (int i = 0; i < childrenMergedLines.Length; i++)
            {
                allMergedLines[i] = childrenMergedLines[i];
            }
            for (int i = childrenMergedLines.Length; i < allMergedLines.Length; i++)
            {
                allMergedLines[i] = currTaskLines[i - childrenMergedLines.Length];
            }

            // Return the string
            Debug.Assert(allMergedLines.All(
                (string line) => line.Length == allMergedLines.Last().Length
            ));
            return string.Join( Environment.NewLine, allMergedLines );
        }

        public void Dispose()
        {
            this.ClearChangeListeners();
            foreach (Task child in this._children)
            {
                child.Dispose();
            }
        }
        #endregion Public data methods
    }

}