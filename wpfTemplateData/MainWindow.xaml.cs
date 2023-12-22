using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace wpfTemplateData
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<MyTask> listTasks;
        MyTask newTask = new();
        public MainWindow()
        {
            InitializeComponent();

            List<string> listPhones = new() { "iPhone 6S", "Nexus 7", "Galaxy A32" };
            listBoxPhones.ItemsSource = listPhones;

            listTasks = new()
            {
                new MyTask() {TaskName="Поспать сладко", Description="На кроватке.", Priority=1},
                new MyTask() {TaskName="Пойти делать отжумання", Description="В качалку.", Priority=2},
                new MyTask() {TaskName="По ржать", Description="с дураков", Priority=3},
                new MyTask() {TaskName="делать курсач", Description="по уебунту", Priority=4},
            };
            listBoxTasks.ItemsSource = listTasks;

            stackPanel_add.DataContext = newTask;

            buAdd.Click += BuAdd_Click;
        }

        private void BuAdd_Click(object sender, RoutedEventArgs e)
        {
            //listTasks.Add(new MyTask() { TaskName = "1111" });
            listTasks.Add(newTask);
        }
    }

    class MyTask: IDataErrorInfo
    {
        public string? TaskName { get; set; }
        public string? Description { get; set; }
        public int? Priority { get; set; }

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch(columnName)
                {
                    case "TaskName":
                        break;
                    case "Priority":
                        if ((this.Priority < 0) || (this.Priority > 10))
                        {
                            error = "Приоритет должен быть больше 0 и меньше 10";
                        }
                        break;
                }
                return string.Empty;
            }
        }

        public string Error => throw new NotImplementedException();
    }
}
