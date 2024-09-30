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
    public partial class Faculty : Form
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=F:\project\Portal\Portal\db\StudentData.mdf;Integrated Security=True;Connect Timeout=30";
        public Faculty()
        {
            InitializeComponent();
            LoadEnrollmentsData();
            customDesign();
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Columns["StudentPhoto"].Width = 100;

        }

        private void customDesign()
        {
            panel5.Visible = false;


        }

        private void hideSubmenu()
        {
            if (panel5.Visible == true)
            {
                panel5.Visible = false;
            }

        }
        private void showSubmenu(Panel submenu)
        {
            if (submenu.Visible == false)
            {
                hideSubmenu();
                submenu.Visible = true;

            }
            else
                submenu.Visible = false;
        }

        private void LoadEnrollmentsData()
        {
            // Define the connection string (ensure it matches your database configuration)
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=F:\project\Portal\Portal\db\StudentData.mdf;Integrated Security=True;Connect Timeout=30";

            // SQL query to retrieve student enrollment information
            string query = @"
            SELECT 
            s.Photo AS StudentPhoto,  
            e.StudentId,              
            s.StudentName,            
            e.CourseId,               
            c.CourseTitle AS CourseName, 
            s.Status,
            e.Marks,
            e.Grade
        FROM 
            Enrollments e
        JOIN 
            Student s ON e.StudentId = s.StudentId
        JOIN 
            Courses c ON e.CourseId = c.CourseId;";

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    // Create a SqlDataAdapter to execute the query
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();

                    // Fill the DataTable with data from the database
                    da.Fill(dt);

                    // Set the DataSource of the DataGridView to display the data
                    dataGridView1.DataSource = dt;

                    // Configure the DataGridView for displaying images
                    if (dataGridView1.Columns["StudentPhoto"] is DataGridViewImageColumn imageColumn)
                    {
                        imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;  
                        imageColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None; 
                        imageColumn.Width = 100;  
                    }

                    
                    dataGridView1.Columns["StudentId"].Width = 80;
                    dataGridView1.Columns["StudentName"].Width = 150;
                    dataGridView1.Columns["CourseId"].Width = 80;
                    dataGridView1.Columns["CourseName"].Width = 100;
                    dataGridView1.Columns["Status"].Width = 60;
                    dataGridView1.Columns["Marks"].Width = 80;  
                    dataGridView1.Columns["Grade"].Width = 80;  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void enrollBtn_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(studentIdTF.Text) || string.IsNullOrWhiteSpace(courseIdTF.Text) || string.IsNullOrWhiteSpace(marksTF.Text))
            {
                MessageBox.Show("Please enter all required fields: Student ID, Course ID, and Marks.");
                return;
            }

            try
            {
                int studentId = int.Parse(studentIdTF.Text);
                int courseId = int.Parse(courseIdTF.Text);
                int marks = int.Parse(marksTF.Text);


                if (marks < 0 || marks > 100)
                {
                    MessageBox.Show("Marks must be between 0 and 100.");
                    return;
                }

                // Verify if StudentId exists
                if (!DoesStudentExist(studentId))
                {
                    MessageBox.Show("Student ID does not exist.");
                    return;
                }

                // Verify if StudentId exists
                if (!DoesStudentExist(studentId))
                {
                    MessageBox.Show("Student ID does not exist.");
                    return;
                }

                // Verify if CourseId exists
                if (!DoesCourseExist(courseId))
                {
                    MessageBox.Show("Course ID does not exist.");
                    return;
                }

                // Calculate the grade based on marks
                string grade = CalculateGrade(marks);

                // Update Marks and Grade for the student in the Enrollments table
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(@"
                UPDATE Enrollments
                SET Marks = @Marks, 
                    Grade = @Grade
                WHERE StudentId = @StudentId AND CourseId = @CourseId", con);


                   
                    if (dataGridView1.Columns["StudentPhoto"] is DataGridViewImageColumn imageColumn)
                    {
                        imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;  
                        imageColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None; 
                        imageColumn.Width = 100;  
                    }

                    cmd.Parameters.AddWithValue("@Marks", marks);
                    cmd.Parameters.AddWithValue("@Grade", grade);
                    cmd.Parameters.AddWithValue("@StudentId", studentId);
                    cmd.Parameters.AddWithValue("@CourseId", courseId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Marks and Grade Updated");

                    }
                    else
                    {
                        MessageBox.Show("No matching record found for the given Student ID and Course ID.");
                    }
                    LoadEnrollmentsData();
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter valid numeric values for Student ID, Course ID, and Marks.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        
        private bool DoesStudentExist(int studentId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Student WHERE StudentId = @StudentId", con);
                cmd.Parameters.AddWithValue("@StudentId", studentId);

                int count = (int)cmd.ExecuteScalar();
                return count > 0; // Returns true if the student exists
            }
        }

        // COURSE CHECK
        private bool DoesCourseExist(int courseId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Courses WHERE CourseId = @CourseId", con);
                cmd.Parameters.AddWithValue("@CourseId", courseId);

                int count = (int)cmd.ExecuteScalar();
                return count > 0; // Returns true if the course exists
            }
        }

        // GRADE CALCULATION
        private string CalculateGrade(int marks)
        {
            if (marks >= 90)
                return "A";
            else if (marks >= 80)
                return "B";
            else if (marks >= 70)
                return "C";
            else if (marks >= 60)
                return "D";
            else
                return "F";
        }
        

        private void button6_Click(object sender, EventArgs e)
        {
            showSubmenu(panel5);
        }
         
            
        

        private void button3_Click_1(object sender, EventArgs e)
        {
            LoadEnrollmentsData();
        }

        private void AddGrade_Load(object sender, EventArgs e)
        {

        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            CourseRoutineFaculty ww =new CourseRoutineFaculty();
            ww.Show();
            hideSubmenu();
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            hideSubmenu();

        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            showSubmenu(panel5);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Are you sure you want to logout?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (check == DialogResult.Yes)
            {
                Form1 lForm = new Form1();
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
