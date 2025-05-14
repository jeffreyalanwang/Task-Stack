using System.Windows.Forms;

namespace Task_Stack.GUI
{
    partial class TaskTreeControl
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
            this.unbindFromState();
            this.setTabName = null;
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
            this.treeView = new System.Windows.Forms.TreeView();
            this.infoPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.taskNameLabel = new System.Windows.Forms.Label();
            this.taskNameBox = new System.Windows.Forms.TextBox();
            this.taskDescriptionLabel = new System.Windows.Forms.Label();
            this.taskDescriptionBox = new System.Windows.Forms.TextBox();
            this.doneCheckBox = new System.Windows.Forms.CheckBox();
            this.addDeleteButtonsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.deleteTaskButton = new System.Windows.Forms.Button();
            this.addChildTaskButton = new System.Windows.Forms.Button();
            this.infoPanel.SuspendLayout();
            this.addDeleteButtonsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(700, 758);
            this.treeView.Margin = new System.Windows.Forms.Padding(0);
            this.treeView.ShowNodeToolTips = true;
            this.treeView.TabIndex = 0;
            // 
            // infoPanel
            // 
            this.infoPanel.Controls.Add(this.taskNameLabel);
            this.infoPanel.Controls.Add(this.taskNameBox);
            this.infoPanel.Controls.Add(this.taskDescriptionLabel);
            this.infoPanel.Controls.Add(this.taskDescriptionBox);
            this.infoPanel.Controls.Add(this.doneCheckBox);
            this.infoPanel.Controls.Add(this.addDeleteButtonsPanel);
            this.infoPanel.Margin = new System.Windows.Forms.Padding(0);
            this.infoPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.infoPanel.Location = new System.Drawing.Point(700, 0);
            this.infoPanel.Name = "infoPanel";
            this.infoPanel.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.infoPanel.Size = new System.Drawing.Size(382, 758);
            this.infoPanel.TabIndex = 0;
            // 
            // taskNameLabel
            // 
            this.taskNameLabel.AutoSize = true;
            this.taskNameLabel.BackColor = System.Drawing.SystemColors.Control;
            this.taskNameLabel.Location = new System.Drawing.Point(3, 3);
            this.taskNameLabel.Margin = new System.Windows.Forms.Padding(0);
            this.taskNameLabel.Name = "taskNameLabel";
            this.taskNameLabel.Size = new System.Drawing.Size(121, 25);
            this.taskNameLabel.TabIndex = 0;
            this.taskNameLabel.Text = "Task Name";
            // 
            // taskNameBox
            // 
            this.taskNameBox.BackColor = System.Drawing.SystemColors.Control;
            this.taskNameBox.Location = new System.Drawing.Point(3, 28);
            this.taskNameBox.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.taskDescriptionBox.Padding = new System.Windows.Forms.Padding(3);
            this.taskNameBox.Name = "taskNameBox";
            this.taskNameBox.Size = new System.Drawing.Size(376, 31);
            this.taskNameBox.TabIndex = 0;
            this.taskNameBox.Text = "Task Name";
            this.taskNameBox.Visible = false;
            this.taskNameBox.Validating += new System.ComponentModel.CancelEventHandler(this.taskNameBox_Validating);
            this.taskNameBox.Validated += new System.EventHandler(this.taskNameBox_Validated);
            // 
            // taskDescriptionLabel
            // 
            this.taskDescriptionLabel.AutoSize = true;
            this.taskDescriptionLabel.Location = new System.Drawing.Point(3, 69);
            this.taskDescriptionLabel.Margin = new System.Windows.Forms.Padding(0);
            this.taskDescriptionLabel.Name = "taskDescriptionLabel";
            this.taskDescriptionLabel.Size = new System.Drawing.Size(173, 25);
            this.taskDescriptionLabel.TabIndex = 0;
            this.taskDescriptionLabel.Text = "Task Description";
            // 
            // taskDescriptionBox
            // 
            this.taskDescriptionBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.taskDescriptionBox.BackColor = System.Drawing.SystemColors.Control;
            this.taskDescriptionBox.Location = new System.Drawing.Point(3, 94);
            this.taskDescriptionBox.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.taskDescriptionBox.Padding = new System.Windows.Forms.Padding(3);
            this.taskDescriptionBox.MinimumSize = new System.Drawing.Size(376, 250);
            this.taskDescriptionBox.Multiline = true;
            this.taskDescriptionBox.Name = "taskDescriptionBox";
            this.taskDescriptionBox.Size = new System.Drawing.Size(194, 250);
            this.taskDescriptionBox.TabIndex = 0;
            this.taskDescriptionBox.Text = "Task Description";
            this.taskDescriptionBox.Visible = false;
            this.taskDescriptionBox.Validated += new System.EventHandler(this.taskDescriptionBox_Validated);
            // 
            // doneCheckBox
            // 
            this.doneCheckBox.AutoSize = true;
            this.doneCheckBox.Location = new System.Drawing.Point(3, 354);
            this.doneCheckBox.Margin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.doneCheckBox.Name = "doneCheckBox";
            this.doneCheckBox.Size = new System.Drawing.Size(147, 29);
            this.doneCheckBox.TabIndex = 0;
            this.doneCheckBox.Text = "Completed";
            this.doneCheckBox.Visible = false;
            this.doneCheckBox.CheckStateChanged += new System.EventHandler(this.doneCheckBox_CheckStateChanged);
            // 
            // addDeleteButtonsPanel
            // 
            this.addDeleteButtonsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.addDeleteButtonsPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.addDeleteButtonsPanel.Controls.Add(this.deleteTaskButton);
            this.addDeleteButtonsPanel.Controls.Add(this.addChildTaskButton);
            this.addDeleteButtonsPanel.Location = new System.Drawing.Point(3, 393);
            this.addDeleteButtonsPanel.Margin = new System.Windows.Forms.Padding(0);
            this.addDeleteButtonsPanel.Name = "addDeleteButtonsPanel";
            this.addDeleteButtonsPanel.Size = new System.Drawing.Size(376, 39);
            this.addDeleteButtonsPanel.TabIndex = 0;
            // 
            // deleteTaskButton
            // 
            this.deleteTaskButton.Enabled = false;
            this.deleteTaskButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteTaskButton.Location = new System.Drawing.Point(2, 3);
            this.deleteTaskButton.Margin = new System.Windows.Forms.Padding(20, 3, 12, 3);
            this.deleteTaskButton.Name = "deleteTaskButton";
            this.deleteTaskButton.Size = new System.Drawing.Size(156, 33);
            this.deleteTaskButton.TabIndex = 0;
            this.deleteTaskButton.Text = "Delete";
            this.deleteTaskButton.UseVisualStyleBackColor = true;
            this.deleteTaskButton.Click += new System.EventHandler(this.deleteTaskButton_Click);
            // 
            // addChildTaskButton
            // 
            this.addChildTaskButton.Enabled = false;
            this.addChildTaskButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addChildTaskButton.Location = new System.Drawing.Point(97, 3);
            this.addChildTaskButton.Margin = new System.Windows.Forms.Padding(12, 3, 20, 3);
            this.addChildTaskButton.Name = "addChildTaskButton";
            this.addChildTaskButton.Size = new System.Drawing.Size(156, 33);
            this.addChildTaskButton.TabIndex = 0;
            this.addChildTaskButton.Text = "Add child";
            this.addChildTaskButton.UseVisualStyleBackColor = true;
            this.addChildTaskButton.Click += new System.EventHandler(this.addChildTaskButton_Click);
            // 
            // TaskTreeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeView);
            this.Controls.Add(this.infoPanel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Padding = new System.Windows.Forms.Padding(0);
            this.Name = "TaskTreeControl";
            this.Size = new System.Drawing.Size(1082, 758);
            this.addDeleteButtonsPanel.ResumeLayout(false);
            this.infoPanel.ResumeLayout(false);
            this.infoPanel.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.FlowLayoutPanel infoPanel;
        private System.Windows.Forms.Label taskNameLabel;
        private System.Windows.Forms.TextBox taskNameBox;
        private System.Windows.Forms.Label taskDescriptionLabel;
        private System.Windows.Forms.TextBox taskDescriptionBox;
        private System.Windows.Forms.CheckBox doneCheckBox;
        private System.Windows.Forms.Button addChildTaskButton;
        private System.Windows.Forms.Button deleteTaskButton;
        private System.Windows.Forms.FlowLayoutPanel addDeleteButtonsPanel;
    }
}
