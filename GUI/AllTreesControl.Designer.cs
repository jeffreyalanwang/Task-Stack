using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Task_Stack.State;

namespace Task_Stack.GUI
{
    partial class AllTreesControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            this._ProgramState_addTree = null;
            this._ProgramState_removeTree = null;
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.defaultTabPage = new System.Windows.Forms.TabPage();
            this.StartInstructions = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.DellCurrButton = new System.Windows.Forms.Button();
            this.AddNewButton = new System.Windows.Forms.Button();
            this.defaultTabPage.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // defaultTabPage
            // 
            this.defaultTabPage.Controls.Add(this.StartInstructions);
            this.defaultTabPage.Location = new System.Drawing.Point(8, 8);
            this.defaultTabPage.Name = "defaultTabPage";
            this.defaultTabPage.Margin = new System.Windows.Forms.Padding(3);
            this.defaultTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.defaultTabPage.Size = new System.Drawing.Size(1088, 764);
            this.defaultTabPage.TabIndex = 0;
            this.defaultTabPage.Text = "[empty]";
            this.defaultTabPage.UseVisualStyleBackColor = true;
            // 
            // StartInstructions
            // 
            this.StartInstructions.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.StartInstructions.AutoSize = true;
            this.StartInstructions.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartInstructions.Location = new System.Drawing.Point(296, 286);
            this.StartInstructions.Name = "StartInstructions";
            this.StartInstructions.Size = new System.Drawing.Size(490, 183);
            this.StartInstructions.TabIndex = 0;
            this.StartInstructions.Text = "To begin, select\r\n\"New\" at the bottom\r\nof your screen.";
            this.StartInstructions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabControl
            // 
            this.tabControl.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl.Controls.Add(this.defaultTabPage);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1100, 774);
            this.tabControl.TabIndex = 0;
            // 
            // DellCurrButton
            // 
            this.DellCurrButton.Enabled = false;
            this.DellCurrButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DellCurrButton.Location = new System.Drawing.Point(900, 736);
            this.DellCurrButton.Name = "DellCurrButton";
            this.DellCurrButton.Size = new System.Drawing.Size(92, 40);
            this.DellCurrButton.TabIndex = 1;
            this.DellCurrButton.Text = "Delete";
            this.DellCurrButton.UseVisualStyleBackColor = true;
            this.DellCurrButton.Click += new System.EventHandler(this.DellCurrButton_Click);
            // 
            // AddNewButton
            // 
            this.AddNewButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddNewButton.Location = new System.Drawing.Point(998, 736);
            this.AddNewButton.Name = "AddNewButton";
            this.AddNewButton.Size = new System.Drawing.Size(92, 40);
            this.AddNewButton.TabIndex = 2;
            this.AddNewButton.Text = "New";
            this.AddNewButton.UseVisualStyleBackColor = true;
            this.AddNewButton.Click += new System.EventHandler(this.AddNewButton_Click);
            // 
            // AllTreesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.AddNewButton);
            this.Controls.Add(this.DellCurrButton);
            this.Controls.Add(this.tabControl);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "AllTreesControl";
            this.Size = new System.Drawing.Size(1100, 776);
            this.defaultTabPage.ResumeLayout(false);
            this.defaultTabPage.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private int _taskTreesCreated = 0; // Count spans lifetime of application
        private TaskTreeControl controls_appendTree(TaskTree tree)
        {
            this._taskTreesCreated++;

            TaskTreeControl taskTreeControl = new TaskTreeControl();
            TabPage tabPage = new TabPage();
            this.taskTreeTabPages.Add(tabPage); // Save to private register of controls

            this.SuspendLayout();
            tabPage.SuspendLayout();
            taskTreeControl.SuspendLayout();

            Action<string> setTabName = (string tabName) => tabPage.Text = tabName;

            taskTreeControl.Location = new System.Drawing.Point(3, 3);
            taskTreeControl.Padding = new System.Windows.Forms.Padding(0);
            taskTreeControl.Margin = new System.Windows.Forms.Padding(0);
            taskTreeControl.Size = new System.Drawing.Size(1082, 758);
            taskTreeControl.Name = $"TaskTreeControl{this._taskTreesCreated}";
            taskTreeControl.InitializeToState(tree, setTabName); // Give taskTreeControl its contents
            taskTreeControl.ResumeLayout(false);

            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.defaultTabPage.Location = new System.Drawing.Point(8, 8);
            tabPage.Margin = new System.Windows.Forms.Padding(3);
            tabPage.Padding = new System.Windows.Forms.Padding(3);
            tabPage.Size = new System.Drawing.Size(1088, 764);
            tabPage.Name = $"taskTreeTabPage{this._taskTreesCreated}";
            tabPage.UseVisualStyleBackColor = true;
            setTabName.Invoke(tree.Name);
            tabPage.Controls.Add(taskTreeControl); // Give tabPage its contents
            tabPage.ResumeLayout(false);

            
            this.tabControl.Controls.Add(tabPage);
            this._welcomePageVisible = false;
            this._canRemove = true;
            this.tabControl.SelectTab(this.tabControl.TabCount - 1);
            this.ResumeLayout(false);

            return taskTreeControl;
        }

        // For convenience, also returns the Tree object that was in the treeControl.
        private TaskTree controls_removeTree(TabPage taskTreeTabPage)
        {
            // Remove the UI page
            this.taskTreeTabPages.Remove(taskTreeTabPage);
            this.tabControl.TabPages.Remove(taskTreeTabPage);

            // Remove the taskTreeControl
            TaskTree returnVal = stateFrom(taskTreeTabPage); // save return value for below
            taskTreeTabPage.Dispose();

            // If we have no more trees,
            if (this.taskTreeTabPages.Count == 0)
            {
                this._welcomePageVisible = true; // Display the default tabPage
                this._canRemove = false; // There would be nothing left to remove
            }

            // So we can remove the corresponding tree from ProgramState
            return returnVal;
        }

        private System.Windows.Forms.TabPage defaultTabPage;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.Button DellCurrButton;
        private System.Windows.Forms.Button AddNewButton;
        private System.Windows.Forms.Label StartInstructions;
        private readonly List<TabPage> taskTreeTabPages = new List<TabPage>();
    }
}
