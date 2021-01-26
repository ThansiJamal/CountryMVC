using CountryMVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CountryMVC.Controllers
{
    public class CountryController : Controller
    {
        // GET: Country
        public ActionResult GetCallingCode()
        {
            return View();
        }
        [HttpPost]
            public ActionResult GetCallingCode(Country c )
            {
            WebClient client = new WebClient();
            string downloadString = client.DownloadString("https://restcountries.eu/rest/v2/callingcode/"+ c.callingCodes);
           dynamic response = JsonConvert.DeserializeObject<dynamic>(downloadString);
            // c.name = response.name;
            //Country countrys = JsonConvert.DeserializeObject<Country>(downloadString);
            foreach (dynamic item in response)
            {
                c.name = item.name;
                c.capital = item.capital;
                // c.callingCodes =item.callingCodes.ToString();
                c.population = item.population;
            }
            SqlConnection connect = new SqlConnection(@"Server =DESKTOP-1GLFNAF\SQLEXPRESS;Database=CountryDB;trusted_connection=true");
            connect.Open();
            SqlCommand cmd = new SqlCommand("CountrySave", connect);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name", c.name);
            cmd.Parameters.AddWithValue("@capital", c.capital);
            cmd.Parameters.AddWithValue("@callingCodes", c.callingCodes);
            cmd.Parameters.AddWithValue("@Population", c.population);
            cmd.ExecuteNonQuery();
            connect.Close();
            return View("CallingCodeDetails", c);
            }
        
    }
}