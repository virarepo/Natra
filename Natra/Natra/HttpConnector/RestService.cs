using Natra.Helpers;
using Natra.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Natra.HttpConnector
{
    public class RestService
    {


        public async Task<List<Stok>> getAllStoks()
        {
            try
            {

                //string js = "{\"OlcuBirimi1\":\"kilo\",\"OlcuBirimi2\":\"kasa\",\"StokAciklamasi\":null,\"StokKodu\":\"alp1\",\"bakiye\":90,\"KDV\":0,\"Carpan\":10,\"SatisFiyati1\":10}";
               // string js = "{\"OlcuBirimi1\":\"kilo\",\"OlcuBirimi2\":\"kasa\",\"StokAciklamasi\":null,\"StokKodu\":\"alp1\",\"bakiye\":90,\"KDV\":0,\"Carpan\":10,\"SatisFiyati1\":10}";

                //var Mycustomclassnamedr = Newtonsoft.Json.JsonConvert.DeserializeObject<Stok>(js);
                var client = new HttpClient();
                //client.BaseAddress = new Uri("http://192.168.1.101:5000");
                //client.BaseAddress = new Uri("http://192.168.1.107:57601/api/Natra");
                client.BaseAddress = new Uri("http://192.168.1.107:57601/api/Natra/getStoks");
                //client.BaseAddress = new Uri("localhost:/api/Natra");

                client.DefaultRequestHeaders.Add("customHeader", "customHeaderData");

                string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(App.AppInstance.currentUser);

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");


                //HttpResponseMessage response = await client.GetAsync("");
                HttpResponseMessage response = await client.PostAsync(client.BaseAddress, content);

                // this result string should be something like: "{"token":"rgh2ghgdsfds"}"
                var result = await response.Content.ReadAsStringAsync();

                
                result = result.Replace("\\", "");
                result = result.Substring(1);
                result = result.Substring(0,result.Length - 1);
                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Stok>>(result);
                //List<Stok> Mycustomclassname = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Stok>>(result);
            }
            catch (Exception s)
            {
                return null;
            }

        }


        public async Task<bool> sepetOnay(List<Siparis> siparises)
        {
            try
            {

                //string js = "{\"OlcuBirimi1\":\"kilo\",\"OlcuBirimi2\":\"kasa\",\"StokAciklamasi\":null,\"StokKodu\":\"alp1\",\"bakiye\":90,\"KDV\":0,\"Carpan\":10,\"SatisFiyati1\":10}";
                // string js = "{\"OlcuBirimi1\":\"kilo\",\"OlcuBirimi2\":\"kasa\",\"StokAciklamasi\":null,\"StokKodu\":\"alp1\",\"bakiye\":90,\"KDV\":0,\"Carpan\":10,\"SatisFiyati1\":10}";

                //var Mycustomclassnamedr = Newtonsoft.Json.JsonConvert.DeserializeObject<Stok>(js);
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://192.168.1.107:57601/api/Natra/sepetOnay");

                client.DefaultRequestHeaders.Add("userData", Newtonsoft.Json.JsonConvert.SerializeObject(App.AppInstance.currentUser));

                string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(siparises);

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");


                HttpResponseMessage response = await client.PostAsync(client.BaseAddress, content);

                // this result string should be something like: "{"token":"rgh2ghgdsfds"}"
                var result = await response.Content.ReadAsStringAsync();

                result = result.Replace("\\", "");
                result = result.Substring(1);
                result = result.Substring(0, result.Length - 1);

                if (result as string == "onay") return true;
                if (result as string == "red") return false;


                
                //List<Stok> Mycustomclassname = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Stok>>(result);
            }
            catch (Exception s)
            {
                Logger.errLog("RestService.cs-sepetOnay", s);
                throw s;
            }

            return false;
        }


    }
}
