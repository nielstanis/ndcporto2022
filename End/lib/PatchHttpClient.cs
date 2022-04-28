using System;
using HarmonyLib;

namespace Lib
{
    [HarmonyPatch(typeof(System.Net.Http.HttpClient), "GetStreamAsync", typeof(string))]
    public class PatchHttpClient
    {
        static void Prefix(ref string requestUri)
        {
            throw new NotSupportedException("HttpClient.GetStreamAsync not supported");
        }
    }
}