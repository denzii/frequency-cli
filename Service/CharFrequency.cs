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
        private readonly bool _caseSensitive;
        public CharFrequency(bool caseSensitive)
        {
            _caseSensitive = caseSensitive;
        }
        private IEnumerable<char> FilterContent(string content) => content.Where(c => char.IsLetter(c));
        
        private IEnumerable<KeyValuePair<char, int>> GetTop10MostFrequent(string content)
        {
            var filteredContent = FilterContent(content);
            filteredContent = _caseSensitive ? filteredContent : filteredContent.Select(c => char.ToLower(c));

            return filteredContent
                .GroupBy(x => x)
                .Select(e => new KeyValuePair<char, int>(e.Key, e.Count()))
                .OrderByDescending(b => b.Value)
                .Take(10);
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
