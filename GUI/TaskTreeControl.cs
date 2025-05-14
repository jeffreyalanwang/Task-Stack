using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Task_Stack.State;

namespace Task_Stack.GUI
{
    public partial class TaskTreeControl : UserControl
    {
        private TaskTree __state;
        private ChangeNotifier.DataChangeReaction _stateBoundValuesCallback;
        private TaskTree _state
        {
            get => this.__state;
            set
            {
                // Deregister previous value's callbacks
                if (this.__state != null) this.unbindFromState();
                // Bind value (via callback)
                this.Text = value.Name;
                _stateBoundValuesCallback = () =>
                {
                    if (value.Name != this.Text)
                    {
                        this.Text = value.Name;
                    }
                };
                value.OnDataChange(_stateBoundValuesCallback);
                // Set value
                this.__state = value;
            }
        }
        public TaskTree Tree
        {
            get => this._state;
        }
        
        private TaskControl _focusedTaskControl = null;
        private TaskControl focusedTaskControl
        {
            get => this._focusedTaskControl;
            set
            {
                if (value == this._focusedTaskControl) return;
                bool anyTaskIsSelected = value == null;
                // display/hide fields
                this.taskNameBox.Visible = anyTaskIsSelected;
                this.taskDescriptionBox.Visible = anyTaskIsSelected;
                this.doneCheckBox.Visible = anyTaskIsSelected;
                if (anyTaskIsSelected)
                {
                    this.taskNameBox.Text = value.TaskName;
                    this.taskDescriptionBox.Text = value.TaskDescription;
                    this.doneCheckBox.Checked = value.TaskDone;
                }
                // enable/disable buttons
                this.addChildTaskButton.Enabled = anyTaskIsSelected;
                this.deleteTaskButton.Enabled = anyTaskIsSelected;
            }
        }

        public TaskTreeControl()
        {
            InitializeComponent();

            // Stay updated to new treeNode selections.
            this.treeView.NodeMouseClick += new TreeNodeMouseClickEventHandler(handleTreeViewInteraction);
            this.treeView.KeyDown += new KeyEventHandler(handleTreeViewInteraction);

            // Change color for placeholder text values.
            taskNameBox.TextChanged += new EventHandler(handleInputTextColor);
            taskDescriptionBox.TextChanged += new EventHandler(handleInputTextColor);

            //this.BackColor = Color.Blue; // Debug
            //this.treeView.BackColor = Color.Green; // Debug
            //this.infoPanel.BackColor = Color.Yellow; // Debug
        }

        // Call immediately after constructor.
        public void InitializeToState(TaskTree tree, Action<string> setTabName)
        {
            this._state = tree;

            // Add the root node, then do the rest via recursion inside init of TaskControl 
            treeView.BeginUpdate();

            TaskControl rootNode = new TaskControl();
            this.treeView.Nodes.Add(rootNode);
            rootNode.InitializeToState(tree.RootTask);

            treeView.EndUpdate();
            this.setTabName = setTabName;
        }

        private void handleTreeViewInteraction(object sender, EventArgs e)
        {
            TreeNode selectedNode = this.treeView.SelectedNode;
            TaskControl selectedTaskNode = (TaskControl)selectedNode;
            this.focusedTaskControl = selectedTaskNode;
        }
        private void handleInputTextColor(object sender, EventArgs e)
        {
            TextBox control = (TextBox)sender;
            string defaultText;
            switch (control.Name)
            {
                case "taskNameBox":
                    defaultText = "Task Name";
                    break;
                case "taskDescriptionBox":
                    defaultText = "Task Description";
                    break;
                default:
                    throw new Exception(control.Name);
            }
            Color newColor = control.Text == defaultText
                                ? Color.Gray
                                : Color.Black;
            if (control.ForeColor != newColor)
                control.ForeColor = newColor;
        }

        // * modify fields
        private void taskNameBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // ** name
            string newText = this.taskNameBox.Text;
            if (newText.Length == 0)
            {
                e.Cancel = true;
            }
        }

        private void taskNameBox_Validated(object sender, EventArgs e)
        {
            // ** name
            string newText = this.taskNameBox.Text;
            if (newText != this.focusedTaskControl.TaskName)
            {
                this.focusedTaskControl.TaskName = newText;
            }
            if (this.focusedTaskControl == this.treeView.Nodes[0]) // This value determines the whole TaskTree's name
            {
                setTabName(this._state.Name);
            }
        }

        private void taskDescriptionBox_Validated(object sender, EventArgs e)
        {
            // ** description
            string newText = this.taskDescriptionBox.Text;
            if (newText != this.focusedTaskControl.TaskDescription)
            {
                this.focusedTaskControl.TaskDescription = newText;
            }
        }

        private void doneCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            // ** done
            bool newState = this.doneCheckBox.Checked;
            if (newState != this.focusedTaskControl.TaskDone)
            {
                this.focusedTaskControl.TaskDone = newState;
            }
        }
        private void addChildTaskButton_Click(object sender, EventArgs e)
        {
            // * add child_state
            this.focusedTaskControl.createChild();
        }

        private void deleteTaskButton_Click(object sender, EventArgs e)
        {
            // * delete this task
            // check for parent being null already happens within TaskControl object
            this.focusedTaskControl.Remove();
        }

        private Action<string> setTabName;

        private void unbindFromState()
        {
            this.__state.removeChangeListener(_stateBoundValuesCallback);
        }
    }
}
