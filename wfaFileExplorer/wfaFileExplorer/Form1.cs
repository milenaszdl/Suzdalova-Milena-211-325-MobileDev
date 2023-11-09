namespace wfaFileExplorer
{
    public partial class Form1 : Form
    {
        private string curDir;

        public string CurDir
        {
            get => curDir; private set
            {
                curDir = value;
                edDir.Text = curDir;
            }
        }
        public Form1()
        {
            InitializeComponent();

            //CurDir = "D:\\";
            CurDir = Directory.GetCurrentDirectory();

            //buBack.Click;
            //buForward.Click;
            buUp.Click += (s, e) => LoadDir(Directory.GetParent(CurDir).ToString());
            edDir.KeyDown += EdDir_KeyDown;
            //buDirSelect.Click;


            LoadDir(CurDir);
        }

        private void EdDir_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadDir(edDir.Text);
            }
        }

        private void LoadDir(string newDir)
        {
            DirectoryInfo directoryInfo = new(newDir);

            listView1.Items.Clear();
            foreach (var item in directoryInfo.GetDirectories())
            {
                listView1.Items.Add(item.Name, 0);
            }
            foreach (var item in directoryInfo.GetFiles())
            {
                listView1.Items.Add(item.Name, 1);
            }
            listView1.EndUpdate();

            CurDir = newDir;
        }
    }
}