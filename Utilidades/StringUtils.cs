using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace Utilidades
{
    public static class StringUtils
    {
        public static bool esCaracterSalteable(char x)
        {
            if ((char.IsWhiteSpace(x)) || esRetornoDeCarro(x) || esNuevaLinea(x))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static bool esRetornoDeCarro(char x)
        {
            if (x == '\r')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool esNuevaLinea(char x)
        {
            if (x == '\n')
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static void InsertarLineaEnArchivo(string archLeer, string textoAInsertar, int linea)
        {

            

            string tempfile = Path.GetTempFileName();
            StreamWriter writer = new StreamWriter(tempfile);
            StreamReader reader = new StreamReader(archLeer);
            
            int cantLineasLeidas = 1;
            while (!reader.EndOfStream)
            {
                if (cantLineasLeidas == linea)
                {
                    writer.WriteLine(textoAInsertar);
                }

                writer.WriteLine(reader.ReadLine());
                cantLineasLeidas++;
            }
            writer.Close();
            reader.Close();
            File.Copy(tempfile, archLeer, true);

            File.Delete(tempfile);

        }

        public static string InsertarLineaEnTexto(string texto, string textoAInsertar, int linea)
        {
            string tempfile = Path.GetTempFileName();
            StreamWriter streamTextoActual = new StreamWriter(tempfile);
            streamTextoActual.WriteLine(texto);
            streamTextoActual.Close();

            string tempfile2 = Path.GetTempFileName();
            StreamReader reader = new StreamReader(tempfile);
            StreamWriter writer = new StreamWriter(tempfile2);
            
            int cantLineasLeidas = 1;
            while (!reader.EndOfStream)
            {
                if (cantLineasLeidas == linea)
                {
                    writer.WriteLine(textoAInsertar);
                }

                writer.WriteLine(reader.ReadLine());
                cantLineasLeidas++;
            }
            writer.Close();
            reader.Close();

            StreamReader reader2 = new StreamReader(tempfile2);
            string textoModif = reader2.ReadToEnd();
            reader2.Close();

            File.Delete(tempfile);
            File.Delete(tempfile2);

            return textoModif;
        
        }

        static Regex regexNuevasLineas = new Regex("\r\n", RegexOptions.Multiline);
        public static int CantidadDeLineas(string texto)
        {
            MatchCollection mc = regexNuevasLineas.Matches(texto);
            return mc.Count;
        }

        public static string InsertarLineaEnTextoEntreLineas(string texto, string textoAInsertar, int lineaCom, int lineaFin)
        {
            string tempfile = Path.GetTempFileName();
            StreamWriter streamTextoActual = new StreamWriter(tempfile);
            streamTextoActual.WriteLine(texto);
            streamTextoActual.Close();

            string tempfile2 = Path.GetTempFileName();
            StreamReader reader = new StreamReader(tempfile);
            StreamWriter writer = new StreamWriter(tempfile2);

            //Primero le saco las lineas
            int cantLineasLeidas = 1;
            while (!reader.EndOfStream)
            {
                if (!(cantLineasLeidas > lineaCom && cantLineasLeidas <= lineaFin))
                {
                    writer.WriteLine(reader.ReadLine());
                }
                else
                {
                    reader.ReadLine();
                }
                cantLineasLeidas++;
            }
            writer.Close();
            reader.Close();

            //Ahora le agrego la linea justo en la lineaCom

            string tempfile3 = Path.GetTempFileName();
            reader = new StreamReader(tempfile2);
            writer = new StreamWriter(tempfile3);

            cantLineasLeidas = 1;
            while (!reader.EndOfStream)
            {
                

                writer.WriteLine(reader.ReadLine());
                if (cantLineasLeidas == lineaCom)
                {
                    writer.WriteLine(textoAInsertar);
                }
                cantLineasLeidas++;
            }
            writer.Close();
            reader.Close();

            StreamReader reader2 = new StreamReader(tempfile3);
            string textoModif = reader2.ReadToEnd();
            reader2.Close();

            File.Delete(tempfile);
            File.Delete(tempfile2);
            File.Delete(tempfile3);

            return textoModif;
        }
    }
}
