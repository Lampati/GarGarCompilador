using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;

namespace CompiladorGargar.Sintactico.ErroresManager
{
    internal static class ValidacionesFactory
    {
        private delegate bool ChequeosTerminalesDelegate(Terminal x);


        #region Declaraciones de constantes y variables
        internal static bool ValidarDefTipoDatoFaltante(List<Terminal> lista, List<Terminal> listaHastaAhora)
        {
            int cantidad = lista.FindAll(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.TipoDato).Count;

            return cantidad > 0;
        }

        internal static bool ValidarDefTipoDatoRepetido(List<Terminal> lista, List<Terminal> listaHastaAhora)
        {
            int cantidad = lista.FindAll(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.TipoDato).Count;

            return cantidad < 2;
        }

        internal static bool ValidarTipoDatoFaltante(List<Terminal> lista, List<Terminal> listaHastaAhora)
        {
            int cantidad = lista.FindAll(x => TerminalesHelpers.EsTipoDeDato(x)).Count;

            return cantidad > 0;
        }

        internal static bool ValidarTipoDatoRepetido(List<Terminal> lista, List<Terminal> listaHastaAhora)
        {
            int cantidad = lista.FindAll(x => TerminalesHelpers.EsTipoDeDato(x)).Count;

            return cantidad < 2;
        }

        internal static bool ValidarAsignacionConstanteFaltante(List<Terminal> lista, List<Terminal> listaHastaAhora)
        {
            int cantidad = lista.FindAll(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Igual).Count;

            return cantidad > 0;
        }

        internal static bool ValidarAsignacionConstanteRepetido(List<Terminal> lista, List<Terminal> listaHastaAhora)
        {
            int cantidad = lista.FindAll(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Igual).Count;

            return cantidad < 2;
        }

        internal static bool ValidarAsignacionValorConstanteFaltante(List<Terminal> lista, List<Terminal> listaHastaAhora)
        {
            int cantidad = lista.FindAll(x => TerminalesHelpers.EsTerminalConValorConstante(x)).Count;

            return cantidad > 0;
        }

        internal static bool ValidarAsignacionValorConstanteRepetido(List<Terminal> lista, List<Terminal> listaHastaAhora)
        {
            int cantidad = lista.FindAll(x => TerminalesHelpers.EsTerminalConValorConstante(x)).Count;

            return cantidad < 2;
        }

        internal static bool ValidarDefTipoDatoSinArreglo(List<Terminal> lista, List<Terminal> listaHastaAhora)
        {
            bool retorno = true;

            if (lista.Count > 0)
            {
                retorno = !(lista[0].Componente.Token == Lexicografico.ComponenteLexico.TokenType.Arreglo);
            }

            return retorno;
        }

        internal static bool ValidarEstarDefiniendoSoloUnID(List<Terminal> lista, List<Terminal> listaHastaAhora)
        {
            int cantidad = lista.FindAll(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.TipoDato).Count;

            return cantidad < 2;
        }

        internal static bool IdRepetido(List<Terminal> lista, List<Terminal> listaHastaAhora)
        {
            int cantidad = lista.FindAll(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Identificador).Count;

            return cantidad > 0;
        }

        internal static bool IdFaltante(List<Terminal> lista, List<Terminal> listaHastaAhora)
        {
            int cantidad = lista.FindAll(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Identificador).Count;

            return cantidad > 0;
        }

        internal static bool CantidadIdsCorrectaYOrdenadosPorComas(List<Terminal> lista, List<Terminal> listaHastaAhora)
        {
            bool retorno = true;

            if (lista.Count > 1)
            {

                List<Terminal> listaComas = lista.FindAll(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Coma);

                return ChequeoContiguosIguales(lista, listaComas, TerminalesHelpers.EsId);
            }
            else
            {
                if (lista.Count > 0)
                {
                    retorno = lista[0].Componente.Token == Lexicografico.ComponenteLexico.TokenType.Identificador;
                }
                else
                {
                    retorno = false;
                }
            }
            return retorno;
        }

        internal static bool ArregloRepetido(List<Terminal> lista, List<Terminal> listaHastaAhora)
        {
            int cantidad = lista.FindAll(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Arreglo).Count;

            return cantidad == 0 || cantidad == 1;
        }

        

        #endregion



        internal static bool ValidarFinMientras(List<Terminal> lista, List<Terminal> listaHastaAhora)
        {
            bool retorno = lista.Count == 2 
                && lista[0].Componente.Token == Lexicografico.ComponenteLexico.TokenType.MientrasFin
                && lista[1].Componente.Token == Lexicografico.ComponenteLexico.TokenType.FinSentencia;        

            return retorno;
        }

        internal static bool ValidarFinSi(List<Terminal> lista, List<Terminal> listaHastaAhora)
        {
            bool retorno = lista.Count == 2
                && lista[0].Componente.Token == Lexicografico.ComponenteLexico.TokenType.SiFin
                && lista[1].Componente.Token == Lexicografico.ComponenteLexico.TokenType.FinSentencia;

            return retorno;
        }


        internal static bool ValidarFinProc(List<Terminal> lista, List<Terminal> listaHastaAhora)
        {
            bool retorno = lista.Count == 2
                && lista[0].Componente.Token == Lexicografico.ComponenteLexico.TokenType.ProcedimientoFin
                && lista[1].Componente.Token == Lexicografico.ComponenteLexico.TokenType.FinSentencia;

            return retorno;
        }

        internal static bool AsignacionRepetido(List<Terminal> lista, List<Terminal> listaHastaAhora)
        {
            // flanzani 9/11/2012
            // IDC_APP_3
            // Cambiar el := por =
            // Cambio el componenteLexico por el Igual, ya que ahora es el que indica asignacion
            int cantidad = lista.FindAll(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Igual).Count;

            return cantidad < 2;
        }

       

        internal static bool AsignacionFaltante(List<Terminal> lista, List<Terminal> listaHastaAhora)
        {
            // flanzani 9/11/2012
            // IDC_APP_3
            // Cambiar el := por =
            // Cambio el componenteLexico por el Igual, ya que ahora es el que indica asignacion
            int cantidad = lista.FindAll(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Igual).Count;

            return cantidad > 0;
        }

        

        internal static bool AsignacionParteDerechaCorrecta(List<Terminal> lista, List<Terminal> listaHastaAhora)
        {
            int cantidad = lista.FindAll(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Igual).Count;

            bool ultElementoFinSentencia = 
                listaHastaAhora[listaHastaAhora.Count-1].Componente.Token == Lexicografico.ComponenteLexico.TokenType.FinSentencia;

            bool contieneElementosIncompatiblesAsignacion =
                lista.Exists(
                    x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Arreglo
                        || x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Comenzar
                        || x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Const
                        || x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Constantes
                        || x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.De
                        || x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.FuncionComienzo
                        || x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.FuncionFin
                        || x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Leer
                        || x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Llamar
                        || x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.MientrasComienzo
                        || x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.MientrasFin
                        || x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.MientrasHacer
                        || x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Mostrar
                        || x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.ProcedimientoComienzo
                        || x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.ProcedimientoFin
                        || x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Principal
                        || x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Referencia
                        || x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.SiComienzo
                        || x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.SiEntonces
                        || x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.SiFin
                        || x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.TipoBooleano
                        || x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.TipoDato
                        || x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.TipoTexto
                        || x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.TipoNumero
                        || x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Var
                        || x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Variables                        
                    );

            return ultElementoFinSentencia && !contieneElementosIncompatiblesAsignacion;
        }

        internal static bool AsignacionParteIzqCorrecta(List<Terminal> lista, List<Terminal> listaHastaAhora)
        {
            bool res = lista.Exists(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Igual 
                            );
            
            return res;
        }

        internal static bool ParentesisBalanceados(List<Terminal> lista, List<Terminal> listaHastaAhora)
        {
            int cantidadAbiertos = lista.FindAll(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.ParentesisApertura).Count;
            int cantidadCerrados = lista.FindAll(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.ParentesisClausura).Count;

            return cantidadAbiertos == cantidadCerrados;
        }

        internal static bool CorchetesBalanceados(List<Terminal> lista, List<Terminal> listaHastaAhora)
        {
            int cantidadAbiertos = lista.FindAll(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.CorcheteApertura).Count;
            int cantidadCerrados = lista.FindAll(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.CorcheteClausura).Count;

            return cantidadAbiertos == cantidadCerrados;
        }

        internal static bool ElementosConValorNoContiguos(List<Terminal> lista, List<Terminal> listaHastaAhora)
        {
            List<Terminal> listaElementosConValorContiguos = lista.FindAll(x => TerminalesHelpers.EsTerminalConValor(x));

            return ChequeoContiguosIguales(lista, listaElementosConValorContiguos, TerminalesHelpers.EsTerminalConValor);
        }

        internal static bool ElementosOperadoresNoContiguos(List<Terminal> lista, List<Terminal> listaHastaAhora)
        {
            List<Terminal> listaElementosOperadoresContiguos = lista.FindAll(x => TerminalesHelpers.EsOperador(x));

            return ChequeoContiguosIguales(lista, listaElementosOperadoresContiguos, TerminalesHelpers.EsOperador);
        }

        internal static bool NegacionesCorrectas(List<Terminal> lista, List<Terminal> listaHastaAhora)
        {
            bool retorno = true;

            List<Terminal> listaElementos = lista.FindAll(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Negacion);

            foreach (Terminal item in listaElementos)
            {
                int indice = lista.IndexOf(item);
                if (indice < lista.Count - 1)
                {
                    retorno = !(lista[indice + 1].Componente.Token == Lexicografico.ComponenteLexico.TokenType.ParentesisApertura);
                }
                else
                {
                    //es el ultimo elemento. esta mal.
                    retorno = false;
                }

                if (!retorno)
                {
                    break;
                }
            }
            return retorno;
        }


        internal static bool ForzarFalso(List<Terminal> lista, List<Terminal> listaHastaAhora)
        {
            return false;
        }

        internal static bool ForzarVerdadero(List<Terminal> lista, List<Terminal> listaHastaAhora)
        {
            return true;
        }

        private static bool ChequeoContiguosIguales(List<Terminal> lista, List<Terminal> listaElementosConValorContiguos, ChequeosTerminalesDelegate del)
        {
            bool retorno = true;

            foreach (Terminal item in listaElementosConValorContiguos)
            {
                int indice = lista.IndexOf(item);
                if (indice > 0 && indice < lista.Count - 1)
                {
                    retorno = !(del.Invoke(lista[indice - 1]) || del.Invoke(lista[indice + 1]));
                }
                else
                {
                    if (indice > 0)
                    {
                        retorno = !(del.Invoke(lista[indice - 1]));
                    }
                    else
                    {
                        if (indice < lista.Count - 1)
                        {
                            retorno = !(del.Invoke(lista[indice + 1]));
                        }
                        else
                        {
                            //Tiene un solo elemento. Cumple la validacion
                            retorno = true;
                        }
                    }
                }

                if (!retorno)
                {
                    break;
                }
            }
            return retorno;
        }

        internal static bool LeerRepetido(List<Terminal> lista, List<Terminal> listaHastaAhora)
        {
            int cantidad = lista.FindAll(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Leer).Count;

            return cantidad < 2;
        }

        internal static bool LeerSolo(List<Terminal> lista, List<Terminal> listaHastaAhora)
        {
            return lista.Count > 2;
        }

        internal static bool LeerNoIdentificador(List<Terminal> lista, List<Terminal> listaHastaAhora)
        {
            if (lista.Count > 0)
            {
                return lista[0].Componente.Token == Lexicografico.ComponenteLexico.TokenType.Identificador;
            }
            else
            {
                return false;
            }

        }
        
    }
}
