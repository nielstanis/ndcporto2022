using System;
using System.Threading.Tasks;

namespace DocumentProcessor
{
    public class Processor
    {
        public async Task<Tuple<bool,string>> ProcessDocumentAsync(string inputFile, string outputFile)
        {
            bool result = true;
            string resultMessage = string.Empty;

            try
            {
                if (System.IO.File.Exists(inputFile))
                {
                    System.IO.File.Copy(inputFile,outputFile);
                }
                else
                {
                    var httpClient = new System.Net.Http.HttpClient();
                    var stream = await httpClient.GetStreamAsync(inputFile);
                    var fileStream = new System.IO.FileStream(outputFile, System.IO.FileMode.CreateNew);
                    await stream.CopyToAsync(fileStream);
                }
            }
            catch (Exception ex)
            {
                result = false;
                resultMessage = ex.Message;
            }
            return new Tuple<bool, string>(result, resultMessage);
        }
    }
}
