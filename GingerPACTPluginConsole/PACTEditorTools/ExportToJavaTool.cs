//using Amdocs.Ginger.Plugin.Core;
//using GingerPACTPluginUI;
//using PactNet.Mocks.MockHttpService.Models;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Text;

//namespace Ginger_PACT_Plugin.PACTEditorTools
//{
//    public class ExportToJavaTool : ITextEditorToolBarItem
//    {
//        public string ToolText { get { return "Export to Java";  } }

//        public string ToolTip { get { return "Generate Java class to include in unit tests"; } }

//        string JavaTemaplate = string.Empty;

//        public void Execute(ITextEditor textEditor)
//        {
//            //textEditor.TextHandler.AppendText("Exported to Java test");
//            // textEditor.TextHandler.InsertText("Exported to Java test");
//            // textEditor.TextHandler.ShowMessage(MessageType.Info, "Exported!");



//            PACTEditor2 editor = (PACTEditor2)textEditor;
//            editor.Compile();
//            //    if (!string.IsNullOrEmpty(Args.ErrorMessage))
//            //        return;

//            string SaveToPath = @"c:\temp\pact.java";  //FIXME

//            //string SaveToPath = string.Empty;
//            //    if (!OpenFolderDialog("Select folder for saving the created Java file", ref SaveToPath))
//            //    {                    
//            //        textEditor.TextHandler.ShowMessage(MessageType.Info, "Export To Java Aborted");
//            //        return;
//            //    }

//            //    string JavaTemaplate = string.Empty;
//            //    if (MessageBox.Show("Do you want to use a default Java template?", "Java template to be used", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.No)
//            //    {
//            //        string SelectedFilePath = string.Empty;
//            //        if (OpenFileDialog("Select Java Temaplate File", ref SelectedFilePath))
//            //            JavaTemaplate = System.IO.File.ReadAllText(SelectedFilePath);
//            //        else
//            //        {
//            //            Args.ErrorMessage = "Export To Java Aborted";
//            //            return;
//            //        }
//            //    }
//            //    else
//            //    {
//            //        JavaTemaplate = Resources.JavaTemplate;
//            //    }

//            List<ProviderServiceInteraction> PSIList = editor.ParsePACT(editor.TextHandler.Text);
//            string Consumer = editor.ParseProperty(editor.TextHandler.Text, "Consumer");
//            string Provider = editor.ParseProperty(editor.TextHandler.Text, "Provider");

//            GenerateJava(JavaTemaplate, PSIList, SaveToPath, Consumer, Provider);
//            //if (string.IsNullOrEmpty(Args.ErrorMessage))
//            //{
//            //    Process.Start(SaveToPath);
//            //    Args.SuccessMessage = "Export to Java finished successfully";
//            //}
//            textEditor.TextHandler.ShowMessage(MessageType.Info, "Exported to Java!");
//        }


//        // TODO: fix me and improve!
//        private void GenerateJava(string JavaTemaplate, List<ProviderServiceInteraction> pSIList, string SaveToPath, string Consumer, string Provider)
//        {
//            //TODO: create java code for Junit like: https://github.com/DiUS/pact-jvm/blob/master/pact-jvm-consumer-junit/src/test/java/au/com/dius/pact/consumer/examples/ExampleJavaConsumerPactRuleTest.java
//            //TODO: use external file/resource with all the text and place holder to replace

//            string txt = string.Empty;
//            string tab1 = "\t";
//            string tab2 = "\t";

//            int i = 1;
//            foreach (ProviderServiceInteraction PSI in pSIList)
//            {
//                txt += tab2 + "//Data setup for Interaction: <<" + PSI.ProviderState + "  /  " + PSI.Description + ">> " + Environment.NewLine;
//                txt += tab1 + "Map<String, String> requestHeaders" + i + " = new HashMap<>();" + Environment.NewLine;
//                foreach (var header in PSI.Request.Headers)
//                {
//                    txt += tab1 + "requestHeaders" + i + ".put(\"" + header.Key + "\", \"" + header.Value + "\");" + Environment.NewLine;
//                }
//                txt += tab1 + "Map<String, String> responseHeaders" + i + " = new HashMap<>();" + Environment.NewLine;
//                foreach (var header in PSI.Response.Headers)
//                {
//                    txt += tab1 + "responseHeaders" + i + ".put(\"" + header.Key + "\", \"" + header.Value + "\");" + Environment.NewLine;
//                }
//                txt += Environment.NewLine;
//                i = i + 1;
//            }

//            txt += tab2 + "return builder" + Environment.NewLine;
//            i = 1;
//            foreach (ProviderServiceInteraction PSI in pSIList)
//            {
//                //txt += tab2 + "//Add Interaction: <<" + PSI.Description + ">>" + Environment.NewLine;
//                //txt += tab2 + ".given(\"" + PSI.ProviderState + "\")" + Environment.NewLine;
//                //txt += tab1 + tab2 + ".uponReceiving(\"" + PSI.Description + "\")" + Environment.NewLine;
//                //txt += tab1 + tab2 + ".path(\"" + PSI.Request.Path + "\")" + Environment.NewLine;
//                //txt += tab1 + tab2 + ".method(\"" + PSI.Request.Method.ToString() + "\")" + Environment.NewLine;
//                //txt += tab1 + tab2 + ".headers(requestHeaders" + i + ")" + Environment.NewLine;
//                //txt += tab1 + tab2 + ".body(\"" + PSI.Request.Body + "\")" + Environment.NewLine;
//                //txt += tab2 + ".willRespondWith()" + Environment.NewLine;
//                //txt += tab1 + tab2 + ".status(" + PSI.Response.Status + ")" + Environment.NewLine;   //TODO: get status convert OK...
//                //txt += tab1 + tab2 + ".headers(responseHeaders" + i + ")" + Environment.NewLine;
//                //txt += tab1 + tab2 + ".body(\"" + PSI.Response.Body + "\")" + Environment.NewLine;
//                //i = i + 1;
//            }

//            //bool IsPlaceHolderExist = JavaTemaplate.Contains(InteractionData);

//            //if (!IsPlaceHolderExist)
//            //{
//            //    Args.ErrorMessage = Args.ErrorMessage + "Place Holder " + InteractionData + " is missing from the customized Java template." + Environment.NewLine + "Export to Java Process Aborted.";
//            //    return;
//            //}

//            //string JavaFileContent = JavaTemaplate.Replace(InteractionData, txt);
//            //JavaFileContent = JavaFileContent.Replace(ProviderData, Provider);
//            //JavaFileContent = JavaFileContent.Replace(ConsumerData, Consumer);

//            //String timeStamp = DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss");

//            //System.IO.File.WriteAllText(SaveToPath + @"\PactToJava" + timeStamp + ".Java", JavaFileContent);
//        }




//    }
//}
