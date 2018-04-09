using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XCYN.Print.XmlAndJson
{
    public class BookProduct : Product
    {
        [XmlAttribute("Isbn")]
        public string ISBN { get; set; }
    }
}
