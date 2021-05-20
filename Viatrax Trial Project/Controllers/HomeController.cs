using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Viatrax_Trial_Project.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult deviceListingDetails()
        {
  
            //Object
            var myRequestData = new
            {
                commandstring = "get_devices",
                token = "d1b95a4c22f546faa851a8961e0d20f9",
            };

            string responseFromServer = "";
            //Tranform it to Json object
            string jsonData = JsonConvert.SerializeObject(myRequestData);

            //Create a web Request to a given Url
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://gps.trak-4.com/api/v2/");
            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(jsonData);
                streamWriter.Flush();
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            if (httpResponse.StatusDescription == "OK")
            {
                // Open a Stream to Get Resposne Stream.
                using (Stream dataStream = httpResponse.GetResponseStream())
                {
                    // Open the stream using a StreamReader for easy access.
                    StreamReader reader = new StreamReader(dataStream);
                    // Read the content.
                    responseFromServer = reader.ReadToEnd();

                    //Deserialize Object Data into DeviceListingViewModel
                    DeviceListingViewModel tmp = JsonConvert.DeserializeObject<DeviceListingViewModel>(responseFromServer);
                    ViewData["DeviceList"] = tmp;
                   
                    return View("Index");

                }
            }
            else
            {
                return Json(httpResponse.StatusDescription , JsonRequestBehavior.AllowGet);
            }
           // return View();
        }

        public ActionResult DeviceDetails(string KeyCode , string IMEI , float Voltage)
        {


            //Send Request Object to get Report
            var myRequestData = new {
                commandstring = "get_reports_single_device",
                identifier = IMEI,
                datetime_start = "02/20/2016 00:00:00",
                datetime_end = "02/22/2021 00:00:00",
                coredataonly = true,
                token = "d1b95a4c22f546faa851a8961e0d20f9"
            };

            string responseFromServer = "";
            //Tranform it to Json object
            string jsonData = JsonConvert.SerializeObject(myRequestData);

            //Create a web Request to a given Url
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://gps.trak-4.com/api/v2/");
            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(jsonData);
                streamWriter.Flush();
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            if (httpResponse.StatusDescription == "OK")
            {
                // Open a Stream to Get Resposne Stream.
                using (Stream dataStream = httpResponse.GetResponseStream())
                {
                    // Open the stream using a StreamReader for easy access.
                    StreamReader reader = new StreamReader(dataStream);
                    // Read the content.
                    responseFromServer = reader.ReadToEnd();

                    //Deserialize Object Data into DeviceListingViewModel
                    DeviceListingViewModel tmp = JsonConvert.DeserializeObject<DeviceListingViewModel>(responseFromServer);
                    ViewData["DeviceList"] = tmp;

                    return View("Index");

                }
            }
            else
            {
                return Json(httpResponse.StatusDescription, JsonRequestBehavior.AllowGet);
            }

            return View("Index");
        }

    }

    public class DeviceListingViewModel
    {
        public string CommandString { get; set; }
        public string timestamp { get; set; }
        public string  rate_limit_stats { get; set; }
        public List<DataViewModel> data { get; set; }
      

    }

    public class DataViewModel
    {
        public int deviceId { get; set; }
        public int OrgID { get; set; }
        public int imsi { get; set; }
        public string iccid { get; set; }
        public string imei { get; set; }
        public string serviceProvider { get; set; }
        public string lastIP { get; set; }
        public string lastReportTime { get; set; }
        public bool lastReportGPSIsCellTower { get; set; }
        public float LastReportLatitude { get; set; }
        public float LastReportLongitude { get; set; }
        public string LastReportUpdateTime { get; set; }
        public float LastReportVoltage { get; set; }
        public string customerLabel { get; set; }
        public  string consumerLabel { get; set; }
        public string consumerKey { get; set; }
        public string customerDesc { get; set; }
        public string consumerDesc { get; set; }
        public string  firmwareVersion { get; set; }
        public float serviceHours { get; set; }
        public string serviceHours_Date { get; set; }
        public float serviceMileage { get; set; }
        public string serviceMileage_Date { get; set; }
        public float serviceHours_Start { get; set; }
        public float serviceMileage_Start { get; set; }
        public int  serviceMileage_Mode { get; set; }
        public string Image_URL { get; set; }

    }


    public class ReportsViewModel
    {
        public int reportId { get; set; }
        public int deviceId { get; set; }
        public string iccid { get; set; }
        public string imei { get; set; }
        public string receivedDate  { get; set; }
        public string updateTime { get; set; }
        public string longitude { get; set; }
        public bool latitude { get; set; }
        public float voltage1 { get; set; }
        public float temp1 { get; set; }
        public bool GPS_isCellTower { get; set; }
        public int GPS_speed { get; set; }
        public int GPS_heading { get; set; }

    }


}