using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Sintactico.ErroresManager.Errores;

namespace CompiladorGargar.Sintactico.ErroresManager.Tipos
{
    class DeclaracionVariable: TipoBase
    {
        public DeclaracionVariable(List<Terminal> lista, int fila, int col) 
            : base(fila,col)
        {
            listaLineaEntera = lista;

            AgregarValidacionTipoDatoDefFaltante();
            AgregarValidacionTipoDatoDefRepetido();
            AgregarValidacionTipoDatoFaltante();
            AgregarValidacionTipoDatoRepetido();
            AgregarValidacionParteIzquierdaCorrecta();
            AgregarValidacionCantArregloNoRepetido();
            //AgregarValidacionElementoQueSobraErroneo();


            AgregarValidacionPorDefault();
        }

        private void AgregarValidacionPorDefault()
        {
            MensajeError mensajeError = new ErrorDeclaracionVariableValidacionPorDefault();
            short importancia = 1;


            Validacion valRep = new Validacion(listaLineaEntera, mensajeError, importancia, ValidacionesFactory.ForzarFalso, FilaDelError, ColumnaDelError);
            listaValidaciones.Add(valRep);
        }


        private void AgregarValidacionTipoDatoDefRepetido()
        {
            MensajeError mensajeError = new ErrorTipoDatoDefRepetido();
            short importancia = 10;

            Validacion valRep = new Validacion(listaLineaEntera, mensajeError, importancia, ValidacionesFactory.ValidarDefTipoDatoRepetido, FilaDelError, ColumnaDelError);

            listaValidaciones.Add(valRep);
        }

        private void AgregarValidacionTipoDatoDefFaltante()
        {
            MensajeError mensajeError = new ErrorTipoDatoDefFaltante();
            short importancia = 10;

            Validacion valRep = new Validacion(listaLineaEntera, mensajeError, importancia, ValidacionesFactory.ValidarDefTipoDatoFaltante, FilaDelError, ColumnaDelError);

            listaValidaciones.Add(valRep);
        }

        private void AgregarValidacionTipoDatoRepetido()
        {
            MensajeError mensajeError = new ErrorTipoDatoRepetido();
            short importancia = 9;

            List<Terminal> aux = ArmarSubListaDerechaDe(listaLineaEntera, Lexicografico.ComponenteLexico.TokenType.TipoDato);
            List<Terminal> final = ArmarSubListaIzquierdaDe(aux, Lexicografico.ComponenteLexico.TokenType.Igual);

            Validacion valRep = new Validacion(final, mensajeError, importancia, ValidacionesFactory.ValidarTipoDatoRepetido, FilaDelError, ColumnaDelError);

            listaValidaciones.Add(valRep);
        }

        private void AgregarValidacionTipoDatoFaltante()
        {
            MensajeError mensajeError = new ErrorTipoDatoFaltante();
            short importancia = 9;

            List<Terminal> aux = ArmarSubListaDerechaDe(listaLineaEntera, Lexicografico.ComponenteLexico.TokenType.TipoDato);
            List<Terminal> final = ArmarSubListaIzquierdaDe(aux, Lexicografico.ComponenteLexico.TokenType.Igual);

            Validacion valRep = new Validacion(final, mensajeError, importancia, ValidacionesFactory.ValidarTipoDatoFaltante, FilaDelError, ColumnaDelError);

            listaValidaciones.Add(valRep);
        }

        private void AgregarValidacionParteIzquierdaCorrecta()
        {
            MensajeError mensajeError = new ErrorDeclaracionVariableParteIzquierdaValida();
            short importancia = 8;

            List<Terminal> aux = ArmarSubListaDerechaDe(listaLineaEntera, Lexicografico.ComponenteLexico.TokenType.Var);
            List<Terminal> final = ArmarSubListaIzquierdaDe(aux, Lexicografico.ComponenteLexico.TokenType.TipoDato);

            Validacion valRep = new Validacion(final, mensajeError, importancia, ValidacionesFactory.CantidadIdsCorrectaYOrdenadosPorComas, FilaDelError, ColumnaDelError);

            listaValidaciones.Add(valRep);
        }

        private void AgregarValidacionCantArregloNoRepetido()
        {
            MensajeError mensajeError = new  ErrorDeclaracionVariableCantArregloNoRepetido();
            short importancia = 7;

            Validacion valRep = new Validacion(listaLineaEntera, mensajeError, importancia, ValidacionesFactory.ArregloRepetido, FilaDelError, ColumnaDelError);

            listaValidaciones.Add(valRep);
        }

        private void AgregarValidacionCorchetesBalanceadosParteIzq()
        {
            MensajeError mensajeError = new ErrorDeclaracionVariableCorchetesBalanceadosParteIzq();
            short importancia = 6;

            int cantArreglos = listaLineaEntera.FindAll(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Arreglo).Count;

            if (cantArreglos > 0)
            {
                List<Terminal> parteDefArreglo = ArmarSubListaDerechaDe(listaLineaEntera, Lexicografico.ComponenteLexico.TokenType.TipoDato);

                Validacion valRep = new Validacion(parteDefArreglo, mensajeError, importancia, ValidacionesFactory.CorchetesBalanceados, FilaDelError, ColumnaDelError);
                listaValidaciones.Add(valRep);
            }
        }

        private void AgregarValidacionElementoQueSobraErroneo()
        {
            
            short importancia = 5;

            int i = 0;
            Terminal terminalErroneo = null;

            List<Terminal> aux = ArmarSubListaDerechaDe(listaLineaEntera, Lexicografico.ComponenteLexico.TokenType.TipoDato);
            List<Terminal> parteDefArreglo = ArmarSubListaIzquierdaDe(aux, Lexicografico.ComponenteLexico.TokenType.FinSentencia);

            int cantArreglos = parteDefArreglo.FindAll(x => x.Componente.Token == Lexicografico.ComponenteLexico.TokenType.Arreglo).Count;

            if (cantArreglos > 0)
            {

                while (i < parteDefArreglo.Count && terminalErroneo == null)
                {
                    switch (i)
                    {
                        case 0:
                            if (parteDefArreglo[i].Componente.Token != Lexicografico.ComponenteLexico.TokenType.Arreglo)
                            {
                                terminalErroneo = parteDefArreglo[i];
                            }
                            break;
                        case 1:
                            if (parteDefArreglo[i].Componente.Token != Lexicografico.ComponenteLexico.TokenType.CorcheteApertura)
                            {
                                terminalErroneo = parteDefArreglo[i];
                            }
                            break;
                        case 2:
                            if (!TerminalesHelpers.EsTerminalConValor(parteDefArreglo[i]))
                            {
                                terminalErroneo = parteDefArreglo[i];
                            }
                            break;
                        case 3:
                            if (parteDefArreglo[i].Componente.Token != Lexicografico.ComponenteLexico.TokenType.CorcheteClausura)
                            {
                                terminalErroneo = parteDefArreglo[i];
                            }
                            break;
                        case 4:
                            if (parteDefArreglo[i].Componente.Token != Lexicografico.ComponenteLexico.TokenType.De)
                            {
                                terminalErroneo = parteDefArreglo[i];
                            }
                            break;
                        case 5:
                            if (!TerminalesHelpers.EsTipoDeDato(parteDefArreglo[i]))
                            {
                                terminalErroneo = parteDefArreglo[i];
                            }
                            break;
                        default:
                            terminalErroneo = parteDefArreglo[i];
                            break;
                    }

                    i++;
                }

                MensajeError mensajeError = new ErrorDeclaracionVariableElementoQueSobraErroneo(string.Empty);
                Validacion valRep;

                if (i < listaLineaEntera.Count)
                {
                    mensajeError = new ErrorDeclaracionVariableElementoQueSobraErroneo(terminalErroneo.Componente.Lexema);
                    valRep = new Validacion(listaLineaEntera, mensajeError, importancia, ValidacionesFactory.ForzarFalso, FilaDelError, ColumnaDelError);
                }
                else
                {
                    valRep = new Validacion(listaLineaEntera, mensajeError, importancia, ValidacionesFactory.ForzarVerdadero, FilaDelError, ColumnaDelError);
                }

                listaValidaciones.Add(valRep);
            }
        }


    }
}
