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
    public partial class BookPanel : Form
    {
        Process p = new Process();
        
        

        public BookPanel()
        {
            InitializeComponent();
        }

        private void BookPanel_Load(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            List_load(p.Fetch_View_Book());
        }

        private void Search_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            List_load(p.Search_View_Book(tbinfo.Text));
            Detail_load(p.Search_View_Book(tbinfo.Text));
        }
        
        private void Confirm_Click(object sender, EventArgs e)
        {
            bool flag = Check_Entry(tbname.Text, taname.Text, tpname.Text, tbsid.Text, tq.Text, aq.Text);
            string msg = string.Empty;


            if (string.Compare(comboBox1.Text, "Insert") == 0 && flag == true)
            {
                msg = p.Insert_Book(tbname.Text, taname.Text, tpname.Text,Convert.ToInt16(tbsid.Text), Convert.ToInt16(tq.Text), Convert.ToInt16(aq.Text));
                MessageBox.Show(msg);
                listView1.Items.Clear();
                List_load(p.Fetch_View_Book());
            }

            if (string.Compare(comboBox1.Text, "Update") == 0 && flag == true)
            {
                int parse;
                bool bid = int.TryParse(tbinfo.Text, out parse);

                if(bid == true)
                {
                    msg = p.Update_Book(Convert.ToInt16(tbinfo.Text), tbname.Text, taname.Text, tpname.Text, Convert.ToInt16(tbsid.Text), Convert.ToInt16(tq.Text), Convert.ToInt16(aq.Text));
                    MessageBox.Show(msg);
                    listView1.Items.Clear();
                    List_load(p.Fetch_View_Book());
                }

                else
                {
                    MessageBox.Show("Book ID invalid");
                }
                
            }

            if (string.Compare(comboBox1.Text, "Delete") == 0 && flag == true)
            {
                int parse;
                bool bid = int.TryParse(tbinfo.Text, out parse);

                if (bid == true)
                {
                    msg = p.Delete_Book(Convert.ToInt16(tbinfo.Text));
                    MessageBox.Show(msg);
                    listView1.Items.Clear();
                    List_load(p.Fetch_View_Book());
                }
                
            }

        }

        private void List_load(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                ListViewItem listitem = new ListViewItem(dr["ID"].ToString());
                listitem.SubItems.Add(dr["NAME"].ToString());
                listitem.SubItems.Add(dr["AUTHOR"].ToString());
                listitem.SubItems.Add(dr["PUBLISHER"].ToString());
                listitem.SubItems.Add(dr["TOTAL_QUANTITY"].ToString());
                listitem.SubItems.Add(dr["AVAILABLE_QUANTITY"].ToString());
                listitem.SubItems.Add(dr["AVAILABLE"].ToString());
                listitem.SubItems.Add(dr["SELF_NAME"].ToString());
                listView1.Items.Add(listitem);
            }
        }

        private void Detail_load(DataTable dt)
        {
            if(dt.Rows.Count == 1)
            {
                DataRow dr = dt.Rows[dt.Rows.Count-1];
                tbname.Text = dr["NAME"].ToString();
                taname.Text = dr["AUTHOR"].ToString();
                tpname.Text = dr["PUBLISHER"].ToString();
                tq.Text = dr["TOTAL_QUANTITY"].ToString();
                aq.Text = dr["AVAILABLE_QUANTITY"].ToString();
                cata.Text= dr["CATEGORY"].ToString();
                tbsid.Text = dr["SELF_ID"].ToString();
                sno.Text = dr["SELF_NO"].ToString();
                sname.Text = dr["SELF_NAME"].ToString();
            }
            
        }

        private void Detail_load_BS(DataTable dt)
        {
            if (dt.Rows.Count == 1)
            {
                DataRow dr = dt.Rows[dt.Rows.Count - 1];
                cata.Text = dr["CATEGORY"].ToString();
                sno.Text = dr["NUMB"].ToString();
                sname.Text = dr["NAME"].ToString();
            }

        }

        private bool Check_Entry(string bname, string aname, string pname, string sbsid, string total_quality, string available_quality)
        {
            int parse;
            bool flag = false;

            bool bn = !string.IsNullOrEmpty(bname);
            bool a = !string.IsNullOrEmpty(aname);
            bool p = !string.IsNullOrEmpty(pname);
            bool bsi = !string.IsNullOrEmpty(sbsid);
            bool tq = !string.IsNullOrEmpty(total_quality);
            bool aq = !string.IsNullOrEmpty(available_quality);

            
            bool bsid = int.TryParse(sbsid, out parse);
            bool tquantity = int.TryParse(total_quality, out parse);
            bool aquantity = int.TryParse(available_quality, out parse);

            if (bn && a && p && bsi && tq && aq)
            {
                if (bsid && tquantity && aquantity)
                {
                    flag = true;
                }
                else
                {
                    MessageBox.Show("Empty or Improper entry");
                }
            }

            else
            {
                MessageBox.Show("Empty or Improper entry");
            }

            return flag;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home h = new Home();
            h.Show();
        }

        private void Check_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbsid.Text))
            {
                int parse;
                bool b = int.TryParse(tbsid.Text, out parse);
                if (b)
                {
                    Detail_load_BS(p.Search_Book_Self(tbsid.Text));
                }

            }
        }
    }
}
