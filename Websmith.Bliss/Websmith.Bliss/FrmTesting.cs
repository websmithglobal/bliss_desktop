using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Websmith.Bliss
{
    public partial class FrmTesting : Form
    {
        public FrmTesting()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Decimal TotalAmount = 0;
            Decimal Fees = 0;
            Decimal ItemTotal = 0;
            Decimal Tax1 = 0;
            Decimal Tax2 = 0;
            Decimal Tax1Amount = 0;
            Decimal Tax2Amount = 0;
            Decimal SubTotal = 0;
            Decimal TaxSum = 0;
            Decimal TotalTaxAmount = 0;

            TotalAmount = Convert.ToDecimal(txtTotalAmount.Text);
            Fees = Convert.ToDecimal(txtFees.Text);
            Tax1 = Convert.ToDecimal(txtTax1.Text);
            Tax2 = Convert.ToDecimal(txtTax2.Text);

            TaxSum = Tax1 + Tax2;
            SubTotal = TotalAmount - Fees;

            ItemTotal = (SubTotal * 100) / (100 + TaxSum);

            TotalTaxAmount = SubTotal - ItemTotal;
            Tax1Amount = TotalTaxAmount / 2;
            Tax2Amount = TotalTaxAmount / 2;

            txtItemTotal.Text = Math.Round(ItemTotal, 2).ToString(); 
            txtTax1Amount.Text = Math.Round(Tax1Amount, 2).ToString(); 
            txtTax2Amount.Text = Math.Round(Tax2Amount, 2).ToString(); 
        }

        private void FrmTesting_Load(object sender, EventArgs e)
        {

        }
    }
}
