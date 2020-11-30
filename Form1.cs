
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

        private async void b_Automate_p3_Click(object sender, EventArgs e)
        {
            
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

        private async Task ChangePage(int newPage)
        {
            if (newPage <= 0 || newPage > c_amountOfPages)
            {
                throw new PageNumberOutOfBoundException(newPage);
            }

            IEnumerable<int> pagesToHide = Enumerable.Range(1, c_amountOfPages).Where(p => p != newPage);

            await Task.Run(() => HidePages(pagesToHide)).ConfigureAwait(false);

            await Task.Run(() => ShowPage(newPage)).ConfigureAwait(false);
        }

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
