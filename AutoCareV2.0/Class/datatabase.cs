using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace AutoCareV2._0.Class
{
    class datatabase
    {
        //public static string connect = @"Data Source=NGUYENDUCANH;Initial Catalog=Autocare_Sercurity;Persist Security Info=True;User ID=admin;Password=12";
       // public static string connect = @"Data Source=RATLAHAMCHOI\RATLAHAMCHOI;Initial Catalog=Autocare_Sercurity;Persist Security Info=True;User ID=sa;Password=123456;MultipleActiveResultSets=True";
        public static string connect = @"Data Source=125.212.201.52;Initial Catalog=Autocare_Sercurity;Persist Security Info=True;User ID=autocare;Password=autocare@2020;MultipleActiveResultSets=True";
        //public static string connect = @"Data Source=125.212.201.52;Initial Catalog=Autocare_Sercurity;Persist Security Info=True;Connection Timeout = 0;User ID=autocare;Password=autocare@2020;MultipleActiveResultSets=True";

        public static SqlConnection getConnection()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connect);
                return conn;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Kết Nối Thất bại. Vui lòng kiểm tra lại đường truyền.", "Lỗi" + ex.Message);
                return null;
            }
        }

        public static DataTable getData(SqlCommand command)
        {
            using(SqlConnection con = getConnection())
            {
                DataTable dt = new DataTable();
                SqlCommand cmd = command;
                cmd.Connection = con;
                cmd.CommandTimeout = 0;
                if (con.State == ConnectionState.Closed)
                    con.Open();
                try
                {
                    SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
                    Adapter.Fill(dt);
                    con.Close();
                    con.Dispose();
                }
                catch(Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Lỗi " + ex.Message + command.CommandText);
                    con.Close();
                    con.Dispose();
                }
                return dt;
            }

        }

        public static DataSet GetDataSet(SqlCommand command)
        {
            using (SqlConnection con = getConnection())
            {
                SqlCommand cmd = command;
                cmd.Connection = con;

                if (con.State == ConnectionState.Closed)
                    con.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                if (con.State == ConnectionState.Open)
                    con.Close();

                return ds;
            }
        }

        public static int ExcuteNonQuery(SqlCommand command)
        {

            SqlCommand cmd = command;
            cmd.Connection = getConnection();
            if (cmd.Connection.State == 0)
                cmd.Connection.Open();
            int i = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            cmd.Connection.Dispose();
            return i;
        }

        public static string ExecuteScalar(SqlCommand cmdCommand)
        {
            SqlCommand cmd = cmdCommand;
            cmd.Connection = getConnection();
            if (cmd.Connection.State == 0)
                cmd.Connection.Open();
            string i = cmd.ExecuteScalar().ToString();
            cmd.Connection.Close();
            cmd.Connection.Dispose();
            return i;
        }

        public static int ExcuteNonQuery(List<string> sql)
        {
            int index = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = getConnection();
            cmd.Connection.Open();
            SqlTransaction tran = cmd.Connection.BeginTransaction();
            cmd.Transaction = tran;
            try
            {
                foreach (string cmd1 in sql)
                {

                    cmd.CommandText = cmd1;

                    if (cmd.Connection.State == 0)
                        cmd.Connection.Open();
                    index += cmd.ExecuteNonQuery();

                }
                tran.Commit();
            }
            catch
            {
                if (cmd.Connection.State == 0)
                    cmd.Connection.Open();
                tran.Rollback();
                cmd.Connection.Close();
                index = 0;
            }
            return index;
        }
    }
}
