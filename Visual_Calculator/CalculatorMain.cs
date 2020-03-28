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
        enum op { NONE, RESULT, MULTIPLY, DIVIDE, SUBSTRACT, ADD };

        long lastResult = 0;
        long result = 0;
        op lastOperation = op.NONE;

        Dictionary<op, char> opChars = new Dictionary<op, char> {
            { op.ADD,'+' },
            { op.SUBSTRACT,'-' },
            { op.MULTIPLY,'*' },
            { op.DIVIDE,'/' },
            { op.RESULT,'=' },
        };

        String history = "";
        bool clearResult = true;
        bool clearHistory = true;

        public KalkulatorMain()
        {
            InitializeComponent();
            updateTexts();
        }

        private void updateTexts()
        {
            txtBoxResult.Text = result.ToString();
            lblInfo.Text = history;
        }

        private void onNumberKey(int nr)
        {
            result = clearResult ? nr : result * 10 + (Math.Sign(result) != 0 ? (Math.Sign(result) * nr) : nr);
            clearResult = false;
            updateTexts();
        }
        private long executeOp(op o, long firstOp, long secondOp)
        {
            switch (o)
            {
                case op.ADD:
                    return firstOp + secondOp;
                case op.SUBSTRACT:
                    return firstOp - secondOp;
                case op.MULTIPLY:
                    return firstOp * secondOp;
                case op.DIVIDE:
                    return firstOp / secondOp;
            }
            return 0;
        }

        private void doOperation(op o)
        {
            if (clearHistory) history = "";    
            
            history += " " + result.ToString() + " " + opChars[o];

            clearHistory = o == op.RESULT ? true : false;

            if (lastOperation != op.NONE)
            {
                try
                {
                    result = lastOperation == op.RESULT ? result : executeOp(lastOperation, lastResult, result);
                }
                catch (DivideByZeroException e)
                {
                    result = 0;
                    o = op.NONE;
                    history += " Błąd!";
                    clearHistory = true;
                }
            }
            lastResult = result;
            clearResult = true;
            lastOperation = o;
            updateTexts();
        }
        private void doChangeSign()
        {
            result = -result;
            updateTexts();
        }
        private void btnKey1_Click(object sender, EventArgs e)
        {
            onNumberKey(1);
        }

        private void btnKey2_Click(object sender, EventArgs e)
        {
            onNumberKey(2);
        }

        private void btnKey3_Click(object sender, EventArgs e)
        {
            onNumberKey(3);
        }

        private void btnKey4_Click(object sender, EventArgs e)
        {
            onNumberKey(4);
        }

        private void btnKey5_Click(object sender, EventArgs e)
        {
            onNumberKey(5);
        }

        private void btnKey6_Click(object sender, EventArgs e)
        {
            onNumberKey(6);
        }

        private void btnKey7_Click(object sender, EventArgs e)
        {
            onNumberKey(7);
        }
        private void btnKey8_Click(object sender, EventArgs e)
        {
            onNumberKey(8);
        }

        private void btnKey9_Click(object sender, EventArgs e)
        {
            onNumberKey(9);
        }

        private void btnKey0_Click(object sender, EventArgs e)
        {
            onNumberKey(0);
        }

        private void btnKeyComma_Click(object sender, EventArgs e)
        {

        }

        private void btnKeySign_Click(object sender, EventArgs e)
        {
            doChangeSign();
        }

        private void btnKeyDivide_Click(object sender, EventArgs e)
        {
            doOperation(op.DIVIDE);
        }

        private void btnKeyMultiply_Click(object sender, EventArgs e)
        {
            doOperation(op.MULTIPLY);
        }

        private void btnKeySubstract_Click(object sender, EventArgs e)
        {
            doOperation(op.SUBSTRACT);
        }

        private void btnKeyAdd_Click(object sender, EventArgs e)
        {
            doOperation(op.ADD);
        }

        private void btnKeyEqual_Click(object sender, EventArgs e)
        {
            doOperation(op.RESULT);
        }

    }
}
