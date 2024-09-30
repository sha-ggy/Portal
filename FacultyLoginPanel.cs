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
    public partial class FacultyLoginPanel : Form
    {

        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=F:\project\Portal\Portal\db\StudentData.mdf;Integrated Security=True;Connect Timeout=30";

        public FacultyLoginPanel()
        {
            InitializeComponent();
        }

        private void studentIdTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void passwordTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            FChangePassword ee= new FChangePassword();
            ee.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Check if inputs are valid
            if (string.IsNullOrWhiteSpace(facultyIdTextBox.Text) || string.IsNullOrWhiteSpace(passwordTextBox.Text))
            {
                MessageBox.Show("Please enter both Faculty ID and Password.");
                return;
            }

            try
            {
                // Parse FacultyId
                int facultyId;
                if (!int.TryParse(facultyIdTextBox.Text, out facultyId))
                {
                    MessageBox.Show("Please enter a valid Faculty ID.");
                    return;
                }



                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();


                    SqlCommand checkFacultyCmd = new SqlCommand("SELECT COUNT(*) FROM Faculty WHERE FacultyID = @FacultyID", con);
                    checkFacultyCmd.Parameters.AddWithValue("@FacultyID", int.Parse(facultyIdTextBox.Text));

                    int facultyExists = (int)checkFacultyCmd.ExecuteScalar();

                    // If FacultyID does not exist, show a message and stop the process
                    if (facultyExists == 0)
                    {
                        MessageBox.Show("Faculty ID does not exist. Please enter a valid Faculty ID.");
                        return;
                    }

                    // Query to check if the FacultyId and Password match
                    SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Faculty WHERE FacultyId = @FacultyId AND Password = @Password", con);
                    cmd.Parameters.AddWithValue("@FacultyId", facultyId);
                    cmd.Parameters.AddWithValue("@Password", passwordTextBox.Text); // Assuming password is stored as plain text

                    int result = (int)cmd.ExecuteScalar();

                    if (result > 0)
                    {
                        // Successful login
                        MessageBox.Show("Login successful! Welcome to the faculty portal.");

                        // Navigate to another form or dashboard (if needed)
                        Faculty gg = new Faculty();
                        gg.Show();
                        this.Hide();
                    }
                    else
                    {
                        // Login failed
                        MessageBox.Show("Invalid Faculty ID or Password. Please try again.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while trying to log in: " + ex.Message);
            }
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Are you sure you want to logout?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (check == DialogResult.Yes)
            {
                Application.Exit();


            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Hello gg = new Hello();
            gg.Show();
            this.Hide();
        }
    }
}
