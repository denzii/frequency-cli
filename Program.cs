using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using System.Threading;
using System;
using System.Text.RegularExpressions;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Linq;
using swordgroup.Model;
using swordgroup.Service;
using System.Text.Json;
using System.Collections.Generic;
using swordgroup.Extension;

namespace swordgroup
{
     partial class Program
    {
        static void Main(string[] args)
        {
            (string? filename, bool caseSensitive) = getArgs(args);
            var NoFileProvided = () => {
                const string sample = "The...... three did feed the deer\r\n" +  "The quick brown fox..., jumped over the lazy dog";
                Console.WriteLine($"Will use the Sample input file - Sample.txt (in memory) in the mode (caseSensitive:{caseSensitive}).\r\n" + sample);
                DisplayAnalysisResults(sample, caseSensitive);
            };
            var InvalidFile = () => {
                Console.WriteLine("Given file cannot be found. Please try again");
                Environment.Exit(1);
            };
            var FileFound = () => {
                var fileContent = File.ReadAllText(filename);
                Console.WriteLine($"Will use the provided input file - {filename} in caseSensitive:{caseSensitive} mode.\r\n" + fileContent);
                DisplayAnalysisResults(fileContent, caseSensitive);
            };

            Action scenarioToRun = string.IsNullOrEmpty(filename) 
                ? NoFileProvided
                : (!File.Exists(filename) ? InvalidFile : FileFound);

            scenarioToRun();
        }
        private static (string?, bool) getArgs(string[] args)
        {
            bool validate1stArg(string arg) =>  arg.EndsWith(".txt");
            bool validate2ndArg(string arg) => arg != null && arg.StartsWith("--case=") && (arg == "--case=insensitive" || arg == "--case=sensitive");
            
            string? getFirstArgOrDefault(string arg) => validate1stArg(arg) ? arg : null;
            bool getSecondArgOrDefault(string arg) => validate2ndArg(arg) ? (arg.Replace("--case=", "") != "insensitive") : true;

            if (args.Length >= 1 && !string.IsNullOrWhiteSpace(args[0]))
            {
                string? firstArg = getFirstArgOrDefault(args[0]);

                if (args[0].StartsWith("--case="))
                {
                    return (null, getSecondArgOrDefault(args[0]));
                }
                
                return !string.IsNullOrWhiteSpace(args.ElementAtOrDefault(1))
                        ? (firstArg, getSecondArgOrDefault(args[1]))
                        : (firstArg, true);             
            }

            return (null, true);
        }
        private static void DisplayAnalysisResults(string fileContent, bool isCaseSensitive)
        {
            string strippedContent = string.Concat(fileContent.Where(c => !char.IsWhiteSpace(c)));

            AnalysisResult result = new CharFrequency(isCaseSensitive).CalculateStats(strippedContent);
            Console.WriteLine( "\n"+
                $"Total Characters: {result.TotalCharacters}\r\n" +
                string.Join("\n", result.Top10MostFrequent.Select((KeyValuePair<char, int> kvp) => kvp.ToNaturalString()))
            );
        }


    }

}
