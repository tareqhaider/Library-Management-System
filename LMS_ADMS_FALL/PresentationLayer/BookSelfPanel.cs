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
    public partial class BookSelfPanel : Form
    {
        Process p = new Process();
        DataTable d = new DataTable();
        

        public BookSelfPanel()
        {
            InitializeComponent();
            
        }

        private void BookSelfPanel_Load(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            List_load(p.Get_All_Book_Self());
        }

        private void Search_Click(object sender, EventArgs e)
        {

            string part = tbsid.Text;
            listView1.Items.Clear();
            d = p.Search_Book_Self(part);
            List_load(d);
            Detail_load(d);

        }

        private void List_load(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                ListViewItem listitem = new ListViewItem(dr["ID"].ToString());
                listitem.SubItems.Add(dr["NAME"].ToString());
                listitem.SubItems.Add(dr["NUMB"].ToString());
                listitem.SubItems.Add(dr["CATEGORY"].ToString());
                listView1.Items.Add(listitem);
            }
        }

        private void Detail_load(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                tbsid.Text = dr["ID"].ToString();
                sno.Text = dr["NUMB"].ToString();
                sname.Text = dr["NAME"].ToString();
                cata.Text = dr["CATEGORY"].ToString();
            }
        }

        private void Confirm_Click(object sender, EventArgs e)
        {
            if (string.Compare(comboBox1.Text, "Insert") == 0)
            {
                string msg = p.Insert_Book_Self(sname.Text,cata.Text,sno.Text);
                MessageBox.Show(msg);
                listView1.Items.Clear();
                List_load(p.Get_All_Book_Self());
            }

            else if (string.Compare(comboBox1.Text, "Update") == 0)
            {
                string msg = p.Update_Book_Self_Info(Convert.ToInt16(tbsid.Text),sname.Text, cata.Text, sno.Text);
                MessageBox.Show(msg);
                listView1.Items.Clear();
                List_load(p.Get_All_Book_Self());
            }

            else if (string.Compare(comboBox1.Text, "Delete") == 0)
            {
                string msg = p.Delete_BS(Convert.ToInt16(tbsid.Text));
                MessageBox.Show(msg);
                listView1.Items.Clear();
                List_load(p.Get_All_Book_Self());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home h = new Home();
            h.Show();
        }
    }
}
