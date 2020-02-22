using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Students {
    [XmlInclude(typeof(Bachelor))]
    [XmlInclude(typeof(Master))]
    public abstract class Student {
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Faculty { get; set; }
    }
}