namespace MauiCalculator
{
    public partial class MainPage : ContentPage
    {
        int currentState = 1;
        string operatorMath;
        double firstNum, secondNum;

        //Calc calc = new();
        public MainPage()
        {
            InitializeComponent();
            OnClear(this, null);

            busqr.Clicked += (s, e) =>
            {
                if (firstNum == 0)
                    return;
                firstNum=Calc.PressOperator(firstNum, firstNum, "x^2");
                this.laDisplay.Text = firstNum.ToString();
            };

            busqrt.Clicked += (s, e) =>
            {
                if (firstNum == 0)
                    return;
                firstNum = Calc.PressOperator(firstNum, firstNum, "sqrt");
                this.laDisplay.Text = firstNum.ToString();
            };

            buOneDel.Clicked += (s, e) =>
            {
                if (firstNum == 0)
                    return;
                firstNum = Calc.PressOperator(firstNum, firstNum, "1/x");
                this.laDisplay.Text = firstNum.ToString();
            };
        }

        void OnClear(object sender, EventArgs e)
        {
            firstNum = 0;
            secondNum = 0;
            currentState = 1;
            this.laDisplay.Text = "0";
        }

        void OnNumberSelection(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string btnPressed = button.Text;

            if (this.laDisplay.Text == "0" || currentState < 0)
            {
                this.laDisplay.Text = string.Empty;
                if (currentState < 0)
                    currentState *= -1;
            }

            this.laDisplay.Text += btnPressed;

            double number;
            if (double.TryParse(this.laDisplay.Text, out number))
            {
                this.laDisplay.Text = number.ToString("N0");
                if (currentState == 1)
                {
                    firstNum = number;
                }
                else
                {
                    secondNum = number;
                }
            }
        }

        void onOperatorSelection(object sender, EventArgs e)
        {
            currentState = -2;
            Button button = (Button)sender;
            string btnPressed = button.Text;
            operatorMath = btnPressed;
        }

        void onCalculate(object sender, EventArgs e)
        {
            if (currentState == 2)
            {
                var result = Calc.PressOperator(firstNum, secondNum, operatorMath);

                this.laDisplay.Text = result.ToString();
                firstNum = result;
                currentState = -1;
            }
        }
    }
}