using System;
using System.Threading.Tasks;
using iLib;
using HarmonyLib;

namespace lib
{
    public class MyProcessor : IProcessor
    {
        static MyProcessor()
        {
            var harmony = new Harmony("Lib.MyProcessor");
            harmony.PatchAll();     
        }
        
        Task<Tuple<bool, string>> IProcessor.ProcessDocumentAsync(string inputFile, string outputFile)
        {
            Console.WriteLine($"MyProcessor - {typeof(System.Net.Http.HttpClient).Assembly.Location}");
            
            var proccesor = new DocumentProcessor.Processor();
            return proccesor.ProcessDocumentAsync(inputFile, outputFile);
        }
    }
}
