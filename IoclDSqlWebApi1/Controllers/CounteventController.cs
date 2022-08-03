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

public class GetDeviceIdDetailsController : ApiController
{
    public string InntegrateDbConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
    [HttpGet]
    [Route("api/GetDeviceIdDetailsController/GetDeviceid/{Date}")]
    [Route("api/GetDeviceIdDetailsController/GetDeviceiddetails/{todayDate}")]

    public Array GetDeviceid(string Date)
    {
        var path = ConfigurationManager.AppSettings["urlPath"];


        var data = Date.Split(',');
        try
        {
            using (SqlConnection con = new SqlConnection(InntegrateDbConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "mss_GetEventData1";
                cmd.Parameters.AddWithValue("@_OnDate", data[0]);
                cmd.Parameters.AddWithValue("@_Device_Id", data[1]);
                cmd.Parameters.AddWithValue("@_Event_code", data[2]);

                con.Open();
                DataTable countdt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(countdt);
                da.Dispose();
                cmd.Dispose();
                con.Close();


                var result = countdt.AsEnumerable().Select(item => new
                {
                    DeviceName = item["_Device_Name"],
                    DeviceId = item["_Device_Id"],

                    Details = item["_Event_code"],
                    Bottles = item["_Count"],
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

    public Array GetDeviceiddetails(string todayDate)
    {
        var path = ConfigurationManager.AppSettings["urlPath"];


        var data = todayDate.Split(',');
        try
        {
            using (SqlConnection con = new SqlConnection(InntegrateDbConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "mss_GetEventData";
                cmd.Parameters.AddWithValue("@_OnDate", data[0]);

                con.Open();
                DataTable countdt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(countdt);
                da.Dispose();
                cmd.Dispose();
                con.Close();


                var result = countdt.AsEnumerable().Select(item => new
                {
                    DeviceId = item["_Device_Id"],

                    Details = item["_Event_code"],
                    Bottles = item["_Count"],
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

}

