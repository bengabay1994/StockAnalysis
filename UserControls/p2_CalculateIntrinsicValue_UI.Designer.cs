namespace StockAnalysis.UserControls
{
    partial class p2_CalculateIntrinsicValue_UI
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
            this.lt_CurrentEps = new System.Windows.Forms.Label();
            this.lt_EpsGrowth = new System.Windows.Forms.Label();
            this.lt_ForwardPe = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.b_CalculateValue = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.textBox3, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBox2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lt_CurrentEps, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lt_EpsGrowth, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lt_ForwardPe, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.b_CalculateValue, 0, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(15, 10);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(285, 158);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lt_CurrentEps
            // 
            this.lt_CurrentEps.AutoSize = true;
            this.lt_CurrentEps.Location = new System.Drawing.Point(3, 0);
            this.lt_CurrentEps.Name = "lt_CurrentEps";
            this.lt_CurrentEps.Size = new System.Drawing.Size(72, 15);
            this.lt_CurrentEps.TabIndex = 0;
            this.lt_CurrentEps.Text = "Current EPS:";
            // 
            // lt_EpsGrowth
            // 
            this.lt_EpsGrowth.AutoSize = true;
            this.lt_EpsGrowth.Location = new System.Drawing.Point(3, 39);
            this.lt_EpsGrowth.Name = "lt_EpsGrowth";
            this.lt_EpsGrowth.Size = new System.Drawing.Size(71, 15);
            this.lt_EpsGrowth.TabIndex = 0;
            this.lt_EpsGrowth.Text = "EPS Growth:";
            // 
            // lt_ForwardPe
            // 
            this.lt_ForwardPe.AutoSize = true;
            this.lt_ForwardPe.Location = new System.Drawing.Point(3, 78);
            this.lt_ForwardPe.Name = "lt_ForwardPe";
            this.lt_ForwardPe.Size = new System.Drawing.Size(69, 15);
            this.lt_ForwardPe.TabIndex = 0;
            this.lt_ForwardPe.Text = "Forward PE:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(145, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.PlaceholderText = "Current EPS";
            this.textBox1.Size = new System.Drawing.Size(137, 23);
            this.textBox1.TabIndex = 1;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(145, 42);
            this.textBox2.Name = "textBox2";
            this.textBox2.PlaceholderText = "EPS Growth in %";
            this.textBox2.Size = new System.Drawing.Size(137, 23);
            this.textBox2.TabIndex = 1;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(145, 81);
            this.textBox3.Name = "textBox3";
            this.textBox3.PlaceholderText = "Forward PE";
            this.textBox3.Size = new System.Drawing.Size(137, 23);
            this.textBox3.TabIndex = 1;
            // 
            // b_CalculateValue
            // 
            this.b_CalculateValue.Location = new System.Drawing.Point(3, 120);
            this.b_CalculateValue.Name = "b_CalculateValue";
            this.b_CalculateValue.Size = new System.Drawing.Size(136, 23);
            this.b_CalculateValue.TabIndex = 2;
            this.b_CalculateValue.Text = "Calculate Value";
            this.b_CalculateValue.UseVisualStyleBackColor = true;
            // 
            // p2_CalculateIntrinsicValue_UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "p2_CalculateIntrinsicValue_UI";
            this.Size = new System.Drawing.Size(318, 181);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label lt_CurrentEps;
        private System.Windows.Forms.Label lt_EpsGrowth;
        private System.Windows.Forms.Label lt_ForwardPe;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button b_CalculateValue;
    }
}
