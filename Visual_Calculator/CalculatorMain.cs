using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Visual_Calculator
{
    public partial class KalkulatorMain : Form
    {
        enum op { NONE, MULTIPLY, DIVIDE, SUBSTRACT, ADD, RESULT };

        decimal lastResult;
        
        decimal _result;
        private decimal Result {
            get { 
                return _result; 
            }
            set { 
                _result = value;
                txtBoxResult.Text = _result.ToString();
            }
        }

        op lastOperation = op.NONE;

        Dictionary<op, char> opChars = new Dictionary<op, char> {
            { op.ADD,'+' },
            { op.SUBSTRACT,'-' },
            { op.MULTIPLY,'*' },
            { op.DIVIDE,'/' },
            { op.RESULT,'=' },
        };

        Dictionary<Keys, Action<object, EventArgs>> keyBindings;

        String _history = "";
        private String History { 
            get {
                return _history;
            }
            set {
                _history = value;
                lblInfo.Text = _history;
            } 
        }
        bool clearResult = true;
        bool clearHistory = true;

        public KalkulatorMain()
        {
            InitializeComponent();
            initBindings();

        }
        private void initBindings()
        {
            keyBindings = new Dictionary<Keys, Action<object, EventArgs>>{
                { Keys.D1, this.btnKey1_Click },
                { Keys.D2, this.btnKey2_Click },
                { Keys.D3, this.btnKey3_Click },
                { Keys.D4, this.btnKey4_Click },
                { Keys.D5, this.btnKey5_Click },
                { Keys.D6, this.btnKey6_Click },
                { Keys.D7, this.btnKey7_Click },
                { Keys.D8, this.btnKey8_Click },
                { Keys.D9, this.btnKey9_Click },
                { Keys.D0, this.btnKey0_Click },
                { Keys.Add, this.btnKeyAdd_Click },
                { Keys.Divide, this.btnKeyDivide_Click },
                { Keys.Subtract, this.btnKeySubstract_Click },
                { Keys.Multiply, this.btnKeyMultiply_Click },
                { Keys.D8 | Keys.Shift, this.btnKeyMultiply_Click },
                { Keys.Execute, this.btnKeyEqual_Click },
                { Keys.Return, this.btnKeyEqual_Click },
                { Keys.OemMinus, this.btnKeySubstract_Click },
                { Keys.Oemplus | Keys.Shift, this.btnKeyAdd_Click },
                { Keys.Oemplus, this.btnKeyEqual_Click },
                { Keys.OemQuestion, this.btnKeyDivide_Click },
                { Keys.Escape, this.CloseKeyBinding},

                { Keys.NumPad1, this.btnKey1_Click },
                { Keys.NumPad2, this.btnKey2_Click },
                { Keys.NumPad3, this.btnKey3_Click },
                { Keys.NumPad4, this.btnKey4_Click },
                { Keys.NumPad5, this.btnKey5_Click },
                { Keys.NumPad6, this.btnKey6_Click },
                { Keys.NumPad7, this.btnKey7_Click },
                { Keys.NumPad8, this.btnKey8_Click },
                { Keys.NumPad9, this.btnKey9_Click },
                { Keys.NumPad0, this.btnKey0_Click },
            };

        }        
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyBindings.ContainsKey(keyData))
            {
                keyBindings[keyData](null, null);
                return true;
            }
            
            if(keyData != (Keys.Shift | Keys.ShiftKey)) MessageBox.Show("Klawisz "+ keyData.ToString());
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void onNumberKey(int nr)
        {
            Result = clearResult ? nr : Result * 10 + (Math.Sign(Result) != 0 ? (Math.Sign(Result) * nr) : nr);
            clearResult = false;
        }
        private decimal executeOp(op o, decimal firstOp, decimal secondOp)
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
            if (clearHistory) History = "";

            History += " " + Result.ToString() + " " + opChars[o];

            clearHistory = o == op.RESULT ? true : false;

            if (lastOperation != op.NONE)
            {
                try
                {
                    Result = lastOperation == op.RESULT ? Result : executeOp(lastOperation, lastResult, Result);
                }
                catch (DivideByZeroException)
                {
                    Result = 0;
                    o = op.NONE;
                    History += " Błąd!";
                    clearHistory = true;
                }
            }
            lastResult = Result;
            clearResult = true;
            lastOperation = o;
        }
        private void doChangeSign()
        {
            Result = -Result;
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

        private void CloseKeyBinding(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
