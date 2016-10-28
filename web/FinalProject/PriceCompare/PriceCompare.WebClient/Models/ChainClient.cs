using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Mvc;

namespace PriceCompare.WebClient.Models
{
    public class ChainClient
    {
        private string BASE_URL = "http://localhost:52120/api/";

        public IEnumerable<Chain> findAll()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(BASE_URL);
                client.DefaultRequestHeaders.Accept.Add(
                         new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("chains").Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<IEnumerable<Chain>>().Result;
                }
                return null;

            }
            catch
            {
                return null;
            }

        }
    }
}