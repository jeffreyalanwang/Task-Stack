using System.Diagnostics;
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
            if (this._globalData != null) this._globalData.Dispose();
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
            this.beginMessageControl = new System.Windows.Forms.Label();
            this.saveLoadControl = new Task_Stack.GUI.SaveLoadControl();
            this.SuspendLayout();
            // 
            // beginMessageControl
            // 
            this.beginMessageControl.AutoSize = true;
            this.beginMessageControl.BackColor = System.Drawing.Color.Transparent;
            this.beginMessageControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.beginMessageControl.Location = new System.Drawing.Point(380, 338);
            this.beginMessageControl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.beginMessageControl.Name = "beginMessageControl";
            this.beginMessageControl.Size = new System.Drawing.Size(340, 255);
            this.beginMessageControl.TabIndex = 0;
            this.beginMessageControl.Text = "Welcome\r\n\r\nStart or Load a\r\nnew Task Stack\r\nsession";
            this.beginMessageControl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // saveLoadControl
            // 
            this.saveLoadControl.Location = new System.Drawing.Point(0, 782);
            this.saveLoadControl.Margin = new System.Windows.Forms.Padding(0);
            this.saveLoadControl.Name = "saveLoadControl";
            this.saveLoadControl.Size = new System.Drawing.Size(788, 150);
            this.saveLoadControl.TabIndex = 1;
            // 
            // Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 930);
            this.Controls.Add(this.beginMessageControl);
            this.Controls.Add(this.saveLoadControl);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "Window";
            this.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.Text = "Task Stack";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private void newAllTreesControl()
        {
            this.allTreesControl = new AllTreesControl();
            this.SuspendLayout();
            
            // Set properties
            this.allTreesControl.BackColor = System.Drawing.Color.Transparent;
            this.allTreesControl.Location = new System.Drawing.Point(0, 0);
            this.allTreesControl.Name = "allTreesControl";
            this.allTreesControl.Size = new System.Drawing.Size(1100, 776);
            this.allTreesControl.TabStop = true;
            this.allTreesControl.TabIndex = 0;
            this.allTreesControl.Margin = new System.Windows.Forms.Padding(0);
            
            // Add to layout at specific location
            this.Controls.Add(this.allTreesControl);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label beginMessageControl;
        private AllTreesControl allTreesControl;
        private SaveLoadControl saveLoadControl;
    }
}