using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace StockAnalysis.UserControls
{
    using Config;

    public partial class p5_Settings_UI : UserControl
    {
        public SettingsConfig m_SettingsConfig;

        public p5_Settings_UI()
        {
            InitializeComponent();
        }

        private void b_FavoriteStocksExcel_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                
            }
        }
    }
}
