﻿using System;
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
    public class CompiladorTest
    {
        const string PROGRAMA_BASICO = "PROCEDIMIENTO SALIDA()comenzar finproc; PROCEDIMIENTO PRINCIPAL() comenzar  llamar SALIDA(); finproc;";

        const string PROGRAMA_ERRONEO = "MIENTO PRINCIPAL() comenzar  llamar SALIDA(); finproc;";

        const string RESULTADO_CORRECTO_EJECUCION = "OK";

        private string ObtenerErrores(ResultadoCompilacion res)
        {
            StringBuilder strBldr = new StringBuilder();
            foreach (var item in res.ListaErrores)
            {
                strBldr.AppendLine(string.Format("{0},{1}: {2}", item.Fila, item.Columna, item.Mensaje.Descripcion));
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
            Compilador compilador = new Compilador(true, "basica");

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
             Compilador compilador = new Compilador(true, "test");

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
             Compilador compilador = new Compilador(true, "validacionesSemanticasCorrectas");

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
         public void CompilacionMenorTresSegundos()
         {
             Compilador compilador = new Compilador("validacionesSemanticasCorrectas");

             string resourceName = "UnitTestCompiladorGarGar.Programas.validacionesSemanticasCorrectas.gar";
             string programa = Utilidades.FileManager.LeerArchivoEnteroDeAssembly(Assembly.GetExecutingAssembly(), resourceName);

             ResultadoCompilacion res = compilador.Compilar(programa);


             Assert.IsTrue(res.CompilacionGarGarCorrecta, ObtenerErrores(res));

             Assert.IsTrue(res.ResultadoCompPascal.CompilacionPascalCorrecta);

             Assert.IsTrue(res.TiempoCompilacionTotal < 3);
         }

         [TestMethod]
         public void CompilacionCorrectaSinProcSalida()
         {
             string resourceName = "UnitTestCompiladorGarGar.Programas.testSinProcSalida.gar";
             Compilador compilador = new Compilador(true, "testSinProcSalida");

             string programa = Utilidades.FileManager.LeerArchivoEnteroDeAssembly(Assembly.GetExecutingAssembly(), resourceName);

             ResultadoCompilacion res = compilador.Compilar(programa);


             Assert.IsTrue(res.CompilacionGarGarCorrecta, ObtenerErrores(res));

             Assert.IsTrue(res.ResultadoCompPascal.CompilacionPascalCorrecta);
         }

        [TestMethod]
         public void CompilacionErrorLexico()
         {
             

             string resourceName = "UnitTestCompiladorGarGar.Programas.testErrorLexico2.gar";

             Compilador compilador = new Compilador(true, resourceName);
             string programa = Utilidades.FileManager.LeerArchivoEnteroDeAssembly(Assembly.GetExecutingAssembly(), resourceName);

             ResultadoCompilacion res = compilador.Compilar(programa);

             

             Assert.IsTrue(res.ListaErrores[0].Mensaje.CodigoGlobal == 43);
         }


        [TestMethod]
        public void CompilacionErrorLexicoPrimerCaracter()
        {
            Compilador compilador = new Compilador();

            string resourceName = "UnitTestCompiladorGarGar.Programas.testErrorLexico.gar";
            string programa = Utilidades.FileManager.LeerArchivoEnteroDeAssembly(Assembly.GetExecutingAssembly(), resourceName);

            ResultadoCompilacion res = compilador.Compilar(programa);



            Assert.IsTrue(res.ListaErrores[0].Mensaje.CodigoGlobal == 43);
        }




        [TestMethod]
        public void CompilarLecturaCinco()
        {
            Compilador compilador = new Compilador(true, "pruebaLecturaCinco");

            string resourceName = "UnitTestCompiladorGarGar.Programas.pruebaLecturaCinco.gar";
            string programa = Utilidades.FileManager.LeerArchivoEnteroDeAssembly(Assembly.GetExecutingAssembly(), resourceName);

            ResultadoCompilacion res = compilador.Compilar(programa);

            Assert.IsTrue(res.CompilacionGarGarCorrecta, ObtenerErrores(res));

            Assert.IsTrue(res.ResultadoCompPascal.CompilacionPascalCorrecta);

        }

        [TestMethod]
        public void CompilarMientrasTest()
        {
            Compilador compilador = new Compilador(true, "ejecucionMientrasTest");

            string resourceName = "UnitTestCompiladorGarGar.Programas.ejecucionMientrasTest.gar";
            string programa = Utilidades.FileManager.LeerArchivoEnteroDeAssembly(Assembly.GetExecutingAssembly(), resourceName);

            ResultadoCompilacion res = compilador.Compilar(programa);

            Assert.IsTrue(res.CompilacionGarGarCorrecta, ObtenerErrores(res));

            Assert.IsTrue(res.ResultadoCompPascal.CompilacionPascalCorrecta);

        }

        
    }
}
