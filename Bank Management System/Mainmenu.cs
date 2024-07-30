using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bank_Management_System
{
    public partial class Mainmenu : Form
    {
        public Mainmenu()
        {
            InitializeComponent();
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            // Create and show the Setting form
            Setting obj = new Setting();

            // Subscribe to the FormClosed event of the Setting form
            obj.FormClosed += (s, args) =>
            {
                // Bring the main menu form to the front and activate it
                this.Show();
                this.BringToFront();
            };

            // Show the Setting form
            obj.Show();

        }


        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Transaction obj = new Transaction();
            obj.Show();
            this.Hide();
        }  

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            AddAccounts obj = new AddAccounts();
            obj.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {
            //this.Close();
        }

        private void label5_Click_1(object sender, EventArgs e)
        {
            //Application.Exit();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
