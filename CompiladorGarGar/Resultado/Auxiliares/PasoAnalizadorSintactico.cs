using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.ErroresManager.Errores;

namespace CompiladorGargar.Resultado.Auxiliares
{
    public class PasoAnalizadorSintactico
    {
        private string mensajeAMostrar;
        private int filaAMostrar;
        private int colAMostrar;
        private Sintactico.ErroresManager.Errores.MensajeError mensajeError;

        public PasoAnalizadorSintactico()
        {

        }

        public PasoAnalizadorSintactico(string desc, GlobalesCompilador.TipoError tipo, int f, int c, bool parar)
        {
            this.Descripcion = desc;
            this.TipoError = tipo;
            this.Fila = f;
            this.Columna = c;
            this.PararCompilacion = parar;
        }

        public PasoAnalizadorSintactico(string desc, GlobalesCompilador.TipoError tipo, int f, int c)
        {
            this.Descripcion = desc;
            this.TipoError = tipo;
            this.Fila = f;
            this.Columna = c;
            this.PararCompilacion = false;
        }

        public PasoAnalizadorSintactico(string mensajeAMostrar, GlobalesCompilador.TipoError tipoError, int filaAMostrar, int colAMostrar, bool pararCompilacion, Sintactico.ErroresManager.Errores.MensajeError mensajeError)
        {
            
            this.TipoError = tipoError;
            this.Fila = filaAMostrar;
            this.Columna = colAMostrar;
            this.PararCompilacion = pararCompilacion;
            this.MensajeError = mensajeError;

            this.Descripcion = mensajeAMostrar;
        }

        public string Descripcion { get; set; }
        public bool PararCompilacion { get; set; }

        public MensajeError MensajeError { get; set; }

        public int Fila { get; set; }
        public int Columna { get; set; }

        public CompiladorGargar.GlobalesCompilador.TipoError TipoError { get; set; }
    }
}
