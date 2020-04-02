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
    public partial class CalculatorMain : CalculatorBaseForm
    {
        public CalculatorMain()
            : base()
        {
            InitializeComponent();
            Result = 0;
        }

        OP lastOperation = OP.None;
        
        readonly Dictionary<OP, char> opChars = new Dictionary<OP, char> {
            { OP.Add,       '+' },
            { OP.Substract, '-' },
            { OP.Multiply,  '*' },
            { OP.Divide,    '/' },
            { OP.Eval,      '=' },
        };

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


        protected void UpdateTextBox()
        {
            txtBoxResult.Text = Denomine(Result, Denominator).ToString();
        }
        private void ErrorHandler(String msg)
        {
            Result = 0;
            Denominator = 0;
            lastResult = 0;
            History += " " + msg;
            clearHistory = true;
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
        protected override void DoNumberKey(int nr)
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
        protected override void DoOperation(OP oper)
        {
            if (clearHistory) History = "";

            History += " " + Denomine(Result, Denominator).ToString() + " " + opChars[oper];

            clearHistory = oper == OP.Eval ? true : false;

            if (lastOperation != OP.None)
            {
                try
                {
                    Result = Normalize(lastOperation == OP.Eval ?
                            Denomine(Result, Denominator) :
                            ExecuteOp(lastOperation, lastResult, Denomine(Result, Denominator)));
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
            Result = Denomine(Result, Denominator);
            Denominator = 0;
            lastResult = Result;
            clearResult = true;
            lastOperation = oper;
        }
        protected override void DoChangeSign()
        {
            try
            {
                Result = -Result;
            }
            catch (OverflowException)
            {
                ErrorHandler("Overflow!");
            }
        }
        protected override void DoComa()
        {
            if (Denominator == 0) Denominator = 1;
            if (clearResult) Result = 0;
        }
        protected override void DoBack()
        {
            Result = clearResult ?
                0 :
                Math.Truncate(Result / 10);
            if (Denominator > 0) Denominator--;
        }
    }
}
