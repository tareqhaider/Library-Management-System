using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using LMS_ADMS_FALL.DataAccessLayer;

namespace LMS_ADMS_FALL.BusinessLogicLayer
{
    internal class Process
    {
        
        
        DataAccess da = new DataAccess();
        DataAccess dd = new DataAccess();
        public int Get_Log_Info(string u,string p)
        {
            u = u.Replace(" ", string.Empty);
            p = p.Replace(" ", string.Empty);

            int t = 0;
            t = da.Return_User_Type(u,p);
            Log.num = t;
            return t;
            
        }

        public string Get_Database_Date()
        {
            string date = da.Return_Database_Date();

            return date;
        }

        public int Get_Count_BookBorrowed(int id)
        {
            string rtn = da.Fetch_Count_By_ID(id,"History","mid");
            int count = Convert.ToInt16(rtn);

            return count;
        }

        public DataTable Get_All_Book()
        {
            DataTable dt = da.Get_Table("Book");
            return dt;
        }
        public DataTable Fetch_View_Book()
        {
            DataTable dt = da.Get_Table("View_Book");
            return dt;
        }


        public DataTable Get_All_Member()
        {
            DataTable dt = da.Get_Table("Member");
            return dt;
        }

        public DataTable Get_All_Book_Self()
        {
            DataTable dt = da.Get_Table("Book_Self");
            return dt;
        }

        public DataTable Get_All_History()
        {
            DataTable dt = da.Get_Table("History");
            return dt;
        }

        public DataTable Fetch_View_History()
        {
            DataTable dt = da.Get_Table("View_History");
            return dt;
        }

        public DataTable Search_Book(string key)
        {
            key = key.Replace(" ", string.Empty);

            DataTable dt = da.Search_By_Key(key, "Book");
            return dt;
        }

        public DataTable Search_Book_Self(string key)
        {
            key = key.Replace(" ", string.Empty);

            DataTable dt = da.Search_By_Key(key, "Book_Self");
            return dt;
        }

        public DataTable Search_View_Book(string key)
        {
            key = key.Replace(" ", string.Empty);

            DataTable dt = da.Search_By_Key(key, "View_Book");
            return dt;
        }

        public DataTable Search_Member(string key)
        {
            key = key.Replace(" ", string.Empty);
            DataTable dt = da.Search_By_Key(key, "Member");
            return dt;
        }

        public DataTable Search_History(string key)
        {
            key = key.Replace(" ", string.Empty);
            DataTable dt = da.Search_By_Key(key, "History");
            return dt;
        }

        public string Return_BookSelf_Name(int id, string column)
        {
            string bsn = da.Fetch_Name_By_ID(id, "Book_Self", column);
            return bsn;

        }

        public string Insert_Book(string name, string author, string publisher, int bsid, int tq, int aq)
        {
            string s = string.Empty;
            

            int quantity = 0;
            int available = 0;
            int flag = 0;

            if (tq == aq && tq > 0)
            {
                available = 1;
                quantity = tq;

                flag = da.Insert_Into_Book(name, author, publisher, bsid, quantity, available);

                if (flag == 1)
                {
                    s = "Record successfully inserted";
                }

                else
                {
                    s = "Failed";
                }
            }

            return s;
            
        }

        public string Update_Book(int id,string name, string author, string publisher, int bsid, int tq, int aq)
        {
            string s = string.Empty;


            int quantity = 0;
            int available = 0;
            int flag = 0;

            if (tq == aq && tq > 0)
            {
                available = 1;
                quantity = tq;

                flag = da.Update_Book_Info(id, name, author, publisher, bsid, quantity, available);

                if (flag == 1)
                {
                    s = "Record successfully updated";
                }

                else
                {
                    s = "Failed";
                }
            }

            return s;

        }

        public string Insert_Book_Self(string name, string category,string numb)
        {
            string s = string.Empty;

            
            int flag = da.Insert_Into_Book_Self(name, category, numb);

            if (flag == 1)
            {
                s = "Record successfully inserted";
            }

            else
            {
                s = "Failed";
            }

            return s;
        }

        public string Update_Book_Self_Info(int id,string name, string category, string numb)
        {
            string s = string.Empty;


            int flag = da.Update_Book_Self(id,name, category, numb);

            if (flag == 1)
            {
                s = "Record successfully Updated";
            }

            else
            {
                s = "Failed";
            }

            return s;
        }

        public string Delete_Book(int id)
        {
            int flag = 0;
            string s = string.Empty;

            flag = da.Delete_By_ID(id,"BOOK");

            if (flag == 1)
            {
                s = "Record successfully deleted";
            }

            else
            {
                s = "Failed";
            }

            return s;



        }

        public string Insert_Member(string NAME, string USERNAME, string PASSWORD, string PHONE, string EMAIL, string TYPE)
        {
            string s = string.Empty;


            
            int flag = 0;

            

                flag = da.Insert_Into_Member(NAME,USERNAME,PASSWORD,PHONE,EMAIL,TYPE);

                if (flag == 1)
                {
                    s = "Record successfully inserted";
                }

                else
                {
                    s = "Failed";
                }
            

            return s;

        }

        public string Update_Member(int ID, string NAME, string USERNAME, string PASSWORD, string PHONE, string EMAIL, string TYPE)
        {
            string s = string.Empty;

            int flag = 0;

            flag = da.Update_Member_Info(ID, NAME, USERNAME, PASSWORD, PHONE, EMAIL, TYPE);

            if (flag == 1)
            {
                s = "Record successfully updated";
            }

            else
            {
                s = "Failed";
            }

            return s;

        }

        public string Delete_Member(int id)
        {
            int flag = 0;
            string s = string.Empty;

            flag = da.Delete_By_ID(id, "MEMBER");

            if (flag == 1)
            {
                s = "Record successfully deleted";
            }

            else
            {
                s = "Failed";
            }

            return s;
        }

        public string Delete_BS(int id)
        {
            int flag = 0;
            string s = string.Empty;

            flag = da.Delete_By_ID(id, "BOOK_SELF");

            if (flag == 1)
            {
                s = "Record successfully deleted";
            }

            else
            {
                s = "Failed";
            }

            return s;
        }

        public int Issue_Book(int mid,int bid)
        {
            return 1;
        }

        public int Check_Book_Mode(string id)
        {
            string rtn = string.Empty;
            rtn = da.Fetch_Name_By_ID(Convert.ToInt16(id),"BOOK","AVAILABLE");
            return Convert.ToInt16(rtn);
        }

        public int Count_Member_Overdue(string id)
        {
            string rtn = string.Empty;
            rtn = da.Return_History_Status((Convert.ToInt16(id)), "OVERDUE");
            return Convert.ToInt16(rtn);
        }

        public int Count_Member_Issue_Status(string id,string bid)
        {
            string rtn = string.Empty;
            rtn = da.Fetch_History_Status((Convert.ToInt16(id)), (Convert.ToInt16(bid)),"ISSUED");
            return Convert.ToInt16(rtn);
        }

        public int Count_Member_Issue(string id)
        {
            string rtn = string.Empty;
            rtn = da.Return_History_Status((Convert.ToInt16(id)), "ISSUED");
            return Convert.ToInt16(rtn);
        }

        public string Operation_Issue_Book(string tmid, string tbid,int da)
        {
            string s = string.Empty;
            int flag = dd.Operation_Issue_History((Convert.ToInt16(tmid)), (Convert.ToInt16(tbid)), da);
            dd.Cal_Call(Convert.ToInt16(tbid), 1);
            if (flag == 1)
            {
                s = "Record successfully inserted";
            }

            else
            {
                s = "Failed";
            }
            return s;
        }

        public string Operation_Return_Book(string tmid, string tbid)
        {
            string s = string.Empty;
            int flag = dd.Operation_Return_History((Convert.ToInt16(tmid)), (Convert.ToInt16(tbid)));
            dd.Cal_Call(Convert.ToInt16(tbid), -1);
            if (flag == 1)
            {
                s = "Record successfully updated";
            }

            else
            {
                s = "Failed";
            }
            return s;
        }

        public DataTable Fhistory(string name,string pass)
        {
            DataTable dt = da.Fetch_User_History(name,pass);
            return dt;
        }
    }

    internal static class Log
    {
        public static int num;
        public static int tab;
        public static string userl;
        public static string passl;
    }


}
