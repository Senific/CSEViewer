using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CSEViewer
{
    public class CSEHelper
    {
        public static List<CompanyModel> GetCompanies()
        {
            List<CompanyModel> companies = new List<CompanyModel>();

            string json = null; 
            using (WebClient wc = new WebClient())
            {
                json = wc.DownloadString("https://www.cse.lk/api/allSecurityCode");
            }

            companies = JsonConvert.DeserializeObject<List<CompanyModel>>(json);

             

            return companies;
        }

        public static List<Sector> GetSectors()
        {
            var client = new RestClient("https://www.cse.lk/api/sector_list");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            return JsonConvert.DeserializeObject<IndiciesListModel>(response.Content).indicesList.ToList();
        }

        public static List<ReqIndustryBySector> GetCompanieBySector(int sectorId , double investment)
        {
            var client = new RestClient("https://www.cse.lk/api/listBySector");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AlwaysMultipartFormData = true;
            request.AddParameter("sectorId", sectorId.ToString()); 
            IRestResponse response = client.Execute(request);
            var model = JsonConvert.DeserializeObject<CompanyBySectorRequestModel>(response.Content).reqIndustryBySectors.ToList();

            //Fill External Data
            foreach(var m in model)
            {
                m.Investment = investment;
                var e = ExternalDataHelper.data.Companies.First(x => x.Symbol == m.symbol); 
                m.Dividence = e.Dividence;
                m.NAVPS = e.NAVPS;
                m.EPS = e.EPS;
                m.CurrentRatio = e.CurrentRatio;
            }

            return model;
        }
    }
}
