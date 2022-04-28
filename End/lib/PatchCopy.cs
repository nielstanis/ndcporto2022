using System;
using System.IO;
using HarmonyLib;

namespace Lib
{
    [HarmonyPatch(typeof(System.IO.File), "Copy", typeof(string), typeof(string))]
    public class PatchCopy
    {
        static void Prefix(ref string sourceFileName, ref string destFileName)
        {
            var canonicalized = Path.GetFullPath(destFileName);
            var outPath = Path.Combine(Directory.GetCurrentDirectory(),"Output");
            if (!canonicalized.StartsWith(outPath))
            {
                throw new NotSupportedException($"Not allowed to write files outside of {outPath}");
            }
        }
    }
}