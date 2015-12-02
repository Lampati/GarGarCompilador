using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Sintactico.ErroresManager.Errores;

namespace CompiladorGargar.Sintactico.ErroresManager.Tipos
{
    class FinProc : TipoBase
    {
        public FinProc(List<Terminal> lista, int fila, int col) 
            : base(fila,col)
        {
            listaLineaEntera = lista;

            AgregarValidacionFin();

        }


        private void AgregarValidacionFin()
        {
            MensajeError mensajeError = new ErrorFinProcValidacionFin();
            short importancia = 10;

            Validacion valRep = new Validacion(listaLineaEntera, mensajeError, importancia, ValidacionesFactory.ValidarFinProc, FilaDelError, ColumnaDelError);

            listaValidaciones.Add(valRep);
        }
    }
}
