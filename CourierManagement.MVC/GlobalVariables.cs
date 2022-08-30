using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web;

namespace CourierManagement.MVC
{
    public class GlobalVariables
    {
        public static HttpClient client = new HttpClient();

        static GlobalVariables()
        {
            //client = new HttpClient();
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["api"]);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", BearerToken);
        }
        public static string BearerToken
        {
            get
            { 
                return ((ClaimsPrincipal)System.Web.HttpContext.Current.User).FindFirst("AcessToken").Value;
            }
        }
    }
}