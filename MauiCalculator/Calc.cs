namespace MauiCalculator
{
    internal class Calc
    {
        private double firstNumber;

        public string CurText { get; private set; }
        public string CurOper { get; private set; }

        public event EventHandler Changed;

        internal void Clear()
        {
            CurText = "0";
            Changed?.Invoke(this, EventArgs.Empty);
        }


        internal static double PressOperator(double val1, double val2, string oper)
        {
            double result = 0;

            switch (oper)
            {
                case "/":
                    result = val1 / val2;
                    break;
                case "-":
                    result = val1 - val2;
                    break;
                case "*":
                    result = val1 * val2;
                    break;
                case "+":
                    result = val1 + val2;
                    break;
                case "1/x":
                    result = 1/val1;
                    break;
                case "x^2":
                    result = val1 * val2;
                    break;
                case "sqrt":
                    result = Math.Sqrt(val1);
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}