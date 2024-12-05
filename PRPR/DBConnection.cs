using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRPR
{
    internal class DBConnection
    {
        SqlConnection conn = new SqlConnection(Properties.Settings.Default.Conn);
        DataSet ds;
        SqlDataAdapter da;

        public DBConnection() {}

        //Thực thi trả về DataTable
        public DataTable Excute(string sqlStr)
        {
            da = new SqlDataAdapter(sqlStr, conn);
            ds = new DataSet();
            da.Fill(ds);
            return ds.Tables[0];
        }

        //Thực thi trả về SqlCommand
        public SqlCommand Excute2(string sqlStr)
        {
            SqlCommand command = null;
            try
            {
                conn.Open();
                command = new SqlCommand(sqlStr, conn);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            conn.Close();
            return command;
        }


        //update, insert, delete
        public void ExcuteNonQuery(string strSQL)
        {
            SqlCommand sqlcmd = new SqlCommand(strSQL, conn);
            conn.Open();
            sqlcmd.ExecuteNonQuery();
            conn.Close();
        }

        public int ExecuteScalar(string strSQL)
        {
            int result = 0;
            conn.Open();
            SqlCommand sqlcmd = new SqlCommand(strSQL, conn);
            result = Convert.ToInt32(sqlcmd.ExecuteScalar());
            conn.Close();
            return result;
        }
        public SqlConnection GetSqlConnection()
        {
            return conn;
        }

        public void ExcuteNonQueryCmd(SqlCommand cmd)
        {
            try
            {
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    cmd.Connection.Open();
                }
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thực thi câu lệnh SQL: " + ex.Message);
            }
            finally
            {
                if (cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
        }

    }
}
