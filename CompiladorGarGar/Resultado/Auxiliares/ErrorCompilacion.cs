using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.ErroresManager.Errores;

namespace CompiladorGargar.Resultado.Auxiliares
{
    public class ErrorCompilacion
    {

        public bool PararCompilacion { get; set; }

        public MensajeError Mensaje { get; set; }

        public int Fila { get; set; }
        public int Columna { get; set; }

        public CompiladorGargar.GlobalesCompilador.TipoError TipoError { get; set; }

        public ErrorCompilacion(MensajeError mensajeError, GlobalesCompilador.TipoError tipo, int f, int c, bool parar)
        {
            this.TipoError = tipo;
            this.Fila = f;
            this.Columna = c;
            this.PararCompilacion = parar;
            this.Mensaje = mensajeError;
        }

        public ErrorCompilacion(MensajeError mensajeError, GlobalesCompilador.TipoError tipo, int f, int c)
            : this(mensajeError,tipo,f,c,false)
        {
          
        }

    }
}
