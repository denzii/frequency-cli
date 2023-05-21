using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace swordgroup.Extension
{
    public static class KeyValuePairExtensions
    {
        public static string ToNaturalString(this KeyValuePair<char,int> kvp) => $"{kvp.Key} ({kvp.Value})";
    }
}
