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
using System.Xml.Linq;

namespace Bank_Management_System
{
    public partial class Agents : Form
    {
        public Agents()
        {
            InitializeComponent();
            DisplayAgents();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=SAKSHI-24\SQLEXPRESS;Initial Catalog=BankDB;Integrated Security=True");
        private void DisplayAgents()
        {
            try
            {
                Con.Open();
                string query = "SELECT * FROM AgentTbl";
                SqlDataAdapter sda = new SqlDataAdapter(query, Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                AgentDGV.DataSource = dt;
                Con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void Reset()
        {
            // Reset any input fields and KEY variable
            // Example:
            ANametb.Text = "";
            PasswordTb.Text = "";
            AddressTb.Text = "";
            PhoneTb.Text = "";
            key = 0;

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void submit_btn_Click(object sender, EventArgs e)
        {
            if (ANametb.Text == "" || PasswordTb.Text == "" || PhoneTb.Text == "" || AddressTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    using (SqlConnection Con = new SqlConnection(@"Data Source=SAKSHI-24\SQLEXPRESS;Initial Catalog=BankDB;Integrated Security=True"))
                    {
                        Con.Open();
                        SqlCommand cmd = new SqlCommand("INSERT INTO AgentTbl (AName, APass, APhone, Aadress) VALUES (@AN, @APA, @AAH, @AA)", Con);

                        cmd.Parameters.AddWithValue("@AN", ANametb.Text);
                        cmd.Parameters.AddWithValue("@APA", PasswordTb.Text);
                        cmd.Parameters.AddWithValue("@AAH", PhoneTb.Text);
                        cmd.Parameters.AddWithValue("@AA", AddressTb.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Agent Added!!");
                        Con.Close();
                        Reset();
                        DisplayAgents();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }


        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Cancel_btn_Click(object sender, EventArgs e)
        {
            // Debug statement to check the value of key
            MessageBox.Show($"Value of key: {key}");

            if (key == 0)
            {
                MessageBox.Show("Select the Account");
            }
            else
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(Con.ConnectionString))
                    {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand("DELETE FROM AgentTbl WHERE AId = @Ackey", con))
                        {
                            cmd.Parameters.AddWithValue("@Ackey", key);

                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Account Deleted!!");
                                Reset();
                                DisplayAgents();
                            }
                            else
                            {
                                MessageBox.Show("Account not found or unable to delete.");
                            }
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("A database error occurred: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        int key = 0;
        private void AgentDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure a row is selected before accessing its cells
            if (AgentDGV.SelectedRows.Count > 0)
            {
                // Retrieve and set the values from the selected row
                ANametb.Text = AgentDGV.SelectedRows[0].Cells[1].Value.ToString();
                PasswordTb.Text = AgentDGV.SelectedRows[0].Cells[2].Value.ToString();
                PhoneTb.Text = AgentDGV.SelectedRows[0].Cells[3].Value.ToString();
                AddressTb.Text = AgentDGV.SelectedRows[0].Cells[4].Value.ToString();

                // Check if ANametb.Text is empty and set the key accordingly
                if (string.IsNullOrEmpty(ANametb.Text))
                {
                    key = 0;
                }
                else
                {
                    key = Convert.ToInt32(AgentDGV.SelectedRows[0].Cells[0].Value);
                }
            }
            else
            {
                // Handle the case where no row is selected
                MessageBox.Show("Please select a row to retrieve data.", "No row selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }



        }

        private void Edit_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(ANametb.Text) || string.IsNullOrEmpty(PasswordTb.Text) || string.IsNullOrEmpty(AddressTb.Text) || string.IsNullOrEmpty(PhoneTb.Text))
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {
                    MessageBox.Show($"Updating record with key: {key}");

                    using (SqlConnection con = new SqlConnection(@"Data Source=SAKSHI-24\SQLEXPRESS;Initial Catalog=BankDB;Integrated Security=True"))
                    {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand("UPDATE AgentTbl SET AName=@AN, APass=@APA, APhone=@AAH, Aadress=@AA WHERE AId=@AKey", con))
                        {
                            cmd.Parameters.AddWithValue("@AN", ANametb.Text);
                            cmd.Parameters.AddWithValue("@APA", PasswordTb.Text);
                            cmd.Parameters.AddWithValue("@AAH", PhoneTb.Text);
                            cmd.Parameters.AddWithValue("@AA", AddressTb.Text);
                            cmd.Parameters.AddWithValue("@AKey", key);

                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Account Updated!!");
                            }
                            else
                            {
                                MessageBox.Show("Update failed. Please ensure the account key is correct.");
                            }
                        }
                    }
                    Reset();
                    DisplayAgents();
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }



        private void Agents_Load(object sender, EventArgs e)
        {
            Setting Obj = new Setting();
            Obj.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AgentDGV_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = AgentDGV.Rows[e.RowIndex];
                key = Convert.ToInt32(row.Cells["AId"].Value); // Assuming AId is the primary key column
                ANametb.Text = row.Cells["AName"].Value.ToString();
                PasswordTb.Text = row.Cells["APass"].Value.ToString();
                PhoneTb.Text = row.Cells["APhone"].Value.ToString();
                AddressTb.Text = row.Cells["Aadress"].Value.ToString();
            }
        }

    }
}
