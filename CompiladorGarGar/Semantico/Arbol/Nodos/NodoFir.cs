using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Semantico.Arbol.Nodos.Auxiliares;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoFir : NodoArbolSemantico
    {
        public NodoFir(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            this.ListaFirma = new List<Firma>();
        }

        public override void HeredarAtributosANodo(NodoArbolSemantico hijoAHeredar)
        {
    
        }

        public override void SintetizarAtributosANodo(NodoArbolSemantico hijoASintetizar)
        {
            this.ListaFirma.AddRange(hijoASintetizar.ListaFirma);
        }

        public override void ChequearAtributos(Terminal t)
        {
            
        }

        public override NodoArbolSemantico SalvarAtributosParaContinuar()
        {
            return this;
        }

        public override void CalcularCodigo()
        {
            StringBuilder strBldr = new StringBuilder();           
            strBldr.Append(this.hijosNodo[0].Codigo);            
            strBldr.Append(this.hijosNodo[1].Codigo);   
            this.Codigo = strBldr.ToString();
        }
    }
}
