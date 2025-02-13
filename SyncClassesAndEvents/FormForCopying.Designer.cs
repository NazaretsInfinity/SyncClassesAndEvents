namespace SyncClassesAndEvents
{
    partial class FormForCopying
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
            this.CopyButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CopyButton
            // 
            this.CopyButton.Location = new System.Drawing.Point(68, 35);
            this.CopyButton.Name = "CopyButton";
            this.CopyButton.Size = new System.Drawing.Size(270, 47);
            this.CopyButton.TabIndex = 0;
            this.CopyButton.Text = "copy";
            this.CopyButton.UseVisualStyleBackColor = true;
            this.CopyButton.Click += new System.EventHandler(this.CopyButton_Click);
            // 
            // FormForCopying
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 109);
            this.Controls.Add(this.CopyButton);
            this.Name = "FormForCopying";
            this.Text = "Form for copies";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button CopyButton;
    }
}

