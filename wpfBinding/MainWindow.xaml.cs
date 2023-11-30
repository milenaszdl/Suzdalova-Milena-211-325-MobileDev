using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace wpfBinding
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Binding binding = new();
            //binding.ElementName = "edCaption";
            binding.ElementName = nameof(edCaption);
            //binding.Path = new("Text");
            binding.Path = new(nameof(edCaption.Text));
            txtCaption.SetBinding(TextBlock.TextProperty, binding);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var x = (HerroEx)this.Resources["myHerroAbu"];
            x.Description = "duihefuiehfeu!!!";
        }
    }

    class Herro
    {
        public string? Name { get; set; }
        public string? Clan { get; set; }
        public string? Description { get; set; }
        public int HP { get; set; } = 100;
    }

    class HerroEx : INotifyPropertyChanged
    {
        private string? name;
        private string? clan;
        private string? description;
        private int hP = 100;

        public string? Name
        {
            get => name; set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string? Clan
        {
            get => clan; set
            {
                clan = value;
                OnPropertyChanged(nameof(Clan));
            }
        }
        public string? Description
        {
            get => description; set
            {
                description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
        public int HP { get => hP; set 
            { 
                hP = value;
                OnPropertyChanged(nameof(HP));
            } 
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
