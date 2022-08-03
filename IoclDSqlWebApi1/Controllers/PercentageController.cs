using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;


namespace IoclDSqlWebApi1.Controllers
{
    public class PercentageController : ApiController
    {
        // GET: Percentage
       

        public string InntegrateDbConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection1"].ConnectionString;
        [HttpGet]
        [Route("api/PercentageController/GetPercentageDetails")]

        public Dictionary<string,decimal> GetPercentageDetails()
        {
            List<string> list = new List<string>();
            Dictionary<string, decimal> values = new Dictionary<string, decimal>();
            try
            {
                using (SqlConnection con = new SqlConnection(InntegrateDbConnectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "mss_percent";

                    con.Open();
                    DataSet ds = new DataSet();
                    DataTable countdt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    da.Dispose();
                    cmd.Dispose();
                    con.Close();
                  
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        values.Add(ds.Tables[0].Rows[i]["device"].ToString()  ,  Convert.ToDecimal (ds.Tables[0].Rows[i]["value"]));
                    }

                    return values;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}