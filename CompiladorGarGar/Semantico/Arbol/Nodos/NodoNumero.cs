using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using System.Globalization;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoNumero : NodoArbolSemantico
    {
        public NodoNumero(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            
        }

    
        public override void SintetizarAtributosANodo(NodoArbolSemantico hijoASintetizar)
        {
            this.TipoDato = hijoASintetizar.TipoDato;
            
            double valor;
            double.TryParse(hijoASintetizar.Lexema, NumberStyles.Number, new CultureInfo("en-US"), out valor);

            if (valor > GlobalesCompilador.MAX_VALOR_NUMERO || double.IsPositiveInfinity(valor))
            {
                throw new ErrorSemanticoException(string.Format("No se pueden manejar valores numericos mas grandes que {0} en tiempo de compilacion", GlobalesCompilador.MAX_VALOR_NUMERO), GlobalesCompilador.UltFila, GlobalesCompilador.UltCol);
            }
            else
            {
                if (valor < GlobalesCompilador.MIN_VALOR_NUMERO || double.IsNegativeInfinity(valor))
                {
                    throw new ErrorSemanticoException(string.Format("No se pueden manejar valores numericos mas chicos que {0} en tiempo de compilacion", GlobalesCompilador.MIN_VALOR_NUMERO), GlobalesCompilador.UltFila, GlobalesCompilador.UltCol);
                }
                else
                {
                    this.ValorConstanteNumerica = valor;
                }
            }

            this.Lexema = hijoASintetizar.Lexema;
            this.Gargar = hijoASintetizar.Gargar;
            this.NoEsAptaPasajeReferencia = true;
        }

       
      

        public override void CalcularCodigo(bool modoDebug)
        {
            StringBuilder strBldr = new StringBuilder();
            strBldr.Append(this.hijosNodo[0].Lexema);


          

            this.Codigo = strBldr.ToString();
        }
    }
}
