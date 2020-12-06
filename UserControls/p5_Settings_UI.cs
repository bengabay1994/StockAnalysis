
namespace StockAnalysis.UserControls
{
    using System;
    using System.Windows.Forms;

    public partial class p5_Settings_UI : UserControl
    {
        public p5_Settings_UI()
        {
            InitializeComponent();
        }

        private void b_FavoriteStocksExcel_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                tb_FavoriteStocksExcel.Text = fbd.SelectedPath;
                Properties.Settings.Default.FavoritStocksExcelLocation = tb_FavoriteStocksExcel.Text;
            }
        }

        private void b_StocksKeyRatiosFiles_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                tb_StocksKeyRatiosFiles.Text = fbd.SelectedPath;
                Properties.Settings.Default.StocksKeyRatiosLocation = tb_StocksKeyRatiosFiles.Text;
            }
        }
    }
}
