using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Semantico.Arbol.Nodos.Auxiliares;

namespace CompiladorGargar.Semantico.Arbol.Nodos
{
    class NodoFS : NodoArbolSemantico
    {
        public NodoFS(NodoArbolSemantico nodoPadre, ElementoGramatica elem)
            : base(nodoPadre,elem)
        {
            this.ListaFirma = new List<Firma>();
        }

     

        public override void SintetizarAtributosANodo(NodoArbolSemantico hijoASintetizar)
        {
            if (hijoASintetizar.ListaFirma != null)
            {
                this.ListaFirma.AddRange(hijoASintetizar.ListaFirma);
            }
            
        }


        public override void CalcularCodigo()
        {
            StringBuilder strBldr = new StringBuilder();

            if (this.hijosNodo.Count > 1)
            {
                strBldr.Append("; ");
                strBldr.Append(this.hijosNodo[1].Codigo);
                strBldr.Append(this.hijosNodo[2].Codigo);
            }

            this.Codigo = strBldr.ToString();
        }
    }
}
