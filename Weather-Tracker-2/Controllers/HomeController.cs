using System;
using System.Web.Mvc;
using System.Net;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using WeatherForecast.Models;
using System.Configuration;

namespace Weather_Tracker_2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Weather()
        {
            ViewBag.Message = "Your Weather Page.";

            return View();
        }



        [HttpPost]
        public String WeatherFetch(string City)
        {
            SqlCommand command;
            SqlDataReader dataReader;
            SqlDataAdapter adapter = new SqlDataAdapter();
            String sql, Output = "";

            sql = "SELECT * FROM dbo.WeatherHistory WHERE ResortCity='"+City+"'";

            Connections.Init();
            command = new SqlCommand(sql, Connections.Connection);

            dataReader = command.ExecuteReader();
            dataReader.Read();

            Output = StringifyDataHistory(dataReader);
            Connections.Close();

            command.Dispose();
 
            return Output;
        }

        //Creating a JSON String because that is what is expected to be parsed inside the Weather.cshtml
        public String StringifyDataHistory(SqlDataReader dataReader)
        {
            return "{\"City\":\"" + dataReader.GetValue(0) + "\", " +
                "\"Lat\":\"" + dataReader.GetValue(6) + "\", " +
                "\"Lon\":\"" + dataReader.GetValue(7) + "\", " +
                "\"TempFeelsLike\":\"" + dataReader.GetValue(10) + "\", " +
                "\"Humidity\":\"" + dataReader.GetValue(8) + "\", " +
                "\"WeatherIcon\":\"" + dataReader.GetValue(9) + "\", " +
                "\"Description\":\"" + dataReader.GetValue(1) + "\", " +
                "\"Temp\":\"" + dataReader.GetValue(2) + "\", " +
                "\"TempMax\":\"" + dataReader.GetValue(3) + "\", " +
                "\"TempMin\":\"" + dataReader.GetValue(4) + "\", " +
                "\"Date\":\"" + dataReader.GetValue(5) + "\" " +
                "}";
        }

        [HttpPost]
        public String WeatherDetail(string City)
        {
            //Assign API KEY which received from OPENWEATHERMAP.ORG  
            string appId = "7d21012a49bcafc004eb5353cecc5c42";

            //API path with CITY parameter and other parameters.  
            string url = string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&units=metric&cnt=1&APPID={1}", City, appId);

            //Make the actual API call to fetch the data from openweathermap
            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);

                //*******************************************************************//  
                //     This is an example of what the received JSON looks like.   
                //*******************************************************************//  
                //{"coord":{ "lon":72.85,"lat":19.01},  
                //"weather":[{"id":711,"main":"Smoke","description":"smoke","icon":"50d"}],  
                //"base":"stations",  
                //"main":{"temp":31.75,"feels_like":31.51,"temp_min":31,"temp_max":32.22,"pressure":1014,"humidity":43},  
                //"visibility":2500,  
                //"wind":{"speed":4.1,"deg":140},  
                //"clouds":{"all":0},  
                //"dt":1578730750,  
                //"sys":{"type":1,"id":9052,"country":"IN","sunrise":1578707041,"sunset":1578746875},  
                //"timezone":19800,  
                //"id":1275339,  
                //"name":"Mumbai",  
                //"cod":200}  
 
                RootObject weatherInfo = (new JavaScriptSerializer()).Deserialize<RootObject>(json);

                //Special VIEWMODEL design to send only required fields not all fields which received from   
                //www.openweathermap.org api  
                ResultViewModel rslt = new ResultViewModel();

                rslt.Country = weatherInfo.sys.country;
                rslt.City = weatherInfo.name;
                rslt.Lat = Convert.ToString(weatherInfo.coord.lat);
                rslt.Lon = Convert.ToString(weatherInfo.coord.lon);
                rslt.Description = weatherInfo.weather[0].description;
                rslt.Humidity = Convert.ToString(weatherInfo.main.humidity);
                rslt.Temp = Convert.ToString(weatherInfo.main.temp);
                rslt.TempFeelsLike = Convert.ToString(weatherInfo.main.feels_like);
                rslt.TempMax = Convert.ToString(weatherInfo.main.temp_max);
                rslt.TempMin = Convert.ToString(weatherInfo.main.temp_min);
                rslt.WeatherIcon = weatherInfo.weather[0].icon;
                rslt.Date = Convert.ToString(DateTime.Now);

                //Converting OBJECT to JSON String   
                var jsonstring = new JavaScriptSerializer().Serialize(rslt);

                PersistWeatherData(rslt);
                return jsonstring;
            }
        }

        public bool PersistWeatherData(ResultViewModel rslt)
        {
            Connections.Init();

            SqlDataAdapter adapter = new SqlDataAdapter();
            string sql = "INSERT INTO dbo.WeatherHistory (ResortCity, ResortTemp, ResortTempMin, ResortTempMax, ResortDescription, ResortTime, ResortLatitude, ResortLongitude, ResortHumidity, ResortTempFeels, ResortCurrentWeather) " +
                "values ('" + rslt.City + "', " + rslt.Temp + ", " + rslt.TempMin + ", " + rslt.TempMax + ", '" + rslt.Description + "', '" + rslt.Date + "', '" + rslt.Lat + "', '" + rslt.Lon + "', '" + rslt.Humidity + "', '" + rslt.TempFeelsLike + "', '" + rslt.WeatherIcon + "')";

            SqlCommand command = new SqlCommand(sql, Connections.Connection);

            command = new SqlCommand(sql, Connections.Connection);

            adapter.InsertCommand = new SqlCommand(sql, Connections.Connection);
            adapter.InsertCommand.ExecuteNonQuery();

            command.Dispose();
            Connections.Close();

            return true;
        }
    }
}