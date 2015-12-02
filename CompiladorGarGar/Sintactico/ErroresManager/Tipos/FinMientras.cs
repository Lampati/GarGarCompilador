using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Sintactico.ErroresManager.Errores;

namespace CompiladorGargar.Sintactico.ErroresManager.Tipos
{
    class FinMientras : TipoBase
    {
        public FinMientras(List<Terminal> lista, int fila, int col) 
            : base(fila,col)
        {
            listaLineaEntera = lista;

            AgregarValidacionFin();
        }


        private void AgregarValidacionFin()
        {
            MensajeError mensajeError = new ErrorFinMientrasValidacionFin();
            short importancia = 10;

            Validacion valRep = new Validacion(listaLineaEntera, mensajeError, importancia, ValidacionesFactory.ValidarFinMientras, FilaDelError, ColumnaDelError);

            listaValidaciones.Add(valRep);
        }
    }
}
