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
    public partial class MemberPanel : Form
    {
        Process p = new Process();
        
        public MemberPanel()
        {
            InitializeComponent();
        }

        private void MemberPanel_Load(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            List_load(p.Get_All_Member());
            
        }

        private void Search_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            List_load(p.Search_Member(mid.Text));
            Detail_load(p.Search_Member(mid.Text));
        }

        private void List_load(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                ListViewItem listitem = new ListViewItem(dr["ID"].ToString());
                listitem.SubItems.Add(dr["NAME"].ToString());
                listitem.SubItems.Add(dr["USERNAME"].ToString());
                listitem.SubItems.Add(dr["PASSWORD"].ToString());
                listitem.SubItems.Add(dr["PHONE"].ToString());
                listitem.SubItems.Add(dr["EMAIl"].ToString());
                listitem.SubItems.Add(dr["TYPE"].ToString());
                listView1.Items.Add(listitem);
            }
        }

        private void Detail_load(DataTable dt)
        {
            if (dt.Rows.Count == 1)
            {
                DataRow dr = dt.Rows[dt.Rows.Count - 1];
                mname.Text = dr["NAME"].ToString();
                muname.Text = dr["USERNAME"].ToString();
                mpass.Text = dr["PASSWORD"].ToString();
                mphone.Text = dr["PHONE"].ToString();
                memail.Text = dr["EMAIL"].ToString();
                mtype.Text = dr["TYPE"].ToString();
               
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home h = new Home();
            h.Show();
        }

        private void Confirm_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;

            bool flag = Check_Entry(mname.Text, muname.Text, mpass.Text, mphone.Text, memail.Text, mtype.Text);

            if (string.Compare(Operation.Text, "Insert") == 0 && flag == true)
            {
                //int a = 1;
                msg = p.Insert_Member(mname.Text, muname.Text, mpass.Text, mphone.Text, memail.Text, mtype.Text);
                MessageBox.Show(msg);
                listView1.Items.Clear();
                List_load(p.Fetch_View_Book());
            }

            if (string.Compare(Operation.Text, "Update") == 0 && flag == true)
            {
                int parse;
                bool bid = int.TryParse(mid.Text, out parse);

                if (bid == true)
                {
                    //int a = 1;
                    msg = p.Update_Member(Convert.ToInt16(mid.Text),mname.Text, muname.Text, mpass.Text, mphone.Text, memail.Text, mtype.Text);
                    MessageBox.Show(msg);
                    listView1.Items.Clear();
                    List_load(p.Fetch_View_Book());
                }

                else
                {
                    MessageBox.Show("Book ID invalid");
                }

            }

            if (string.Compare(Operation.Text, "Delete") == 0 && flag == true)
            {
                int parse;
                bool bid = int.TryParse(mid.Text, out parse);

                if (bid == true)
                {
                    msg = p.Delete_Member(Convert.ToInt16(mid.Text));
                    MessageBox.Show(msg);
                    listView1.Items.Clear();
                    List_load(p.Fetch_View_Book());
                }

            }

            listView1.Items.Clear();
            List_load(p.Get_All_Member());
        }

        private bool Check_Entry(string mname, string muname, string mpass, string phone, string email, string type)
        {
            bool flag = false;

            bool bn = !string.IsNullOrEmpty(mname);
            bool a = !string.IsNullOrEmpty(muname);
            bool p = !string.IsNullOrEmpty(mpass);
            bool bsi = !string.IsNullOrEmpty(phone);
            bool tq = !string.IsNullOrEmpty(email);
            bool aq = !string.IsNullOrEmpty(type);


            

            if (bn && a && p && bsi && tq && aq)
            {
                flag = true;
            }

            else
            {
                MessageBox.Show("Empty or Improper entry");
            }

            return flag;
        }
    }
}
