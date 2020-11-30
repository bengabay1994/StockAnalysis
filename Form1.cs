
namespace StockAnalysis
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using Exceptions;
    using Common;
    using Extensions;

    public partial class Form1 : Form
    {
        private const int c_amountOfPages = 6;

        public Form1()
        {
            InitializeComponent();
        }

        private void b_CalculateStockData_p1_Click(object sender, EventArgs e)
        {
            ChangePage(StocksEnums.Pages.CalculateStockData);
        }

        private void b_CalculateIntrinsicValue_p2_Click(object sender, EventArgs e)
        {
            ChangePage(StocksEnums.Pages.CalculateIntrinsicValue);
        }

        private async void b_Automate_p3_Click(object sender, EventArgs e)
        {
            ChangePage(StocksEnums.Pages.Automate);
        }

        private void b_UpdateExcel_p4_Click(object sender, EventArgs e)
        {
            ChangePage(StocksEnums.Pages.UpdateExcel);
        }

        private void b_Settings_p5_Click(object sender, EventArgs e)
        {
            ChangePage(StocksEnums.Pages.Settings);
        }

        private void b_About_p6_Click(object sender, EventArgs e)
        {
            ChangePage(StocksEnums.Pages.About);
        }

        private void b_Exit_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (ObjectDisposedException exc)
            {
                // show Error message box
            }
            catch (InvalidOperationException exc)
            {
                // show Error message box
            }
        }

        /***************************************************
         *********      Gui Helper Functions    ************
         ***************************************************/

        private void HidePages(IEnumerable<int> pages)
        {
            ChangeControlsTo(pages, false);
        }

        private void ShowPage(int pageNumber)
        {
            // check under and above limits

            ChangeControlsTo(new int[] { pageNumber }, true);
        }

        private void ChangePage(StocksEnums.Pages newPage)
        {
            int page = (int)newPage;

            if (page <= 0 || page > c_amountOfPages)
            {
                throw new PageNumberOutOfBoundException(page);
            }

            IEnumerable<int> pagesToHide = Enumerable.Range(1, c_amountOfPages).Where(p => p != page);

            HidePages(pagesToHide);

            ShowPage(page);
        }

        /**************************************************************************************
         ****  EveryThing Below This Fucntion should be above Gui Helper Function     *********
         **************************************************************************************/

        private void ChangeControlsTo(IEnumerable<int> pages, bool visible)
        {
            IEnumerable<string> pagesStr = pages.Select(p => "p" + p.ToString());

            IEnumerable<Control> controlsToChange = splitContainer1.Panel2.Controls.Cast<Control>();

            controlsToChange = controlsToChange.Where(control => pagesStr.Contains(control.Name.Substring(0, 2)));

            foreach (Control control in controlsToChange)
            {
                control.Visible = visible;
            }
        }

    }
}
