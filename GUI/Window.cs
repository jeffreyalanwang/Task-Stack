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
            saveLoadControl1._setGlobalProgramState = this.applyProgramState;
        }

        internal void applyProgramState(ProgramState newState)
        {
            this._globalData = newState;
            this.BeginMessageControl.Visible = false;
            this.AllTreesControl.Visible = true;
        }
    }
}
