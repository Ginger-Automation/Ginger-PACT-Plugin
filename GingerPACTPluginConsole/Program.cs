using Amdocs.Ginger.Plugin.Core;
using Ginger_PACT_Plugin;
using System;

namespace GingerPACTPluginConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting PACT Plugin");
            using (GingerNodeStarter gingerNodeStarter = new GingerNodeStarter())
            {
                if (args.Length > 0)
                {
                    gingerNodeStarter.StartFromConfigFile(args[0]);  // file name 
                }
                else
                {
                    gingerNodeStarter.StartNode("PACT Service 1", new PACTService(), "172.24.64.1", 15004);
                }
                gingerNodeStarter.Listen();
            }
        }
    }
}