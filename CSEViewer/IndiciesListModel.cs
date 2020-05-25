using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSEViewer
{
    public class Sector
    {
        public int id { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
        public string indexCode { get; set; }
        public string indexCodeSp { get; set; }
    }
     
    public class IndiciesListModel
    {
        public IList<Sector> indicesList { get; set; }
    }
}
