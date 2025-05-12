using Task_Stack.State;

namespace Task_Stack.GUI
{
    partial class Window
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BeginMessageControl = new System.Windows.Forms.Label();
            this.saveLoadControl1 = new SaveLoadControl();
            this.SuspendLayout();
            // 
            // BeginMessageControl
            // 
            this.BeginMessageControl.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BeginMessageControl.AutoSize = true;
            this.BeginMessageControl.BackColor = System.Drawing.Color.Transparent;
            this.BeginMessageControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BeginMessageControl.Location = new System.Drawing.Point(122, 140);
            this.BeginMessageControl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.BeginMessageControl.Name = "BeginMessageControl";
            this.BeginMessageControl.Size = new System.Drawing.Size(179, 130);
            this.BeginMessageControl.TabIndex = 1;
            this.BeginMessageControl.Text = "Welcome\r\n\r\nStart or Load a\r\nnew Task Stack\r\nsession";
            this.BeginMessageControl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // saveLoadControl1
            // 
            this.saveLoadControl1.Location = new System.Drawing.Point(0, 0);
            this.saveLoadControl1.Margin = new System.Windows.Forms.Padding(1);
            this.saveLoadControl1.Name = "saveLoadControl1";
            this.saveLoadControl1.Size = new System.Drawing.Size(394, 78);
            this.saveLoadControl1.TabIndex = 2;
            // 
            // Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 359);
            this.Controls.Add(this.saveLoadControl1);
            this.Controls.Add(this.BeginMessageControl);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Window";
            this.Text = "Window";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label BeginMessageControl;
        private SaveLoadControl saveLoadControl1;
    }
}