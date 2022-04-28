using System;
using System.Threading.Tasks;
using iLib;

namespace lib
{
    public class MyProcessor : IProcessor
    {
        Task<Tuple<bool, string>> IProcessor.ProcessDocumentAsync(string inputFile, string outputFile)
        {
            Console.WriteLine($"MyProcessor - {typeof(System.Net.Http.HttpClient).Assembly.Location}");
            
            var proccesor = new DocumentProcessor.Processor();
            return proccesor.ProcessDocumentAsync(inputFile, outputFile);
        }
    }
}
