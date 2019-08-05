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
            //PACTService s = new PACTService();
            //GingerAction GA = new GingerAction("aa");
            //s.StartPACTServer(ref GA, 4444);
            //Console.WriteLine("Done!");            

            //GingerNodeStarter.StartNode(new PACTService(), "PACT Service 1");
            using (GingerNodeStarter gingerNodeStarter = new GingerNodeStarter())
            {
                if (args.Length > 0)
                {
                    gingerNodeStarter.StartFromConfigFile(args[0]);
                }
                else
                {
                    gingerNodeStarter.StartNode("PACT Service 1", new PACTService());
                }
                gingerNodeStarter.Listen();
            }

            //GingerNode gingerNode = new GingerNode(new PACTService());
            //if (args.Length == 0)
            //{                
            //    gingerNode.StartGingerNode("PACT 1", SocketHelper.GetLocalHostIP(), 15001);
            //}
            //else
            //{
            //    gingerNode.StartGingerNode(args[0]);  // start with nodeConfigFileName
            //}


            // test def
            //PACTEditor2 editor = new PACTEditor2();
            //var v = editor.HighlightingDefinition;


            //TODO: Wait for?

            Console.ReadKey();
        }
    }
}
