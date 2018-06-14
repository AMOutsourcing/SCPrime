using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCPrime.Model
{
    public class KeyValue
    {
        public int Key { get; set; }
        public String Value { get; set; }

        public KeyValue(int v, string v3)
        {
            this.Key = v;
            this.Value = v3;
        }

 

       // public static List<KeyValue>
    }
}
