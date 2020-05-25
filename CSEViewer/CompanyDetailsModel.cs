using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSEViewer
{
    public class ReqSymbolBetaInfo
    {
        public int securityId { get; set; }
        public double triASIBetaValue { get; set; }
        public double betaValueSPSL { get; set; }
        public string triASIBetaPeriod { get; set; }
        public int quarter { get; set; }

    }
    public class ReqLogo
    {
        public int id { get; set; }
        public string path { get; set; }

    }
    public class ReqSymbolInfo
    {
        public int id { get; set; }
        public string symbol { get; set; }
        public string name { get; set; }
        public DateTime issueDate { get; set; }
        public int quantityIssued { get; set; }
        public int parValue { get; set; }
        //public IList<undefined> issuedPrice { get; set; }
        public int lastTradedPrice { get; set; }
        public int wtdHiPrice { get; set; }
        public double mtdHiPrice { get; set; }
        public double ytdHiPrice { get; set; }
        public int p12HiPrice { get; set; }
        public double allHiPrice { get; set; }
        public double allLowPrice { get; set; }
        public double wtdLowPrice { get; set; }
        public double mtdLowPrice { get; set; }
        public double ytdLowPrice { get; set; }
        public double p12LowPrice { get; set; }
        public int tdyShareVolume { get; set; }
        public int wdyShareVolume { get; set; }
        public int mtdShareVolume { get; set; }
        public int ytdShareVolume { get; set; }
        public int p12ShareVolume { get; set; }
        public int tdyTradeVolume { get; set; }
        //public IList<undefined> wtdTradeVolume { get; set; }
        //public IList<undefined> mtdTradeVolume { get; set; }
        //public IList<undefined> ytdTradeVolume { get; set; }
        //public IList<undefined> p12TradeVolume { get; set; }
        public int tdyTurnover { get; set; }
        public int wtdTurnover { get; set; }
        public int mtdTurnover { get; set; }
        public int ytdTurnover { get; set; }
        //public IList<undefined> p12Turnover { get; set; }
        public double previousClose { get; set; }
        //public IList<undefined> foreignHoldings { get; set; }
        //public IList<undefined> foreignPercentage { get; set; }
        //public IList<undefined> instrumentsDate { get; set; }
        public double hiTrade { get; set; }
        public int lowTrade { get; set; }
        public int closingPrice { get; set; }
        public int marketCap { get; set; }
        public double marketCapPercentage { get; set; }
        public double change { get; set; }
        public double changePercentage { get; set; }
        public int symbolIndexShareVolume { get; set; }
        public string isin { get; set; }

    } 

    public class CompanyDetailsModel
    {
        public ReqSymbolBetaInfo reqSymbolBetaInfo { get; set; }
        public ReqLogo reqLogo { get; set; }
        public ReqSymbolInfo reqSymbolInfo { get; set; }

    }
}
