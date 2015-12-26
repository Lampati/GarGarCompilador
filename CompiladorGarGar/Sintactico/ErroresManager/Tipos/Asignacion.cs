using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Sintactico.ErroresManager.Errores;

namespace CompiladorGargar.Sintactico.ErroresManager.Tipos
{
    class Asignacion : TipoBase
    {

        // flanzani 9/11/2012
        // IDC_APP_3
        // Cambiar el := por =
        // Cambio el componenteLexico por el Igual, ya que ahora es el que indica asignacion EN TODO EL DOCUMENTO
        public Asignacion(List<Terminal> listaEntera, List<Terminal> listaHastaAhora, int fila, int col)
            : base(listaEntera, listaHastaAhora, fila, col)
        {         


            // flanzani 9/11/2012
            // IDC_APP_3
            // Cambiar el := por =
            // Esta validaciones queda comentada ya que al ser compartido el lexema de asignacion, es correcto que quede repetido
            //AgregarValidacionAsignacionRepetido();

            AgregarValidacionAsignacionFaltante();

            AgregarValidacionCorchetesBalanceadosParteDer();
            AgregarValidacionCorchetesBalanceadosParteIzq();
            AgregarValidacionParentesisBalanceadosParteDer();
            AgregarValidacionParentesisBalanceadosParteIzq();
            AgregarValidacionParteIzqCorrecta();
            AgregarValidacionTerminaCorrectamente();

            //AgregarValidacionElementosConValorNoContiguosParteIzq();
            //AgregarValidacionElementosConValorNoContiguosParteDer();

            AgregarValidacionPorDefault();
        }

        private void AgregarValidacionAsignacionRepetido()
        {
            MensajeError mensajeError = new ErrorAsignacionRepetido();
            short importancia = 10;

            Validacion valRep = new Validacion(listaLineaEntera, mensajeError, importancia, ValidacionesFactory.AsignacionRepetido, FilaDelError, ColumnaDelError);

            listaValidaciones.Add(valRep);
        }


        

        private void AgregarValidacionAsignacionFaltante()
        {
            MensajeError mensajeError = new ErrorAsignacionFaltante();
            short importancia = 9;

            Validacion valRep = new Validacion(listaLineaEntera, mensajeError, importancia, ValidacionesFactory.AsignacionFaltante, FilaDelError, ColumnaDelError);

            listaValidaciones.Add(valRep);
        }

        
           private void AgregarValidacionParteIzqCorrecta()
        {
            MensajeError mensajeError = new ErrorAsignacionParteIzqCorrecta();
            short importancia = 3;

            List<Terminal> parteIzq = ArmarSubListaIzquierdaDe(listaLineaEntera, Lexicografico.ComponenteLexico.TokenType.Igual); 

            Validacion valRep = new Validacion(listaLineaHastaAhora, mensajeError, importancia, ValidacionesFactory.AsignacionParteIzqCorrecta, FilaDelError, ColumnaDelError);

            listaValidaciones.Add(valRep);
        }

        private void AgregarValidacionTerminaCorrectamente()
        {
            MensajeError mensajeError = new ErrorAsignacionParteDerechaCorrecta();
            short importancia = 2;

            List<Terminal> parteDer = ArmarSubListaDerechaDe(listaLineaEntera, Lexicografico.ComponenteLexico.TokenType.Igual);

            Validacion valRep = new Validacion(parteDer, listaLineaHastaAhora, mensajeError, importancia, ValidacionesFactory.AsignacionParteDerechaCorrecta, FilaDelError, ColumnaDelError);

            listaValidaciones.Add(valRep);
        }

     
        private void AgregarValidacionParentesisBalanceadosParteIzq()
        {
            MensajeError mensajeError = new ErrorAsignacionParentesisBalanceadosParteIzq();
            short importancia = 6;

            List<Terminal> parteIzq = ArmarSubListaIzquierdaDe(listaLineaEntera, Lexicografico.ComponenteLexico.TokenType.Igual); 

            Validacion valRep = new Validacion(parteIzq, mensajeError, importancia, ValidacionesFactory.ParentesisBalanceados, FilaDelError, ColumnaDelError);
            listaValidaciones.Add(valRep);            
        }

       

        private void AgregarValidacionParentesisBalanceadosParteDer()
        {
            MensajeError mensajeError = new ErrorAsignacionParentesisBalanceadosParteDer();
            short importancia = 6;

            List<Terminal> parteDer = ArmarSubListaDerechaDe(listaLineaEntera, Lexicografico.ComponenteLexico.TokenType.Igual);            

            Validacion valRep = new Validacion(parteDer, mensajeError, importancia, ValidacionesFactory.ParentesisBalanceados, FilaDelError, ColumnaDelError);
            listaValidaciones.Add(valRep);
        }

        private void AgregarValidacionCorchetesBalanceadosParteIzq()
        {
            MensajeError mensajeError = new ErrorAsignacionCorchetesBalanceadosParteIzq();
            short importancia = 5;

            List<Terminal> parteIzq = ArmarSubListaIzquierdaDe(listaLineaEntera, Lexicografico.ComponenteLexico.TokenType.Igual); 

            Validacion valRep = new Validacion(parteIzq, mensajeError, importancia, ValidacionesFactory.CorchetesBalanceados, FilaDelError, ColumnaDelError);
            listaValidaciones.Add(valRep);
        }

        private void AgregarValidacionCorchetesBalanceadosParteDer()
        {
            MensajeError mensajeError = new ErrorAsignacionCorchetesBalanceadosParteDer();
            short importancia = 5;

            List<Terminal> parteDer = ArmarSubListaDerechaDe(listaLineaEntera, Lexicografico.ComponenteLexico.TokenType.Igual);            

            Validacion valRep = new Validacion(parteDer, mensajeError, importancia, ValidacionesFactory.CorchetesBalanceados, FilaDelError, ColumnaDelError);
            listaValidaciones.Add(valRep);
        }

        private void AgregarValidacionElementosConValorNoContiguosParteIzq()
        {
            MensajeError mensajeError = new ErrorAsignacionElementosConValorNoContiguosParteIzq();
            short importancia = 4;

            List<Terminal> parteIzq = ArmarSubListaIzquierdaDe(listaLineaEntera, Lexicografico.ComponenteLexico.TokenType.Igual); 

            Validacion valRep = new Validacion(parteIzq, mensajeError, importancia, ValidacionesFactory.ElementosConValorNoContiguos, FilaDelError, ColumnaDelError);
            listaValidaciones.Add(valRep);
        }

        private void AgregarValidacionElementosConValorNoContiguosParteDer()
        {
            MensajeError mensajeError = new ErrorAsignacionElementosConValorNoContiguosParteDer();
            short importancia = 4;

            List<Terminal> parteDer = ArmarSubListaDerechaDe(listaLineaEntera, Lexicografico.ComponenteLexico.TokenType.Igual);            

            Validacion valRep = new Validacion(parteDer, mensajeError, importancia, ValidacionesFactory.ElementosConValorNoContiguos, FilaDelError, ColumnaDelError);
            listaValidaciones.Add(valRep);
        }


        private void AgregarValidacionPorDefault()
        {
            MensajeError mensajeError = new ErrorAsignacionValidacionPorDefault();
            short importancia = 1;


            Validacion valRep = new Validacion(listaLineaEntera, mensajeError, importancia, ValidacionesFactory.ForzarFalso, FilaDelError, ColumnaDelError);
            listaValidaciones.Add(valRep);
        }



       
    }
}
