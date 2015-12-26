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
            : base(mensErr.Mensaje)
        {
            this.Tipo = "Semantico";

            //Cambiado a partir de ahora, toma de global
            this.Fila = GlobalesCompilador.UltFila;
            this.Columna = GlobalesCompilador.UltCol;

            Mensaje = mensErr;
        }

         

    }
}
