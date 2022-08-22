//using Amdocs.Ginger.Plugin.Core;
//using Ginger_PACT_Plugin;
//using Ginger_PACT_Plugin.PACTEditorTools;
//using PactNet.Mocks.MockHttpService.Models;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;

//namespace GingerPACTPluginUI
//{
//    // NOTUSED !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

//    public class PACTEditor2 : ITextEditor
//    {

//        //    public override byte[] HighlightingDefinition
//        //    { get {
//        //            //string img = "pack://application:,,,/Ginger-PACT-Plugin;component/Images/@Variable_32x32.png";
//        //            //return  Properties.Resources.PACTHighlighting;
//        //            return null;
//        //        } 
//        //}

//        //    public override List<TextEditorToolBarItem> Tools => throw new NotImplementedException();

//        string ITextEditor.Name { get { return "PACT Editor"; } }

//        public IFoldingStrategy FoldingStrategy
//        {
//            get
//            {
//                return null;
//            }
//        }

//        List<string> ITextEditor.Extensions { get { return new List<string>() { ".pact" }; } }

//        //string mEditorText;
//        //public string Text { get { return mEditorText; } set { mEditorText = value; } }

//        public byte[] HighlightingDefinition
//        {
//            get
//            {
//                string highlightingDefinitionLocation = "Ginger_PACT_Plugin.PACTHighlighting.xshd";
//                Assembly asm = typeof(PACTEditor2).Assembly;
//                string[] names = asm.GetManifestResourceNames();

//                Stream stream = asm.GetManifestResourceStream(highlightingDefinitionLocation);
//                if (stream == null)
//                {
//                    throw new Exception("Cannot find editor Highlighting Definition: " + highlightingDefinitionLocation);
//                }
//                byte[] data = ReadFully(stream);
//                return data;
//            }
//        }

//        public static byte[] ReadFully(Stream input)
//        {
//            using (MemoryStream ms = new MemoryStream())
//            {
//                input.CopyTo(ms);
//                return ms.ToArray();
//            }
//        }

//        List<ITextEditorToolBarItem> ITextEditor.Tools
//        {
//            get
//            {
//                //TODO: cache
//                List<ITextEditorToolBarItem> Tools = new List<ITextEditorToolBarItem>();

//                Tools.Add(new ExportToJavaTool());
//                Tools.Add(new ExportToJSONTool());
//                // Resources._JAVAdoc_16x16);


//                //Image ToolImage2 = new Image();
//                //file = BitmapImageToFile(Resources._JSONdoc_16x16);
//                //ToolImage2.Source = new BitmapImage(new Uri(file));
//                //mTools.Add(new TextEditorToolBarItem() { Image = ToolImage2, toolTip = "Export to json", clickHandler = new ToolClickRoutedEventHandler(ExportToJson) });

//                //Image ToolImage3 = new Image();
//                //file = BitmapImageToFile(Resources._Compile_16x16);
//                //ToolImage3.Source = new BitmapImage(new Uri(file));
//                //mTools.Add(new TextEditorToolBarItem() { Image = ToolImage3, toolTip = "Compile", clickHandler = new ToolClickRoutedEventHandler(Compile) });

//                //Image ToolImage4 = new Image();
//                //file = BitmapImageToFile(Resources._Add_16x16);
//                //ToolImage4.Source = new BitmapImage(new Uri(file));
//                //mTools.Add(new TextEditorToolBarItem() { Image = ToolImage4, toolTip = "Add Interaction", clickHandler = new ToolClickRoutedEventHandler(AddNewInteractionTemplate) });

//                return Tools;

//            }
//        }



//        ITextHandler mTextHandler;
//        public ITextHandler TextHandler { get { return mTextHandler; } set { mTextHandler = value; } }

//        //private void AddNewInteractionTemplate(TextEditorToolRoutedEventArgs Args)
//        //{
//        //    Args.txt = Args.txt.Substring(0, Args.CaretLocation) + Environment.NewLine + Resources.NewInteractionTemplate + Environment.NewLine + Args.txt.Substring(Args.CaretLocation);
//        //}

//        public void Compile()
//        {
//            ServiceVirtualization SV = new ServiceVirtualization();
//            try
//            {
//                List<int> BodyLines = new List<int>();
//                List<int> InteractionLines = new List<int>();
//                string EditorText = mTextHandler.Text;
//                List<int> ErrorLines = new List<int>();
//                string ErrorMessage = "";
//                string[] lines = EditorText.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

//                for (int i = 0; i < lines.Length; i++)
//                {
//                    string st = lines[i].TrimStart();

//                    if (st.StartsWith("PACT:"))
//                    { }
//                    else if (string.IsNullOrEmpty(st))
//                    { }
//                    else if (st.StartsWith("@"))
//                    {
//                        string[] Tags = st.Split(new string[] { " " }, StringSplitOptions.None);
//                        foreach (string tag in Tags)
//                        {
//                            if (!tag.TrimStart().StartsWith("@"))
//                            {
//                                ErrorLines.Add(i + 2);
//                                ErrorMessage = ErrorMessage + "Line number: " + (i + 2) + "-Tags must start with @" + Environment.NewLine;

//                            }
//                        }
//                    }
//                    else if (st.StartsWith("Interaction") || st.StartsWith("Interaction Outline"))
//                    {
//                        InteractionLines.Add(i);
//                        if (lines[i + 1].TrimStart().StartsWith("Given"))
//                        {
//                            if (string.IsNullOrEmpty(GetStringBetween(lines[i + 1], "\"", "\"")))
//                            {
//                                ErrorLines.Add(i + 2);
//                                ErrorMessage = ErrorMessage + "Line number: " + (i + 2) + "-Given value cannot be empty" + Environment.NewLine;
//                            }
//                        }
//                        else
//                        {
//                            ErrorLines.Add(i + 2);
//                            ErrorMessage = ErrorMessage + "Line number: " + (i + 2) + "-Given is missing." + Environment.NewLine;
//                            i = i - 1;
//                        }

//                        if (lines[i + 2].TrimStart().StartsWith("Upon Receiving"))
//                        {
//                            if (string.IsNullOrEmpty(GetStringBetween(lines[i + 2], "\"", "\"")))
//                            {
//                                ErrorLines.Add(i + 3);
//                                ErrorMessage = ErrorMessage + "Line number: " + (i + 3) + "-Upon Receiving value cannot be empty" + Environment.NewLine;
//                            }
//                        }
//                        else
//                        {
//                            ErrorLines.Add(i + 3);
//                            ErrorMessage = ErrorMessage + "Line number: " + (i + 3) + "-Upon Receiving is missing." + Environment.NewLine;
//                            i = i - 1;
//                        }

//                        if (lines[i + 3].TrimStart().StartsWith("Method"))
//                        {
//                            if (string.IsNullOrEmpty(GetStringBetween(lines[i + 3], "\"", "\"")))
//                            {
//                                ErrorLines.Add(i + 4);
//                                ErrorMessage = ErrorMessage + "Line number: " + (i + 4) + "-Method value cannot be empty" + Environment.NewLine;
//                            }
//                        }
//                        else
//                        {
//                            ErrorLines.Add(i + 4);
//                            ErrorMessage = ErrorMessage + "Line number: " + (i + 4) + "-Method is missing." + Environment.NewLine;
//                            i = i - 1;
//                        }

//                        if (lines[i + 4].TrimStart().StartsWith("Path"))
//                        {
//                            if (string.IsNullOrEmpty(GetStringBetween(lines[i + 4], "\"", "\"")))
//                            {
//                                ErrorLines.Add(i + 5);
//                                ErrorMessage = ErrorMessage + "Line number: " + (i + 5) + "-Path value cannot be empty" + Environment.NewLine;
//                            }
//                        }
//                        else
//                        {
//                            ErrorLines.Add(i + 5);
//                            ErrorMessage = ErrorMessage + "Line number: " + (i + 5) + "-Path is missing." + Environment.NewLine;
//                            i = i - 1;
//                        }

//                        if (lines[i + 5].TrimStart().StartsWith("Headers:"))
//                        {
//                            string HeadersColumns = lines[i + 6].TrimStart();
//                            Dictionary<string, string> HeadersDict = GetDictBetween(HeadersColumns, "|", "|", true);
//                            if (!HeadersDict.ContainsKey("Key") || HeadersDict["Key"] != "Value")
//                            {
//                                ErrorLines.Add(i + 7);
//                                ErrorMessage = ErrorMessage + "Line number: " + (i + 7) + "-Headers Columns has to be Key and Value" + Environment.NewLine;
//                            }
//                        }
//                        else
//                        {
//                            ErrorLines.Add(i + 7);
//                            ErrorMessage = ErrorMessage + "Line number: " + (i + 7) + "-Headers definition is missing." + Environment.NewLine;
//                            i = i - 1;
//                        }
//                        int j = i + 7;

//                        while (lines[j].TrimStart().StartsWith("|"))
//                        {
//                            string line = lines[j].TrimStart();
//                            Dictionary<string, string> ValuesDict = GetDictBetween(line, "|", "|");
//                            if (ValuesDict.ContainsKey("") || ValuesDict.ContainsValue(""))
//                            {
//                                ErrorLines.Add(j + 1);
//                                ErrorMessage = ErrorMessage + "Line number: " + (j + 1) + "-Headers Values have to be wrapped by quotes and cannot be empty" + Environment.NewLine;
//                            }
//                            j = j + 1;
//                        }
//                        i = j;
//                        if (lines[i].TrimStart().StartsWith("Body"))
//                        {
//                            BodyLines.Add(i + 2);
//                        }
//                        else
//                        {
//                            ErrorLines.Add(i + 1);
//                            ErrorMessage = ErrorMessage + "Line number: " + (i + 1) + "-Body definition is missing.";
//                            i = i - 1;
//                        }

//                        j = i + 1;

//                        while (lines[j].TrimStart().StartsWith("{"))
//                        {
//                            string line = lines[j].TrimStart();
//                            //TODO: Add code for validating Body
//                            j = j + 1;
//                        }

//                        i = j;

//                        if (lines[i].TrimStart().StartsWith("Will Respond With"))
//                        {

//                        }
//                        else
//                        {
//                            ErrorLines.Add(i + 1);
//                            ErrorMessage = ErrorMessage + "Line number: " + (i + 1) + "-Will Respond With is missing." + Environment.NewLine;
//                            i = i - 1;
//                        }

//                        if (lines[i + 1].TrimStart().StartsWith("Status"))
//                        {
//                            if (string.IsNullOrEmpty(GetStringBetween(lines[i + 1], "\"", "\"")))
//                            {
//                                ErrorLines.Add(i + 2);
//                                ErrorMessage = ErrorMessage + "Line number: " + (i + 2) + "-Status value cannot be empty" + Environment.NewLine;
//                            }
//                        }
//                        else
//                        {
//                            ErrorLines.Add(i + 3);
//                            ErrorMessage = ErrorMessage + "Line number: " + (i + 3) + "-Status is missing." + Environment.NewLine;
//                            i = i - 1;
//                        }

//                        if (lines[i + 2].TrimStart().StartsWith("Headers:"))
//                        {
//                            string HeadersColumns = lines[i + 3].TrimStart();
//                            Dictionary<string, string> HeadersDict = GetDictBetween(HeadersColumns, "|", "|", true);
//                            if (!HeadersDict.ContainsKey("Key") || HeadersDict["Key"] != "Value")
//                            {
//                                ErrorLines.Add(i + 4);
//                                ErrorMessage = ErrorMessage + "Line number: " + (i + 4) + "-Headers Columns has to be Key and Value" + Environment.NewLine;
//                            }
//                        }
//                        else
//                        {
//                            ErrorLines.Add(i + 4);
//                            ErrorMessage = ErrorMessage + "Line number: " + (i + 4) + "-Headers definition is missing." + Environment.NewLine;
//                        }

//                        j = i + 4;

//                        while (lines[j].TrimStart().StartsWith("|"))
//                        {
//                            string line = lines[j].TrimStart();
//                            Dictionary<string, string> ValuesDict = GetDictBetween(line, "|", "|");
//                            if (ValuesDict.ContainsKey("") || ValuesDict.ContainsValue(""))
//                            {
//                                ErrorLines.Add(j + 1);
//                                ErrorMessage = ErrorMessage + "Line number: " + (j + 1) + "-Headers Values have to be wrapped by quotes and cannot be empty" + Environment.NewLine;
//                            }
//                            j = j + 1;
//                        }
//                        i = j;

//                        if (lines[i].TrimStart().StartsWith("Body"))
//                        {
//                            BodyLines.Add(i + 2);
//                        }
//                        else
//                        {
//                            ErrorLines.Add(i + 1);
//                            ErrorMessage = ErrorMessage + "Line number: " + (i + 1) + "-Body definition is missing." + Environment.NewLine;
//                            i = i - 1;
//                        }

//                        j = i + 1;

//                        while (lines[j].TrimStart().StartsWith("{"))
//                        {
//                            string line = lines[j].TrimStart();
//                            //TODO: Add code for validating Body
//                            j = j + 1;
//                        }
//                        i = j;
//                    }
//                }
//            }

//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }


//        public List<ProviderServiceInteraction> ParsePACT(string txt)
//        {            
//            // First we parse the text to list of PSIs
//            string[] lines = txt.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
//            List<ProviderServiceInteraction> PSIList = new List<ProviderServiceInteraction>();
//            ProviderServiceInteraction PSI = null;
//            bool RequestHeadersPassed = false;
//            foreach (string s in lines)
//            {
//                string st = s.TrimStart();
//                if (st.StartsWith("Interaction"))
//                {
//                    RequestHeadersPassed = false;
//                    if (PSI != null)
//                    {
//                        PSIList.Add(PSI);
//                    }
//                    PSI = new ProviderServiceInteraction();
//                    PSI.Request = new ProviderServiceRequest();
//                    PSI.Response = new ProviderServiceResponse();
//                    //PSI.Request.Headers = new Dictionary<string, string>();
//                    //PSI.Response.Headers = new Dictionary<string, string>();
//                }
//                else if (st.StartsWith("Given"))
//                {
//                    string st2 = GetStringBetween(s, "\"", "\"");
//                    PSI.ProviderState = st2;
//                }
//                else if (st.StartsWith("Upon Receiving"))
//                {
//                    string st2 = GetStringBetween(s, "\"", "\"");
//                    PSI.Description = st2;

//                }
//                else if (st.StartsWith("Status"))
//                {
//                    string st2 = GetStringBetween(s, "\"", "\"").ToUpper().Trim();
//                    if (st2 == "OK" || st2 == "200")
//                    {
//                        PSI.Response.Status = 200;
//                    }
//                    else if (st2 == "CONTINUE" || st2 == "100")
//                    {
//                        PSI.Response.Status = 100;
//                    }
//                    else if (st2 == "CREATED" || st2 == "201")
//                    {
//                        PSI.Response.Status = 201;
//                    }
//                    else if (st2 == "ACCEPTED" || st2 == "202")
//                    {
//                        PSI.Response.Status = 202;
//                    }
//                    else if (st2 == "NON-AUTHORITATIVE INFORMATION" || st2 == "203")
//                    {
//                        PSI.Response.Status = 203;
//                    }
//                    else if (st2 == "NO CONTENT" || st2 == "204")
//                    {
//                        PSI.Response.Status = 204;
//                    }
//                    else if (st2 == "RESET CONTENT" || st2 == "205")
//                    {
//                        PSI.Response.Status = 205;
//                    }
//                    else if (st2 == "PARTIAL CONTENT" || st2 == "206")
//                    {
//                        PSI.Response.Status = 206;
//                    }
//                    else if (st2 == "MULTI-STATUS" || st2 == "207")
//                    {
//                        PSI.Response.Status = 207;
//                    }
//                    else if (st2 == "IM USED" || st2 == "226")
//                    {
//                        PSI.Response.Status = 226;
//                    }
//                    else if (st2 == "MULTIPLE CHOICE" || st2 == "300")
//                    {
//                        PSI.Response.Status = 300;
//                    }
//                    else if (st2 == "MOVED PERMANENTLY" || st2 == "301")
//                    {
//                        PSI.Response.Status = 301;
//                    }
//                    else if (st2 == "FOUND" || st2 == "302")
//                    {
//                        PSI.Response.Status = 302;
//                    }
//                    else if (st2 == "SEE OTHER" || st2 == "303")
//                    {
//                        PSI.Response.Status = 303;
//                    }
//                    else if (st2 == "NOT MODIFIED" || st2 == "304")
//                    {
//                        PSI.Response.Status = 304;
//                    }
//                    else if (st2 == "USE PROXY" || st2 == "305")
//                    {
//                        PSI.Response.Status = 305;
//                    }
//                    else if (st2 == "UNUSED" || st2 == "306")
//                    {
//                        PSI.Response.Status = 306;
//                    }
//                    else if (st2 == "TEMPORARY REDIRECT" || st2 == "307")
//                    {
//                        PSI.Response.Status = 307;
//                    }
//                    else if (st2 == "PERMANENT REDIRECT" || st2 == "308")
//                    {
//                        PSI.Response.Status = 308;
//                    }
//                    else if (st2 == "BAD REQUEST" || st2 == "400")
//                    {
//                        PSI.Response.Status = 400;
//                    }
//                    else if (st2 == "UNAUTHORIZED" || st2 == "401")
//                    {
//                        PSI.Response.Status = 401;
//                    }
//                    else if (st2 == "PAYMENT REQUIRED" || st2 == "402")
//                    {
//                        PSI.Response.Status = 402;
//                    }
//                    else if (st2 == "FORBIDDEN" || st2 == "403")
//                    {
//                        PSI.Response.Status = 403;
//                    }
//                    else if (st2 == "NOT FOUND" || st2 == "404")
//                    {
//                        PSI.Response.Status = 404;
//                    }
//                    else if (st2 == "METHOD NOT ALLOWE" || st2 == "405")
//                    {
//                        PSI.Response.Status = 405;
//                    }
//                    else if (st2 == "NOT ACCEPTABLE" || st2 == "406")
//                    {
//                        PSI.Response.Status = 406;
//                    }
//                    else if (st2 == "PROXY AUTHENTICATION REQUIRED" || st2 == "407")
//                    {
//                        PSI.Response.Status = 407;
//                    }
//                    else if (st2 == "REQUEST TIMEOUT" || st2 == "408")
//                    {
//                        PSI.Response.Status = 408;
//                    }
//                    else if (st2 == "CONFLICT" || st2 == "409")
//                    {
//                        PSI.Response.Status = 409;
//                    }
//                    else if (st2 == "GONE" || st2 == "410")
//                    {
//                        PSI.Response.Status = 410;
//                    }
//                    else if (st2 == "LENGTH REQUIRED" || st2 == "411")
//                    {
//                        PSI.Response.Status = 411;
//                    }
//                    else if (st2 == "PRECONDITION FAILED" || st2 == "412")
//                    {
//                        PSI.Response.Status = 412;
//                    }
//                    else if (st2 == "PAYLOAD TOO LARGE" || st2 == "413")
//                    {
//                        PSI.Response.Status = 413;
//                    }
//                    else if (st2 == "URI TOO LONG" || st2 == "414")
//                    {
//                        PSI.Response.Status = 414;
//                    }
//                    else if (st2 == "UNSUPPORTED MEDIA TYPE" || st2 == "415")
//                    {
//                        PSI.Response.Status = 415;
//                    }
//                    else if (st2 == "REQUESTED RANGE NOT SATISFIABLE" || st2 == "416")
//                    {
//                        PSI.Response.Status = 416;
//                    }
//                    else if (st2 == "EXPECTATION FAILED" || st2 == "417")
//                    {
//                        PSI.Response.Status = 417;
//                    }
//                    else if (st2 == "I'M A TEAPOT" || st2 == "418")
//                    {
//                        PSI.Response.Status = 418;
//                    }
//                    else if (st2 == "MISDIRECTED REQUEST" || st2 == "421")
//                    {
//                        PSI.Response.Status = 421;
//                    }
//                    else if (st2 == "UNPROCESSABLE ENTITY" || st2 == "422")
//                    {
//                        PSI.Response.Status = 422;
//                    }
//                    else if (st2 == "LOCKED" || st2 == "423")
//                    {
//                        PSI.Response.Status = 423;
//                    }
//                    else if (st2 == "FAILED DEPENDENCY" || st2 == "424")
//                    {
//                        PSI.Response.Status = 424;
//                    }
//                    else if (st2 == "UPGRADE REQUIRED" || st2 == "426")
//                    {
//                        PSI.Response.Status = 426;
//                    }
//                    else if (st2 == "PRECONDITION REQUIRED" || st2 == "428")
//                    {
//                        PSI.Response.Status = 428;
//                    }
//                    else if (st2 == "TOO MANY REQUESTS" || st2 == "431")
//                    {
//                        PSI.Response.Status = 431;
//                    }
//                    else if (st2 == "UNAVAILABLE FOR LEGAL REASONS" || st2 == "451")
//                    {
//                        PSI.Response.Status = 451;
//                    }
//                    else if (st2 == "INTERNAL SERVER ERROR" || st2 == "500")
//                    {
//                        PSI.Response.Status = 500;
//                    }
//                    else if (st2 == "NOT IMPLEMENTED" || st2 == "501")
//                    {
//                        PSI.Response.Status = 501;
//                    }
//                    else if (st2 == "BAD GATEWAY" || st2 == "502")
//                    {
//                        PSI.Response.Status = 502;
//                    }
//                    else if (st2 == "GATEWAY TIMEOUT" || st2 == "503")
//                    {
//                        PSI.Response.Status = 503;
//                    }
//                    else if (st2 == "HTTP VERSION NOT SUPPORTED" || st2 == "505")
//                    {
//                        PSI.Response.Status = 505;
//                    }
//                    else if (st2 == "VARIANT ALSO NEGOTIATES" || st2 == "506")
//                    {
//                        PSI.Response.Status = 506;
//                    }
//                    else if (st2 == "INSUFFICIENT STORAGE" || st2 == "507")
//                    {
//                        PSI.Response.Status = 507;
//                    }
//                    else if (st2 == "LOOP DETECTED" || st2 == "508")
//                    {
//                        PSI.Response.Status = 508;
//                    }
//                    else if (st2 == "NOT EXTENDED" || st2 == "510")
//                    {
//                        PSI.Response.Status = 510;
//                    }
//                    else if (st2 == "NETWORK AUTHENTICATION REQUIRED" || st2 == "511")
//                    {
//                        PSI.Response.Status = 511;
//                    }

//                    //TODO: all other response
//                }
//                else if (st.StartsWith("Method"))
//                {
//                    string st2 = GetStringBetween(s, "\"", "\"").ToUpper();
//                    switch (st2)
//                    {
//                        case "GET":
//                            PSI.Request.Method = HttpVerb.Get;
//                            break;
//                        case "PUT":
//                            PSI.Request.Method = HttpVerb.Put;
//                            break;
//                        case "NOTSET":
//                            PSI.Request.Method = HttpVerb.NotSet;
//                            break;
//                        case "POST":
//                            PSI.Request.Method = HttpVerb.Post;
//                            break;
//                        case "DELETE":
//                            PSI.Request.Method = HttpVerb.Delete;
//                            break;
//                        case "HEAD":
//                            PSI.Request.Method = HttpVerb.Head;
//                            break;
//                        case "PATCH":
//                            PSI.Request.Method = HttpVerb.Patch;
//                            break;
//                            //TODO: add the rest !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
//                    }
//                }
//                else if (st.StartsWith("Path"))
//                {
//                    string st2 = GetStringBetween(s, "\"", "\"");
//                    PSI.Request.Path = st2;
//                }
//                else if (st.StartsWith("Will Respond With"))
//                {
//                    RequestHeadersPassed = true;
//                }
//                else if (st.StartsWith("|"))
//                {
//                    Dictionary<string, string> st2 = GetDictBetween(s, "|", "|");
//                    if (!st2.ContainsKey(""))
//                    {
//                        //foreach (KeyValuePair<string, string> KVP in st2)
//                        //    if (!RequestHeadersPassed)
//                        //        PSI.Request.Headers.Add(KVP);
//                        //    else
//                        //        PSI.Response.Headers.Add(KVP);
//                    }
//                }
//                else if (st.StartsWith("{"))
//                {
//                    //string st2 = st.Substring(0, st.LastIndexOf('}') + 1);
//                    //if (!RequestHeadersPassed)
//                    //    PSI.Request.Body += st2;
//                    //else
//                    //    PSI.Response.Body += st2;

//                }
//            }
//            // Add the last PSI created
//            PSIList.Add(PSI);

//            return PSIList;
//        }

//        public string ParseProperty(string txt, string property)
//        {
//            string[] lines = txt.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
//            foreach (string s in lines)
//            {
//                string st = s.TrimStart();
//                if (st.StartsWith(property))
//                {
//                    return GetStringBetween(s, "\"", "\"");
//                }
//            }
//            return string.Empty;
//        }

//        //move to UCTextEditor and add to interface

//        //private bool OpenFolderDialog(string Description, ref string SaveToPath)
//        //{
//        //    var dlgf = new System.Windows.Forms.FolderBrowserDialog();
//        //    dlgf.Description = Description;
//        //    if (string.IsNullOrEmpty(PreviusSelectedFolder))
//        //        dlgf.RootFolder = Environment.SpecialFolder.MyComputer;
//        //    else
//        //        dlgf.SelectedPath = PreviusSelectedFolder;
//        //    dlgf.ShowNewFolderButton = true;
//        //    System.Windows.Forms.DialogResult resultf = dlgf.ShowDialog();
//        //    if (resultf == System.Windows.Forms.DialogResult.OK)
//        //    {
//        //        SaveToPath = dlgf.SelectedPath;
//        //        PreviusSelectedFolder = SaveToPath;
//        //    }
//        //    else
//        //    {
//        //        return false;
//        //    }
//        //    return true;
//        //}

//        //private bool OpenFileDialog(string Description, ref string SelectedFilePath)
//        //{

//        //    System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog();
//        //    dlg.Filter = "TXT Files (*.txt)|*.txt|All Files (*.*)|*.*";
//        //    dlg.FilterIndex = 1;
//        //    System.Windows.Forms.DialogResult result = dlg.ShowDialog();
//        //    if (result == System.Windows.Forms.DialogResult.OK)
//        //    {
//        //        SelectedFilePath = dlg.FileName;
//        //    }
//        //    else
//        //    {
//        //        return false;
//        //    }
//        //    return true;
//        //}




//        public Dictionary<string, string> GetDictBetween(string STR, string FirstString, string LastString = null, bool HeaderNeededOnly = false)
//        {
//            Dictionary<string, string> Dict = new Dictionary<string, string>();
//            string str = "";
//            int Pos1 = STR.IndexOf(FirstString) + FirstString.Length;
//            int Pos2;
//            if (LastString != null)
//            {
//                Pos2 = STR.LastIndexOf(LastString);
//            }
//            else
//            {
//                Pos2 = STR.Length;
//            }

//            if ((Pos2 - Pos1) > 0)
//            {
//                str = STR.Substring(Pos1, Pos2 - Pos1);
//            }
//            else
//            {
//                str = "";
//            }
//            string[] result = str.Split('|');
//            if (!HeaderNeededOnly)
//                for (int i = 0; i < result.Length; i++)
//                    result[i] = GetStringBetween(result[i], "\"", "\"");
//            Dict.Add(result[0].Trim(), result[1].Trim());
//            return Dict;
//        }


//        public string GetStringBetween(string STR, string FirstString, string LastString = null)
//        {
//            string str = "";
//            int Pos1 = STR.IndexOf(FirstString) + FirstString.Length;
//            int Pos2;
//            if (LastString != null)
//            {
//                Pos2 = STR.IndexOf(LastString, Pos1);
//            }
//            else
//            {
//                Pos2 = STR.Length;
//            }

//            if ((Pos2 - Pos1) > 0)
//            {
//                str = STR.Substring(Pos1, Pos2 - Pos1);
//                return str;
//            }
//            else
//            {
//                return "";
//            }
//        }


//        //public void ShowMessage(MessageType messageType, string text)
//        //{
//        //    //throw new NotImplementedException();
//        //}
//    }
//}
