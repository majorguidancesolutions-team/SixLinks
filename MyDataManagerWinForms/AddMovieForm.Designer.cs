namespace MyDataManagerWinForms
{
    partial class AddMovieForm
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
            this.lblMovieTitle = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMovieTitle = new System.Windows.Forms.TextBox();
            this.txtMovieYear = new System.Windows.Forms.TextBox();
            this.btnOkMovie = new System.Windows.Forms.Button();
            this.btnCancelMovie = new System.Windows.Forms.Button();
            this.txtMovieId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblMovieTitle
            // 
            this.lblMovieTitle.AutoSize = true;
            this.lblMovieTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblMovieTitle.Location = new System.Drawing.Point(55, 112);
            this.lblMovieTitle.Name = "lblMovieTitle";
            this.lblMovieTitle.Size = new System.Drawing.Size(105, 25);
            this.lblMovieTitle.TabIndex = 0;
            this.lblMovieTitle.Text = "Movie Title";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(55, 155);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Release Year";
            // 
            // txtMovieTitle
            // 
            this.txtMovieTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtMovieTitle.Location = new System.Drawing.Point(192, 109);
            this.txtMovieTitle.Name = "txtMovieTitle";
            this.txtMovieTitle.Size = new System.Drawing.Size(231, 32);
            this.txtMovieTitle.TabIndex = 1;
            // 
            // txtMovieYear
            // 
            this.txtMovieYear.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtMovieYear.Location = new System.Drawing.Point(192, 155);
            this.txtMovieYear.Name = "txtMovieYear";
            this.txtMovieYear.Size = new System.Drawing.Size(231, 32);
            this.txtMovieYear.TabIndex = 2;
            // 
            // btnOkMovie
            // 
            this.btnOkMovie.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnOkMovie.Location = new System.Drawing.Point(108, 247);
            this.btnOkMovie.Name = "btnOkMovie";
            this.btnOkMovie.Size = new System.Drawing.Size(94, 53);
            this.btnOkMovie.TabIndex = 3;
            this.btnOkMovie.Text = "Ok";
            this.btnOkMovie.UseVisualStyleBackColor = true;
            this.btnOkMovie.Click += new System.EventHandler(this.btnOkMovie_Click);
            // 
            // btnCancelMovie
            // 
            this.btnCancelMovie.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnCancelMovie.Location = new System.Drawing.Point(273, 247);
            this.btnCancelMovie.Name = "btnCancelMovie";
            this.btnCancelMovie.Size = new System.Drawing.Size(94, 53);
            this.btnCancelMovie.TabIndex = 4;
            this.btnCancelMovie.Text = "Cancel";
            this.btnCancelMovie.UseVisualStyleBackColor = true;
            this.btnCancelMovie.Click += new System.EventHandler(this.btnCancelMovie_Click);
            // 
            // txtMovieId
            // 
            this.txtMovieId.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtMovieId.Location = new System.Drawing.Point(192, 63);
            this.txtMovieId.Name = "txtMovieId";
            this.txtMovieId.ReadOnly = true;
            this.txtMovieId.Size = new System.Drawing.Size(231, 32);
            this.txtMovieId.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(55, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 25);
            this.label1.TabIndex = 5;
            this.label1.Text = "Movie ID";
            // 
            // AddMovieForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 355);
            this.Controls.Add(this.txtMovieId);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancelMovie);
            this.Controls.Add(this.btnOkMovie);
            this.Controls.Add(this.txtMovieYear);
            this.Controls.Add(this.txtMovieTitle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblMovieTitle);
            this.Name = "AddMovieForm";
            this.Text = "AddMovieForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lblMovieTitle;
        private Label label2;
        private TextBox txtMovieTitle;
        private TextBox txtMovieYear;
        private Button btnOkMovie;
        private Button btnCancelMovie;
        private TextBox txtMovieId;
        private Label label1;
    }
}