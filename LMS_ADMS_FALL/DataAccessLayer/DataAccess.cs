using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Windows.Forms;

namespace LMS_ADMS_FALL.DataAccessLayer
{
    internal class DataAccess
    {
        OracleConnection con = new OracleConnection(@"DATA SOURCE=DESKTOP-C8F0I5B:1521/XE;PERSIST SECURITY INFO=True;USER ID=SYSTEM;PASSWORD=636213;");
        

        public DataTable Get_Table(string table)
        {
            DataTable dt = new DataTable();
            

            try
            {
                
                string qry = string.Format("SELECT * FROM {0}", table);
                OracleCommand cmd = new OracleCommand(qry, con);
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                da.Fill(dt);
            }

            catch (Exception X)
            {
                MessageBox.Show(X.Message);
            }

            return dt;
        }
        

        public int Validate_User(string username, string password)
        {
            int rtn = 0;
            string type = "";

            try
            {
                con.Open();
                type = string.Format("SELECT Validate_User('{0}','{1}') FROM DUAL",username,password);
                OracleCommand cmd = new OracleCommand(type, con);
                rtn = Convert.ToInt16(cmd.ExecuteScalar());
                con.Close();
            }

            catch (Exception X)
            {
                MessageBox.Show(X.Message);
            }
            return rtn;
        }

        public int Return_User_Type(string username, string password)
        {
            int rtn = 0;
            string type = "";

            try
            {
                con.Open();
                type = string.Format("SELECT Return_User_Type('{0}','{1}') FROM DUAL", username, password);
                OracleCommand cmd = new OracleCommand(type, con);
                rtn = Convert.ToInt16(cmd.ExecuteScalar());
            }

            catch(Exception X)
            {
                MessageBox.Show(X.Message);
            }
            return rtn;
        }

        public string Return_Database_Date()
        {
            string rtn = string.Empty;
           
            string type = string.Empty;

            try
            {
                con.Open();
                type = string.Format("SELECT Return_System_Date() FROM DUAL");
                OracleCommand cmd = new OracleCommand(type, con);
                rtn = Convert.ToString(cmd.ExecuteScalar());

                con.Close();
            }

            catch (Exception X)
            {
                MessageBox.Show(X.Message);
            }
            return rtn;
        }

        public string Return_Due_Date(int days)
        {
            string rtn = string.Empty;

            string type = string.Empty;

            try
            {
                con.Open();
                type = string.Format("SELECT Return_Due_Date({0}) FROM DUAL", days);
                OracleCommand cmd = new OracleCommand(type, con);
                rtn = Convert.ToString(cmd.ExecuteScalar());

                con.Close();
            }

            catch (Exception X)
            {
                MessageBox.Show(X.Message);
            }
            return rtn;
        }

        public DataTable Search_By_Key(string key, string table)
        {
            DataTable dt = new DataTable();
            con.Close();

            try
            {
                con.Open();

                int parse;

                bool stat = int.TryParse(key, out parse);

                if (stat == true)
                {
                    string qr = string.Format("SELECT * FROM {0} WHERE {0}.id = {1}", table, int.Parse(key));
                    OracleCommand cmd = new OracleCommand(qr, con);
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    da.Fill(dt);
                }

                else
                {
                    string qr = string.Format("select * from {0} where {0}.name like '" + key + "%' or {0}.name like '%" + key + "%' or {0}.name like '%" + key + "'", table);
                    OracleCommand cmd = new OracleCommand(qr, con);
                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    da.Fill(dt);
                }


                con.Close();

            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return dt;
        }

        public string Fetch_Name_By_ID(int id,string table,string column)
        {
            string s = string.Empty;

            try
            {
                con.Open();
                string qr = string.Format("SELECT {0}.{1} FROM {0} WHERE {0}.id = {2}", table, column, id);
                OracleCommand cmd = new OracleCommand(qr, con);
                s = cmd.ExecuteScalar().ToString();
                con.Close();
                
            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return s;
        }

        public DataTable Fetch_User_History(string mname,string pass)
        {
            string s = string.Empty;
            int mid = 0;
            DataTable dt = new DataTable();
            try
            {
                con.Open();

                string q = string.Format("SELECT member.id FROM member WHERE member.username = '{0}' and member.password = '{1}'", mname,pass);

                OracleCommand cmd1 = new OracleCommand(q, con);
                mid = Convert.ToInt16(cmd1.ExecuteScalar().ToString());

                con.Close();
                con.Open();
                

                string qr = string.Format("SELECT * FROM History WHERE History.mid = {0} and History.Status='{1}'", mid,"Issued");
                OracleCommand cmd = new OracleCommand(qr, con);
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                da.Fill(dt);

               

                con.Close();

            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return dt;
            //return s;
        }

        public string Fetch_Count_By_ID(int id, string table, string column)
        {
            string s = string.Empty;

            try
            {
                con.Open();
                string qr = string.Format("SELECT COUNT(*) FROM {1} WHERE {1}.{2} = {0}", id, table, column);
                OracleCommand cmd = new OracleCommand(qr, con);
                s = cmd.ExecuteScalar().ToString();
                con.Close();

            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return s;
        }

        public string Return_History_Status(int id,string key)
        {
            string s = string.Empty;

            try
            {
                con.Open();
                string qr = string.Format("SELECT COUNT(*) FROM HISTORY WHERE HISTORY.MID = {0} AND HISTORY.STATUS = '{1}'", id, key);
                OracleCommand cmd = new OracleCommand(qr, con);
                s = cmd.ExecuteScalar().ToString();
                con.Close();

            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return s;
        }

        public string Fetch_History_Status(int id,int bid, string key)
        {
            string s = string.Empty;

            try
            {
                con.Open();
                string qr = string.Format("SELECT COUNT(*) FROM HISTORY WHERE HISTORY.MID = {0} AND HISTORY.BID = {1} AND HISTORY.STATUS = '{2}'", id, bid, key);
                OracleCommand cmd = new OracleCommand(qr, con);
                s = cmd.ExecuteScalar().ToString();
                con.Close();

            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return s;
        }



        public string Fetch_Borrow_Count_By_ID(int id)
        {
            string s = string.Empty;

            try
            {
                con.Open();
                string qr = string.Format("SELECT COUNT(*) FROM HISTORY WHERE HISTORY.MID = {0} AND HISTORY.STATUS = 'ISSSED' OR HISTORY.STATUS = 'OVERDUE'", id);
                OracleCommand cmd = new OracleCommand(qr, con);
                s = cmd.ExecuteScalar().ToString();
                con.Close();

            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return s;
        }

        public int Delete_By_ID(int id,string table)
        {
            
            int i = -1;
            try
            {
                con.Open();

                string qr = string.Format("Delete FROM {0} WHERE {0}.id = {1}", table, id);
                OracleCommand cmd = new OracleCommand(qr, con);
                i = cmd.ExecuteNonQuery();
       
                con.Close();

            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return i;
        }

        public int Insert_Into_Book(string name, string author, string publisher, int bsid, int quantity, int available)
        {
            con.Close();

            int rtn = -1;

            try
            {
                int iq = 0;
                con.Open();

                string qr = string.Format("Insert into SYSTEM.BOOK(NAME,AUTHOR,PUBLISHER,BOOK_SELF_ID,TOTAL_QUANTITY,AVAILABLE_QUANTITY,AVAILABLE,ISSUED_QUANTITY) VALUES ('{0}','{1}','{2}',{3},{4},{4},{5},{6})", name, author, publisher, bsid, quantity,available,iq);
                OracleCommand cmd = new OracleCommand(qr, con);
                rtn = cmd.ExecuteNonQuery();
                con.Close();

            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return rtn;
        }
        public int Insert_Into_Book_Self(string name, string category, string numb)
        {
            con.Close();
            int i = 0;
            try
            {
                con.Open();
                string qr = string.Format("Insert into SYSTEM.BOOK_SELF(NAME, CATEGORY, NUMB) values('{0}', '{1}', '{2}')", name, category, numb);
                OracleCommand cmd = new OracleCommand(qr, con);
                i = cmd.ExecuteNonQuery();
            }

            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }

            return i;
        }
        public int Insert_Into_Member(string NAME, string USERNAME, string PASSWORD, string PHONE, string EMAIL, string TYPE)
        {
            con.Close();
            int rtn = -1;
            try
            {
                con.Open();

                string qr = string.Format("Insert into SYSTEM.MEMBER(NAME,USERNAME,PASSWORD,PHONE,EMAIL,TYPE) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}')", NAME, USERNAME, PASSWORD, PHONE, EMAIL, TYPE);
                OracleCommand cmd = new OracleCommand(qr, con);
                rtn = cmd.ExecuteNonQuery();
                con.Close();

            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return rtn;
        }

        

        public int Update_Member_Info(int ID, string NAME, string USERNAME, string PASSWORD, string PHONE, string EMAIL, string TYPE)
        {
            con.Close();
            int rtn = -1;
            try
            {
                con.Open();

                string qr = string.Format("Update SYSTEM.MEMBER SET NAME='{1}', USERNAME='{2}',PASSWORD='{3}',PHONE='{4}',EMAIL='{5}',TYPE='{6}' WHERE MEMBER.ID = {0}", ID, NAME, USERNAME, PASSWORD, PHONE, EMAIL, TYPE);
                OracleCommand cmd = new OracleCommand(qr, con);
                rtn = cmd.ExecuteNonQuery();

                con.Close();

            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return rtn;
        }
        public int Update_Book_Info(int id, string name, string author, string publisher, int bsid, int quantity, int available)
        {
            con.Close();
            int rtn = -1;
            try
            {
                con.Open();

                string qr = string.Format("Update SYSTEM.BOOK SET NAME='{1}', AUTHOR='{2}',PUBLISHER='{3}',BOOK_SELF_ID={4},TOTAL_QUANTITY={5},AVAILABLE_QUANTITY={5},AVAILABLE={6} WHERE BOOK.ID = {0}", id, name, author, publisher, bsid, quantity, available);
                OracleCommand cmd = new OracleCommand(qr, con);
                rtn = cmd.ExecuteNonQuery();

                con.Close();

            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return rtn;
        }
        public int Update_Book_Self(int id, string name, string category, string numb)
        {
            con.Close();
            int i = 0;
            try
            {
                con.Open();
                string qr = string.Format("Update SYSTEM.BOOK_SELF SET NAME = '{1}', CATEGORY='{2}', NUMB='{3}' WHERE BOOK_SELF.ID={0}", id, name, category, numb);
                OracleCommand cmd = new OracleCommand(qr, con);
                i = cmd.ExecuteNonQuery();
            }

            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }

            return i;
        }

        public int Book_Quantity_Manage(int id,int q)
        {
            int rtn = -1;

            string type = string.Empty;

            try
            {
                con.Open();
                type = string.Format("SELECT BOOK_QUANTITY_ISSUE({0},{1}) FROM DUAL",id,q);
                OracleCommand cmd = new OracleCommand(type, con);
                rtn = Convert.ToInt16(cmd.ExecuteScalar());

                con.Close();
            }

            catch (Exception X)
            {
                MessageBox.Show(X.Message);
            }
            return rtn;
        }

        public void Cal_Call(int id, int qt )
        {
            con.Close();
            try
            {
                con.Open();

                OracleCommand cmd = new OracleCommand("Cal_BOOK_QTY", con);
                cmd.BindByName = true;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("bid", OracleDbType.Int32, id, ParameterDirection.Input);
                cmd.Parameters.Add("qty", OracleDbType.Int32, qt, ParameterDirection.Input);
                

                cmd.ExecuteNonQuery();

                con.Close();
            }

            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public int Operation_Issue_History(int mid,int bid,int da)
        {
            
            string rd = Return_Database_Date();
            string dd = Return_Due_Date(da);
            
            int rtn = -1;
            try
            {
                if(con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                


                string qr = string.Format("Insert into SYSTEM.HISTORY(MID,BID,ISSUE_DATE,DUE_DATE,RETURN_DATE,STATUS) VALUES ( {0} , {1} ,'{2}','{3}','{4}','{5}')", mid, bid,rd,dd, "EMPTY", "ISSUED");
                OracleCommand cmd = new OracleCommand(qr, con);
                cmd.BindByName = true;
                rtn = cmd.ExecuteNonQuery();
                
                con.Close();

            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return rtn;
        }

        public int Operation_Return_History(int mid, int bid)
        {
            //con.Close();
            string rd = Return_Database_Date();
            int rtn = -1;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                string qr = string.Format("Update SYSTEM.HISTORY SET RETURN_DATE ='{0}',STATUS ='{1}' WHERE HISTORY.MID ={2} and HISTORY.BID ={3} and HISTORY.STATUS='ISSUED' or HISTORY.STATUS='OVERDUE'", rd,"RETURNED",mid,bid);
                OracleCommand cmd = new OracleCommand(qr, con);
                cmd.BindByName = true;
                rtn = cmd.ExecuteNonQuery();
                

                con.Close();



            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return rtn;
        }



    }
}
