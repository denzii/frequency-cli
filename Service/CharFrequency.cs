using swordgroup.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace swordgroup.Service
{
    public class CharFrequency
    {
        public CharFrequency() { }
        public CharFrequency(bool caseSensitive)
        {
            this.caseSensitive = caseSensitive;
        }
        public bool caseSensitive { get; set; }
        private List<KeyValuePair<char, int>> GetTop10MostFrequent(string content)
        {
            content = caseSensitive ? content :  content.ToLower();
            
            var frequencies = new Dictionary<char, int>();
            foreach (var character in content)
            {
                    if (!frequencies.ContainsKey(character))
                        frequencies[character] = 1;
                    else frequencies[character]++;
            }

            return frequencies.OrderByDescending(pair => pair.Value)
                                   .Take(10)
                                   .ToList();
        }

        public AnalysisResult CalculateStats(string content)
        {
            return new AnalysisResult
            {
                TotalCharacters = content.ToCharArray().Length,
                Top10MostFrequent = GetTop10MostFrequent(content)
            };
        }
    }
}
