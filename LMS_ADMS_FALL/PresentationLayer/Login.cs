using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LMS_ADMS_FALL.BusinessLogicLayer;
using LMS_ADMS_FALL.PresentationLayer;

namespace LMS_ADMS_FALL
{
    public partial class Login : Form
    {
        
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string user = textBox1.Text;
            string pass = textBox2.Text;
            
            

            if(!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(pass))
            {
                Process p = new Process();
                int temp = p.Get_Log_Info(user, pass);

                
                if(temp > 0)
                {
                    Log.userl = user;
                    Log.passl = pass;
                    this.Hide();
                    Home h = new Home();
                    h.Show();
                }

                else
                {
                    MessageBox.Show("Invalid User");
                }
            }
        }
    }
}
