using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico;
using CompiladorGargar.Lexicografico;

namespace CompiladorGargar
{
    public class Identador
    {
        private AnalizadorLexicografico analizadorLexico;
        private short cantTabs;

        private bool esNuevaLinea;
        private bool esNuevaLineaDiferido;
        private bool generaLineaSeparadora;

        public Identador(string codigo)
        {
            analizadorLexico = new AnalizadorLexicograficoParaIdentar();
            analizadorLexico.CargarTexto(codigo);
            cantTabs = 0;
            esNuevaLinea = false;
            generaLineaSeparadora = false;
        }

        
        public string Identar()
        {
            StringBuilder strBlder = new StringBuilder();

            ComponenteLexico comp = analizadorLexico.ObtenerProximoToken();

            while (comp.Token != ComponenteLexico.TokenType.EOF)
            {
                if (esNuevaLineaDiferido && GeneraLineaNuevaDiferida(comp.Token))
                {
                    strBlder.AppendLine();
                    esNuevaLineaDiferido = false;
                }

                if (GeneraLineaSeparadora(comp.Token))
                {
                    strBlder.AppendLine();
                }

                if (GeneraMenosIdentacion(comp.Token))
                {
                    cantTabs--;
                }

                if (esNuevaLinea)
                {
                    for (int i = 0; i < cantTabs; i++)
                    {
                        strBlder.Append("\t");
                    }
                    esNuevaLinea = false;
                }

                strBlder.Append(comp.Lexema);
                strBlder.Append(" ");


                if (GeneraNuevaLinea(comp.Token))
                {
                    strBlder.AppendLine();
                    esNuevaLinea = true;
                }

                if (GeneraNuevaLineaDiferido(comp.Token))
                {
                    esNuevaLineaDiferido = true;
                }

                if (GeneraMasIdentacion(comp.Token))
                {
                    cantTabs++;
                }

              
                

                comp = analizadorLexico.ObtenerProximoToken();
            }

            return strBlder.ToString();
        }

        private bool GeneraLineaNuevaDiferida(ComponenteLexico.TokenType tokenType)
        {
            return (tokenType == Lexicografico.ComponenteLexico.TokenType.Var
           || tokenType == Lexicografico.ComponenteLexico.TokenType.Const
           || tokenType == Lexicografico.ComponenteLexico.TokenType.Comenzar
           );
        }

        private bool GeneraNuevaLineaDiferido(ComponenteLexico.TokenType tokenType)
        {
            return (tokenType == Lexicografico.ComponenteLexico.TokenType.ProcedimientoComienzo
            || tokenType == Lexicografico.ComponenteLexico.TokenType.FuncionComienzo
            );
        }

        private bool GeneraLineaSeparadora(ComponenteLexico.TokenType tokenType)
        {
            return (tokenType == Lexicografico.ComponenteLexico.TokenType.ProcedimientoComienzo
            || tokenType == Lexicografico.ComponenteLexico.TokenType.FuncionComienzo
            || tokenType == Lexicografico.ComponenteLexico.TokenType.Variables
            );
        }

        private static bool GeneraNuevaLinea(Lexicografico.ComponenteLexico.TokenType tokenType)
        {
            return (tokenType == Lexicografico.ComponenteLexico.TokenType.FinSentencia
                || tokenType == Lexicografico.ComponenteLexico.TokenType.Comenzar
              || tokenType == Lexicografico.ComponenteLexico.TokenType.SiEntonces
              || tokenType == Lexicografico.ComponenteLexico.TokenType.SiSino
              || tokenType == Lexicografico.ComponenteLexico.TokenType.MientrasHacer
              || tokenType == Lexicografico.ComponenteLexico.TokenType.Variables
              || tokenType == Lexicografico.ComponenteLexico.TokenType.Constantes
              || tokenType == Lexicografico.ComponenteLexico.TokenType.Comentario
              );
        }

        private static bool GeneraMasIdentacion(Lexicografico.ComponenteLexico.TokenType tokenType)
        {
            return (tokenType == Lexicografico.ComponenteLexico.TokenType.Comenzar
             || tokenType == Lexicografico.ComponenteLexico.TokenType.SiEntonces
             || tokenType == Lexicografico.ComponenteLexico.TokenType.SiSino
             || tokenType == Lexicografico.ComponenteLexico.TokenType.MientrasHacer
             );
        }

        private static bool GeneraMenosIdentacion(Lexicografico.ComponenteLexico.TokenType tokenType)
        {
            return (tokenType == Lexicografico.ComponenteLexico.TokenType.ProcedimientoFin
            || tokenType == Lexicografico.ComponenteLexico.TokenType.FuncionFin
            || tokenType == Lexicografico.ComponenteLexico.TokenType.SiSino
            || tokenType == Lexicografico.ComponenteLexico.TokenType.SiFin
            || tokenType == Lexicografico.ComponenteLexico.TokenType.MientrasFin
            );
        }
    }
}
