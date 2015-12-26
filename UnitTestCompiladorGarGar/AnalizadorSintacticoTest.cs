using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CompiladorGargar;
using CompiladorGargar.Resultado;
using System.Reflection;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace UnitTestCompiladorGarGar
{
    [TestClass]
    public class AnalizadorSintacticoTest
    {
        private void PruebaSintactica(int errorPretendido, string pruebaBis = "")
        {
            Compilador compilador = new Compilador();

            string resourceName = string.Format("UnitTestCompiladorGarGar.Programas.ErroresSintacticos.error{0}{1}.gar", errorPretendido, pruebaBis);
            string programa = Utilidades.FileManager.LeerArchivoEnteroDeAssembly(Assembly.GetExecutingAssembly(), resourceName);

            ResultadoCompilacion res = compilador.Compilar(programa);

            Assert.IsFalse(res.CompilacionGarGarCorrecta,
                string.Format("La compilacion da correcta y esperaba error {0}", errorPretendido));

            Assert.IsTrue(res.ListaErrores[0].Mensaje.CodigoGlobal == errorPretendido,
                string.Format("Error {0} y esperaba {1}. Error: {2}",
                res.ListaErrores[0].Mensaje.CodigoGlobal, errorPretendido, res.ListaErrores[0].Mensaje.Descripcion));
        }

        [TestMethod]
        public void SintacticoError1()
        {          
            PruebaSintactica(1);            
        }

        [TestMethod]
        public void SintacticoError2()
        {
            PruebaSintactica(2);    
        }

        [TestMethod]
        public void SintacticoError3()
        {
            PruebaSintactica(3);
        }

        [TestMethod]
        public void SintacticoError4()
        {
            PruebaSintactica(4);
        }

        [TestMethod]
        public void SintacticoError4_2()
        {
            PruebaSintactica(4, "_2");
        }

        [TestMethod]
        public void SintacticoError5()
        {
            PruebaSintactica(5);
        }

        [TestMethod]
        public void SintacticoError6()
        {
            PruebaSintactica(7);
        }

        [TestMethod]
        public void SintacticoError7()
        {
            PruebaSintactica(7);
        }

        [TestMethod]
        public void SintacticoError8()
        {
            PruebaSintactica(8);
        }

        [TestMethod]
        public void SintacticoError8_2()
        {
            PruebaSintactica(8,"_2");
        }


        [TestMethod]
        public void SintacticoError9()
        {
            PruebaSintactica(9);
        }


        [TestMethod]
        public void SintacticoError10()
        {
            PruebaSintactica(10);
        }

        [TestMethod]
        public void SintacticoError11()
        {
            PruebaSintactica(11);
        }

        [TestMethod]
        public void SintacticoError11_2()
        {
            PruebaSintactica(11,"_2");
        }


        //La saco pq no se tiene mas en cuenta
        //[TestMethod]
        //public void SintacticoError12()
        //{
        //    PruebaSintactica(12);
        //}



        [TestMethod]
        public void SintacticoError13()
        {
            PruebaSintactica(13);
        }



        [TestMethod]
        public void SintacticoError14()
        {
            PruebaSintactica(14);
        }

        [TestMethod]
        public void SintacticoError15()
        {
            PruebaSintactica(15);
        }

        [TestMethod]
        public void SintacticoError16()
        {
            PruebaSintactica(16);
        }



        [TestMethod]
        public void SintacticoError17()
        {
            PruebaSintactica(17);
        }

        [TestMethod]
        public void SintacticoError18()
        {
            PruebaSintactica(18);
        }

        [TestMethod]
        public void SintacticoError19()
        {
            PruebaSintactica(19);
        }

        // No lo uso pq es muy complicado que se de.
        //[TestMethod]
        //public void SintacticoError20()
        //{
        //    PruebaSintactica(20);
        //}

        // No lo uso pq es muy complicado que se de.
        //[TestMethod]
        //public void SintacticoError21()
        //{
        //    PruebaSintactica(21);
        //}

        //[TestMethod]
        //public void SintacticoError22()
        //{
        //    PruebaSintactica(22);
        //}


        [TestMethod]
        public void SintacticoError23()
        {
            PruebaSintactica(23);
        }

        [TestMethod]
        public void SintacticoError23_2()
        {
            PruebaSintactica(23,"_2");
        }

        [TestMethod]
        public void SintacticoError23_3()
        {
            PruebaSintactica(23, "_3");
        }
        [TestMethod]
        public void SintacticoError24()
        {
            PruebaSintactica(24);
        }

        [TestMethod]
        public void SintacticoError24_2()
        {
            PruebaSintactica(24, "_2");
        }

        [TestMethod]
        public void SintacticoError24_3()
        {
            PruebaSintactica(24, "_3");
        }
        [TestMethod]
        public void SintacticoError25()
        {
            PruebaSintactica(25);
        }
        [TestMethod]
        public void SintacticoError26()
        {
            PruebaSintactica(26);
        }
        [TestMethod]
        public void SintacticoError27()
        {
            PruebaSintactica(27);
        }
        [TestMethod]
        public void SintacticoError28()
        {
            PruebaSintactica(28);
        }

        [TestMethod]
        public void SintacticoError31()
        {
            PruebaSintactica(31);
        }

        [TestMethod]
        public void SintacticoError35()
        {
            PruebaSintactica(35);
        }

        [TestMethod]
        public void SintacticoError35_2()
        {
            PruebaSintactica(35, "_2");
        }

        [TestMethod]
        public void SintacticoError35_3()
        {
            PruebaSintactica(35, "_3");
        }

        [TestMethod]
        public void SintacticoError35_4()
        {
            PruebaSintactica(35, "_4");
        }

        [TestMethod]
        public void SintacticoError36()
        {
            PruebaSintactica(36);
        }

        [TestMethod]
        public void SintacticoError36_2()
        {
            PruebaSintactica(36,"_2");
        }

        [TestMethod]
        public void SintacticoError36_3()
        {
            PruebaSintactica(36, "_3");
        }

        [TestMethod]
        public void SintacticoError37()
        {
            PruebaSintactica(37);
        }
        [TestMethod]
        public void SintacticoError38()
        {
            PruebaSintactica(38);
        }

        [TestMethod]
        public void SintacticoError39()
        {
            PruebaSintactica(39);
        }

        [TestMethod]
        public void SintacticoError40()
        {
            PruebaSintactica(40);
        }

        [TestMethod]
        public void SintacticoError40_2()
        {
            PruebaSintactica(40,"_2");
        }


        [TestMethod]
        public void SintacticoError41()
        {
            PruebaSintactica(41);
        }

        [TestMethod]
        public void SintacticoError42()
        {
            PruebaSintactica(42);
        }
    }
}
