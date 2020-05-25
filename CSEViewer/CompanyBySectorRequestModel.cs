using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace CSEViewer
{
    public class ReqIndustryBySector
    { 
        public string Profile { get { return $"https://www.cse.lk/home/company-info/{symbol}/trade-summery"; } }
        public int id { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
        public object lastTradedTime { get; set; }
        public double price { get; set; }
        public double? turnover { get; set; }
        public int? sharevolume { get; set; }
        public int? tradevolume { get; set; }
        public double change { get; set; }
        public double changePercentage { get; set; }

        //External Data
        public double Dividence { get; set; }
        public double NAVPS { get; set; } 
        public double EPS { get; set; }
        public double CurrentRatio { get; set; }
        public double EPSGR { get { return 0.1; } }
        public double DY { get { return (Dividence / price); } }
        public double PE {  get { return (price / EPS); } }
        public double PEG {  get { return PE / (EPSGR * 100); } }
        public string CRStatus
        {
            get
            {
                if (CurrentRatio < 1.5f)
                {
                    return "Problem"; 
                }
                else if (CurrentRatio == 1.5)
                {
                    return "Normal"; 
                }
                else if (CurrentRatio > 2)
                {
                    return "Why ?"; 
                }
                else if (CurrentRatio > 1.5)
                {
                    return "Good";
                }else
                {
                    return "Undefined";
                }
            }
        }
        public double PNAVPS {  get { return price /NAVPS ; } } 
        public double Value { get { return EPS * (8.5 + 2 * EPSGR); } }
        //------------------------
        public double Investment { get; set;  }
        public double Shares { get { return Investment / price; } }
        public double FullDivident { get { return Investment * DY; } } 
    }

    public class CompanyBySectorRequestModel
    {
        public IList<ReqIndustryBySector> reqIndustryBySectors { get; set; }
    } 
}
