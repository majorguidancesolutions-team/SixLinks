using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyDataManagerWinForms
{
    public partial class ProgressBarForm : Form
    {
        public ProgressBarForm()
        {
            InitializeComponent();
            //InitializeMyTimer();
        }

        private void ProgressBarForm_Load(object sender, EventArgs e)
        {
            InitializeMyTimer();
        }

        private void InitializeMyTimer()
        {
            // Set the interval for the timer.
            timer.Interval = 250;
            // Connect the Tick event of the timer to its event handler.
            timer.Tick += new EventHandler(IncreaseProgressBar);
            // Start the timer.
            timer.Start();
        }

        private void IncreaseProgressBar(object sender, EventArgs e)
        {
            // Increment the value of the ProgressBar a value of one each time.
            progressBar.Increment(1);
            // Display the textual value of the ProgressBar in the StatusBar control's first panel.
            txtProgress.Text = $"Database loading: {progressBar.Value.ToString()}% Completed";
            // Determine if we have completed by comparing the value of the Value property to the Maximum value.
            if (progressBar.Value == progressBar.Maximum)
            {
                // Stop the timer.
                timer.Stop();
                return;
            }
               
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
