
namespace StockAnalysis.UserControls
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Threading;
    using System.Windows.Forms;

    using Forms;
    using Common;

    public partial class p1_CalculateStockData_UI : UserControl
    {
        public p1_CalculateStockData_UI()
        {
            InitializeComponent();
        }

        private void b_GetOnlineData_Click(object sender, EventArgs e)
        {

            // NEED TO ADD CHECK IF USE SETTING IS CHECKED AND ACT ON IT
            string symbol = tb_Symbol.Text;

            if (!symbol.All(Char.IsLetter))
            {
                MessageBox.Show("Please Enter a Valid Stock Symbol","Wrong Symbol", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show("Downloading File...", "Donwload");

            FileHandeling.DownloadKeyRatioFile(symbol);
        }
    }
}
