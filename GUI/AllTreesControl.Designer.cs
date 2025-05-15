using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Task_Stack.State;
using Windows.UI;

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
            this.delCurrButton = new System.Windows.Forms.Button();
            this.addNewButton = new System.Windows.Forms.Button();
            this.defaultTabPage.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // defaultTabPage
            // 
            this.defaultTabPage.Controls.Add(this.StartInstructions);
            this.defaultTabPage.Location = new System.Drawing.Point(8, 8);
            this.defaultTabPage.Name = "defaultTabPage";
            this.defaultTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.defaultTabPage.Size = new System.Drawing.Size(1084, 729);
            this.defaultTabPage.TabIndex = 0;
            this.defaultTabPage.Text = "[empty]";
            this.defaultTabPage.UseVisualStyleBackColor = true;
            // 
            // StartInstructions
            // 
            this.StartInstructions.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.StartInstructions.AutoSize = true;
            this.StartInstructions.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartInstructions.Location = new System.Drawing.Point(296, 257);
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
            this.tabControl.Anchor = System.Windows.Forms.AnchorStyles.Top |
                                        System.Windows.Forms.AnchorStyles.Bottom |
                                        System.Windows.Forms.AnchorStyles.Left |
                                        System.Windows.Forms.AnchorStyles.Right;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1108, 776);
            this.tabControl.TabIndex = 0;
            // 
            // delCurrButton
            // 
            this.delCurrButton.Enabled = false;
            this.delCurrButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.delCurrButton.Location = new System.Drawing.Point(900, 737);
            this.delCurrButton.Name = "DelCurrButton";
            this.delCurrButton.Size = new System.Drawing.Size(92, 40);
            this.delCurrButton.TabStop = true;
            this.delCurrButton.TabIndex = 1;
            this.delCurrButton.Text = "Delete";
            this.delCurrButton.UseVisualStyleBackColor = true;
            this.delCurrButton.Click += new System.EventHandler(this.DellCurrButton_Click);
            this.delCurrButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // addNewButton
            // 
            this.addNewButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addNewButton.Location = new System.Drawing.Point(998, 737);
            this.addNewButton.Name = "AddNewButton";
            this.addNewButton.Size = new System.Drawing.Size(92, 40);
            this.addNewButton.TabIndex = 1;
            this.addNewButton.Text = "New";
            this.addNewButton.TabStop = true;
            this.addNewButton.UseVisualStyleBackColor = true;
            this.addNewButton.Click += new System.EventHandler(this.AddNewButton_Click);
            this.addNewButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // AllTreesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.delCurrButton);
            this.Controls.Add(this.addNewButton);
            this.Controls.Add(this.tabControl);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "AllTreesControl";
            this.Size = new System.Drawing.Size(1102, 876);
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
            taskTreeControl.Size = new System.Drawing.Size(1078, 723);
            taskTreeControl.Name = $"TaskTreeControl{this._taskTreesCreated}";
            taskTreeControl.Dock = DockStyle.Fill;
            taskTreeControl.InitializeToState(tree, setTabName); // Give taskTreeControl its contents
            taskTreeControl.ResumeLayout(false);

            this.defaultTabPage.Location = new System.Drawing.Point(8, 8);
            tabPage.Margin = new System.Windows.Forms.Padding(3);
            tabPage.Padding = new System.Windows.Forms.Padding(3, 3, 3, 9);
            tabPage.Size = new System.Drawing.Size(1084, 729);
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
        private System.Windows.Forms.Button delCurrButton;
        private System.Windows.Forms.Button addNewButton;
        private System.Windows.Forms.Label StartInstructions;
        private readonly List<TabPage> taskTreeTabPages = new List<TabPage>();
    }
}
