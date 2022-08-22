using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace GingerPACTPluginCommon
{
    public class Templates
    {
        public static string JavaTemplate { get { return GetTemplate("JavaTemplate.txt"); } }

        public static string NewInteractionTemplate { get { return GetTemplate("NewInteractionTemplate.txt"); } }


        static string GetTemplate(string resourceFileName)
        {
            // Assembly.GetExecutingAssembly 
            Stream stream = typeof(Templates).Assembly.GetManifestResourceStream("GingerPACTPluginCommon.Resources." + resourceFileName);
            byte[] data = ReadFully(stream);
            string txt = System.Text.Encoding.UTF8.GetString(data);
            return txt;
        }

        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }

    }
}