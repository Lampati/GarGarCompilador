using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CompiladorGargar;
using CompiladorGargar.Resultado;
using System.Reflection;
using System.Text;

namespace UnitTestCompiladorGarGar
{

    

    [TestClass]
    public class CompiladorTest
    {
        const string PROGRAMA_BASICO = "PROCEDIMIENTO SALIDA()comenzar finproc; PROCEDIMIENTO PRINCIPAL() comenzar  llamar SALIDA(); finproc;";

        const string PROGRAMA_ERRONEO = "MIENTO PRINCIPAL() comenzar  llamar SALIDA(); finproc;";

        private string ObtenerErrores(ResultadoCompilacion res)
        {
            StringBuilder strBldr = new StringBuilder();
            foreach (var item in res.ListaErrores)
            {
                strBldr.AppendLine(string.Format("{0},{1}: {2}", item.Fila, item.Columna, item.Descripcion));
            }
            return strBldr.ToString();
        }

        [TestMethod]
        public void InstanciarCompilador()
        {
            Compilador compilador = new Compilador();

            Assert.IsNotNull(compilador);
        }

        [TestMethod]
        public void CompilacionGarGarVacia()
        {
            Compilador compilador = new Compilador();

            ResultadoCompilacion res = compilador.Compilar("");

            Assert.IsFalse(res.CompilacionGarGarCorrecta);
        }

        [TestMethod]
        public void CompilacionGarGarIncorrectaConTexto()
        {
            Compilador compilador = new Compilador();

            ResultadoCompilacion res = compilador.Compilar(PROGRAMA_ERRONEO);

            Assert.IsFalse(res.CompilacionGarGarCorrecta);
        }

         [TestMethod]
        public void CompilacionGarGarCorrectaBasica()
        {
            Compilador compilador = new Compilador();

            ResultadoCompilacion res = compilador.Compilar(PROGRAMA_BASICO);

            Assert.IsTrue(res.CompilacionGarGarCorrecta, ObtenerErrores(res));
            Assert.IsTrue(res.ResultadoCompPascal.CompilacionPascalCorrecta);
        }


         [TestMethod]
         public void CompilacionCorrectaTest()
         {
             Compilador compilador = new Compilador();

             string resourceName = "UnitTestCompiladorGarGar.Programas.test.gar";
             string programa = Utilidades.FileManager.LeerArchivoEnteroDeAssembly(Assembly.GetExecutingAssembly(), resourceName);

             ResultadoCompilacion res = compilador.Compilar(programa);

             Assert.IsTrue(res.CompilacionGarGarCorrecta, ObtenerErrores(res));

             Assert.IsTrue(res.ResultadoCompPascal.CompilacionPascalCorrecta);
         }

         [TestMethod]
         public void CompilacionCorrectaDosSeguidas()
         {
             Compilador compilador = new Compilador();

             string resourceName = "UnitTestCompiladorGarGar.Programas.test.gar";
             string programa = Utilidades.FileManager.LeerArchivoEnteroDeAssembly(Assembly.GetExecutingAssembly(), resourceName);

             ResultadoCompilacion res = compilador.Compilar(programa);

             Assert.IsTrue(res.CompilacionGarGarCorrecta, ObtenerErrores(res));

             Assert.IsTrue(res.ResultadoCompPascal.CompilacionPascalCorrecta);

             ResultadoCompilacion res2 = compilador.Compilar(programa);

             Assert.IsTrue(res2.CompilacionGarGarCorrecta, ObtenerErrores(res));

             Assert.IsTrue(res2.ResultadoCompPascal.CompilacionPascalCorrecta);
         }



         [TestMethod]
         public void CompilacionCorrectaValidacionesSemanticasCorrectas()
         {
             Compilador compilador = new Compilador();

             string resourceName = "UnitTestCompiladorGarGar.Programas.validacionesSemanticasCorrectas.gar";
             string programa = Utilidades.FileManager.LeerArchivoEnteroDeAssembly(Assembly.GetExecutingAssembly(), resourceName);

             ResultadoCompilacion res = compilador.Compilar(programa);
            
             
             Assert.IsTrue(res.CompilacionGarGarCorrecta,ObtenerErrores(res));

             Assert.IsTrue(res.ResultadoCompPascal.CompilacionPascalCorrecta);
         }


         //[TestMethod]
         //public void CompilacionIncorrectaSinProcSalida()
         //{
         //    Compilador compilador = new Compilador();

         //    string resourceName = "UnitTestCompiladorGarGar.Programas.testSinProcSalida.gar";
         //    string programa = Utilidades.FileManager.LeerArchivoEnteroDeAssembly(Assembly.GetExecutingAssembly(), resourceName);

         //    ResultadoCompilacion res = compilador.Compilar(programa);

         //    Assert.IsFalse(res.CompilacionGarGarCorrecta);
         //    Assert.IsTrue(res.ListaErrores[0].TipoError == GlobalesCompilador.TipoError.Semantico);
         //    Assert.IsTrue(res.ListaErrores[0].Descripcion.Trim().ToLower() == "el procedimiento principal debe tener una llamada al procedimiento salida");
         //}


         [TestMethod]
         public void CompilacionCorrectaSinProcSalida()
         {
             Compilador compilador = new Compilador();

             string resourceName = "UnitTestCompiladorGarGar.Programas.testSinProcSalida.gar";
             string programa = Utilidades.FileManager.LeerArchivoEnteroDeAssembly(Assembly.GetExecutingAssembly(), resourceName);

             ResultadoCompilacion res = compilador.Compilar(programa);


             Assert.IsTrue(res.CompilacionGarGarCorrecta, ObtenerErrores(res));

             Assert.IsTrue(res.ResultadoCompPascal.CompilacionPascalCorrecta);
         }


    }
}
