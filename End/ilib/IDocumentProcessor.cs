using System;
using System.Threading.Tasks;

namespace iLib
{
    public interface IProcessor
    {
        Task<Tuple<bool,string>> ProcessDocumentAsync(string inputFile, string outputFile);
    }
}