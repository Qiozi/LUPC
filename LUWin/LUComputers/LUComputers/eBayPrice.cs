using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LUComputers
{
    public partial class eBayPrice : Form
    {
        public eBayPrice()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            decimal lcd;
            decimal.TryParse(this.textBox2.Text, out lcd);

            decimal cost;
            decimal.TryParse(this.textBox_cost.Text , out cost);

            label3.Text = Helper.eBayPriceHelper.eBayPartPrice(lcd, cost, 0M).ToString();
        }

    }

}
