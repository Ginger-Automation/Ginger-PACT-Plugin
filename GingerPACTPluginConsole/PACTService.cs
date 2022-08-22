using Amdocs.Ginger.Plugin.Core;
using Amdocs.Ginger.Plugin.Core.ActionsLib;
using GingerPACTPluginCommon;
using System;
using System.IO;

namespace Ginger_PACT_Plugin
{
    [GingerService("PACT", "PACT Server")]
    public class PACTService
    {
        // Need to be here or in json !!!??
        // public override string Name { get { return "PACTService"; } }


        ServiceVirtualization SV;

        [GingerAction("StartPACTServer", "Start PACT Server")]
        public void StartPACTServer(IGingerAction GA, int port)
        {
            Console.WriteLine("Starting PACT server at port: " + port);
            // check input params and add errors if invalid
            if (port == 0)
            {
                GA.AddError("Port cannot be 0");
                return;
            }
            try
            {
                SV = new ServiceVirtualization(port);
                GA.AddOutput("port", port);
                GA.AddOutput("url", SV.MockProviderServiceBaseUri);

                //ExInfo
                GA.AddExInfo("PACT Mock Server Started on port: " + port + " " + SV.MockProviderServiceBaseUri);
                Console.WriteLine("PACT Server started");
            }
            catch (Exception ex)
            {
                GA.AddError("Error starting PACT Mock Server: " + ex.Message);
                Console.WriteLine("Error starting PACT Mock Server: " + ex.Message);
            }

        }


        [GingerAction("StopPACTServer", "Stop PACT Server")]
        public void StopPACTServer(IGingerAction GA)
        {

            // Act
            SV.MockProviderService.Stop();
            SV = null;
            //ExInfo
            GA.AddExInfo("PACT Mock Server Stopped");
        }

        [GingerAction("LoadInteractionsFile", "Load Interactions File")]
        public void LoadInteractionsFile(IGingerAction GA, string fileName)
        {
            if (SV == null)
            {
                GA.AddError("Service Virtualization not started yet");
                return;
            }

            if (File.Exists(fileName))
            {
                try
                {
                    int count = SV.LoadInteractions(fileName);
                    GA.AddExInfo("Interaction file loaded: '" + fileName + "', " + count + " Interactions loaded");
                    GA.AddOutput("Interactions file", fileName + "");
                    GA.AddOutput("Interactions count", count + "");
                }
                catch (Exception ex)
                {
                    GA.AddError("Error when loading interaction file: " + fileName + " " + ex.Message);
                }
            }
            else
            {
                GA.AddError("Interaction file not found - " + fileName);
            }
        }


        [GingerAction("ClearInteractions", "Clear Interactions")]
        public void ClearInteractions(IGingerAction GA)
        {
            if (SV == null)
            {
                GA.AddError("Error: Service Virtualization not started yet");
                return;
            }

            //if (s.StartsWith("~"))
            //    s = Path.GetFullPath(s.Replace("~", act.SolutionFolder));
            //if (File.Exists(s))
            //{
            //    int count = SV.LoadInteractions(s);
            //    act.ExInfo += "Interaction file loaded: '" + s + "', " + count + " Interactions loaded";
            //    act.AddOutput("Interaction Loaded", count + "");
            //}
            //else
            //{
            //    act.Error += "Interaction file not found - " + s;
            //}

            // Act
            SV.ClearInteractions();

            //ExInfo
            GA.AddExInfo("Interactions cleared");
        }

        // TODO: LoadInteractionsFolder

        //case cAddInteraction:
        //    if (SV == null)
        //    {
        //        act.Error += "Error: Service Virtualization not started yet";
        //        return;
        //    }

        //    //TODO: get it from the edit page
        //    ProviderServiceInteraction PSI = new ProviderServiceInteraction();
        //    PSI.ProviderState = act.GetOrCreateParam("ProviderState").GetValueAsString();
        //    PSI.Description = act.GetOrCreateParam("Description").GetValueAsString();
        //    PSI.Request = new ProviderServiceRequest();


        //    string HTTPType = act.GetOrCreateParam("RequestType").GetValueAsString();
        //    switch (HTTPType)
        //    {
        //        case "Get":
        //            PSI.Request.Method = HttpVerb.Get;
        //            break;
        //        case "Post":
        //            PSI.Request.Method = HttpVerb.Post;
        //            break;
        //                case "PUT":
        //            PSI.Request.Method = HttpVerb.Put;
        //            break;

        //            //TODO: add all the rest and include default for err
        //    }

        //    PSI.Request.Path = act.GetOrCreateParam("Path").GetValueAsString();
        //    Dictionary<string, string> d = new Dictionary<string, string>();
        //    d.Add("Accept", "application/json");  //TODO: fixme
        //    PSI.Request.Headers = d;
        //    PSI.Response = new ProviderServiceResponse();
        //    PSI.Response.Status = 200;  //TODO: fixme
        //    Dictionary<string, string> r = new Dictionary<string, string>();
        //    r.Add("Content-Type", "application/json; charset=utf-8");   //TODO: fixme
        //    PSI.Response.Headers = r;
        //    PSI.Response.Body = act.GetOrCreateParam("ResposneBody").GetValueAsString();

        //    SV.AddInteraction(PSI);

        //    act.ExInfo += "Interaction added";

        //    break;
        //case cSaveInteractions:
        //    SV.SaveInteractions();
        //    act.ExInfo += "Interactions saved to file";
        //    // TODO: add file name to ex info
        //    break;


    }
}
