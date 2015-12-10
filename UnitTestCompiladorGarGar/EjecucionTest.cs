using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
using CompiladorGargar;
using CompiladorGargar.Resultado;
using System.Reflection;
using System.Diagnostics;
using System.IO;


namespace UnitTestCompiladorGarGar
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class EjecucionTest
    {

        private string ObtenerResultadoArchivo()
        {
            string resultado = string.Empty;
            string archivo = Path.Combine(TestContext.TestDeploymentDir, "resultado.txt");

            using (StreamReader sr = File.OpenText(archivo))
            {
                resultado = sr.ReadToEnd();
            }

            return resultado;
        }


        public EjecucionTest()
        {
        }

        [TestMethod]
        public void EjecucionBasicaTest()
        {            
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = @"C:\Users\FEDE\AppData\Local\Temp\basica.exe";
            
         
            using (ApplicationUnderTest app = ApplicationUnderTest.Launch(start))
            {
                app.SetFocus();

                Keyboard.SendKeysDelay = 2000; //2 segundos
                       
                //Keyboard.SendKeys("5");
                Keyboard.SendKeys("{ENTER}");     
            }

            string resultado = ObtenerResultadoArchivo();

            Assert.IsTrue(resultado == "");
            
        }

        [TestMethod]
        public void EjecucionPruebaLecturaCincoTest()
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = @"C:\Users\FEDE\AppData\Local\Temp\pruebaLecturaCinco.exe";


            using (ApplicationUnderTest app = ApplicationUnderTest.Launch(start))
            {
                app.SetFocus();

                Keyboard.SendKeysDelay = 2000; //2 segundos

                Keyboard.SendKeys("5");
                Keyboard.SendKeys("{ENTER}");


            }

            string resultado = ObtenerResultadoArchivo();

            Assert.IsTrue(resultado.Contains("OK"));

         
        }



        #region Additional test attributes

        // You can use the following additional attributes as you write your tests:

        ////Use TestInitialize to run code before running each test 
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        ////Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        #endregion

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        private TestContext testContextInstance;
    }
}
