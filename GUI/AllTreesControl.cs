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
    public partial class AllTreesControl : UserControl
    {
        private int currentTree = -1;

        public List<TaskTree> _data
        {
            private get;
            set;
        }

        public AllTreesControl()
        {
            InitializeComponent();
        }

        private void showTreeOverview();
        private void showTree(int tree);
        private void addTree();
        private void removeTree();
    }
}
