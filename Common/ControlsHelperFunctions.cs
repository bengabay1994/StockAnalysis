
namespace StockAnalysis.Common
{
    using System.Windows.Forms;

    static class ControlsHelperFunctions
    {
        private static Label CreateStockDataLabel(int column, int row)
        {
            Label l = new Label();
            l.AutoSize = true;
            l.Name = $"templ{row}{column}";
            l.Size = new System.Drawing.Size(59, 30);
            l.Text = "";
            return l;
        }

        public static void CreateStockDataLabels(TableLayoutPanel p1_LayoutPanel)
        {
            for (int row = 2; row <= 7; row++)
            {
                for (int column = 1; column <= 11; column++)
                {
                    p1_LayoutPanel.Controls.Add(CreateStockDataLabel(column, row), column, row);
                }
            }

            for (int row = 10; row <= 12; row++)
            {
                for (int column = 1; column <= 7; column++)
                {
                    if (column == 6)
                    {
                        continue;
                    }
                    p1_LayoutPanel.Controls.Add(CreateStockDataLabel(column, row), column, row);
                }
            }
        }
    }
}
