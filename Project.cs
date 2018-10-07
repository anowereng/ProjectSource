using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace TaskEntry.Models
{
    public class Project
    {
        //public readonly IConfiguration Configuration;


        public int ProjId { get; set; }
        public string ProjName { get; set; }
        public string Company { get; set; }

        private void prcSetData(IDataRecord reader)
        {
            ProjId = Convert.ToInt32(reader["ProjId"].ToString());
            ProjName = reader["ProjName"].ToString();
            Company = reader["Company"].ToString();
        }


        public static List<Project> prcGetData()
        {
            IDataReader reader = null;
            List<Project> list = new List<Project>();

            //SoftifySQLConnection clsCon = new SoftifySQLConnection("SoftifyFleetManagement", true);
           
        //CoreSQLConnection clsCon = new CoreSQLConnection();

            //string constr = clsCon.GetConnectionString("MyConString");
            try
            {
                string sqlQuery = "SELECT * FROM tblProject";
                //reader = clsCon.CoreReader(sqlQuery);
                while (reader.Read())
                {
                    Project Item = new Project();
                    Item.prcSetData(reader);
                    list.Add(Item);
                }
                return list;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                //clsCon = null;
            }
        }

    }

}
