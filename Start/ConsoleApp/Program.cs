using System;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello NDC Security 2022!");

            DocumentProcessor.Processor proc = new DocumentProcessor.Processor();
            //var result = await proc.ProcessDocumentAsync("Docs/schedule.pdf","schedule2022.pdf");
            var result = await proc.ProcessDocumentAsync("https://ndc-security.com/","Output/ndcsecurity.html");
            
            Console.WriteLine("Done...");
        }
    }
}