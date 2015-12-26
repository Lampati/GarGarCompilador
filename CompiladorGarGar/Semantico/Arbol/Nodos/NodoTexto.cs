using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Sintactico.ErroresManager.Errores;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoTexto : NodoArbolSemantico
    {
        public NodoTexto(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }

        

        public override void SintetizarAtributosANodo(NodoArbolSemantico hijoASintetizar)
        {
            this.Lexema = hijoASintetizar.Lexema;
          
            this.TipoDato = hijoASintetizar.TipoDato;

            this.Gargar = hijoASintetizar.Gargar;
            this.NoEsAptaPasajeReferencia = true;

            if (Lexema.Length > GlobalesCompilador.MAX_LONG_CADENA)
            {
                throw new ErrorSemanticoException(new ErrorCantMaxCaracteresEnCadenaTexto( GlobalesCompilador.MAX_LONG_CADENA));
            }

        }

      

        public override void CalcularCodigo(bool modoDebug)
        {
            StringBuilder strBldr = new StringBuilder();


            strBldr.Append(this.hijosNodo[0].Lexema);
            

            this.Codigo = strBldr.ToString();
        }
    }
}
