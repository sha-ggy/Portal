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
    public partial class FChangePassword : Form
    {

        //private int facultyId;  // Store FacultyId for updating password
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=F:\project\Portal\Portal\db\StudentData.mdf;Integrated Security=True;Connect Timeout=30";
        public FChangePassword()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(facultyIdTextBox.Text) || string.IsNullOrWhiteSpace(passwordTextBox.Text))
            {
                MessageBox.Show("Please enter both Faculty ID and Old Password.");
                return;
            }

            if (string.IsNullOrWhiteSpace(newPasswordTextBox.Text) || string.IsNullOrWhiteSpace(confirmPasswordTextBox.Text))
            {
                MessageBox.Show("Please enter both New Password and Confirm Password.");
                return;
            }

            // Check if new password matches the confirm password
            if (newPasswordTextBox.Text != confirmPasswordTextBox.Text)
            {
                MessageBox.Show("New Password and Confirm Password do not match.");
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

                    // Step 1: Check if the FacultyId and Old Password match
                    SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Faculty WHERE FacultyId = @FacultyId AND Password = @Password", con);
                    checkCmd.Parameters.AddWithValue("@FacultyId", facultyId);
                    checkCmd.Parameters.AddWithValue("@Password", passwordTextBox.Text);  // Old Password

                    int result = (int)checkCmd.ExecuteScalar();

                    if (result > 0)
                    {
                        // Step 2: Update password if the old password is correct
                        SqlCommand updateCmd = new SqlCommand("UPDATE Faculty SET Password = @NewPassword WHERE FacultyId = @FacultyId", con);
                        updateCmd.Parameters.AddWithValue("@NewPassword", newPasswordTextBox.Text);  // New Password
                        updateCmd.Parameters.AddWithValue("@FacultyId", facultyId);

                        updateCmd.ExecuteNonQuery();

                        MessageBox.Show("Password updated successfully!");

                        // Optionally clear the fields after successful update
                        ClearFields();

                        this.Hide();
                        FacultyLoginPanel tt =new FacultyLoginPanel();
                        tt.Show();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Faculty ID or Old Password. Please try again.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while trying to change the password: " + ex.Message);
            }
        }

        private void ClearFields()
        {
            facultyIdTextBox.Clear();
            passwordTextBox.Clear();
            newPasswordTextBox.Clear();
            confirmPasswordTextBox.Clear();
        }
        private void newPasswordTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void FChangePassword_Load(object sender, EventArgs e)
        {

        }

        private void confirmPasswordTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void facultyId_TextChanged(object sender, EventArgs e)
        {

        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Are you sure you want to logout?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (check == DialogResult.Yes)
            {
                Application.Exit();
                

            }
        }
    }
}
