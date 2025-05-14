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
            treeView = new TreeView();
            infoPanel = new FlowLayoutPanel();
            taskNameLabel = new Label();
            taskNameBox = new TextBox();
            taskDescriptionLabel = new Label();
            taskDescriptionBox = new TextBox();
            doneCheckBox = new CheckBox();
            addDeleteButtonsPanel = new FlowLayoutPanel();
            deleteTaskButton = new Button();
            addChildTaskButton = new Button();
            infoPanel.SuspendLayout();
            addDeleteButtonsPanel.SuspendLayout();
            SuspendLayout();
            // 
            // treeView
            // 
            treeView.Dock = DockStyle.Fill;
            treeView.Location = new System.Drawing.Point(0, 0);
            treeView.Margin = new Padding(0);
            treeView.Name = "treeView";
            treeView.ShowNodeToolTips = true;
            treeView.Size = new System.Drawing.Size(758, 970);
            treeView.TabIndex = 0;
            // 
            // infoPanel
            // 
            infoPanel.Controls.Add(taskNameLabel);
            infoPanel.Controls.Add(taskNameBox);
            infoPanel.Controls.Add(taskDescriptionLabel);
            infoPanel.Controls.Add(taskDescriptionBox);
            infoPanel.Controls.Add(doneCheckBox);
            infoPanel.Controls.Add(addDeleteButtonsPanel);
            infoPanel.Dock = DockStyle.Right;
            infoPanel.Location = new System.Drawing.Point(758, 0);
            infoPanel.Margin = new Padding(0);
            infoPanel.Name = "infoPanel";
            infoPanel.Padding = new Padding(3, 4, 3, 0);
            infoPanel.Size = new System.Drawing.Size(414, 970);
            infoPanel.TabIndex = 0;
            // 
            // taskNameLabel
            // 
            taskNameLabel.AutoSize = true;
            taskNameLabel.BackColor = System.Drawing.SystemColors.Control;
            taskNameLabel.Location = new System.Drawing.Point(3, 4);
            taskNameLabel.Margin = new Padding(0);
            taskNameLabel.Name = "taskNameLabel";
            taskNameLabel.Size = new System.Drawing.Size(129, 32);
            taskNameLabel.TabIndex = 0;
            taskNameLabel.Text = "Task Name";
            // 
            // taskNameBox
            // 
            taskNameBox.BackColor = System.Drawing.SystemColors.Control;
            taskNameBox.Enabled = false;
            taskNameBox.Location = new System.Drawing.Point(3, 36);
            taskNameBox.Margin = new Padding(0, 0, 0, 13);
            taskNameBox.Name = "taskNameBox";
            taskNameBox.Size = new System.Drawing.Size(407, 39);
            taskNameBox.TabIndex = 0;
            taskNameBox.Text = "Task Name";
            taskNameBox.Validating += taskNameBox_Validating;
            taskNameBox.Validated += taskNameBox_Validated;
            // 
            // taskDescriptionLabel
            // 
            taskDescriptionLabel.AutoSize = true;
            taskDescriptionLabel.BackColor = System.Drawing.SystemColors.Control;
            taskDescriptionLabel.Location = new System.Drawing.Point(3, 88);
            taskDescriptionLabel.Margin = new Padding(0);
            taskDescriptionLabel.Name = "taskDescriptionLabel";
            taskDescriptionLabel.Size = new System.Drawing.Size(186, 32);
            taskDescriptionLabel.TabIndex = 0;
            taskDescriptionLabel.Text = "Task Description";
            // 
            // taskDescriptionBox
            // 
            taskDescriptionBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            taskDescriptionBox.BackColor = System.Drawing.SystemColors.Control;
            taskDescriptionBox.Enabled = false;
            taskDescriptionBox.Location = new System.Drawing.Point(3, 120);
            taskDescriptionBox.Margin = new Padding(0, 0, 0, 13);
            taskDescriptionBox.MinimumSize = new System.Drawing.Size(407, 319);
            taskDescriptionBox.Multiline = true;
            taskDescriptionBox.Name = "taskDescriptionBox";
            taskDescriptionBox.Size = new System.Drawing.Size(407, 319);
            taskDescriptionBox.TabIndex = 0;
            taskDescriptionBox.Text = "Task Description";
            taskDescriptionBox.Validated += taskDescriptionBox_Validated;
            // 
            // doneCheckBox
            // 
            doneCheckBox.AutoSize = true;
            doneCheckBox.BackColor = System.Drawing.SystemColors.Control;
            doneCheckBox.Enabled = false;
            doneCheckBox.Location = new System.Drawing.Point(3, 452);
            doneCheckBox.Margin = new Padding(0, 0, 0, 13);
            doneCheckBox.Name = "doneCheckBox";
            doneCheckBox.Padding = new Padding(3, 0, 0, 0);
            doneCheckBox.Size = new System.Drawing.Size(167, 36);
            doneCheckBox.TabIndex = 0;
            doneCheckBox.Text = "Completed";
            doneCheckBox.UseVisualStyleBackColor = false;
            doneCheckBox.CheckStateChanged += doneCheckBox_CheckStateChanged;
            // 
            // addDeleteButtonsPanel
            // 
            addDeleteButtonsPanel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            addDeleteButtonsPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            addDeleteButtonsPanel.Controls.Add(deleteTaskButton);
            addDeleteButtonsPanel.Controls.Add(addChildTaskButton);
            addDeleteButtonsPanel.Location = new System.Drawing.Point(3, 501);
            addDeleteButtonsPanel.Margin = new Padding(0);
            addDeleteButtonsPanel.Name = "addDeleteButtonsPanel";
            addDeleteButtonsPanel.Size = new System.Drawing.Size(407, 59);
            addDeleteButtonsPanel.TabIndex = 0;
            // 
            // deleteTaskButton
            // 
            deleteTaskButton.Enabled = false;
            deleteTaskButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            deleteTaskButton.Location = new System.Drawing.Point(22, 4);
            deleteTaskButton.Margin = new Padding(22, 4, 13, 4);
            deleteTaskButton.Name = "deleteTaskButton";
            deleteTaskButton.Size = new System.Drawing.Size(168, 51);
            deleteTaskButton.TabIndex = 0;
            deleteTaskButton.Text = "Delete";
            deleteTaskButton.UseVisualStyleBackColor = true;
            deleteTaskButton.Click += deleteTaskButton_Click;
            // 
            // addChildTaskButton
            // 
            addChildTaskButton.Enabled = false;
            addChildTaskButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            addChildTaskButton.Location = new System.Drawing.Point(216, 4);
            addChildTaskButton.Margin = new Padding(13, 4, 22, 4);
            addChildTaskButton.Name = "addChildTaskButton";
            addChildTaskButton.Size = new System.Drawing.Size(168, 51);
            addChildTaskButton.TabIndex = 0;
            addChildTaskButton.Text = "Add child";
            addChildTaskButton.UseVisualStyleBackColor = true;
            addChildTaskButton.Click += addChildTaskButton_Click;
            // 
            // TaskTreeControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(treeView);
            Controls.Add(infoPanel);
            Margin = new Padding(0);
            Name = "TaskTreeControl";
            Size = new System.Drawing.Size(1172, 970);
            infoPanel.ResumeLayout(false);
            infoPanel.PerformLayout();
            addDeleteButtonsPanel.ResumeLayout(false);
            ResumeLayout(false);
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
