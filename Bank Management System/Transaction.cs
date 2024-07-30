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
    public partial class Transaction : Form
    {
        public Transaction()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=SAKSHI-24\SQLEXPRESS;Initial Catalog=BankDB;Integrated Security=True");
        int Balance;
       
        private void Transaction_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

      
        private void cheakBalance_btn_TextChanged(object sender, EventArgs e)
        {

        }
        private void cheak_bal_btn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cheak_bal_tb.Text))
            {
                MessageBox.Show("Enter Account Number");
            }
            else
            {
                cheakbalance();
                if (balance_Lbl.Text == "Your Balance")
                {
                    MessageBox.Show("Account Not Found");
                    cheak_bal_tb.Text = "";
                }
            }
        }
       
        private void cheakbalance()
        {
            using (SqlConnection Con = new SqlConnection(@"Data Source=SAKSHI-24\SQLEXPRESS;Initial Catalog=BankDB;Integrated Security=True"))

            {
                try
                {
                    Con.Open();
                    string query = "SELECT ACBal FROM AccountTbl WHERE ACNum=@ACNum";
                    using (SqlCommand cmd = new SqlCommand(query, Con))
                    {
                        cmd.Parameters.AddWithValue("@ACNum", cheak_bal_tb.Text);
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                DataRow dr = dt.Rows[0];
                                balance_Lbl.Text = dr["ACBal"].ToString();
                                Balance = Convert.ToInt32(dr["ACBal"].ToString());
                            }
                            else
                            {
                                balance_Lbl.Text = "Your Balance";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void balance_Lbl_Click(object sender, EventArgs e)
        {

        }
        private void Deposit()
        {
            using (SqlConnection Con = new SqlConnection(@"Data Source=SAKSHI-24\SQLEXPRESS;Initial Catalog=BankDB;Integrated Security=True"))
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO TransactionTbl (TName, TDATE, TAmt, TAcNUM) VALUES (@TN, @TD, @TA, @TAC)", Con);
                    cmd.Parameters.AddWithValue("@TN", "Deposit");
                    cmd.Parameters.AddWithValue("@TD", DateTime.Now.Date);
                    cmd.Parameters.AddWithValue("@TA", DepAmtTB.Text);
                    cmd.Parameters.AddWithValue("@TAC", DepositAccountTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Money Deposited!!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }


        private void GetNewBalance()
        {
            using (SqlConnection Con = new SqlConnection(@"Data Source=SAKSHI-24\SQLEXPRESS;Initial Catalog=BankDB;Integrated Security=True"))
            {
                try
                {
                    Con.Open();
                    string query = "SELECT * FROM AccountTbl WHERE ACNum=@ACNum";
                    using (SqlCommand cmd = new SqlCommand(query, Con))
                    {
                        cmd.Parameters.AddWithValue("@ACNum", cheak_bal_tb.Text);
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                DataRow dr = dt.Rows[0];
                                Balance = Convert.ToInt32(dr["AcBal"]);
                                // balance_Lbl.Text = "Rs" + Balance.ToString(); // Uncomment if needed
                            }
                            else
                            {
                                MessageBox.Show("Account Not Found");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }


        private void DepositBtn_Click(object sender, EventArgs e)
        {
            if (DepositAccountTb.Text == "" || DepositAccountTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                Deposit();
                GetNewBalance();
                int newBal=Balance+Convert.ToInt32(DepAmtTB.Text);
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update AccountTbl set AcBal=@AB Where ACNum=@Ackey", Con);
                    cmd.Parameters.AddWithValue("@AB", newBal);
                  
                    cmd.Parameters.AddWithValue("@Ackey", DepositAccountTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Money Deposited !!");
                    Con.Close();
                    DepAmtTB.Text = "";
                    DepositAccountTb.Text = "";
                    balance_Lbl.Text = "Your Balance";
                }catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        //private void substractBal()
        //{
        //    GetNewBalance() ;
        //    int newBal=Balance-Convert.ToInt32(transAmtTb.Text);
        //    try
        //    {
        //        Con.Open();
        //        SqlCommand cmd = new SqlCommand("update AccountTbl set ACBal=@AB where ACNUm=@Ackey", Con);
        //        cmd.Parameters.AddWithValue("@AB", newBal);
        //        cmd.Parameters.AddWithValue("@Ackey", fromtb.Text);
        //        cmd.ExecuteNonQuery();
        //      //  MessageBox.Show("money transfer");
        //        Con.Close();
        //        //DepAmtTB.Text = "";
        //        //DepositAccountTb.Text = "";
        //        //balancelabel.Text = "Your Balance";
        //    }
        //    catch (Exception Ex)
        //    {
        //        MessageBox.Show(Ex.Message);
        //    }
        //}
        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void Withdraw()
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO TransactionTbl (TName,TDATE,TAmt,TAcNUM)");
                cmd.Parameters.AddWithValue("@TN", "Withdrawn");
                cmd.Parameters.AddWithValue("@TD", DateTime.Now.Date);
                cmd.Parameters.AddWithValue("@TA", DepAmtTB.Text);
                cmd.Parameters.AddWithValue("@TAC", DepAmtTB.Text);

            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
                Con.Close();
            }
        }


        private void Wdbtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(WdAccountTb.Text) || string.IsNullOrEmpty(WdAmtTb.Text))
            {
                MessageBox.Show("Missing Information");
                return;
            }

            GetNewBalance();
            int withdrawAmount = Convert.ToInt32(WdAmtTb.Text);

            if (Balance < withdrawAmount)
            {
                MessageBox.Show("Insufficient Balance");
                return;
            }

            int newBal = Balance - withdrawAmount;

            using (SqlConnection Con = new SqlConnection(@"Data Source=SAKSHI-24\SQLEXPRESS;Initial Catalog=BankDB;Integrated Security=True"))
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE AccountTbl SET AcBal=@AB WHERE ACNum=@Ackey", Con);
                    cmd.Parameters.AddWithValue("@AB", newBal);
                    cmd.Parameters.AddWithValue("@Ackey", WdAccountTb.Text);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Money Withdrawn!!");

                    // Insert a transaction record
                    SqlCommand transCmd = new SqlCommand("INSERT INTO TransactionTbl (TName, TDATE, TAmt, TAcNUM) VALUES (@TN, @TD, @TA, @TAC)", Con);
                    transCmd.Parameters.AddWithValue("@TN", "Withdraw");
                    transCmd.Parameters.AddWithValue("@TD", DateTime.Now);
                    transCmd.Parameters.AddWithValue("@TA", withdrawAmount);
                    transCmd.Parameters.AddWithValue("@TAC", WdAccountTb.Text);
                    transCmd.ExecuteNonQuery();

                    WdAmtTb.Text = "";
                    WdAccountTb.Text = "";
                    balance_Lbl.Text = "Your Balance";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void balancelabel_Click(object sender, EventArgs e)
        {

        }
        
        private void CheakAvilableBal()
        {
            using (SqlConnection Con = new SqlConnection(@"Data Source=SAKSHI-24\SQLEXPRESS;Initial Catalog=BankDB;Integrated Security=True"))
            {
                string query = "SELECT * FROM AccountTbl WHERE ACNum = @ACNum";
                using (SqlCommand cmd = new SqlCommand(query, Con))
                {
                    // Use parameterized query to prevent SQL injection
                    cmd.Parameters.AddWithValue("@ACNum", fromtb.Text);

                    // Open the connection
                    Con.Open();

                    // Create a DataTable to hold the results
                    DataTable dt = new DataTable();

                    // Create a SqlDataAdapter to fill the DataTable
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                    }

                    // Process the results
                    foreach (DataRow dr in dt.Rows)
                    {
                        balancelabel.Text = "Rs" + dr["AcBal"].ToString();
                        Balance = Convert.ToInt32(dr["AcBal"]);
                    }

                    // Close the connection (optional here because of using statement)
                    Con.Close();
                }
            }

        }


        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (fromtb.Text == "")
            {
                MessageBox.Show("Enter Source account");
                
            }
            else
            {
                Con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select count(*) from AccountTbl where ACNum='" + fromtb.Text+"'",Con);
               DataTable dt= new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {
                    CheakAvilableBal();

                    //balancelabel.Text = "Rs";
                    //Agents obj= new Agents();
                    //obj.Show();
                    //this.Hide();
                    Con.Close();
                }
                else
                {
                    MessageBox.Show("Account Does not Exist");
                    fromtb.Text = "";
                }
                Con.Close();
            }
        }

        private void pictureBox3_Click_1(object sender, EventArgs e)
        {
            if(totb.Text == "")
            {
                MessageBox.Show("Enter Destination Account ");

            }
            else
            {
                Con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select count(*) from AccountTbl where ACNum='" + totb.Text + "'", Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {//totb.Text=
                    //CheakAvilableBal();
                    Con.Close();
                    MessageBox.Show("Account Found");
                    if(totb.Text==fromtb.Text)
                    {
                        MessageBox.Show("Source And destination Accounts are same");
                        totb.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Account does not Exist");
                    totb.Text = "";
                }
                Con.Close();    
            }
        }
        private void AddBal()
        {
            // Ensure Balance is set properly by the GetNewBalance method
            GetNewBalance();

            // Ensure that the text from transAmtTb can be converted to an integer
            if (int.TryParse(transAmtTb.Text, out int transAmt))
            {
                int newBal = Balance + transAmt;

                try
                {
                    // Open the connection
                    Con.Open();

                    // Ensure totb.Text is not empty
                    if (!string.IsNullOrEmpty(totb.Text))
                    {
                        string query = "UPDATE AccountTbl SET AcBal = @AB WHERE ACNum = @Ackey";
                        using (SqlCommand cmd = new SqlCommand(query, Con))
                        {
                            // Add parameters with proper data types
                            cmd.Parameters.AddWithValue("@AB", newBal);
                            cmd.Parameters.AddWithValue("@Ackey", totb.Text);

                            // Execute the command
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Account number cannot be empty.");
                    }
                }
                catch (Exception ex)
                {
                    // Show detailed error message
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    // Ensure the connection is closed
                    if (Con.State == System.Data.ConnectionState.Open)
                    {
                        Con.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Transaction amount is not a valid number.");
            }
        }

        private void Transfer()
        {
            try
            {
                using (SqlConnection Con = new SqlConnection(@"Data Source=SAKSHI-24\SQLEXPRESS;Initial Catalog=BankDB;Integrated Security=True"))
                {
                    Con.Open();

                    string query = "INSERT INTO TranserTbl (TrSrc, TrDest, TAmt, TrDate) VALUES (@TS, @TD, @TA, @TDa)";

                    using (SqlCommand cmd = new SqlCommand(query, Con))
                    {
                        cmd.Parameters.AddWithValue("@TS", fromtb.Text);
                        cmd.Parameters.AddWithValue("@TD", totb.Text);
                        cmd.Parameters.AddWithValue("@TA", totb.Text);
                        cmd.Parameters.AddWithValue("@TDa", DateTime.Now);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Money Transfer Successfully");
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void SubtractBalance(decimal amount)
        {
            try
            {
                using (SqlConnection Con = new SqlConnection(@"Data Source=SAKSHI-24\SQLEXPRESS;Initial Catalog=BankDB;Integrated Security=True"))
                {
                    Con.Open();
                    string query = "UPDATE AccountTbl SET ACBal = ACBal - @Amount WHERE ACNum = @AccountNumber";
                    using (SqlCommand cmd = new SqlCommand(query, Con))
                    {
                        cmd.Parameters.AddWithValue("@Amount", amount);
                        cmd.Parameters.AddWithValue("@AccountNumber", fromtb.Text);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void transbtn_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(totb.Text) || string.IsNullOrWhiteSpace(fromtb.Text) || string.IsNullOrWhiteSpace(transAmtTb.Text))
            {
                MessageBox.Show("Missing Information");
                return;
            }

            if (!decimal.TryParse(transAmtTb.Text, out decimal transferAmount))
            {
                MessageBox.Show("Invalid Transfer Amount");
                return;
            }

            GetNewBalance();

            if (transferAmount < Balance)
            {
                MessageBox.Show("Insufficient Balance");
                return;
            }

            Transfer();
            SubtractBalance(transferAmount);
            AddBal();
            fromtb.Text = "";
            totb.Text = "";
            transAmtTb.Text = "";
        }

        private void label15_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
            
        }
    }
}