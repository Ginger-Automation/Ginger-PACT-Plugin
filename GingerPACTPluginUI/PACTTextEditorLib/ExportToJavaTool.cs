﻿using Amdocs.Ginger.Plugin.Core;
using GingerPACTPlugIn.PACTTextEditorLib;
using PactNet.Mocks.MockHttpService.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GingerPACTPluginUI.PACTTextEditorLib
{
    public class ExportToJavaTool : ITextEditorToolBarItem
    {
        public string ToolText { get { return "Export to Java"; } }

        public string ToolTip { get { return "Export to Java"; } }

        PACTTextEditor mEditor;

        public ExportToJavaTool(PACTTextEditor editor)
        {
            mEditor = editor;
        }

        public void Execute(ITextEditor textEditor)
        {
            ExportToJava();
        }

        string ErrorMessage;

        string JavaTemaplate = string.Empty;

        private void ExportToJava()
        {
            mEditor.Compile();
            //if (!string.IsNullOrEmpty(ErrorMessage))
            //  return;

            string SaveToPath = string.Empty;
            if (!mEditor.OpenFolderDialog("Select folder for saving the created Java file", ref SaveToPath))
            {
                ErrorMessage = "Export To Java Aborted";
                return;
            }

            
            if (MessageBox.Show("Do you want to use a default Java template?", "Java template to be used", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.No)
            {
                string SelectedFilePath = string.Empty;
                if (mEditor.OpenFileDialog("Select Java Temaplate File", ref SelectedFilePath))
                    JavaTemaplate = System.IO.File.ReadAllText(SelectedFilePath);
                else
                {
                    ErrorMessage = "Export To Java Aborted";
                    return;
                }
            }
            else
            {
                JavaTemaplate = GingerPACTPluginCommon.Templates.JavaTemplate;
            }

            List<ProviderServiceInteraction> PSIList = mEditor.ParsePACT(mEditor.txt);
            string Consumer = mEditor.ParseProperty(mEditor.txt, "Consumer");
            string Provider = mEditor.ParseProperty(mEditor.txt, "Provider");
            GenerateJava(JavaTemaplate, PSIList, SaveToPath, Consumer, Provider);
            if (string.IsNullOrEmpty(ErrorMessage))
            {
                Process.Start(SaveToPath);
                MessageBox.Show("Export to Java finished successfully");
            }
        }


        private void GenerateJava(string JavaTemaplate, List<ProviderServiceInteraction> pSIList, string SaveToPath, string Consumer, string Provider)
        {
            //TODO: create java code for Junit like: https://github.com/DiUS/pact-jvm/blob/master/pact-jvm-consumer-junit/src/test/java/au/com/dius/pact/consumer/examples/ExampleJavaConsumerPactRuleTest.java
            //TODO: use external file/resource with all the text and place holder to replace

            StringBuilder txt = new StringBuilder();
            string tab1 = "\t";
            string tab2 = "\t";

            int i = 1;
            foreach (ProviderServiceInteraction PSI in pSIList)
            {
                txt.Append(tab2).Append("//Data setup for Interaction: <<").Append(PSI.ProviderState).Append("  /  ").Append(PSI.Description).Append(">> ").Append(Environment.NewLine);
                txt.Append(tab1).Append("Map<String, String> requestHeaders").Append(i).Append(" = new HashMap<>();").Append(Environment.NewLine);
                foreach (var header in PSI.Request.Headers)
                {
                    txt.Append(tab1).Append("requestHeaders").Append(i).Append(".put(\"").Append(header.Key).Append("\", \"").Append(header.Value).Append("\");").Append(Environment.NewLine);
                }
                txt.Append(tab1).Append("Map<String, String> responseHeaders").Append(i).Append(" = new HashMap<>();").Append(Environment.NewLine);
                foreach (var header in PSI.Response.Headers)
                {
                    txt.Append(tab1).Append("responseHeaders").Append(i).Append(".put(\"").Append(header.Key).Append("\", \"").Append(header.Value).Append("\");").Append(Environment.NewLine);
                }
                txt.Append(Environment.NewLine);
                i = i + 1;
            }

            txt.Append(tab2).Append("return builder").Append(Environment.NewLine);
            //i = 1;
            // foreach (ProviderServiceInteraction PSI in pSIList)
            // {
                //txt += tab2 + "//Add Interaction: <<" + PSI.Description + ">>" + Environment.NewLine;
                //txt += tab2 + ".given(\"" + PSI.ProviderState + "\")" + Environment.NewLine;
                //txt += tab1 + tab2 + ".uponReceiving(\"" + PSI.Description + "\")" + Environment.NewLine;
                //txt += tab1 + tab2 + ".path(\"" + PSI.Request.Path + "\")" + Environment.NewLine;
                //txt += tab1 + tab2 + ".method(\"" + PSI.Request.Method.ToString() + "\")" + Environment.NewLine;
                //txt += tab1 + tab2 + ".headers(requestHeaders" + i + ")" + Environment.NewLine;
                //txt += tab1 + tab2 + ".body(\"" + PSI.Request.Body + "\")" + Environment.NewLine;
                //txt += tab2 + ".willRespondWith()" + Environment.NewLine;
                //txt += tab1 + tab2 + ".status(" + PSI.Response.Status + ")" + Environment.NewLine;   //TODO: get status convert OK...
                //txt += tab1 + tab2 + ".headers(responseHeaders" + i + ")" + Environment.NewLine;
                //txt += tab1 + tab2 + ".body(\"" + PSI.Response.Body + "\")" + Environment.NewLine;
                //i = i + 1;
             // }

            //bool IsPlaceHolderExist = JavaTemaplate.Contains(InteractionData);

            
            //if (!IsPlaceHolderExist)
            //{
            //    ErrorMessage = ErrorMessage + "Place Holder " + InteractionData + " is missing from the customized Java template." + Environment.NewLine + "Export to Java Process Aborted.";
            //    return;
            //}

            
            string JavaFileContent = JavaTemaplate.Replace("<<Interactions_Data>>", txt.ToString());
            JavaFileContent = JavaFileContent.Replace("<<Provider>>", Provider);
            JavaFileContent = JavaFileContent.Replace("<<Consumer>>", Consumer);

            String timeStamp = DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss");

            System.IO.File.WriteAllText(SaveToPath + @"\PactToJava" + timeStamp + ".Java", JavaFileContent);
        }


    }
}
