using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSEViewer
{ 
    public class CompanyData
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Profile { get { return $"https://www.cse.lk/home/company-info/{Symbol}/trade-summery"; } }
        public double Dividence { get; set; }
        public double NAVPS { get; set; }
        public double EPS { get; set; }
        public double CurrentRatio { get; set; }
    }
    public class ExternalDataModel
    {
        public List<CompanyData> Companies; 
    }

  
    public static class ExternalDataHelper
    {
        public static ExternalDataModel data;

        const string filename = "../CSEData.json"; 
        public static void Load()
        {
            try
            {
                // Read file using StreamReader. Reads file line by line    
                using (StreamReader file = new StreamReader(filename))
                {
                    string text = file.ReadToEnd();
                    data = JsonConvert.DeserializeObject<ExternalDataModel>(text);
                    file.Close();
                }
            } catch(FileNotFoundException ex )
            {
                New();
            }
        }

        public static void Save()
        {
            string json = JsonConvert.SerializeObject(data);

            using (StreamWriter file = new StreamWriter(filename))
            {
                file.WriteLine(json);
                file.Close();
            }

            if(!Directory.Exists("../Backup/"))
            {
                Directory.CreateDirectory("../Backup/");
            }

            using (StreamWriter file = new StreamWriter($"../Backup/CSEData - {DateTime.Now.ToString("MM-dd-yyyy HH-mm") }.json")) 
            {
                file.WriteLine(json);
                file.Close();
            } 
        }

        public static void New()
        {
            data = new ExternalDataModel();
            data.Companies = new List<CompanyData>(); 

            foreach(var company in CSEHelper.GetCompanies())
            {
                data.Companies.Add(new CompanyData()
                {
                     Symbol = company.symbol , Name = company.name , Dividence = 0
                }); 
            };

            string json = JsonConvert.SerializeObject(data);

            using (StreamWriter file = new StreamWriter(filename))
            {
                file.WriteLine(json);
                file.Close();
            }
        }
    }
}
