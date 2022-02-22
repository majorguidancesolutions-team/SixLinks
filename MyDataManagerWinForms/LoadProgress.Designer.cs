namespace MyDataManagerWinForms
{
    partial class LoadProgress
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
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(34, 90);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(390, 28);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 1;
            // 
            // lblProgress
            // 
            this.lblProgress.Location = new System.Drawing.Point(69, 41);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(317, 25);
            this.lblProgress.TabIndex = 2;
            this.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LoadProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.progressBar);
            this.Name = "LoadProgress";
            this.Size = new System.Drawing.Size(459, 151);
            this.ResumeLayout(false);

        }

        #endregion

        private ProgressBar progressBar;
        private Label lblProgress;
    }
}
