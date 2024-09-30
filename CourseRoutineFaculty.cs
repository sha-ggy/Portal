using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Portal
{
    public partial class CourseRoutineFaculty : Form
    {

        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=F:\project\Portal\Portal\db\StudentData.mdf;Integrated Security=True;Connect Timeout=30";

        public CourseRoutineFaculty()
        {
            InitializeComponent();
        }


        private void studentIdTF_TextChanged(object sender, EventArgs e)
        {

        }



        private void button3_Click(object sender, EventArgs e)
        {
            
                if (string.IsNullOrWhiteSpace(facultyIdTextBox.Text))
                {
                    MessageBox.Show("Please enter the Faculty ID.");
                    return;
                }

                try
                {
                    int facultyId;
                    if (!int.TryParse(facultyIdTextBox.Text, out facultyId))
                    {
                        MessageBox.Show("Please enter a valid Faculty ID.");
                        return;
                    }

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();

                        // Query to fetch courses linked to the faculty member
                        SqlCommand cmd = new SqlCommand(
                            "SELECT CourseID, CourseTitle, Status FROM Courses WHERE FacultyID = @FacultyID", con);
                        cmd.Parameters.AddWithValue("@FacultyID", facultyId);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        // Display courses in the DataGridView
                        dataGridView1.DataSource = dt; // 'coursesDataGridView' must exist on your form
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching courses: " + ex.Message);
                }
            }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Are you sure you want to logout?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (check == DialogResult.Yes)
            {
                FacultyLoginPanel lForm = new FacultyLoginPanel();
                lForm.Show();
                this.Hide();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Are you sure you want to exit?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (check == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
