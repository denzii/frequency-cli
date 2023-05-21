using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace swordgroup.Model
{
    public class AnalysisResult
    {
        public int TotalCharacters { get; set; }
        public IEnumerable<KeyValuePair<char, int>> Top10MostFrequent { get; set; }
    }
}
