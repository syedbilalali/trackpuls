using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace trackpuls.DAL
{
    public class DataAdapter
    {
        ConnectionString connectionString = new ConnectionString();
        public DataAdapter()
        {
        }
        public bool Insert(SQLiteParameter[] param, string command)
        {
            bool f = false;
            using (SQLiteConnection con = new SQLiteConnection())
            {
                using (SQLiteCommand cmd = new SQLiteCommand(command, con))
                {
                    cmd.Connection = con;
                    con.Open();
                    for (int i = 0; i < param.Length; i++)
                    {
                        cmd.Parameters.Add(param[i]);
                    }
                    int a = cmd.ExecuteNonQuery();
                    if (a > 0)
                    {
                        f = true;
                    }
                }
            }
            return f;
        }
        public bool InsertSP(SQLiteParameter[] param, string command)
        {
            bool f = false;
            using (SQLiteConnection con = new SQLiteConnection(connectionString.connect()))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(command, con))
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    for (int i = 0; i < param.Length; i++)
                    {
                        cmd.Parameters.Add(param[i]);
                    }
                    int a = cmd.ExecuteNonQuery();
                    if (a > 0)
                    {
                        f = true;
                    }
                }
            }
            return f;
        }
        public DataTable FetchAll(string command)
        {
            using (SQLiteConnection con = new SQLiteConnection(connectionString.connect()))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(command))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter())
                    {
                        cmd.Connection = con;
                        da.SelectCommand = cmd;
                        using (DataTable dt = new System.Data.DataTable())
                        {
                            da.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }
        public DataTable FetchAllSP(String command)
        {
            using (SQLiteConnection con = new SQLiteConnection(connectionString.connect()))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(command))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand = cmd;
                        using (DataTable dt = new System.Data.DataTable())
                        {
                            da.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }
        public DataTable FetchByParamSP(SQLiteParameter[] param, String command)
        {
            using (SQLiteConnection con = new SQLiteConnection(connectionString.connect()))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(command))
                {
                    using (SQLiteDataAdapter da = new SQLiteDataAdapter())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand = cmd;
                        for (int i = 0; i < param.Length; i++)
                        {
                            cmd.Parameters.Add(param[i]);
                        }
                        using (DataTable dt = new System.Data.DataTable())
                        {
                            da.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }
        public DataTable FetchByParameter(SQLiteParameter[] param, string command)
        {
            DataTable dt = new System.Data.DataTable();
            try
            {

                using (SQLiteConnection con = new SQLiteConnection(connectionString.connect()))
                {

                    using (SQLiteCommand cmd = new SQLiteCommand(command))
                    {
                        using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
                        {
                            cmd.Connection = con;
                            da.SelectCommand = cmd;

                            for (int i = 0; i < param.Length; i++)
                            {
                                cmd.Parameters.Add(param[i]);
                            }
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //Exception Caught Here 

            }
            return dt;
        }
        public int getLastID(String columnName, String tableName)
        {

            DataTable dt = new DataTable();
            int value = 0;
            string command = "SELECT MAX(" + columnName + ") AS LastID FROM " + tableName;
            dt = this.FetchAll(command);
            string gValue = dt.Rows[0]["LastID"].ToString();
            switch (gValue)
            {
                case (""):
                    value = 1;
                    break;
                default:
                    value = Int32.Parse(gValue);
                    break;
            }
            return value;
        }
    }
}
