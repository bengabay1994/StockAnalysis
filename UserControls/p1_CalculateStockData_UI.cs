
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
        private Dictionary<StocksEnums.BigFiveNumbersDicKey, IList<string>> m_BigFive;

        private Dictionary<StocksEnums.GrowthNumbersDicKey, IList<string>> m_BigGrowths;

        private string m_Symbol;

        public p1_CalculateStockData_UI()
        {
            InitializeComponent();
            ControlsHelperFunctions.CreateStockDataLabels(this.tableLayoutPanel1);
        }

        private void b_GetOnlineData_Click(object sender, EventArgs e)
        {
            string symbol = tb_Symbol.Text;

            if (!symbol.All(Char.IsLetter) || string.IsNullOrWhiteSpace(symbol))
            {
                MessageBox.Show("Please Enter a Valid Stock Symbol","Wrong Symbol", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            symbol = symbol.ToUpperInvariant();
            MessageBox.Show("Downloading File...", "Donwload");
            FileHandling.DownloadKeyRatioFile(symbol);
        }

        private async void b_GetLocalData_Click(object sender, EventArgs e)
        {
            string stockSymbol = tb_Symbol.Text;

            if (cb_UseSettingStockData.Checked)
            {
                string folderPath = Properties.Settings.Default.StocksKeyRatiosLocation;
                if(string.IsNullOrWhiteSpace(folderPath))
                {
                    MessageBox.Show("Please choose a valid settings in the setting tab for the data files location", "Invalid Settings", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!stockSymbol.All(Char.IsLetter))
                {
                    MessageBox.Show("Please Enter a Valid Stock Symbol", "Wrong Symbol", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                stockSymbol = stockSymbol.ToUpperInvariant();
                string fileName = string.Join("_", stockSymbol, Properties.Settings.Default.KeyRatiosFileNameExtension);
                string filePath = string.Join("\\", folderPath, fileName);
                try
                {
                    await FileHandling.ConvertCsvToXlsxAsync(folderPath, fileName).ConfigureAwait(false);
                    (m_BigFive, m_BigGrowths) = await GetDataAndNumbers.GetStockDataAsync(filePath, folderPath, fileName).ConfigureAwait(false);
                    m_Symbol = stockSymbol;
                    if (this.tableLayoutPanel1.InvokeRequired)
                    {
                        this.tableLayoutPanel1.Invoke(new MethodInvoker(delegate {
                            GetDataAndNumbers.ShowStockData(ref m_BigFive, ref m_BigGrowths, this.tableLayoutPanel1);
                        }));
                    }
                    else
                    {
                        GetDataAndNumbers.ShowStockData(ref m_BigFive, ref m_BigGrowths, this.tableLayoutPanel1);
                    }

                }
                catch (Exception exc)
                {
                    if (exc is BadOrCorruptedFileException)
                    {
                        MessageBox.Show("Invalid file has been choosen ", "Bad File", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (exc is MissingFileException)
                    {
                        MessageBox.Show($"Can't find file: {fileName} inside folder: {folderPath}", "Missing File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Error has occurred", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    // write exception to log file.
                }
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
                        string folderPath, fileName;

                        (folderPath, fileName) = FileHandling.SplitToNameAndPath(filePath);

                        await FileHandling.ConvertCsvToXlsxAsync(folderPath, fileName).ConfigureAwait(false);

                        (m_BigFive, m_BigGrowths) =  await GetDataAndNumbers.GetStockDataAsync(filePath, folderPath, fileName).ConfigureAwait(false);

                        if (string.IsNullOrWhiteSpace(stockSymbol) || !stockSymbol.All(Char.IsLetter))
                        {
                            m_Symbol = "N/A";
                        }
                        else
                        {
                            m_Symbol = stockSymbol;
                        }

                        if (this.tableLayoutPanel1.InvokeRequired)
                        {
                            this.tableLayoutPanel1.Invoke( new MethodInvoker(delegate {
                                GetDataAndNumbers.ShowStockData(ref m_BigFive, ref m_BigGrowths, this.tableLayoutPanel1);
                            }));
                        }
                        else
                        {
                            GetDataAndNumbers.ShowStockData(ref m_BigFive, ref m_BigGrowths, this.tableLayoutPanel1);
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

        private async void b_SaveToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                await FileHandling.CreateFavStockExcelAsync();
            }catch(Exception exc) 
            {
                if(exc is MissConfigurationException)
                {
                    MessageBox.Show(exc.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Error has occurred", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            try
            {
                // save to file.
            }catch(Exception exc)
            {

            }
        }

        private void rb_Green_CheckedChanged(object sender, EventArgs e)
        {
            ActivateSaveStockButton();
        }

        private void rb_Red_CheckedChanged(object sender, EventArgs e)
        {
            ActivateSaveStockButton();
        }

        private void rb_OperatingCash_CheckedChanged(object sender, EventArgs e)
        {
            ActivateSaveStockButton();
        }

        private void rb_Cash_CheckedChanged(object sender, EventArgs e)
        {
            ActivateSaveStockButton();
        }

        private void ActivateSaveStockButton()
        {
            if ((rb_Green.Checked || rb_Red.Checked) && (rb_Cash.Checked || rb_OperatingCash.Checked))
            {
                b_SaveToExcel.Enabled = true;
            }
        }
    }
}
