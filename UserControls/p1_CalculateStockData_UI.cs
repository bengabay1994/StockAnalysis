
namespace StockAnalysis.UserControls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    using Common;
    using Exceptions;

    public partial class p1_CalculateStockData_UI : UserControl
    {
        public p1_CalculateStockData_UI()
        {
            InitializeComponent();
        }

        private void b_GetOnlineData_Click(object sender, EventArgs e)
        {
            string symbol = tb_Symbol.Text;

            if (!symbol.All(Char.IsLetter))
            {
                MessageBox.Show("Please Enter a Valid Stock Symbol","Wrong Symbol", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show("Downloading File...", "Donwload");

            FileHandeling.DownloadKeyRatioFile(symbol);
        }

        private async void b_GetLocalData_Click(object sender, EventArgs e)
        {
            Dictionary<StocksEnums.BigFiveNumbersDicKey, IList<string>> BigFive;
            Dictionary<StocksEnums.GrowthNumbersDicKey, IList<string>> BigGrowths;

            if (cb_UseSettingStockData.Checked)
            {
                
            }
            else
            {
                OpenFileDialog fd = new OpenFileDialog();
                fd.Filter = "Excel files (*.xls or .xlsx)|.xls;*.xlsx";
                if(fd.ShowDialog() == DialogResult.OK) 
                {
                    string filePath = fd.FileName;
                    try
                    {
                        (BigFive, BigGrowths) =  await GetDataAndNumbers.GetStockDataAsync(filePath).ConfigureAwait(false);
                        if (this.tableLayoutPanel1.InvokeRequired)
                        {
                            this.tableLayoutPanel1.Invoke( new MethodInvoker(delegate {
                                GetDataAndNumbers.ShowStockData(ref BigFive, ref BigGrowths, this.tableLayoutPanel1);
                            }));
                        }
                        else
                        {
                            GetDataAndNumbers.ShowStockData(ref BigFive, ref BigGrowths, this.tableLayoutPanel1);

                        }

                    }
                    catch (Exception exc)
                    {
                        if(exc is MissingCategoryException || exc is BadOrCorruptedFileException)
                        {
                            MessageBox.Show("Invalid file has been choosen ", "Bad File", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show("Error has occurred", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        // write exception to log file.
                    }
                }
            }
        }
    }
}
