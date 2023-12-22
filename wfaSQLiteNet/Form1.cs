using SQLite;

namespace wfaSQLiteNet
{
    public partial class Form1 : Form
    {
        private SQLiteConnection db;

        public Form1()
        {
            InitializeComponent();

            db = new SQLiteConnection("myDB.db");
            db.CreateTable<Log>();
            db.CreateTable<User>();
            db.CreateTable<City>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}