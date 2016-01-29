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

        public static void CopyStream(Stream input, Stream output)
        {
            // Insert null checking here for production
            byte[] buffer = new byte[8192];

            int bytesRead;
            while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, bytesRead);
            }
        }


        internal static string ObtenerNombreArchivoDeResource(string resourceName)
        {
            string[] aux = resourceName.Split('.');
            string fileName = null;

            if (aux.Length > 1)
            {
                fileName = string.Format("{0}.{1}", aux[aux.Length - 2], aux[aux.Length - 1]);
            }
            else if (aux.Length > 0)
            {
                fileName = aux[aux.Length - 1];
            }

            return fileName;
        }
    }
}
