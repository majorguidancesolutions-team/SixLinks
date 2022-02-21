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
            this.cboActors = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAddMovie = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnAddActor = new System.Windows.Forms.Button();
            this.btnBacon = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuComboBx = new System.Windows.Forms.ToolStripComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboMovies
            // 
            this.cboMovies.FormattingEnabled = true;
            this.cboMovies.Location = new System.Drawing.Point(183, 85);
            this.cboMovies.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboMovies.Name = "cboMovies";
            this.cboMovies.Size = new System.Drawing.Size(291, 28);
            this.cboMovies.TabIndex = 2;
            this.cboMovies.SelectedIndexChanged += new System.EventHandler(this.cboMovies_SelectedIndexChanged);
            // 
            // dgItems
            // 
            this.dgItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgItems.Location = new System.Drawing.Point(12, 138);
            this.dgItems.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgItems.Name = "dgItems";
            this.dgItems.RowHeadersWidth = 51;
            this.dgItems.RowTemplate.Height = 25;
            this.dgItems.Size = new System.Drawing.Size(495, 400);
            this.dgItems.TabIndex = 3;
            // 
            // cboActors
            // 
            this.cboActors.FormattingEnabled = true;
            this.cboActors.Location = new System.Drawing.Point(183, 36);
            this.cboActors.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboActors.Name = "cboActors";
            this.cboActors.Size = new System.Drawing.Size(291, 28);
            this.cboActors.TabIndex = 1;
            this.cboActors.SelectedIndexChanged += new System.EventHandler(this.cboActors_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(44, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "Actors";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(44, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 25);
            this.label2.TabIndex = 5;
            this.label2.Text = "Movies";
            // 
            // btnAddMovie
            // 
            this.btnAddMovie.FlatAppearance.BorderSize = 2;
            this.btnAddMovie.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnAddMovie.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnAddMovie.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnAddMovie.Location = new System.Drawing.Point(571, 128);
            this.btnAddMovie.Name = "btnAddMovie";
            this.btnAddMovie.Size = new System.Drawing.Size(121, 71);
            this.btnAddMovie.TabIndex = 5;
            this.btnAddMovie.Text = "Add Movie";
            this.btnAddMovie.UseVisualStyleBackColor = true;
            this.btnAddMovie.Click += new System.EventHandler(this.btnAddMovie_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.FlatAppearance.BorderSize = 2;
            this.btnDelete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnDelete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnDelete.Location = new System.Drawing.Point(571, 305);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(121, 71);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.FlatAppearance.BorderSize = 2;
            this.btnUpdate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnUpdate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnUpdate.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnUpdate.Location = new System.Drawing.Point(571, 216);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(121, 71);
            this.btnUpdate.TabIndex = 6;
            this.btnUpdate.Text = "Update ";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnAddActor
            // 
            this.btnAddActor.FlatAppearance.BorderSize = 2;
            this.btnAddActor.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnAddActor.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnAddActor.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnAddActor.Location = new System.Drawing.Point(571, 39);
            this.btnAddActor.Name = "btnAddActor";
            this.btnAddActor.Size = new System.Drawing.Size(121, 71);
            this.btnAddActor.TabIndex = 4;
            this.btnAddActor.Text = "Add Actor";
            this.btnAddActor.UseVisualStyleBackColor = true;
            this.btnAddActor.Click += new System.EventHandler(this.btnAddActor_Click);
            // 
            // btnBacon
            // 
            this.btnBacon.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnBacon.Location = new System.Drawing.Point(542, 402);
            this.btnBacon.Name = "btnBacon";
            this.btnBacon.Size = new System.Drawing.Size(185, 136);
            this.btnBacon.TabIndex = 8;
            this.btnBacon.Text = "Link to Bacon";
            this.btnBacon.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuComboBx});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(748, 32);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuComboBx
            // 
            this.menuComboBx.Items.AddRange(new object[] {
            "Add Actor",
            "Add Movie",
            "Update",
            "Delete",
            "Link Actors/Movies"});
            this.menuComboBx.Name = "menuComboBx";
            this.menuComboBx.Size = new System.Drawing.Size(121, 28);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 592);
            this.Controls.Add(this.btnBacon);
            this.Controls.Add(this.btnAddActor);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAddMovie);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboActors);
            this.Controls.Add(this.dgItems);
            this.Controls.Add(this.cboMovies);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.Text = "Seven to Kevin";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComboBox cboMovies;
        private DataGridView dgItems;
        //private Button BtnDataImport;
        private ComboBox cboActors;
        private Label label1;
        private Label label2;
        private Button btnAddMovie;
        private Button btnDelete;
        private Button btnUpdate;
        private Button btnAddActor;
        private Button btnBacon;
        private MenuStrip menuStrip1;
        private ToolStripComboBox menuComboBx;
    }
}