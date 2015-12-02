using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompiladorGargar.Resultado.Auxiliares
{
    public class PasoCompilacion
    {
        public PasoCompilacion(string pila, string cadena, CompiladorGargar.GlobalesCompilador.TipoError tipoError)
        {
            this.ContenidoPila = pila;
            this.EstadoCadenaEntrada = cadena;

            this.TipoError = tipoError;


        }

        public string ContenidoPila { get; set; }
        public string EstadoCadenaEntrada { get; set; }
        public CompiladorGargar.GlobalesCompilador.TipoError TipoError { get; set; }
    }
}
