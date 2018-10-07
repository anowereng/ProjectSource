using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using TaskEntry.Models;
using System.Collections;
using Microsoft.Extensions.Options;
using Sampan;
namespace TaskEntry.Controllers
{

    public class ProjectController : Controller
    {
        private readonly IConfiguration Configuration;
        public static string connection = "";
        public ProjectController(IConfiguration config)
        {
            this.Configuration = config;
            connection = Configuration.GetConnectionString("TaskRecord");
        }

        public SqlConnection Connection { set; get; }
        public SqlCommand Command { get; set; }
        public ActionResult Create()
        {
            //ViewData["Source"] = _serviceSettings.Value.TaskRecord;

            //string a = model.getURL();

            return View();

        }
        [HttpPost]
        public ActionResult Create(Project model)
        {
            ViewData["Message"] = prcSaveDataSampan(model);
            return View();

        }
        public IActionResult Welcome(string name, int numTimes = 1)
        {
            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = numTimes;

            return View();
        }

        [HttpGet("[action]")]
        public IActionResult Index()
        {
            List<Project> ProductList = new List<Project>();
            SqlConnection Connection = new SqlConnection(connection);
            Connection.Open();
            string Query = "SELECT * FROM tblProject";
            SqlCommand Command = new SqlCommand(Query, Connection);
            SqlDataReader Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                Project aProduct = new Project();
                aProduct.ProjId = Convert.ToInt32(Reader["ProjId"].ToString());
                aProduct.ProjName = Reader["ProjName"].ToString();
                aProduct.Company = Reader["Company"].ToString();
                ProductList.Add(aProduct);
            }
            Connection.Close();
            Reader.Close();
            return View(ProductList);
        }

        public IActionResult ProjectList()
        {

            List<Project> list = Project.prcGetData();
            return View();
        }
        public void Iconf(IConfiguration config)
        {

        }

        // GET: FleetTruck/Edit/5
        public string prcDataSave(Project model)
        {
            Connection = new SqlConnection(connection);
            var Query = "SELECT  cast(Isnull(MAX(ProjId),0) + 1 AS float)  AS ProjId FROM tblProject";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            var variable = (double)Command.ExecuteScalar();
            Connection.Close();
            try
            {
                var sqlQuery = "Insert Into tblProject (ProjId, ProjName, Company)" +
                               " Values (" + variable + ",'" + model.ProjName + "','" + model.Company + "')";
                Command = new SqlCommand(sqlQuery, Connection);
                Connection.Open();
                Command.ExecuteNonQuery();
                Connection.Close();
                return "Successfully Save.";
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {

            }
            //end : prcDataSave
        }

        public string prcSaveData(Project model)
        {
            CoreSQLConnection CoreSQL = new CoreSQLConnection();
            ArrayList arrayList = new ArrayList();
            var Query = "SELECT  cast(Isnull(MAX(ProjId),0) + 1 AS float)  AS ProjId FROM tblProject";
            var variable = CoreSQL.CoreSQL_GetDoubleData(connection, Query);
            //Connection = new SqlConnection(connection);

            try
            {
                var sqlQuery = "Insert Into tblProject (ProjId, ProjName, Company)" +
                               " Values (" + variable + ",'" + model.ProjName + "','" + model.Company + "')";

                arrayList.Add(sqlQuery);
                CoreSQL.CoreSQL_SaveDataUseSQLCommand(connection, arrayList);
                return "Successfully Save.";
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {

            }

        }

        public string prcSaveDataSampan(Project model)
        {
            CoreSQLConnection CoreSQL = new CoreSQLConnection();
            ArrayList arrayList = new ArrayList();
            var Query = "SELECT  cast(Isnull(MAX(ProjId),0) + 1 AS float)  AS ProjId FROM tblProject";
            var variable = CoreSQL.CoreSQL_GetDoubleData(connection, Query);
            try
            {
                var sqlQuery = "Insert Into tblProject (ProjId, ProjName, Company)" +
                               " Values (" + variable + ",'" + model.ProjName + "','" + model.Company + "')";

                arrayList.Add(sqlQuery);
                CoreSQL.CoreSQL_SaveDataUseSQLCommand(connection, arrayList);
                return "Successfully Save.";
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {

            }

        }

    }
}

