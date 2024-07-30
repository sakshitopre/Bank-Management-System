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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private int startP = 0;

        private void label2_Click(object sender, EventArgs e)
        {
            // Add your label click handling code here, if needed.
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            startP += 1;
            progressBar1.Value = startP;

            if (progressBar1.Value >= 90)
            {
                // Stop the timer to prevent it from continuing to tick
                timer1.Stop();

                // Reset progress bar for future use
                progressBar1.Value = 0;
                startP = 0;
                timer1.Stop();

                // Show the Login form
                Login Obj = new Login();
                Obj.Show();

                // Hide the current form
                this.Hide();
            }
        }

        // Ensure the timer is started properly somewhere in your code, such as in the form's Load event
        private void Form1_Load(object sender, EventArgs e)
        {
            // Set timer interval to 100 milliseconds (or as needed)
            timer1.Interval = 100;
            timer1.Tick += new EventHandler(timer1_Tick);

            // Start the timer
            timer1.Start();
        }


        
    }
}
