using CompiladorGargar.Sintactico.ErroresManager.Errores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorGargar.Semantico
{
    class ErrorSemanticoException : ErrorCompilacionException
    {
      
        public ErrorSemanticoException(MensajeError mensErr)
            : base(mensErr.Descripcion, GlobalesCompilador.UltFila, GlobalesCompilador.UltCol)
        {
            this.Tipo = "Semantico";

            Mensaje = mensErr;
        }

         

    }
}
