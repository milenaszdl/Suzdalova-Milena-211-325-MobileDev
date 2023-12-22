
namespace mauiGeneratorRGB
{
    public partial class MainPage : ContentPage
    {
        Random rnd = new ();
        public MainPage()
        {
            InitializeComponent();

            slider_R.ValueChanged += Slider_All_ValueChanged;
            slider_G.ValueChanged += Slider_All_ValueChanged;
            slider_B.ValueChanged += Slider_All_ValueChanged;

            buRandonColor.Clicked += BuRandomColor_Clicked;
        }

        private void BuRandomColor_Clicked(object sender, EventArgs e)
        {
            slider_R.Value = rnd.Next(256);
            slider_G.Value = rnd.Next(256);
            slider_B.Value = rnd.Next(256);
        }

        private void Slider_All_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            boxview_color.Color = Color.FromRgb((byte)slider_R.Value, (byte)slider_G.Value, (byte)slider_B.Value);
        }
    }
}