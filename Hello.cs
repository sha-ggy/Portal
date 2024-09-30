using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Portal
{
    public partial class Hello : Form
    {
        public Hello()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void User_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 ff = new Form1();
            ff.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminPanel ff = new AdminPanel();
            ff.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            FacultyLoginPanel ff = new FacultyLoginPanel();
            ff.Show();
        }

        private void Hello_Load(object sender, EventArgs e)
        {

        }

        private void Hello_Load_1(object sender, EventArgs e)
        {

        }
    }
}
