namespace StockAnalysis
{
    partial class Form1
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.b_UpdateExcel_p4 = new System.Windows.Forms.Button();
            this.b_About_p6 = new System.Windows.Forms.Button();
            this.b_Exit = new System.Windows.Forms.Button();
            this.b_Automate_p3 = new System.Windows.Forms.Button();
            this.b_Settings_p5 = new System.Windows.Forms.Button();
            this.b_CalculateIntrinsicValue_p2 = new System.Windows.Forms.Button();
            this.b_CalculateStockData_p1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.b_UpdateExcel_p4);
            this.splitContainer1.Panel1.Controls.Add(this.b_About_p6);
            this.splitContainer1.Panel1.Controls.Add(this.b_Exit);
            this.splitContainer1.Panel1.Controls.Add(this.b_Automate_p3);
            this.splitContainer1.Panel1.Controls.Add(this.b_Settings_p5);
            this.splitContainer1.Panel1.Controls.Add(this.b_CalculateIntrinsicValue_p2);
            this.splitContainer1.Panel1.Controls.Add(this.b_CalculateStockData_p1);
            this.splitContainer1.Size = new System.Drawing.Size(1116, 651);
            this.splitContainer1.SplitterDistance = 205;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.Text = "splitContainer1";
            // 
            // b_UpdateExcel_p4
            // 
            this.b_UpdateExcel_p4.Location = new System.Drawing.Point(12, 111);
            this.b_UpdateExcel_p4.Name = "b_UpdateExcel_p4";
            this.b_UpdateExcel_p4.Size = new System.Drawing.Size(175, 23);
            this.b_UpdateExcel_p4.TabIndex = 0;
            this.b_UpdateExcel_p4.Text = "Update Excel";
            this.b_UpdateExcel_p4.UseVisualStyleBackColor = true;
            // 
            // b_About_p6
            // 
            this.b_About_p6.Location = new System.Drawing.Point(12, 177);
            this.b_About_p6.Name = "b_About_p6";
            this.b_About_p6.Size = new System.Drawing.Size(175, 23);
            this.b_About_p6.TabIndex = 0;
            this.b_About_p6.Text = "About";
            this.b_About_p6.UseVisualStyleBackColor = true;
            // 
            // b_Exit
            // 
            this.b_Exit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.b_Exit.Location = new System.Drawing.Point(12, 616);
            this.b_Exit.Name = "b_Exit";
            this.b_Exit.Size = new System.Drawing.Size(90, 23);
            this.b_Exit.TabIndex = 0;
            this.b_Exit.Text = "Exit";
            this.b_Exit.UseVisualStyleBackColor = true;
            // 
            // b_Automate_p3
            // 
            this.b_Automate_p3.Location = new System.Drawing.Point(12, 78);
            this.b_Automate_p3.Name = "b_Automate_p3";
            this.b_Automate_p3.Size = new System.Drawing.Size(175, 23);
            this.b_Automate_p3.TabIndex = 0;
            this.b_Automate_p3.Text = "Automate";
            this.b_Automate_p3.UseVisualStyleBackColor = true;
            this.b_Automate_p3.Click += new System.EventHandler(this.b_Automate_p3_Click);
            // 
            // b_Settings_p5
            // 
            this.b_Settings_p5.Location = new System.Drawing.Point(12, 144);
            this.b_Settings_p5.Name = "b_Settings_p5";
            this.b_Settings_p5.Size = new System.Drawing.Size(175, 23);
            this.b_Settings_p5.TabIndex = 0;
            this.b_Settings_p5.Text = "Settings";
            this.b_Settings_p5.UseVisualStyleBackColor = true;
            // 
            // b_CalculateIntrinsicValue_p2
            // 
            this.b_CalculateIntrinsicValue_p2.Location = new System.Drawing.Point(12, 45);
            this.b_CalculateIntrinsicValue_p2.Name = "b_CalculateIntrinsicValue_p2";
            this.b_CalculateIntrinsicValue_p2.Size = new System.Drawing.Size(175, 23);
            this.b_CalculateIntrinsicValue_p2.TabIndex = 0;
            this.b_CalculateIntrinsicValue_p2.Text = "Calculate Intrinsic Value";
            this.b_CalculateIntrinsicValue_p2.UseVisualStyleBackColor = true;
            // 
            // b_CalculateStockData_p1
            // 
            this.b_CalculateStockData_p1.Location = new System.Drawing.Point(12, 12);
            this.b_CalculateStockData_p1.Name = "b_CalculateStockData_p1";
            this.b_CalculateStockData_p1.Size = new System.Drawing.Size(175, 23);
            this.b_CalculateStockData_p1.TabIndex = 0;
            this.b_CalculateStockData_p1.Text = "Calculate Stock Data";
            this.b_CalculateStockData_p1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1116, 651);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button b_Exit;
        private System.Windows.Forms.Button b_AutomateList;
        private System.Windows.Forms.Button b_Automate_p3;
        private System.Windows.Forms.Button b_Settings_p5;
        private System.Windows.Forms.Button b_CalculateIntrinsicValue_p2;
        private System.Windows.Forms.Button b_CalculateStockData_p1;
        private System.Windows.Forms.Button b_About_p6;
        private System.Windows.Forms.Button b_UpdateExcel_p4;
    }
}

