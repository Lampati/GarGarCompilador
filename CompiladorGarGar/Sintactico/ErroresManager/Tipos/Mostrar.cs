using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Sintactico.ErroresManager.Errores;

namespace CompiladorGargar.Sintactico.ErroresManager.Tipos
{
    class Mostrar : TipoBase
    {
        public Mostrar(List<Terminal> listaEntera, List<Terminal> listaHastaAhora, int fila, int col)
            : base(listaEntera, listaHastaAhora, fila, col)
        {         


            AgregarValidacionPorDefault();
        }

    


        private void AgregarValidacionPorDefault()
        {
            MensajeError mensajeError = new ErrorMostrarValidacionPorDefault();
            short importancia = 1;


            Validacion valRep = new Validacion(listaLineaEntera, mensajeError, importancia, ValidacionesFactory.ForzarFalso, FilaDelError, ColumnaDelError);
            listaValidaciones.Add(valRep);
        }
    }
}
