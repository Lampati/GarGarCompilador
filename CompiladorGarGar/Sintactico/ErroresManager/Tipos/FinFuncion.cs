using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Sintactico.ErroresManager.Errores;

namespace CompiladorGargar.Sintactico.ErroresManager.Tipos
{
    class FinFuncion : TipoBase
    {
        public FinFuncion(List<Terminal> listaEntera, List<Terminal> listaHastaAhora, int fila, int col)
            : base(listaEntera, listaHastaAhora, fila, col)
        {         


            AgregarValidacionFin();

            AgregarValidacionPorDefault();
        }

        private void AgregarValidacionPorDefault()
        {
            MensajeError mensajeError = new ErrorFinFuncValidacionPorDefault();
            short importancia = 1;


            Validacion valRep = new Validacion(listaLineaEntera, mensajeError, importancia, ValidacionesFactory.ForzarFalso, FilaDelError, ColumnaDelError);
            listaValidaciones.Add(valRep);
        }


        private void AgregarValidacionFin()
        {
            MensajeError mensajeError = new ErrorFinFuncValidacionFin();
            short importancia = 10;

            Validacion valRep = new Validacion(listaLineaEntera, mensajeError, importancia, ValidacionesFactory.ValidarFinProc, FilaDelError, ColumnaDelError);

            listaValidaciones.Add(valRep);
        }
    }
}
