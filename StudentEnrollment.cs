﻿using System;
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
    public partial class StudentEnrollment : Form
    {
        //private byte[] studentPhoto = null; // Initialize it as null
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=F:\project\Portal\Portal\db\StudentData.mdf;Integrated Security=True;Connect Timeout=30";

        public StudentEnrollment()
        {
            InitializeComponent();
            dataGridView1.AllowUserToAddRows = false;
            customDesign();

            this.Load += new EventHandler(GridForm_Load);


        }
        private void GridForm_Load(object sender, EventArgs e)
        {
            // Set default row height
            dataGridView1.RowTemplate.Height = 100;

            updateBtn_Click(this, EventArgs.Empty);  // Load data into the DataGridView
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

        private void ClearFields()
        {
            courseIDEnroll.Clear();
            studentID.Clear();

        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void enrollBtn_Click(object sender, EventArgs e)
        {

            if (!int.TryParse(studentID.Text, out int studentId) || !int.TryParse(courseIDEnroll.Text, out int courseId))
            {
                MessageBox.Show("Please enter valid Student ID and Course ID.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Check if the student exists
                    string checkStudentQuery = "SELECT COUNT(1) FROM Student WHERE StudentID = @StudentID";
                    SqlCommand checkStudentCmd = new SqlCommand(checkStudentQuery, con);
                    checkStudentCmd.Parameters.AddWithValue("@StudentID", studentId);
                    if ((int)checkStudentCmd.ExecuteScalar() == 0)
                    {
                        MessageBox.Show("Invalid Student ID.");
                        return;
                    }

                    // Check if the course exists
                    string checkCourseQuery = "SELECT COUNT(1) FROM Courses WHERE CourseID = @CourseID";
                    SqlCommand checkCourseCmd = new SqlCommand(checkCourseQuery, con);
                    checkCourseCmd.Parameters.AddWithValue("@CourseID", courseId);
                    if ((int)checkCourseCmd.ExecuteScalar() == 0)
                    {
                        MessageBox.Show("Course does not exist.");
                        return;
                    }

                    // Check if student is already enrolled in the course
                    string checkEnrollmentQuery = "SELECT COUNT(1) FROM Enrollments WHERE StudentID = @StudentID AND CourseID = @CourseID";
                    SqlCommand checkEnrollmentCmd = new SqlCommand(checkEnrollmentQuery, con);
                    checkEnrollmentCmd.Parameters.AddWithValue("@StudentID", studentId);
                    checkEnrollmentCmd.Parameters.AddWithValue("@CourseID", courseId);
                    if ((int)checkEnrollmentCmd.ExecuteScalar() > 0)
                    {
                        MessageBox.Show("Student is already enrolled in this course.");
                        return;
                    }

                    // Insert the enrollment
                    SqlCommand cmd = new SqlCommand("INSERT INTO Enrollments (StudentID, CourseID) VALUES (@StudentID, @CourseID)", con);
                    cmd.Parameters.AddWithValue("@StudentID", studentId);
                    cmd.Parameters.AddWithValue("@CourseID", courseId);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Student enrolled successfully.");
                    ClearFields();
                    LoadDataIntoGridView(); // Refresh the GridView
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void LoadDataIntoGridView()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Correct table name: Courses (assuming it's the correct name in your database)
                    string query = @"
            SELECT 
                s.Photo AS StudentPhoto,
                e.StudentID, 
                s.StudentName,
                e.CourseID, 
                c.CourseTitle,
                c.FacultyID,      
                c.RoomNo,         
                c.Status 
            FROM 
                Enrollments e
            JOIN 
                Student s ON e.StudentID = s.StudentId
            JOIN 
                Courses c ON e.CourseID = c.CourseId";  // Updated table name to Courses

                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;


                    if (dataGridView1.Columns["StudentPhoto"] is DataGridViewImageColumn imageColumn)
                    {
                        imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;  // Set the image layout to zoom
                        imageColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None; // Disable auto-size mode
                        imageColumn.Width = 100;  // Set the desired width for the image column
                    }
                    // Set custom column headers
                    dataGridView1.Columns["StudentID"].HeaderText = "Student ID";
                    dataGridView1.Columns["StudentName"].HeaderText = "Student Name";
                    dataGridView1.Columns["CourseID"].HeaderText = "Course ID";
                    dataGridView1.Columns["CourseTitle"].HeaderText = "Course Name";
                    dataGridView1.Columns["FacultyID"].HeaderText = "Faculty ID";  // New column
                    dataGridView1.Columns["RoomNo"].HeaderText = "Room No";        // New column
                    dataGridView1.Columns["Status"].HeaderText = "Status";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        
        }

            private void dropBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(studentID.Text) || string.IsNullOrWhiteSpace(courseIDEnroll.Text))
            {
                MessageBox.Show("Student ID and Course ID are required.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();



                    string checkStudentQuery = "SELECT COUNT(1) FROM Student WHERE StudentID = @StudentID";
                    SqlCommand checkStudentCmd = new SqlCommand(checkStudentQuery, con);
                    checkStudentCmd.Parameters.AddWithValue("@StudentID", int.Parse(studentID.Text));
                    int studentExists = (int)checkStudentCmd.ExecuteScalar();

                    if (studentExists == 0)
                    {
                        MessageBox.Show("Invalid Student ID.");
                        return;
                    }


                    SqlCommand cmd = new SqlCommand("DELETE FROM Enrollments WHERE StudentID = @StudentID AND CourseID = @CourseID", con);

                    cmd.Parameters.AddWithValue("@StudentID", int.Parse(studentID.Text));
                    cmd.Parameters.AddWithValue("@CourseID", int.Parse(courseIDEnroll.Text));
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Student dropped from the course successfully.");
                    ClearFields();
                    LoadDataIntoGridView();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Are you sure you want to exit?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (check == DialogResult.Yes)
            {
                Application.Exit();
            }
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

        private void button6_Click(object sender, EventArgs e)
        {
            showSubmenu(panel5);
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            LoadDataIntoGridView();
        }

        private void studentID_TextChanged(object sender, EventArgs e)
        {

        }

        private void courseIDEnroll_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        

       

        

        

        private void button13_Click(object sender, EventArgs e)
        {
            hideSubmenu();

        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            AddTeachersForm f = new AddTeachersForm();
            f.Show();
            hideSubmenu();
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            StudentEnrollment f = new StudentEnrollment();
            f.Show();
            hideSubmenu();
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            AddStudentsForm f = new AddStudentsForm();
            f.Show();
            hideSubmenu();
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            AddCoursesForm f = new AddCoursesForm();
            f.Show();
            hideSubmenu();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddGrade f = new AddGrade();
            f.Show();
            hideSubmenu();
        }
    }
}
