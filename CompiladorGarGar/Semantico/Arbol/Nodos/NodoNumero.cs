using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using System.Globalization;
using CompiladorGargar.Sintactico.ErroresManager.Errores;

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
                throw new ErrorSemanticoException(new ErrorValorNumericoMuyGrande(GlobalesCompilador.MAX_VALOR_NUMERO));
            }
            else
            {
                if (valor < GlobalesCompilador.MIN_VALOR_NUMERO || double.IsNegativeInfinity(valor))
                {
                    throw new ErrorSemanticoException(new ErrorValorNumericoMuyChico(GlobalesCompilador.MIN_VALOR_NUMERO));
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
