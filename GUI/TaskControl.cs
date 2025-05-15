using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace Task_Stack.GUI
{
    public partial class TaskControl : TreeNode
    {
        private State.Task __state = null;
        private State.Task _state
        {
            get => this.__state;
            set
            {
                if (this.TreeView != null) this.TreeView.BeginUpdate();

                if (this.__state != null)
                {
                    throw new ReadOnlyException();
                    // If above behavior is changed, then the children of [this] will need to be removed here.
                }
                this.__state = value;
                foreach (State.Task childTask in value.Children)
                {
                    this.addChild(childTask);
                }
                this.TaskName = value.Name;
                this.TaskDescription = value.Description;
                this.TaskDone = value.Done;

                if (this.TreeView != null) this.TreeView.EndUpdate();
            }
        }
        public new string Text { get => base.Text; }
        public string TaskName
        {
            get => this._state.Name;
            set
            {
                base.Text = this.TaskDone
                                ? strikethrough(value)
                                : value;
                this._state.Name = value;
                if (this.TreeView != null) this.TreeView.Invalidate();
            }
        }
        public new string ToolTipText { get => base.ToolTipText; }
        public string TaskDescription
        {
            get => this._state.Description;
            set
            {
                base.ToolTipText = value;
                this._state.Description = value;
                if (this.TreeView != null) this.TreeView.Invalidate();
            }
        }
        public bool TaskDone
        {
            get => this._state.Done;
            set
            {
                // handle recursion to mirror State.Task
                switch (value)
                {
                    case (true): // marking as done
                        foreach (TreeNode child in this.Nodes)
                        {
                            TaskControl childTaskControl = (TaskControl)child;
                            childTaskControl.TaskDone = true;
                        }
                        break;
                    case (false): // marking not done
                        TreeNode parent = this.Parent;
                        if (parent != null)
                        {
                            TaskControl parentTaskControl = (TaskControl)parent;
                            parentTaskControl.TaskDone = false;
                        }
                        break;
                }
                // set opacity
                this.ForeColor = value
                                    ? Color.Gray
                                    : Color.Black;
                // set strikethrough
                base.Text = value
                                ? strikethrough(this.TaskName)
                                : this.TaskName;
                this._state.Done = value;
                if (this.TreeView != null) this.TreeView.Invalidate();
            }
        }

        public TaskControl() : base()
        {
            this.ForeColor = Color.Black;
        }
        // Run immediately after constructor, *and also after being attached to the TreeView*.
        public void InitializeToState(State.Task state)
        {
            this._state = state;
        }
        // Add a child to UI only (should already be present in state).
        private void addChild(State.Task child_state)
        {
            // Create the new TaskControl
            TaskControl childControl = new TaskControl();
            childControl.InitializeToState(child_state);
            // Add the new TaskControl
            this.Nodes.Add(childControl);
            // If new child is not done but we are, we are no longer done
            this.TaskDone = this._state.Done; // no need to duplicate what has already been done
            // In the UI, make sure this node is expanded
            this.Expand();
        }

        // Add a child with filler values.
        // Occurs in state and in UI
        public void createChild()
        {
            State.Task newTask = new State.Task(name: "Name");
            // Add the child_state state to this state
            this._state.AddChild(newTask);
            // Add the child to the UI
            this.addChild(newTask);
        }

        // Remove this Task from its parent in the UI (and the state)
        public new void Remove()
        {
            this._state.Remove();
            base.Remove();
            this.Dispose();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected void Dispose()
        {
        }

        const char strikethrough_char = (char)0x0336;
        private static string strikethrough(string str)
        {
            StringBuilder result = new StringBuilder(strikethrough_char);
            foreach (char currChar in str)
            {
                result.Append(currChar);
                result.Append(strikethrough_char);
            }
            return result.ToString();
        }
    }
}
