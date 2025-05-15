using System;
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

namespace Task_Stack.GUI
{
    public partial class Window : Form
    {
        private ProgramState _globalData = null;

        public Window()
        {
            InitializeComponent();
            saveLoadControl.InitializeToState(this.applyProgramState);
        }

        private void applyProgramState(ProgramState newState)
        {
            this._globalData = newState;

            this.newAllTreesControl();
            this.allTreesControl.InitializeToState(
                initialTrees: newState.AllTrees,
                addDelegate: (tree) => newState.AddTree(tree, notifyChangeListeners: true),
                removeDelegate: (tree) => newState.RemoveTree(tree)
            );

            // UI changes
            if (this.beginMessageControl.Visible)
            {
                this.beginMessageControl.Visible = false;
            }
            this.allTreesControl.Visible = true;
        }
    }
}
