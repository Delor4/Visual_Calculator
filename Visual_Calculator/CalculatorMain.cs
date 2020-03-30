using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Visual_Calculator
{
    public partial class CalculatorMainForm : Form
    {
        enum OP { None, Multiply, Divide, Substract, Add, Eval };

        OP lastOperation = OP.None;

        Dictionary<OP, char> opChars = new Dictionary<OP, char> {
            { OP.Add,       '+' },
            { OP.Substract, '-' },
            { OP.Multiply,  '*' },
            { OP.Divide,    '/' },
            { OP.Eval,      '=' },
        };

        Dictionary<Keys, Action<object, EventArgs>> keyBindings;

        decimal _result;
        private decimal Result
        {
            get => _result;

            set
            {
                _result = value;
                UpdateTextBox();
            }
        }
        private byte _denominator;
        private byte Denominator
        {
            get => _denominator;

            set
            {
                _denominator = value;
                UpdateTextBox();
            }
        }

        decimal lastResult;

        bool clearResult = true;

        String _history = "";
        private String History
        {
            get => _history;

            set
            {
                _history = value;
                lblInfo.Text = _history;
            }
        }

        bool clearHistory = true;

        public CalculatorMainForm()
        {
            InitializeComponent();
            InitBindings();
            Result = 0;

        }
        private void InitBindings()
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

                { Keys.Add,                     this.btnKeyAdd_Click },
                { Keys.Divide,                  this.btnKeyDivide_Click },
                { Keys.Subtract,                this.btnKeySubstract_Click },
                { Keys.Multiply,                this.btnKeyMultiply_Click },
                { Keys.D8 | Keys.Shift,         this.btnKeyMultiply_Click },
                { Keys.Execute,                 this.btnKeyEqual_Click },
                { Keys.Return,                  this.btnKeyEqual_Click },
                { Keys.OemMinus,                this.btnKeySubstract_Click },
                { Keys.Oemplus | Keys.Shift,    this.btnKeyAdd_Click },
                { Keys.Oemplus,                 this.btnKeyEqual_Click },
                { Keys.OemQuestion,             this.btnKeyDivide_Click },

                { Keys.Escape, this.CloseKeyBinding},
                
                { Keys.F1, this.AboutBinding},

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

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void UpdateTextBox()
        {
            txtBoxResult.Text = Denomine(Result).ToString();
        }
        private void ErrorHandler(String msg)
        {
            Result = 0;
            Denominator = 0;
            lastResult = 0;
            History += " " + msg;
            clearHistory = true;
        }
        public decimal Normalize(decimal value)
        {
            return value / 1.000000000000000000000000000000000m;
        }
        private decimal Denomine(decimal nr)
        {
            return Denominator == 0 ?
                nr :
                nr * (decimal)Math.Pow(10, 1 - Denominator);
        }
        private decimal ExecuteOp(OP oper, decimal firstOp, decimal secondOp)
        {
            switch (oper)
            {
                case OP.Add:
                    return firstOp + secondOp;
                case OP.Substract:
                    return firstOp - secondOp;
                case OP.Multiply:
                    return firstOp * secondOp;
                case OP.Divide:
                    return firstOp / secondOp;
            }
            return 0;
        }
        private void DoNumberKey(int nr)
        {
            try
            {
                Result = clearResult ?
                    nr :
                    Result * 10 + (Result < 0 ?
                        -nr :
                        nr);
                if (Denominator > 0) Denominator++;
                clearResult = false;
            }
            catch (OverflowException)
            {
                ErrorHandler("Overflow!");
            }
        }
        private void DoOperation(OP oper)
        {
            if (clearHistory) History = "";

            History += " " + Denomine(Result).ToString() + " " + opChars[oper];

            clearHistory = oper == OP.Eval ? true : false;

            if (lastOperation != OP.None)
            {
                try
                {
                    Result = Normalize(lastOperation == OP.Eval ? Denomine(Result) : ExecuteOp(lastOperation, lastResult, Denomine(Result)));
                    Denominator = 0;
                }
                catch (DivideByZeroException)
                {
                    ErrorHandler("Div0!");
                    oper = OP.None;
                }
                catch (OverflowException)
                {
                    ErrorHandler("Overflow!");
                    oper = OP.None;
                }
            }
            Result = Denomine(Result);
            Denominator = 0;
            lastResult = Result;
            clearResult = true;
            lastOperation = oper;
        }
        private void DoChangeSign()
        {
            try {
                Result = -Result;
            }
            catch (OverflowException)
            {
                ErrorHandler("Overflow!");
            }
        }
        private void DoComa()
        {
            if (Denominator == 0) Denominator = 1;
            if (clearResult) Result = 0;
        }
        private void btnKey1_Click(object sender, EventArgs e)
        {
            DoNumberKey(1);
        }
        private void btnKey2_Click(object sender, EventArgs e)
        {
            DoNumberKey(2);
        }
        private void btnKey3_Click(object sender, EventArgs e)
        {
            DoNumberKey(3);
        }
        private void btnKey4_Click(object sender, EventArgs e)
        {
            DoNumberKey(4);
        }
        private void btnKey5_Click(object sender, EventArgs e)
        {
            DoNumberKey(5);
        }
        private void btnKey6_Click(object sender, EventArgs e)
        {
            DoNumberKey(6);
        }
        private void btnKey7_Click(object sender, EventArgs e)
        {
            DoNumberKey(7);
        }
        private void btnKey8_Click(object sender, EventArgs e)
        {
            DoNumberKey(8);
        }
        private void btnKey9_Click(object sender, EventArgs e)
        {
            DoNumberKey(9);
        }
        private void btnKey0_Click(object sender, EventArgs e)
        {
            DoNumberKey(0);
        }
        private void btnKeyComma_Click(object sender, EventArgs e)
        {
            DoComa();
        }
        private void btnKeySign_Click(object sender, EventArgs e)
        {
            DoChangeSign();
        }
        private void btnKeyDivide_Click(object sender, EventArgs e)
        {
            DoOperation(OP.Divide);
        }
        private void btnKeyMultiply_Click(object sender, EventArgs e)
        {
            DoOperation(OP.Multiply);
        }
        private void btnKeySubstract_Click(object sender, EventArgs e)
        {
            DoOperation(OP.Substract);
        }
        private void btnKeyAdd_Click(object sender, EventArgs e)
        {
            DoOperation(OP.Add);
        }
        private void btnKeyEqual_Click(object sender, EventArgs e)
        {
            DoOperation(OP.Eval);
        }
        private void CloseKeyBinding(object sender, EventArgs e)
        {
            Close();
        }
        private void AboutBinding(object sender, EventArgs e)
        {
            var aboutForm = new AboutForm();
            aboutForm.Icon = this.Icon;
            aboutForm.ShowDialog();
        }
    }
}
