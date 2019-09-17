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

namespace LMS_ADMS_FALL.PresentationLayer
{
    public partial class Home : Form
    {
        int temp = 0;
        Process p = new Process();
        public Home()
        {
            InitializeComponent();
            Show_Controls(false);
            temp = Log.num;
            Log.tab = 0;
            
        }

        private void Home_Load(object sender, EventArgs e)
        {
            

            if (temp == 1)
            {
                //issueToolStripMenuItem.Visible = false;
               // issueToolStripMenuItem.Enabled = false;
            }

            else if (temp == 2)
            {
                issueToolStripMenuItem.Visible = false;
                issueToolStripMenuItem.Enabled = false;

                operationToolStripMenuItem.Visible = false;
                operationToolStripMenuItem.Enabled = false;

                memberToolStripMenuItem.Visible = false;
                memberToolStripMenuItem.Enabled = false;
            }

            else if (temp == 3)
            {
                operationToolStripMenuItem.Visible = false;
                operationToolStripMenuItem.Enabled = false;
            }

            else
            {
                MessageBox.Show("Invalid User");
            }
        }

        private void bookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = p.Get_All_Book();
            Show_Controls(true);
            Log.tab = 1;
        }

        private void memberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = p.Get_All_Member();
            Show_Controls(true);
            Log.tab = 2;
        }

        public void Show_Controls(bool b)
        {
            dataGridView1.Visible = b;
            LH1.Visible = b;
            TH1.Visible = b;
            BH1.Visible = b;
            //catagory.Visible = b;
            //catadropdown.Visible = b;
        }

        private void BH1_Click(object sender, EventArgs e)
        {
            string part = TH1.Text;
            
            if (Log.tab == 1)
            {
               dataGridView1.DataSource = p.Search_Book(part);
            }

            else if (Log.tab == 2)
            {
                dataGridView1.DataSource = p.Search_Member(part);
            }

            else if (Log.tab == 3)
            {
                dataGridView1.DataSource = p.Search_History(part);
            }

        }

        private void bookToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Hide();
            BookPanel b = new BookPanel();
            b.Show();
        }

        private void userToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            MemberPanel b = new MemberPanel();
            b.Show();
        }

        private void bookSelfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            BookSelfPanel b = new BookSelfPanel();
            b.Show();
        }

        private void issueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            History b = new History();
            b.Show();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login b = new Login();
            b.Show();
        }

        private void historyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = p.Fhistory(Log.userl,Log.passl);
            Show_Controls(true);
            Log.tab = 3;
        }
    }
}
