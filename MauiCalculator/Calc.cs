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
            CurText = "";
            Changed?.Invoke(this, EventArgs.Empty);
        }

        internal void PressNum(int num)
        {
            CurText += num.ToString();
            Changed?.Invoke(this, EventArgs.Empty);
        }

        internal void PressOperator(string oper)
        {
            if (firstNumber != 0)
            {
                //Calculate();
            }
            double.TryParse(CurText, out firstNumber);
            CurOper = oper;
        }
    }
}