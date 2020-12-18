using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


/// <summary>
/// Used this class for Sql database provider
/// </summary>
public class SqlDataProvider
{
    /// <summary>
    /// SQL server connection string
    /// </summary>
    static string strConStr = AutoCareV2._0.Class.datatabase.connect;
    /// <summary>
    /// Global SQL server connection
    /// </summary>
    public static SqlConnection connection;
    public SqlDataProvider() {
        if (connection == null) { connection = new SqlConnection(strConStr); }
        //if (connection.State != ConnectionState.Open) { connection.Open(); }
    }
    public static SqlConnection GetConnection()
    {
        if (connection.State == ConnectionState.Closed)
        {
            //connection.Close();
            connection.Open();
            return connection;
        }
        else
            return connection;
    }
    #region DB Access Functions
    public DataTable GetData(string sql) 
    {
        return GetData(GetCommand(sql));
    }

    public DataTable GetData(SqlCommand cmd)
    {
        try
        {
            if (cmd.Connection == null) { cmd.Connection = GetConnection(); }
            using (DataSet ds = new DataSet())
            {
                using (SqlDataAdapter da = new SqlDataAdapter())
                {
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    return ds.Tables[0];
                }
            }
        }
        finally
        {

        }
    }

    public void ExecuteNonQuery(string sql) 
    {
        ExecuteNonQuery(GetCommand(sql));
    }

    public void ExecuteNonQuery(SqlCommand cmd)
    {
        try
        {
            if (cmd.Connection == null) { cmd.Connection = GetConnection(); }
            cmd.ExecuteNonQuery();
        }
        finally
        {

        }
    }

    public object ExecuteScalar(string sql) 
    {
        return ExecuteScalar(GetCommand(sql));
    }

    public object ExecuteScalar(SqlCommand cmd)
    {
        try
        {
            if (cmd.Connection == null) { cmd.Connection = GetConnection(); }
            return cmd.ExecuteScalar();
        }
        finally
        {
                
        }
    }

    private SqlCommand GetCommand(string sql) 
    {
        SqlCommand cmd = new SqlCommand(sql, GetConnection());
        return cmd;
    }

    public string MaxId(string Table, string ColId)
    {
        string strReturn = "";
        strReturn = ExecuteScalar("SELECT max(" + ColId + ") as maxid FROM " + Table).ToString();
        return strReturn;
    }
    public int DBSize()
    {
        using (SqlCommand cmd = new SqlCommand("select sum(size) * 8 * 1024 from sysfiles"))
        {
            cmd.CommandType = CommandType.Text;
            return (int)ExecuteScalar(cmd);
        }
    }
    #endregion
}
