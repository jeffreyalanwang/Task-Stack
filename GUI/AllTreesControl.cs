using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Task_Stack.State;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Task_Stack.GUI
{
    public partial class AllTreesControl : UserControl
    {
        private AddTaskTree _ProgramState_addTree;
        private RemoveTaskTree _ProgramState_removeTree;
        public AllTreesControl()
        {
            InitializeComponent();
            this.tabControl.Size = this.Size + new Size(0, -130); // different resolutions keep anchoring to wrong dimensions otherwise
        }
        
        // Call this immediately after constructor.
        public delegate void AddTaskTree(TaskTree tree);
        public delegate void RemoveTaskTree(TaskTree tree);
        public void InitializeToState(ICollection<TaskTree> initialTrees, AddTaskTree addDelegate, RemoveTaskTree removeDelegate)
        {
            this._ProgramState_addTree = addDelegate;
            this._ProgramState_removeTree = removeDelegate;

            // Generate + add a tabPage for each tree
            foreach (TaskTree curr in initialTrees)
            {
                controls_appendTree(curr);
            }
        }

        private void AddNewButton_Click(object sender, EventArgs e)
        {
            // Generate a new TaskTree
            string name = "UnnamedTaskTree";
            TaskTree newTree = new TaskTree(new State.Task(name: name));
            // Add TaskTree to state
            this._ProgramState_addTree(newTree);
            // Add a tabPage + display
            this.controls_appendTree(newTree);
        }

        private void DellCurrButton_Click(object sender, EventArgs e)
        {
            // Get the current TaskTree's control
            TabPage currTab = tabControl.SelectedTab;
            // Remove the control from display, and extract the TaskTree from the control
            TaskTree tree = this.controls_removeTree( currTab );
            // Remove the TaskTree from state
            this._ProgramState_removeTree(tree);
        }

        private bool _welcomePageVisible
        {
            set
            {
                if (value)
                {
                    if (this.tabControl.TabPages.Contains(this.defaultTabPage)) return;
                    this.tabControl.TabPages.Add(
                        this.defaultTabPage
                    );
                }
                else
                {
                    if (!this.tabControl.TabPages.Contains(this.defaultTabPage)) return;
                    this.tabControl.TabPages.Remove(
                        this.defaultTabPage
                    );
                }
            }
        }
        // Whether we enable the "Delete" button.
        private bool _canRemove
        {
            set
            {
                this.delCurrButton.Enabled = value;
            }
        }

        private static TaskTree stateFrom(TaskTreeControl control)
        {
            return control.Tree;
        }
        private static TaskTree stateFrom(TabPage control)
        {
            return stateFrom(taskTreeControlFrom(control));
        }
        private static TaskTreeControl taskTreeControlFrom(TabPage tabPage)
        {
            return (TaskTreeControl)tabPage.Controls[0];
        }
    }
}
