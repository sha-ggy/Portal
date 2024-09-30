using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Portal
{
    public partial class AdminPanel : Form
    {

        readonly SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=F:\project\Portal\Portal\db\Admin.mdf;Integrated Security=True;Connect Timeout=30");

        public AdminPanel()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Check if AdminID and Password fields are empty
            if (string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please fill all blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // database connection
                connect.Open();

                // SQL  query to sel ect Admin 
                string selectData = "SELECT * FROM Admin WHERE AdminID = @AdminID AND Password = @Password";

                using (SqlCommand cmd = new SqlCommand(selectData, connect))
                {
                    // Add parameters to avoid SQL Injection
                    cmd.Parameters.Add("@AdminID", SqlDbType.VarChar).Value = textBox2.Text.Trim();
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = textBox1.Text.Trim();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    // Check if any rows are returned (valid login)
                    if (table.Rows.Count == 1)
                    {
                        // Get the stored password from the database (if needed for further validation)
                        string storedPassword = table.Rows[0]["Password"].ToString();

                        // verification if the entered password matches the stored password (case-sensitive)
                        if (string.Equals(textBox1.Text.Trim(), storedPassword, StringComparison.Ordinal))
                        {
                            MessageBox.Show("Login Successful", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Navigate to the next form
                            AddTeachersForm mForm = new AddTeachersForm();
                            mForm.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Incorrect AdminID or Password", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        // Handle case where AdminID does not exist or password is incorrect
                        MessageBox.Show("Incorrect AdminID or Password", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine("Stack Trace: " + ex.StackTrace);  // To see where the error is happening
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Always close the connection
                if (connect.State == ConnectionState.Open)
                {
                    connect.Close();
                }
            }
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.PasswordChar = ShowPass.Checked ? '\0' : '*';
        }

        private void AdminPanel_Load(object sender, EventArgs e)
        {

        }

        

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Hello f = new Hello();
            f.Show();
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Are you sure you want to exit?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (check == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
