namespace StockAnalysis.UserControls
{
    partial class p5_Settings_UI
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.b_StocksKeyRatiosFiles = new System.Windows.Forms.Button();
            this.l_FavoriteStocksExcel = new System.Windows.Forms.Label();
            this.l_StocksKeyRatiosFiles = new System.Windows.Forms.Label();
            this.tb_FavoriteStocksExcel = new System.Windows.Forms.TextBox();
            this.tb_StocksKeyRatiosFiles = new System.Windows.Forms.TextBox();
            this.b_FavoriteStocksExcel = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Controls.Add(this.b_StocksKeyRatiosFiles, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.l_FavoriteStocksExcel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.l_StocksKeyRatiosFiles, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tb_FavoriteStocksExcel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tb_StocksKeyRatiosFiles, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.b_FavoriteStocksExcel, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(13, 13);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(678, 88);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // b_StocksKeyRatiosFiles
            // 
            this.b_StocksKeyRatiosFiles.Location = new System.Drawing.Point(612, 47);
            this.b_StocksKeyRatiosFiles.Name = "b_StocksKeyRatiosFiles";
            this.b_StocksKeyRatiosFiles.Size = new System.Drawing.Size(63, 23);
            this.b_StocksKeyRatiosFiles.TabIndex = 3;
            this.b_StocksKeyRatiosFiles.Text = "Browse";
            this.b_StocksKeyRatiosFiles.UseVisualStyleBackColor = true;
            // 
            // l_FavoriteStocksExcel
            // 
            this.l_FavoriteStocksExcel.AutoSize = true;
            this.l_FavoriteStocksExcel.Location = new System.Drawing.Point(3, 0);
            this.l_FavoriteStocksExcel.Name = "l_FavoriteStocksExcel";
            this.l_FavoriteStocksExcel.Size = new System.Drawing.Size(119, 15);
            this.l_FavoriteStocksExcel.TabIndex = 0;
            this.l_FavoriteStocksExcel.Text = "Favorite Stocks Excel:";
            // 
            // l_StocksKeyRatiosFiles
            // 
            this.l_StocksKeyRatiosFiles.AutoSize = true;
            this.l_StocksKeyRatiosFiles.Location = new System.Drawing.Point(3, 44);
            this.l_StocksKeyRatiosFiles.Name = "l_StocksKeyRatiosFiles";
            this.l_StocksKeyRatiosFiles.Size = new System.Drawing.Size(127, 15);
            this.l_StocksKeyRatiosFiles.TabIndex = 1;
            this.l_StocksKeyRatiosFiles.Text = "Stocks Key Ratios Files:";
            // 
            // tb_FavoriteStocksExcel
            // 
            this.tb_FavoriteStocksExcel.Location = new System.Drawing.Point(138, 3);
            this.tb_FavoriteStocksExcel.Name = "tb_FavoriteStocksExcel";
            this.tb_FavoriteStocksExcel.Size = new System.Drawing.Size(468, 23);
            this.tb_FavoriteStocksExcel.TabIndex = 2;
            // 
            // tb_StocksKeyRatiosFiles
            // 
            this.tb_StocksKeyRatiosFiles.Location = new System.Drawing.Point(138, 47);
            this.tb_StocksKeyRatiosFiles.Name = "tb_StocksKeyRatiosFiles";
            this.tb_StocksKeyRatiosFiles.Size = new System.Drawing.Size(468, 23);
            this.tb_StocksKeyRatiosFiles.TabIndex = 2;
            // 
            // b_FavoriteStocksExcel
            // 
            this.b_FavoriteStocksExcel.Location = new System.Drawing.Point(612, 3);
            this.b_FavoriteStocksExcel.Name = "b_FavoriteStocksExcel";
            this.b_FavoriteStocksExcel.Size = new System.Drawing.Size(63, 23);
            this.b_FavoriteStocksExcel.TabIndex = 3;
            this.b_FavoriteStocksExcel.Text = "Browse";
            this.b_FavoriteStocksExcel.UseVisualStyleBackColor = true;
            this.b_FavoriteStocksExcel.Click += new System.EventHandler(this.b_FavoriteStocksExcel_Click);
            // 
            // p5_Settings_UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "p5_Settings_UI";
            this.Size = new System.Drawing.Size(703, 113);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label l_FavoriteStocksExcel;
        private System.Windows.Forms.Label l_StocksKeyRatiosFiles;
        private System.Windows.Forms.TextBox tb_StocksKeyRatiosFiles;
        private System.Windows.Forms.TextBox tb_FavoriteStocksExcel;
        private System.Windows.Forms.Button b_StocksKeyRatiosFiles;
        private System.Windows.Forms.Button b_FavoriteStocksExcel;
    }
}
