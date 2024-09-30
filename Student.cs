using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Portal
{
    public partial class Student : Form
    {
        //private string connectionString = "Data Source=.;Initial Catalog=Schedule;Integrated Security=True";


        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\csharp\db\Schedule.mdf;Integrated Security=True;Connect Timeout=30";
        string connectionString2 = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\csharp\db\StudentData.mdf;Integrated Security=True;Connect Timeout=30";
        public Student()
        {

            InitializeComponent();
            customDesign();
            this.Load += new EventHandler(Student_Load);
            LoadData();
            LoadData1();
            LoadData3();
            LoadData2();
            LoadData4();
            LoadData5();
            LoadData6();
            LoadData7();

            dataGridView1.Visible= true;
            dataGridView1.AutoGenerateColumns = true;
            dataGridView2.AutoGenerateColumns = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView4.AllowUserToAddRows = false;
            dataGridView3.AllowUserToAddRows = false;
            dataGridView3.AutoGenerateColumns = true;
            dataGridView6.AutoGenerateColumns = true;
            dataGridView4.AutoGenerateColumns = true;
            dataGridView6.AllowUserToAddRows = false;
            dataGridView5.AllowUserToAddRows = false;
            dataGridView5.AutoGenerateColumns = true;
            dataGridView7.AutoGenerateColumns = true;
            dataGridView7.AllowUserToAddRows = false;
            dataGridView8.AllowUserToAddRows = false;
            dataGridView8.AutoGenerateColumns = true;



        }
        private void LoadData7()
        {
            using (SqlConnection conn = new SqlConnection(connectionString2))
            {
                try
                {

                    conn.Open();


                    string query = "SELECT * FROM Offcourse";


                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {

                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        DataTable dt = new DataTable();


                        da.Fill(dt);


                        dataGridView8.DataSource = dt;

                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }



        }
        private void LoadData6()
        {
            using (SqlConnection conn = new SqlConnection(connectionString2))
            {
                try
                {

                    conn.Open();


                    string query = "SELECT * FROM Result_C";


                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {

                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        DataTable dt = new DataTable();


                        da.Fill(dt);


                        dataGridView7.DataSource = dt;

                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }



        }
        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
               
                    conn.Open();

                  
                    string query = "SELECT * FROM schedule";

                  
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                       
                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        DataTable dt = new DataTable();

                      
                        da.Fill(dt);

                      
                        dataGridView1.DataSource = dt;
                       
                    }
                }
                catch (Exception ex)
                {
                    
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }

            
            
        }
        private void LoadData5()
        {
            using (SqlConnection conn = new SqlConnection(connectionString2))
            {
                try
                {

                    conn.Open();


                    string query = "SELECT * FROM Result";


                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {

                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        DataTable dt = new DataTable();


                        da.Fill(dt);


                        dataGridView5.DataSource = dt;

                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }



        }
        private void LoadData2()
        {
            using (SqlConnection conn = new SqlConnection(connectionString2))
            {
                try
                {
              
                    conn.Open();

                    string query = "SELECT * FROM Courses";

            
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        
                        SqlDataAdapter da = new SqlDataAdapter(cmd);

             
                        DataTable dt = new DataTable();

                        da.Fill(dt);

                  
                        dataGridView4.DataSource = dt;

                    }
                }
                catch (Exception ex)
                {
      
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }



        }
        private void LoadData3()
        {
            using (SqlConnection conn = new SqlConnection(connectionString2))
            {
                try
                {
          
                    conn.Open();

                
                    string query = "SELECT * FROM addrop";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                   
                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                      
                        DataTable dt = new DataTable();

                        
                        da.Fill(dt);

                     
                        dataGridView3.DataSource = dt;

                    }
                }
                catch (Exception ex)
                {
                   
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }



        }
        private void LoadData1()
        {
            using (SqlConnection conn = new SqlConnection(connectionString2))
            {
                try
                {
                  
                    conn.Open();

                    string query = "SELECT * FROM Grades";

                   
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                    
                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        DataTable dt = new DataTable();

                        
                        da.Fill(dt);

                        dataGridView2.DataSource = dt;

                    }
                }
                catch (Exception ex)
                {
               
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }



        }
        private void LoadData4()
        {
            using (SqlConnection conn = new SqlConnection(connectionString2))
            {
                try
                {

                    conn.Open();

                    string query = "SELECT * FROM addrop";


                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {

                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        DataTable dt = new DataTable();


                        da.Fill(dt);

                        dataGridView6.DataSource = dt;

                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }



        }



        private void Student_Load(object sender, EventArgs e)
        {
            
        }
        private void customDesign()
        {

            panel5.Visible = false;
            panel8.Visible = false;
           panel9.Visible = false;
            panel10.Visible = false;
            panel12.Visible = false;
            panel14.Visible = false;
            panel15.Visible = false;


        }

        private void hideSubmenu()


        {
            

            if (panel5.Visible == true)
            {
                panel5.Visible = false;
            }
            

            if (panel8.Visible == true)
            {
                panel8.Visible = false;
            }
            if (panel9.Visible == true)
            {
                panel9.Visible = false;
            }
            if (panel10.Visible == true)
            {
                panel10.Visible = false;
            }
            if (panel12.Visible == true)
            {
                panel12.Visible = false;
            }
            if (panel14.Visible == true)
            {
                panel14.Visible = false;
            }

            if (panel15.Visible == true)
            {
                panel15.Visible = false;
            }

        }

        private void showSubmenu(Panel submenu)
        {
            if (submenu.Visible == false)
            {
                hideSubmenu();

                submenu.Visible = true;

            }
            
        }
       
       
      

        private void Result_Click(object sender, EventArgs e)
        {

            hideSubmenu();
            panel5.Visible = true;

        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Academics_Click(object sender, EventArgs e)
        {
           
        }

        private void logopanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            hideSubmenu();
            panel12.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            hideSubmenu();
            panel14.Visible = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Registration obj= new Registration();
            obj.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

            hideSubmenu();
            showSubmenu(panel4);

        }

        private void Course_Click(object sender, EventArgs e)
        {
            hideSubmenu();
            showSubmenu(panel9);

        }

        private void button7_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Are you sure you want to logout?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (check == DialogResult.Yes)
            {
                Form1 lForm = new Form1();
                lForm.Show();
                this.Hide();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            hideSubmenu();
            showSubmenu(panel10);
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Add_Click(object sender, EventArgs e)
        {
            showSubmenu(panel8);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textbox5.Text) || string.IsNullOrWhiteSpace(textBox6.Text))
            {
                MessageBox.Show("Please enter some data");
                return;
            }
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString2))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO addrop (CourseID, CourseTitle) VALUES (@CourseID, @CourseTitle)", conn);

                    // Add parameters
                    cmd.Parameters.AddWithValue("@CourseID", int.Parse(textbox5.Text));
                    cmd.Parameters.AddWithValue("@CourseTitle", textBox6.Text);
                    

                    cmd.ExecuteNonQuery();

                    
                    ClearFields(); // Clear fields after insert
                    LoadData3();


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void ClearFields()
        {
            // Clear the input fields
            textbox5.Clear();
            textBox6.Clear();
            textBox10.Clear();
            
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            hideSubmenu();
            showSubmenu(panel4);

        }

        private void button10_Click(object sender, EventArgs e)
        {
            hideSubmenu();
            showSubmenu(panel4);
        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            hideSubmenu();
            showSubmenu(panel4);
        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            hideSubmenu();
            showSubmenu(panel4);
        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            hideSubmenu();
            showSubmenu(panel4);
        }

        private void panel10_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox10.Text))
            {
                MessageBox.Show("Please enter a valid Course");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString2))
                {
                    con.Open();

                    SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM addrop WHERE CourseID = @CourseID", con);
                    checkCmd.Parameters.AddWithValue("@CourseID", int.Parse(textBox10.Text));

                    int recordCount = (int)checkCmd.ExecuteScalar();

                    if (recordCount == 0)
                    {
                        MessageBox.Show("Course is not added");
                        return;
                    }

                    // Delete the record from the database
                    SqlCommand deleteCmd = new SqlCommand("DELETE FROM addrop WHERE CourseID = @CourseID", con);
                    deleteCmd.Parameters.AddWithValue("@CourseID", int.Parse(textBox10.Text));

                    deleteCmd.ExecuteNonQuery();
                    MessageBox.Show("Dropped successfully.");

                    LoadData4();
                    ClearFields();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            LoadData4();

        }

        private void button14_Click(object sender, EventArgs e)
        {
            LoadData3();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            hideSubmenu();
            showSubmenu(panel4);
        }

        private void panel12_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {
            hideSubmenu();
            showSubmenu(panel4);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            hideSubmenu();
            showSubmenu(panel4);
        }

        private void dataGridView8_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            showSubmenu(panel15);


        }
    }
    }

