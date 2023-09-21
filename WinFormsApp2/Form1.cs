namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        private Random rnd = new();

        public Form1()
        {
            InitializeComponent();

            this.MouseDown += label1_Click;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //Label x = new Label();
                //var x = new Label(); //одно и то же, что и предыдущее
                Label x = new(); //самый ходовой аналог

                x.Location = e.Location;
                x.Text = $"({e.X},{e.Y})";
                x.BackColor = Color.Red;
                x.AutoSize = true;
                this.Controls.Add(x);
            }

            if (e.Button == MouseButtons.Right)
            {
                for (int i = 0; i < 10; i++)
                {
                    Label x = new(); //самый ходовой аналог

                    x.Location = new Point(
                        rnd.Next(this.ClientSize.Width),
                        rnd.Next(this.ClientSize.Height));
                    x.Text = $"({x.Location.X},{x.Location.Y})";
                    x.BackColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                    x.AutoSize = true;
                    this.Controls.Add(x);
                }
            }

            if (e.Button == MouseButtons.Middle)
            {
                this.Controls.Clear();
            }

            this.Text = $"{Application.ProductName} : Count = {this.Controls.Count}";
        }
    }
}