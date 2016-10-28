using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PriceCompare.Model.App
{
    public class Download
    {

        //  private string _address = "http://economy.gov.il/Trade/ConsumerProtection/Pages/PriceTransparencyRegulations.aspx";
      //  private string _main_address = "http://www.ybitan.co.il/pirce_update";
        private string _address = "http://www.ybitan.co.il/upload/Price7290725900003_081_201608301605.zip";
        string _path = @"C:\GiT\dotNetCourse\FinalProject\bin\my_file.zip";


        public async void Run()
        {
            HttpClient client = new HttpClient();
            await client.GetAsync(_address).ContinueWith(
              async (requestTask) =>
              {
                  HttpResponseMessage response = requestTask.Result;
                  HttpContent content = response.Content;

                  var NewResponse = client.PostAsync(_address, content);


                  using (var file = System.IO.File.Create(_path))
                  {
                      var contentStream = await content.ReadAsStreamAsync();
                      await contentStream.CopyToAsync(file);
                      await file.FlushAsync();
                  }
              });

            Console.WriteLine("done http request!");
        }


     

    }
}
