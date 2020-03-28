using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Visual_Calculator
{
    public partial class KalkulatorMain : Form
    {
        Double lastResult = .0;
        
        enum op { RESULT, MULTIPLY, DIVIDE, SUBSTRACT, ADD };
        
        op lastOperation = op.RESULT;

        public KalkulatorMain()
        {
            InitializeComponent();
            validateTxtBox();
        }

        private void validateTxtBox()
        {
            //removing starting zeros
            while(txtBoxResult.TextLength > 1 && txtBoxResult.Text.Substring(0, 2) == "00")
            {
                txtBoxResult.Text = txtBoxResult.Text.Substring(1);
            }
            if(txtBoxResult.TextLength > 1 && txtBoxResult.Text[0] == '0' && txtBoxResult.Text[1] != '.')
            {
                txtBoxResult.Text = txtBoxResult.Text.Substring(1);
            }
            if(txtBoxResult.TextLength == 0)
            {
                txtBoxResult.Text = "0";
            }
        }
        private void onNumberKey(char key)
        {
            txtBoxResult.AppendText(key.ToString());
            validateTxtBox();
        }
        private void btnKey1_Click(object sender, EventArgs e)
        {
            onNumberKey('1');
        }

        private void btnKey2_Click(object sender, EventArgs e)
        {
            onNumberKey('2');
        }

        private void btnKey3_Click(object sender, EventArgs e)
        {
            onNumberKey('3');
        }

        private void btnKey4_Click(object sender, EventArgs e)
        {
            onNumberKey('4');
        }

        private void btnKey5_Click(object sender, EventArgs e)
        {
            onNumberKey('5');
        }

        private void btnKey6_Click(object sender, EventArgs e)
        {
            onNumberKey('6');
        }

        private void btnKey7_Click(object sender, EventArgs e)
        {
            onNumberKey('7');
        }

        private void btnKey8_Click(object sender, EventArgs e)
        {
            onNumberKey('8');
        }

        private void btnKey9_Click(object sender, EventArgs e)
        {
            onNumberKey('9');
        }

        private void btnKey0_Click(object sender, EventArgs e)
        {
            onNumberKey('0');
        }

        private void btnKeyComma_Click(object sender, EventArgs e)
        {
            onNumberKey('.');
        }

        private void btnKeySign_Click(object sender, EventArgs e)
        {

        }

        private void btnKeyDivide_Click(object sender, EventArgs e)
        {

        }

        private void btnKeyMultiply_Click(object sender, EventArgs e)
        {

        }

        private void btnKeySubstract_Click(object sender, EventArgs e)
        {

        }

        private void btnKeyAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnKeyEqual_Click(object sender, EventArgs e)
        {

        }
    }
}
