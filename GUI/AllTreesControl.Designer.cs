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
            this.TreeSelector = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // TreeSelector
            // 
            this.TreeSelector.FormattingEnabled = true;
            this.TreeSelector.Location = new System.Drawing.Point(319, 22);
            this.TreeSelector.Name = "TreeSelector";
            this.TreeSelector.Size = new System.Drawing.Size(121, 21);
            this.TreeSelector.TabIndex = 0;
            // 
            // AllTreesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TreeSelector);
            this.Name = "AllTreesControl";
            this.Size = new System.Drawing.Size(461, 379);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox TreeSelector;
    }
}
