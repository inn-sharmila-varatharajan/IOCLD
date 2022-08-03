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
    //[RoutePrefix("")]
}

public class GetCardController : ApiController
{
    public string InntegrateDbConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
    [HttpGet]
    [Route("api/GetCardController/GetDeviceid1/{Date}", Name = "Device1")]
 



    public Array GetDeviceid1(string Date)
    {
        var path = ConfigurationManager.AppSettings["urlPath"];


        try
        {
            using (SqlConnection con = new SqlConnection(InntegrateDbConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "mss_getcarddatas";
                cmd.Parameters.AddWithValue("@date", Date);

                con.Open();
                DataSet ds = new DataSet();
                DataTable countdt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                da.Dispose();
                cmd.Dispose();
                con.Close();


                var result = ds.Tables[0].AsEnumerable().Select(item => new
                {
                    DeviceName = item["Device"],

                    DeviceId = item["Deviceid"],

                    eventcode40 = item["count40"],
                    eventcode41 = item["count41"],
                    //Start_Time = item["Start Time"],
                    //End_Time = item["End Time"],
                    //Rate = item["Rate"]


                }).ToArray();



                return result;

            }
        }
        catch (Exception)
        {
            return new Array[] { };
        }
    }


    [HttpGet]
    [Route("api/GetCardController/GetAverage/{Date}/{deviceid}")]
    public Array GetAverage(string Date,int deviceid)
    {
        var path = ConfigurationManager.AppSettings["urlPath"];


        try
        {
            using (SqlConnection con = new SqlConnection(InntegrateDbConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "mss_RPH";
                cmd.Parameters.AddWithValue("@date", Date);
                cmd.Parameters.AddWithValue("@deviceid",deviceid );


                con.Open();
                DataSet ds = new DataSet();
                DataTable countdt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                da.Dispose();
                cmd.Dispose();
                con.Close();


                var result = ds.Tables[0].AsEnumerable().Select(item => new
                {
                    rph = item["rph"],

                    standard = item["standard"],

                    deviation = item["deviation"]
                    

                }).ToArray();



                return result;

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return new Array[] { };
        }
    }

    [HttpGet]
    [Route("api/GetCardController/Setvalue/{value}/{deviceid}")]
    public int Setvalue(string value, int deviceid)
    {
        var path = ConfigurationManager.AppSettings["urlPath"];


        try
        {
            using (SqlConnection con = new SqlConnection(InntegrateDbConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "mss_setrph";
                cmd.Parameters.AddWithValue("@value", value);
                cmd.Parameters.AddWithValue("@deviceid", deviceid);
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
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return 0;
        }
    }
}

