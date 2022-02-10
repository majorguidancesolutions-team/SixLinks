namespace MyDataManagerWinForms
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cboMovies = new System.Windows.Forms.ComboBox();
            this.dgItems = new System.Windows.Forms.DataGridView();
            this.BtnDataImport = new System.Windows.Forms.Button();
            this.cboActors = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).BeginInit();
            this.SuspendLayout();
            // 
            // cboMovies
            // 
            this.cboMovies.FormattingEnabled = true;
            this.cboMovies.Location = new System.Drawing.Point(81, 39);
            this.cboMovies.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboMovies.Name = "cboMovies";
            this.cboMovies.Size = new System.Drawing.Size(291, 28);
            this.cboMovies.TabIndex = 0;
            this.cboMovies.SelectedIndexChanged += new System.EventHandler(this.cboMovies_SelectedIndexChanged);
            // 
            // dgItems
            // 
            this.dgItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgItems.Location = new System.Drawing.Point(81, 111);
            this.dgItems.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgItems.Name = "dgItems";
            this.dgItems.RowHeadersWidth = 51;
            this.dgItems.RowTemplate.Height = 25;
            this.dgItems.Size = new System.Drawing.Size(657, 365);
            this.dgItems.TabIndex = 1;
            // 
            // BtnDataImport
            // 
            this.BtnDataImport.Location = new System.Drawing.Point(773, 166);
            this.BtnDataImport.Name = "BtnDataImport";
            this.BtnDataImport.Size = new System.Drawing.Size(94, 29);
            this.BtnDataImport.TabIndex = 2;
            this.BtnDataImport.Text = "Load Data";
            this.BtnDataImport.UseVisualStyleBackColor = true;
            this.BtnDataImport.Click += new System.EventHandler(this.BtnDataImport_Click);
            // 
            // cboActors
            // 
            this.cboActors.FormattingEnabled = true;
            this.cboActors.Location = new System.Drawing.Point(447, 39);
            this.cboActors.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboActors.Name = "cboActors";
            this.cboActors.Size = new System.Drawing.Size(291, 28);
            this.cboActors.TabIndex = 3;
            this.cboActors.SelectedIndexChanged += new System.EventHandler(this.cboActors_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 600);
            this.Controls.Add(this.cboActors);
            this.Controls.Add(this.BtnDataImport);
            this.Controls.Add(this.dgItems);
            this.Controls.Add(this.cboMovies);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComboBox cboMovies;
        private DataGridView dgItems;
        private Button BtnDataImport;
        private ComboBox cboActors;
    }
}