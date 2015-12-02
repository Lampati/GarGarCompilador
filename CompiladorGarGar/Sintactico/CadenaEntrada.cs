using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Lexicografico;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Auxiliares;

namespace CompiladorGargar.Sintactico
{
    class CadenaEntrada
    {
        private List<Terminal> cadena;

        public int Count
        {
            get
            {
                return cadena.Count;
            }
        }

        public List<Terminal> CadenaEntera
        {
            get
            {
                return cadena;
            }
        }

        public Terminal UltimoTerminalInsertado
        {
            get 
            {
                if (cadena.Count > 0)
                {
                    return cadena.Last();
                }
                else
                {
                    return null;
                }
            }
        }

        public CadenaEntrada()
        {
            this.cadena = new List<Terminal>();
        }

        public Terminal ObtenerPrimerTerminal()
        {
            return this.cadena.First();
        }

        public void EliminarPrimerTerminal()
        {
            this.cadena.RemoveAt(0);
        }

        public void InsertarTerminal(Terminal t)
        {
            this.cadena.Add(t);
        }

        public override string ToString()
        {
            StringBuilder strBldr = new StringBuilder(string.Empty);

            foreach (Terminal elem in this.cadena)
            {
                strBldr.Append(elem.ToString());
                strBldr.Append(" ");
            }

            return strBldr.ToString();
        }


        internal bool EsVacia()
        {
            return this.cadena.Count == 0;
        }

        internal bool EsFinDeCadena()
        {
            if (!EsVacia())
            {
                return this.ObtenerPrimerTerminal().Componente.Token == ComponenteLexico.TokenType.EOF;
            }
            else
            {
                return false;
            }
        }

        internal bool TieneTerminalRepetidoEnPrimerLugarErroneo(int cantParentesisAbiertos)
        {
            if (!EsVacia())
            {
                if (this.ObtenerPrimerTerminal().Componente.Token != ComponenteLexico.TokenType.ParentesisApertura
                    && this.ObtenerPrimerTerminal().Componente.Token != ComponenteLexico.TokenType.ParentesisClausura
                    && this.ObtenerPrimerTerminal().Componente.Token != ComponenteLexico.TokenType.FinSentencia
                    )
                {
                    if (this.cadena.Count > 1)
                    {
                        return this.cadena[0].Componente.Token == this.cadena[1].Componente.Token;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (this.ObtenerPrimerTerminal().Componente.Token == ComponenteLexico.TokenType.ParentesisClausura
                        && cantParentesisAbiertos <= 0)
                    {
                        if (this.cadena.Count > 1)
                        {
                            return this.cadena[0].Componente.Token == this.cadena[1].Componente.Token;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                    
                }
            }
            else
            {
                return false;
            }
        }
    }
}
