using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Bank_Management_System
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=SAKSHI-24\SQLEXPRESS;Initial Catalog=BankDB;Integrated Security=True");
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void ResetTB_Click(object sender, EventArgs e)
        {
            Uname.Text = "";
            passwordtb.Text = "";
            RoleCb.SelectedIndex = -1;
            RoleCb.Text = "Role";

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if (RoleCb.SelectedIndex == -1)
            {
                MessageBox.Show("Select The Role");
            }
            else if (RoleCb.SelectedIndex == 0) // Admin
            {
                if (string.IsNullOrEmpty(Uname.Text) || string.IsNullOrEmpty(passwordtb.Text))
                {
                    MessageBox.Show("Enter both Admin name and Password");
                }
                else
                {
                    try
                    {
                        using (SqlConnection Con = new SqlConnection(@"Data Source=SAKSHI-24\SQLEXPRESS;Initial Catalog=BankDB;Integrated Security=True"))
                        {
                            Con.Open();
                            SqlDataAdapter sda = new SqlDataAdapter("SELECT COUNT(*) FROM AdminTbl WHERE AdName=@Uname AND AdPass=@Password", Con);
                            sda.SelectCommand.Parameters.AddWithValue("@Uname", Uname.Text);
                            sda.SelectCommand.Parameters.AddWithValue("@Password", passwordtb.Text);
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            if (dt.Rows[0][0].ToString() == "1")
                            {
                                Agents obj = new Agents();
                                obj.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Wrong Admin Name or Password");
                                Uname.Text = "";
                                passwordtb.Text = "";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
            else if (RoleCb.SelectedIndex == 1) // Agent
            {
                if (string.IsNullOrEmpty(Uname.Text) || string.IsNullOrEmpty(passwordtb.Text))
                {
                    MessageBox.Show("Enter both User name and Password");
                }
                else
                {
                    try
                    {
                        using (SqlConnection Con = new SqlConnection(@"Data Source=SAKSHI-24\SQLEXPRESS;Initial Catalog=BankDB;Integrated Security=True"))
                        {
                            Con.Open();
                            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM AgentTbl WHERE AName=@UName AND APass=@Password", Con))
                            {
                                cmd.Parameters.AddWithValue("@UName", Uname.Text);
                                cmd.Parameters.AddWithValue("@Password", passwordtb.Text);

                                int userCount = (int)cmd.ExecuteScalar();

                                if (userCount == 1)
                                {
                                    Mainmenu obj = new Mainmenu();
                                    obj.Show();
                                    this.Hide();
                                }
                                else
                                {
                                    MessageBox.Show("Wrong User Name or Password");
                                    Uname.Text = "";
                                    passwordtb.Text = "";
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void passwordtb_TextChanged(object sender, EventArgs e)
        {

        }

        private void RoleCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
