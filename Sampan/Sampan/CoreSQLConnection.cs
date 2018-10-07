using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections;
using Microsoft.Extensions.Options;

namespace Sampan
{
    public class CoreSQLConnection
    {
        public SqlConnection Connection { set; get; }
        public SqlDataReader Reader { get; set; }
        public SqlCommand Command { get; set; }
        public SqlDataAdapter Adapter { get; set; }
        public DataTable DataTable { get; set; }
        public string Query { set; get; }
        public IDataReader CoreReader(string sql)
        {
            Command = new SqlCommand(Query, Connection);
            return Reader = Command.ExecuteReader();
        }

        // FOR GET DATA USING SQL COMMAND
        public double CoreSQL_GetDoubleData(string connstring, string Query)
        {
            Connection = new SqlConnection(connstring);
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            var variable = (double)Command.ExecuteScalar();
            Connection.Close();
            return variable;
        }

        // FOR DATA SAVE USING SQL COMMAND
        public void CoreSQL_SaveDataUseSQLCommand(string connstring, ArrayList queryList)
        {
            Connection = new SqlConnection(connstring);
            try
            {
                foreach (string query in queryList)
                {
                    Command = new SqlCommand(query, Connection);
                    Connection.Open();
                    Command.ExecuteNonQuery();
                    Connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }


    }
}
