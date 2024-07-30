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

namespace Bank_Management_System
{
    public partial class Setting : Form
    {
        public Setting()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            newpasscb.Text = "";
            themecb.SelectedIndex = -1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (themecb.SelectedIndex == -1)
            {
                MessageBox.Show("Select A Theme");
            }
            else if (themecb.SelectedIndex == 0)
            {
                panel1.BackColor = Color.Gold;
                

            }
            else if (themecb.SelectedIndex == 1)
            {
                panel1.BackColor = Color.Red;
            }
            else
            {
                panel1.BackColor = Color.Green;
            }
        }
        SqlConnection Con = new SqlConnection(@"Data Source=SAKSHI-24\SQLEXPRESS;Initial Catalog=BankDB;Integrated Security=True");
        private void button1_Click(object sender, EventArgs e)
        {
            if (newpasscb.Text == "")
            {
                MessageBox.Show("Enter New Password");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update AdminTbl set ADPass = @AP WHERE ADId = @Akey", Con);
                    cmd.Parameters.AddWithValue("@AP", newpasscb.Text);
                    cmd.Parameters.AddWithValue("@Akey", 1);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Password Updated");
                    newpasscb.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    // Ensure the connection is closed in the finally block
                    if (Con.State == System.Data.ConnectionState.Open)
                    {
                        Con.Close();
                    }
                }
            }
        }

        private void themecb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {
          
            Application.Exit();
        }

        private void Setting_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}