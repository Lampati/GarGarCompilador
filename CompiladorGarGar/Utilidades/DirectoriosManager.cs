using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace Utilidades
{
    public class DirectoriosManager
    {       
        /// <summary>
        /// Borra todos los archivos de un directorio en particular según la extensión definida por parámetro
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="extension"></param>
        public static void BorrarArchivosDelDirPorExtension(string dir, string extension)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(dir);

            foreach (FileInfo item in dirInfo.GetFiles(extension))
            {
                try
                {
                    item.Delete();
                }
                catch (Exception)
                {

                }
            }   
        }



        /// <summary>
        /// Crea el directorio si no existe
        /// </summary>
        /// <param name="path">El path a crear</param>
        /// <param name="esArchivo">Indica si el parametro path es un archivo</param>
        public static void CrearDirectorioSiNoExiste(string path, bool esArchivo)
        {
            string dir = path;

            if (esArchivo)
            {
                dir = Path.GetDirectoryName(path);
            }
           

            bool existe = Directory.Exists(dir);

            if (!existe)
            {
                Directory.CreateDirectory(dir);
            }
        }

       
    }
}
