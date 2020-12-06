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
            this.p5_Settings_ui = new StockAnalysis.UserControls.p5_Settings_UI();
            this.p6_l_AboutLabel = new System.Windows.Forms.Label();
            this.p2_CalculateIntrinsicValue_ui = new StockAnalysis.UserControls.p2_CalculateIntrinsicValue_UI();
            this.p1_CalculateStockData_ui = new StockAnalysis.UserControls.p1_CalculateStockData_UI();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
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
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.p5_Settings_ui);
            this.splitContainer1.Panel2.Controls.Add(this.p6_l_AboutLabel);
            this.splitContainer1.Panel2.Controls.Add(this.p2_CalculateIntrinsicValue_ui);
            this.splitContainer1.Panel2.Controls.Add(this.p1_CalculateStockData_ui);
            this.splitContainer1.Size = new System.Drawing.Size(1147, 669);
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
            this.b_UpdateExcel_p4.Click += new System.EventHandler(this.b_UpdateExcel_p4_Click);
            // 
            // b_About_p6
            // 
            this.b_About_p6.Location = new System.Drawing.Point(12, 177);
            this.b_About_p6.Name = "b_About_p6";
            this.b_About_p6.Size = new System.Drawing.Size(175, 23);
            this.b_About_p6.TabIndex = 0;
            this.b_About_p6.Text = "About";
            this.b_About_p6.UseVisualStyleBackColor = true;
            this.b_About_p6.Click += new System.EventHandler(this.b_About_p6_Click);
            // 
            // b_Exit
            // 
            this.b_Exit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.b_Exit.Location = new System.Drawing.Point(12, 634);
            this.b_Exit.Name = "b_Exit";
            this.b_Exit.Size = new System.Drawing.Size(90, 23);
            this.b_Exit.TabIndex = 0;
            this.b_Exit.Text = "Exit";
            this.b_Exit.UseVisualStyleBackColor = true;
            this.b_Exit.Click += new System.EventHandler(this.b_Exit_Click);
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
            this.b_Settings_p5.Click += new System.EventHandler(this.b_Settings_p5_Click);
            // 
            // b_CalculateIntrinsicValue_p2
            // 
            this.b_CalculateIntrinsicValue_p2.Location = new System.Drawing.Point(12, 45);
            this.b_CalculateIntrinsicValue_p2.Name = "b_CalculateIntrinsicValue_p2";
            this.b_CalculateIntrinsicValue_p2.Size = new System.Drawing.Size(175, 23);
            this.b_CalculateIntrinsicValue_p2.TabIndex = 0;
            this.b_CalculateIntrinsicValue_p2.Text = "Calculate Intrinsic Value";
            this.b_CalculateIntrinsicValue_p2.UseVisualStyleBackColor = true;
            this.b_CalculateIntrinsicValue_p2.Click += new System.EventHandler(this.b_CalculateIntrinsicValue_p2_Click);
            // 
            // b_CalculateStockData_p1
            // 
            this.b_CalculateStockData_p1.Location = new System.Drawing.Point(12, 12);
            this.b_CalculateStockData_p1.Name = "b_CalculateStockData_p1";
            this.b_CalculateStockData_p1.Size = new System.Drawing.Size(175, 23);
            this.b_CalculateStockData_p1.TabIndex = 0;
            this.b_CalculateStockData_p1.Text = "Calculate Stock Data";
            this.b_CalculateStockData_p1.UseVisualStyleBackColor = true;
            this.b_CalculateStockData_p1.Click += new System.EventHandler(this.b_CalculateStockData_p1_Click);
            // 
            // p5_Settings_ui
            // 
            this.p5_Settings_ui.Location = new System.Drawing.Point(12, 15);
            this.p5_Settings_ui.Name = "p5_Settings_ui";
            this.p5_Settings_ui.Size = new System.Drawing.Size(703, 97);
            this.p5_Settings_ui.TabIndex = 3;
            this.p5_Settings_ui.Visible = false;
            // 
            // p6_l_AboutLabel
            // 
            this.p6_l_AboutLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.p6_l_AboutLabel.Location = new System.Drawing.Point(320, 185);
            this.p6_l_AboutLabel.Name = "p6_l_AboutLabel";
            this.p6_l_AboutLabel.Size = new System.Drawing.Size(300, 300);
            this.p6_l_AboutLabel.TabIndex = 2;
            this.p6_l_AboutLabel.Text = "I need to write some shit about me that takes 300 in width";
            this.p6_l_AboutLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.p6_l_AboutLabel.Visible = false;
            // 
            // p2_CalculateIntrinsicValue_ui
            // 
            this.p2_CalculateIntrinsicValue_ui.Location = new System.Drawing.Point(12, 12);
            this.p2_CalculateIntrinsicValue_ui.Name = "p2_CalculateIntrinsicValue_ui";
            this.p2_CalculateIntrinsicValue_ui.Size = new System.Drawing.Size(314, 170);
            this.p2_CalculateIntrinsicValue_ui.TabIndex = 1;
            this.p2_CalculateIntrinsicValue_ui.Visible = false;
            // 
            // p1_CalculateStockData_ui
            // 
            this.p1_CalculateStockData_ui.Location = new System.Drawing.Point(12, 12);
            this.p1_CalculateStockData_ui.Name = "p1_CalculateStockData_ui";
            this.p1_CalculateStockData_ui.Size = new System.Drawing.Size(881, 616);
            this.p1_CalculateStockData_ui.TabIndex = 0;
            this.p1_CalculateStockData_ui.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1147, 669);
            this.Controls.Add(this.splitContainer1);
            this.MinimumSize = new System.Drawing.Size(1110, 620);
            this.Name = "Form1";
            this.Text = "Form1";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
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
        private UserControls.p2_CalculateIntrinsicValue_UI p2_CalculateIntrinsicValue_ui;
        private System.Windows.Forms.Label p6_l_AboutLabel;
        private UserControls.p5_Settings_UI p5_Settings_ui;
        public UserControls.p1_CalculateStockData_UI p1_CalculateStockData_ui;
    }
}

