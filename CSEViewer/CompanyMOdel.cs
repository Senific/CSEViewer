using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSEViewer
{
    public class CompanyModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }

        public CompanyDetailsModel Details { get; set; }
    } 
}
