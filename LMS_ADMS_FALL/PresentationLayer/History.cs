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
    public partial class History : Form
    {
        Process p = new Process();
        
        public History()
        {
            InitializeComponent();
        }

        private void History_Load(object sender, EventArgs e)
        {
            sdate.Text = DateTime.Now.ToString();
            idate.Text = DateTime.Now.ToString();
            rdate.Value = DateTime.Now.AddDays(7);
            listView1.Items.Clear();
            List_load(p.Fetch_View_History());
            //int a = da.Book_Quantity_Manage(2, 1);
        }
        private void search_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tmid.Text) & !string.IsNullOrEmpty(tbid.Text))
            {
                DataTable m = p.Search_Member(tmid.Text);
                DataTable b = p.Search_View_Book(tbid.Text);
                Book_Detail_load(b);
                Member_Detail_load(m);
            }
            else if (string.IsNullOrEmpty(tmid.Text) | string.IsNullOrEmpty(tbid.Text))
            {
                MessageBox.Show("Please Fill Required Fields");
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            bool flag = Check_Entry(tmid.Text, tbid.Text, mmode.Text, bmode.Text);
            TimeSpan t = rdate.Value - DateTime.Now;
            int dd = Convert.ToInt16(t.TotalDays);
            //MessageBox.Show(da.Return_Due_Date(7));
            //days = Convert.ToInt16( );

            //MessageBox.Show(da.Flag_Overdue(1).ToString());

            if (string.Compare(operation.Text, "Issue") == 0 && flag == true)
            {
                if (string.Compare(mmode.Text, "Proceed") == 0 && string.Compare(bmode.Text, "Available") == 0)
                {
                    string msg = p.Operation_Issue_Book(tmid.Text, tbid.Text,dd);
                    MessageBox.Show(msg);

                }
                else
                {
                    MessageBox.Show("Not Allowed");
                }
                Clean_Entrty();
                
                listView1.Items.Clear();
                List_load(p.Fetch_View_History());
            }

            if (string.Compare(operation.Text, "Return") == 0 && flag == true)
            {
                string msg = p.Operation_Return_Book(tmid.Text, tbid.Text);
                MessageBox.Show(msg);
                Clean_Entrty();
                listView1.Items.Clear();
                List_load(p.Fetch_View_History());
                //int parse;
                //bool bid = int.TryParse(tbinfo.Text, out parse);

                //if (bid == true)
                //{
                //    string msg = p.Update_Book(Convert.ToInt16(tbinfo.Text), tbname.Text, taname.Text, tpname.Text, Convert.ToInt16(tbsid.Text), Convert.ToInt16(tq.Text), Convert.ToInt16(aq.Text));
                //    MessageBox.Show(msg);
                //    listView1.Items.Clear();
                //    List_load(p.Fetch_View_Book());
                //}

                //else
                //{
                //    MessageBox.Show("Book ID invalid");
                //}

            }
        }

        private void Book_Detail_load(DataTable dt)
        {
            if (dt.Rows.Count == 1)
            {
                DataRow dr = dt.Rows[dt.Rows.Count - 1];
                bname.Text = dr["NAME"].ToString();
                bauthor.Text = dr["AUTHOR"].ToString();
                int aq = Convert.ToInt16(dr["AVAILABLE_QUANTITY"]);

                if (aq > 0)
                {
                    bmode.Text = "Available";
                }
                else
                {
                    bmode.Text = "Unavailable";
                }
                bsname.Text = dr["SELF_NAME"].ToString();

            }


        }

        private void Member_Detail_load(DataTable dt)
        {
            if (dt.Rows.Count == 1)
            {
                DataRow dr = dt.Rows[dt.Rows.Count - 1];
                mname.Text = dr["NAME"].ToString();
                memail.Text = dr["EMAIL"].ToString();
                mphone.Text = dr["PHONE"].ToString();

                int parse;
                bool mflag = int.TryParse(tmid.Text, out parse);
                bool bflag = int.TryParse(tbid.Text, out parse);

                if (mflag && bflag)
                {
                    string key = tmid.Text;
                    int dcount = p.Count_Member_Overdue(key);
                    int icount = p.Count_Member_Issue_Status(key,tbid.Text);
                    int ccount = p.Count_Member_Issue(key);

                    if (icount == 1)
                    {
                        mmode.Text = "Issued";
                        if (dcount > 0)
                        {
                            mmode.Text = "Overdue";
                        }

                    }

                    else if (ccount >= 3)
                    {
                        mmode.Text = "Limit Reached";
                    }

                    else
                    {
                        mmode.Text = "Proceed";
                    }



                }
                
            }


        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home h = new Home();
            h.Show();
        }

        private void List_load(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                ListViewItem listitem = new ListViewItem(dr["ID"].ToString());
                listitem.SubItems.Add(dr["MNAME"].ToString());
                listitem.SubItems.Add(dr["BNAME"].ToString());
                listitem.SubItems.Add(dr["AVAILABLE_BOOK"].ToString());
                listitem.SubItems.Add(dr["DUE_DATE"].ToString());
                listitem.SubItems.Add(dr["PHONE"].ToString());
                listitem.SubItems.Add(dr["EMAIL"].ToString());
                listitem.SubItems.Add(dr["STATUS"].ToString());
                listView1.Items.Add(listitem);
            }
        }

        private void Clean_Entrty()
        {
            tmid.Text = string.Empty;
            tbid.Text = string.Empty;
            mname.Text = string.Empty;
            mmode.Text = string.Empty;
            memail.Text = string.Empty;
            mphone.Text = string.Empty;
            bname.Text = string.Empty;
            bmode.Text = string.Empty;
            bauthor.Text = string.Empty;
            bsname.Text = string.Empty;
        }



        private bool Check_Entry(string tmid, string tbid, string mmode, string bmode)
        {
            int parse;
            bool flag = false;

            bool mid = int.TryParse(tbid, out parse);
            bool bid = int.TryParse(tbid, out parse);
            bool mm = !string.IsNullOrEmpty(mmode);
            bool bm = !string.IsNullOrEmpty(bmode);


            if (mid && bid && mm && bm)
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
