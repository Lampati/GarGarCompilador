using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Sintactico.ErroresManager.Errores;

namespace CompiladorGargar.Sintactico.ErroresManager.Tipos
{
    class DeclaracionConstante: TipoBase
    {
        public DeclaracionConstante(List<Terminal> lista, int fila, int col) 
            : base(fila,col)
        {
            listaLineaEntera = lista;

            AgregarValidacionAsignarValorRepetido();
            AgregarValidacionTipoDatoDefRepetido();
            AgregarValidacionAsignarValorFaltante();
            AgregarValidacionAsignarValorRepetido();
            AgregarValidacionTipoDatoFaltante();
            AgregarValidacionTipoDatoRepetido();
            AgregarValidacionTipoDatoSinArreglo();
            AgregarValidacionValorFaltante();
            AgregarValidacionValorRepetido();
            AgregarValidacionElementoQueSobraErroneo();

            AgregarValidacionPorDefault();
        }

        private void AgregarValidacionPorDefault()
        {
            MensajeError mensajeError = new ErrorDeclaracionConstanteGenerico();
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



        private void AgregarValidacionAsignarValorRepetido()
        {
            MensajeError mensajeError = new ErrorAsignarValorRepetido();
            short importancia = 9;

            List<Terminal> parteDer = ArmarSubListaDerechaDe(listaLineaEntera, Lexicografico.ComponenteLexico.TokenType.TipoDato);

            Validacion valRep = new Validacion(parteDer, mensajeError, importancia, ValidacionesFactory.ValidarAsignacionConstanteRepetido, FilaDelError, ColumnaDelError);

            listaValidaciones.Add(valRep);
        }

        private void AgregarValidacionAsignarValorFaltante()
        {
            MensajeError mensajeError = new ErrorAsignarValorFaltante();
            short importancia = 9;

            List<Terminal> parteDer = ArmarSubListaDerechaDe(listaLineaEntera,Lexicografico.ComponenteLexico.TokenType.TipoDato);

            Validacion valRep = new Validacion(parteDer, mensajeError, importancia, ValidacionesFactory.ValidarAsignacionConstanteFaltante, FilaDelError, ColumnaDelError);

            listaValidaciones.Add(valRep);
        }

        private void AgregarValidacionTipoDatoSinArreglo()
        {
            MensajeError mensajeError = new ErrorConstanteTipoDatoSinArreglo();
            short importancia = 7;

            List<Terminal> aux = ArmarSubListaDerechaDe(listaLineaEntera, Lexicografico.ComponenteLexico.TokenType.TipoDato);
            List<Terminal> final = ArmarSubListaIzquierdaDe(aux, Lexicografico.ComponenteLexico.TokenType.Igual);

            Validacion valRep = new Validacion(final, mensajeError, importancia, ValidacionesFactory.ValidarDefTipoDatoSinArreglo, FilaDelError, ColumnaDelError);

            listaValidaciones.Add(valRep);
        }

        private void AgregarValidacionTipoDatoRepetido()
        {
            MensajeError mensajeError = new ErrorTipoDatoRepetido();
            short importancia = 7;

            List<Terminal> aux = ArmarSubListaDerechaDe(listaLineaEntera, Lexicografico.ComponenteLexico.TokenType.TipoDato);
            List<Terminal> final = ArmarSubListaIzquierdaDe(aux, Lexicografico.ComponenteLexico.TokenType.Igual);

            Validacion valRep = new Validacion(final, mensajeError, importancia, ValidacionesFactory.ValidarTipoDatoRepetido, FilaDelError, ColumnaDelError);

            listaValidaciones.Add(valRep);
        }

        private void AgregarValidacionTipoDatoFaltante()
        {
            MensajeError mensajeError = new ErrorTipoDatoFaltante();
            short importancia = 7;

            List<Terminal> aux = ArmarSubListaDerechaDe(listaLineaEntera, Lexicografico.ComponenteLexico.TokenType.TipoDato);
            List<Terminal> final = ArmarSubListaIzquierdaDe(aux, Lexicografico.ComponenteLexico.TokenType.Igual);

            Validacion valRep = new Validacion(final, mensajeError, importancia, ValidacionesFactory.ValidarTipoDatoFaltante, FilaDelError, ColumnaDelError);

            listaValidaciones.Add(valRep);
        }

        private void AgregarValidacionValorRepetido()
        {
            MensajeError mensajeError = new ErrorConstanteValorRepetido();
            short importancia = 6;

            List<Terminal> aux = ArmarSubListaDerechaDe(listaLineaEntera, Lexicografico.ComponenteLexico.TokenType.TipoDato);
            List<Terminal> final = ArmarSubListaDerechaDe(aux, Lexicografico.ComponenteLexico.TokenType.Igual);

            Validacion valRep = new Validacion(final, mensajeError, importancia, ValidacionesFactory.ValidarAsignacionValorConstanteRepetido, FilaDelError, ColumnaDelError);

            listaValidaciones.Add(valRep);
        }

        private void AgregarValidacionValorFaltante()
        {
            MensajeError mensajeError = new ErrorConstanteValorFaltante();
            short importancia = 6;

            List<Terminal> aux = ArmarSubListaDerechaDe(listaLineaEntera, Lexicografico.ComponenteLexico.TokenType.TipoDato);
            List<Terminal> final = ArmarSubListaDerechaDe(aux, Lexicografico.ComponenteLexico.TokenType.Igual);

            Validacion valRep = new Validacion(final, mensajeError, importancia, ValidacionesFactory.ValidarAsignacionValorConstanteFaltante, FilaDelError, ColumnaDelError);

            listaValidaciones.Add(valRep);
        }

        private void AgregarValidacionElementoQueSobraErroneo()
        {
            
            short importancia = 5;

            int i = 0;
            Terminal terminalErroneo = null;

            while (i < listaLineaEntera.Count && terminalErroneo == null)
            {
                switch (i)
                {
                    case 0:
                        if (listaLineaEntera[i].Componente.Token != Lexicografico.ComponenteLexico.TokenType.Const)
                        {
                            terminalErroneo = listaLineaEntera[i];
                        }
                        break;
                    case 1:
                        if (listaLineaEntera[i].Componente.Token != Lexicografico.ComponenteLexico.TokenType.Identificador)
                        {
                            terminalErroneo = listaLineaEntera[i];
                        }
                        break;
                    case 2:
                        if (listaLineaEntera[i].Componente.Token != Lexicografico.ComponenteLexico.TokenType.TipoDato)
                        {
                            terminalErroneo = listaLineaEntera[i];
                        }
                        break;
                    case 3:
                        if (!TerminalesHelpers.EsTipoDeDato(listaLineaEntera[i]))
                        {
                            terminalErroneo = listaLineaEntera[i];
                        }
                        break;
                    case 4:
                        if (listaLineaEntera[i].Componente.Token != Lexicografico.ComponenteLexico.TokenType.Igual)
                        {
                            terminalErroneo = listaLineaEntera[i];
                        }
                        break;
                    case 5:
                        if (!TerminalesHelpers.EsTerminalConValorConstante(listaLineaEntera[i]))
                        {
                            terminalErroneo = listaLineaEntera[i];
                        }
                        break;
                    case 6:
                        if (listaLineaEntera[i].Componente.Token != Lexicografico.ComponenteLexico.TokenType.FinSentencia)
                        {
                            terminalErroneo = listaLineaEntera[i];
                        }
                        break;
                    default:
                        terminalErroneo = listaLineaEntera[i];
                        break;
                }

                i++;
            }

            MensajeError mensajeError = new ErrorConstanteElementoQueSobraErroneo(string.Empty);
            Validacion valRep;

            if (i < listaLineaEntera.Count)
            {
                mensajeError = new ErrorConstanteElementoQueSobraErroneo(terminalErroneo.Componente.Lexema);
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
