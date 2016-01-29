using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace Utilidades
{
    public static class FileManager
    {
        public static string LeerArchivoEnteroDeAssembly(Assembly assembly, string resourceName)
        {
            string result = "";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }

            return result;
        }

    }
}
