using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace CompiladorGargar
{
    public class EjecucionManager
    {

        public static string EjecutarConVentana(string archConRuta)
        {
            return Ejecutar(archConRuta, new List<string>(), ProcessWindowStyle.Normal);   
        }

        public static string EjecutarSinVentana(string archConRuta)
        {
            return Ejecutar(archConRuta, new List<string>(), ProcessWindowStyle.Hidden);
        }

        public static string EjecutarSinVentana(string archConRuta, List<string> argumentos)
        {
            return Ejecutar(archConRuta, argumentos, ProcessWindowStyle.Hidden);   
        }



        private static string Ejecutar(string archConRuta, List<string> argumentos, ProcessWindowStyle ventana)
        {
            archConRuta = string.Format("{0}{1}{0}", '"', archConRuta);

            ProcessStartInfo procInfo = new ProcessStartInfo();
            procInfo.CreateNoWindow = false;
            procInfo.FileName = archConRuta;
            procInfo.WindowStyle = ventana;
            procInfo.Arguments = string.Join(" ", argumentos.ToArray());



            if (ventana == ProcessWindowStyle.Hidden)
            {
                procInfo.UseShellExecute = false;
                procInfo.RedirectStandardError = true;
                procInfo.RedirectStandardOutput = true;
                procInfo.CreateNoWindow = true;
            }
            else
            {
                procInfo.UseShellExecute = true;
            }

            
            Process proc = new Process();
            proc.StartInfo = procInfo;
            proc.Start();

            string output = string.Empty;
            if (ventana == ProcessWindowStyle.Hidden)
            {

                //string error = proc.StandardError.ReadToEnd();
                output = proc.StandardOutput.ReadToEnd();
            }

            proc.WaitForExit();

            return output;
        }
    }
}
