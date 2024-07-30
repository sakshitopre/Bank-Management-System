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
using System.Diagnostics.Eventing.Reader;

namespace Bank_Management_System
{
    public partial class AddAccounts : Form
    {
        public AddAccounts()
        {
            InitializeComponent();
            DisplayAccounts();
        }
        SqlConnection Con=new SqlConnection(@"Data Source=SAKSHI-24\SQLEXPRESS;Initial Catalog=BankDB;Integrated Security=True");
        private void DisplayAccounts()
        {
            try
            {
                Con.Open();
                string query = "SELECT * FROM AccountTbl";
                SqlDataAdapter sda = new SqlDataAdapter(query, Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
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
            AcNametb.Text = "";
            AcPhonrTb.Text = "";
            AcAddressTb.Text = "";
            GenderCb.SelectedIndex = -1;
            OccupationTb.Text = "";
            EducationCB.SelectedIndex = -1;
            IncomeTb.Text = "";
            KEY = 0;
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AddAccounts_Load(object sender, EventArgs e)
        {

        }

       

        private void submit_btn_Click(object sender, EventArgs e)
        {
            if (AcNametb.Text == "" || AcPhonrTb.Text == "" || AcAddressTb.Text == "" || GenderCb.SelectedIndex == -1 ||
                OccupationTb.Text == "" || EducationCB.SelectedIndex == -1 || IncomeTb.Text == "") 
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into AccountTBL(ACName,ACPhone,ACAddress,ACGender,ACOccup,ACEduc,ACInc,ACBal)values(@AN,@AP,@AA,@AG,@AO,@AE,@AI,@AB)",Con);
                  
                    cmd.Parameters.AddWithValue("@AN", AcNametb.Text);
                    cmd.Parameters.AddWithValue("@AP", AcPhonrTb.Text);
                    cmd.Parameters.AddWithValue("@AA", AcAddressTb.Text);
                    cmd.Parameters.AddWithValue("@AG", GenderCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@AO", OccupationTb.Text);
                    cmd.Parameters.AddWithValue("@AE", EducationCB.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@AI", IncomeTb.Text);
                    cmd.Parameters.AddWithValue("@AB",0);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Account Created!!");
                    Con.Close();
                    Reset();
                    DisplayAccounts();
                }catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Cancel_btn_Click(object sender, EventArgs e)
        {
            // Check if KEY is set properly
            if (KEY == 0)
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

                        // Using parameterized query to prevent SQL injection
                        using (SqlCommand cmd = new SqlCommand("DELETE FROM AccountTbl WHERE ACNum = @Ackey", con))
                        {
                            cmd.Parameters.AddWithValue("@Ackey", KEY);

                            // Execute the DELETE command
                            int rowsAffected = cmd.ExecuteNonQuery();

                            // Check if any row is affected (deleted)
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Account Deleted!!");
                                Reset();
                                DisplayAccounts();
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

        int KEY = 0;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                KEY = Convert.ToInt32(row.Cells["ACNum"].Value); // Assuming ACNum is the primary key column
                AcNametb.Text = row.Cells["ACName"].Value.ToString();
                AcPhonrTb.Text = row.Cells["ACPhone"].Value.ToString();
                AcAddressTb.Text = row.Cells["ACAddress"].Value.ToString();
                GenderCb.SelectedItem = row.Cells["ACGender"].Value.ToString();
                OccupationTb.Text = row.Cells["ACOccup"].Value.ToString();
                EducationCB.SelectedItem = row.Cells["ACEduc"].Value.ToString();
                IncomeTb.Text = row.Cells["ACInc"].Value.ToString();
            }
        }

        private void Edit_btn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(AcNametb.Text) || string.IsNullOrEmpty(AcPhonrTb.Text) || string.IsNullOrEmpty(AcAddressTb.Text) ||
                GenderCb.SelectedIndex == -1 || string.IsNullOrEmpty(OccupationTb.Text) || EducationCB.SelectedIndex == -1 || string.IsNullOrEmpty(IncomeTb.Text))
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
                        using (SqlCommand cmd = new SqlCommand("UPDATE AccountTBL SET ACName=@AN, ACPhone=@AP, ACAddress=@AA, ACGender=@AG, ACOccup=@AO, ACEduc=@AE, ACInc=@AI WHERE ACNum=@AcKey", Con))
                        {
                            cmd.Parameters.AddWithValue("@AN", AcNametb.Text);
                            cmd.Parameters.AddWithValue("@AP", AcPhonrTb.Text);
                            cmd.Parameters.AddWithValue("@AA", AcAddressTb.Text);
                            cmd.Parameters.AddWithValue("@AG", GenderCb.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@AO", OccupationTb.Text);
                            cmd.Parameters.AddWithValue("@AE", EducationCB.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@AI", IncomeTb.Text);
                            cmd.Parameters.AddWithValue("@AcKey", KEY);

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
                    DisplayAccounts();
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
        }

        private void SelectAccount(int accountKey)
        {
            KEY = accountKey;
            // Fetch the account details from the database and populate the form fields
            using (SqlConnection Con = new SqlConnection(@"Data Source=SAKSHI-24\SQLEXPRESS;Initial Catalog=BankDB;Integrated Security=True"))
            {
                Con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT ACName, ACPhone, ACAddress, ACGender, ACOccup, ACEduc, ACInc FROM AccountTBL WHERE ACNum=@AcKey", Con))
                {
                    cmd.Parameters.AddWithValue("@AcKey", KEY);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            AcNametb.Text = reader["ACName"].ToString();
                            AcPhonrTb.Text = reader["ACPhone"].ToString();
                            AcAddressTb.Text = reader["ACAddress"].ToString();
                            GenderCb.SelectedItem = reader["ACGender"].ToString();
                            OccupationTb.Text = reader["ACOccup"].ToString();
                            EducationCB.SelectedItem = reader["ACEduc"].ToString();
                            IncomeTb.Text = reader["ACInc"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Account not found. Please check the account key.");
                        }
                    }
                }
            }
        }

        private void GenderCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
