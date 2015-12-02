using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Sintactico.ErroresManager.Errores;

namespace CompiladorGargar.Sintactico.ErroresManager.Tipos
{
    class DeclaracionProc : TipoBase
    {
        public DeclaracionProc(List<Terminal> lista, int fila, int col) 
            : base(fila,col)
        {
            listaLineaEntera = lista;


            AgregarValidacionPorDefault();
        }

        private void AgregarValidacionPorDefault()
        {
            MensajeError mensajeError = new ErrorDeclaracionProcedimientoValidacionPorDefault();
            short importancia = 1;


            Validacion valRep = new Validacion(listaLineaEntera, mensajeError, importancia, ValidacionesFactory.ForzarFalso, FilaDelError, ColumnaDelError);
            listaValidaciones.Add(valRep);
        }
    }
}
