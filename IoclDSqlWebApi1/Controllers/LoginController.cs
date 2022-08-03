using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IoclDSqlWebApi1.Controllers
{
}


public class LoginController : ApiController
{
    public string InntegrateDbConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
    [HttpPost]
    [Route("api/LoginController/GetLoginDetails/{username}/{password}")]



    
    public int GetLoginDetails(string username,string password)
    {
        var path = ConfigurationManager.AppSettings["urlPath"];

        try
        {
            using (SqlConnection con = new SqlConnection(InntegrateDbConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "x_Login";
                cmd.Parameters.AddWithValue("@UserName", username);
                cmd.Parameters.AddWithValue("@Password", password);

                con.Open();
                DataTable countdt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(countdt);
                da.Dispose();
                cmd.Dispose();
                con.Close();

               

                return int.Parse(countdt.Rows[0][0].ToString());


                }
            }
        catch (Exception)
        {
            return 0;
        }
    }


}
