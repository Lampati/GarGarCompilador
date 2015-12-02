using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Sintactico
{
    internal static class TerminalesHelpers
    {

        internal static bool EsParentesis(Terminal t)
        {
            return (t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.ParentesisApertura
                 || t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.ParentesisClausura
                 );
        }


        internal static bool EsOperador(Terminal t)
        {
            return (t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.SumaEntero
                 || t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.RestaEntero
                 || t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.MultiplicacionEntero
                 || t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.DivisionEntero
                 || t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.And
                 || t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Or
                 || t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Concatenacion
                 || t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Menor
                 || t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.MenorIgual
                 || t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Igual
                 || t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Distinto
                 || t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Mayor
                 || t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.MayorIgual
                 );
        }


        internal static bool EsTerminalConValor(Terminal t)
        {
            return (t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Numero
                 || t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Literal
                 || t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Verdadero
                 || t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Falso
                 || t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Identificador
                 );
        }

        internal static bool EsTerminalConValorConstante(Terminal t)
        {
            return (t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Numero
                 || t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Literal
                 || t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Verdadero
                 || t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Falso
                 );
        }

        internal static bool EsTipoDeDato(Terminal t)
        {
            return (t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.TipoNumero
                 || t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.TipoBooleano
                 || t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.TipoTexto
                 );
        }

        internal static bool EsId(Terminal t)
        {
            return (t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Identificador);
        }


        internal static bool EsTerminalFinBloque(Terminal t)
        {
            return (t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.MientrasFin
               || t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.SiFin
               || t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.SiSino
               || t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.ProcedimientoFin
               || t.Componente.Token == Lexicografico.ComponenteLexico.TokenType.FuncionFin
               );
        }

        
    }
}
