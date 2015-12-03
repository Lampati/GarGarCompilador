using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Sintactico.TablaGramatica;
using System.Configuration;
using CompiladorGargar.Lexicografico;
using System.Diagnostics;
using System.Windows.Forms;
using CompiladorGargar.Auxiliares;
using CompiladorGargar.Semantico;
using CompiladorGargar.Semantico.Arbol;
using CompiladorGargar.Resultado.Auxiliares;
using CompiladorGargar.Sintactico.ErroresManager.Errores;


namespace CompiladorGargar.Sintactico
{
    class AnalizadorSintactico
    {        
        private Gramatica.Gramatica gramatica;
        private TablaAnalisisGramatica tabla;


        private int cantElementosCadenaEntrada;
        private bool finArch;
        private short cantErroresSintacticos;
        private short cantParentesisAbiertos;


        public AnalizadorLexicografico AnalizadorLexico {get; set;}
        
        public PilaGramatica Pila  {get; private set;}    

        public CadenaEntrada CadenaEntrada  {get; private set;}
       
        public ArbolSemantico ArbolSemantico  {get; private set;}
      
        public bool HabilitarSemantico  {get; set;}

     
        public AnalizadorSintactico(string path)
        {
            gramatica = new Gramatica.Gramatica(path);

            tabla = gramatica.ArmarTablaAnalisis();

            cantElementosCadenaEntrada = Convert.ToInt32(CompiladorGargar.Properties.Resources.CantElementosCadenaEntrada);
            finArch = false;

            cantErroresSintacticos = 0;
            cantParentesisAbiertos = 0;


            AnalizadorLexico = new AnalizadorLexicografico();


        }

        public void ReiniciarAnalizadorSintactico()
        {
            EstadoSintactico.Reiniciar();

            Pila = new PilaGramatica(gramatica.SimboloInicial);
            CadenaEntrada = new CadenaEntrada();

            ArbolSemantico = new ArbolSemantico(gramatica.SimboloInicial);
            Pila.ArbolSemantico = ArbolSemantico;

            finArch = false;
            cantErroresSintacticos = 0;
            cantParentesisAbiertos = 0;

            HabilitarSemantico = true;
        }
     
        internal void CargarAnalizadorLexicografico(string texto)
        {
            AnalizadorLexico.CargarTexto(texto);
        }

        public bool EsFinAnalisisSintactico()
        {
            return (CadenaEntrada.EsFinDeCadena() && Pila.EsFinDePila());
        }      

        public List<PasoAnalizadorSintactico> AnalizarSintacticamenteUnPaso()
        {
            List<PasoAnalizadorSintactico> retorno = new List<PasoAnalizadorSintactico>();
            try
            {
                retorno = AnalizarUnSoloPaso();
            }
            catch (ErrorLexicoException ex)
            {
                cantErroresSintacticos++;
                retorno.Add(new PasoAnalizadorSintactico(ex.Message, GlobalesCompilador.TipoError.Sintactico, ex.Fila, ex.Columna, false, new ErrorLexemaInvalido(ex.Message)));
                //MostrarError(new ErrorCompiladorEventArgs(ex.Tipo, ex.Descripcion, ex.Fila, ex.Columna, false));
            }            
            catch (ErrorSintacticoException ex)
            {
                if (ex.Mostrar)
                {
                    cantErroresSintacticos++;
                }

                string mensajeAMostrar = ex.Message;
                int filaAMostrar = ex.Fila;
                int colAMostrar = ex.Columna;

                bool pararCompilacion = ex.PararAnalisis;

                try
                {
                    ErroresManager.AnalizadorErroresSintacticos analizador = new ErroresManager.AnalizadorErroresSintacticos(
                                                                                   EstadoSintactico.ListaLineaActual,
                                                                                   EstadoSintactico.ContextoGlobal,
                                                                                   EstadoSintactico.ContextoLinea,
                                                                                   CadenaEntrada.CadenaEntera);

                    try
                    {
                        analizador.Validar();
                    }
                    catch (CompiladorGargar.Sintactico.ErroresManager.ValidacionException excepVal)
                    {
                        mensajeAMostrar = excepVal.Message;
                        filaAMostrar = excepVal.Fila != -1 ? excepVal.Fila : filaAMostrar;
                        colAMostrar = excepVal.Columna != -1 ? excepVal.Columna : colAMostrar;
                        HabilitarSemantico = false;
                        pararCompilacion = true; //siempre paro la compilacion al primer error

                        retorno.Add(new PasoAnalizadorSintactico(mensajeAMostrar, GlobalesCompilador.TipoError.Sintactico, filaAMostrar, colAMostrar, pararCompilacion, excepVal.MensjError));
                    }
                }
                catch (ErroresManager.AnalizadorErroresException exAnaliz)
                {
                    mensajeAMostrar = exAnaliz.Message;
                    filaAMostrar = exAnaliz.Fila != -1 ? exAnaliz.Fila : filaAMostrar;
                    colAMostrar = exAnaliz.Columna != -1 ? exAnaliz.Columna : colAMostrar;
                    pararCompilacion = exAnaliz.Parar;

                    CompiladorGargar.Sintactico.ErroresManager.Errores.MensajeError mensErr = new CompiladorGargar.Sintactico.ErroresManager.Errores.ErrorVacio();
                    mensErr.MensajeModoTexto = exAnaliz.Message;
                    mensErr.MensajeModoGrafico = exAnaliz.MensajeModoGrafico;
                    mensErr.EsErrorBienDefinido = false;

                    retorno.Add(new PasoAnalizadorSintactico(mensajeAMostrar, GlobalesCompilador.TipoError.Sintactico, filaAMostrar, colAMostrar, pararCompilacion, mensErr)); //siempre paro la compilacion al primer error
                }


                if (cantErroresSintacticos >= GlobalesCompilador.CANT_MAX_ERRORES_SINTACTICOS)
                {
                    retorno.Add(new PasoAnalizadorSintactico("Se paró la compilacion por la cantidad de errores.", GlobalesCompilador.TipoError.Sintactico, 0, 0, true));
                }
            }
            return retorno;
        }


        internal List<PasoAnalizadorSintactico> AnalizarUnSoloPaso()
        {
            List<PasoAnalizadorSintactico> retorno = new List<PasoAnalizadorSintactico>();

            if (!(CadenaEntrada.EsFinDeCadena() && Pila.EsFinDePila()))
            {

                RellenarCadenaEntrada();

                if (Pila.ObtenerTope().GetType() == typeof(Terminal))
                {
                    Terminal term = (Terminal)Pila.ObtenerTope();

                    if (CadenaEntrada.ObtenerPrimerTerminal().Equals(Pila.ObtenerTope()))
                    {                       

                        if (term.Componente.Token == ComponenteLexico.TokenType.ParentesisApertura)
                        {
                            cantParentesisAbiertos++;
                        }
                        else if (term.Componente.Token == ComponenteLexico.TokenType.ParentesisClausura)
                        {
                            cantParentesisAbiertos--;
                        }

                        //flanzani 8/1/2012
                        //tokens repetidos
                        //Antes de pasar por el semantico, lo que hago es fijarme si el terminal justo no esta repetido, 
                        //pq eso me caga todo el parseo de errores del sintactico
                        //Esto puede arrojar una excepcion sintactica
                        ChequearTokensRepetidosEnCadena(term);
                        
                        if (HabilitarSemantico)
                        {
                            retorno = ArbolSemantico.CalcularAtributos(CadenaEntrada.ObtenerPrimerTerminal());
                        }

                        Pila.DescartarTope();

                        GlobalesCompilador.UltFila = CadenaEntrada.ObtenerPrimerTerminal().Componente.Fila;
                        GlobalesCompilador.UltCol = CadenaEntrada.ObtenerPrimerTerminal().Componente.Columna;

                        EstadoSintactico.AgregarTerminal(term);

                        CadenaEntrada.EliminarPrimerTerminal();
                    }
                    else
                    {
                        if (term.NoEsLambda())
                        {
                            StringBuilder strbldr = new StringBuilder(string.Empty);
                            strbldr.Append("Se esperaba ");
                            strbldr.Append(EnumUtils.stringValueOf(term.Componente.Token));
                            strbldr.Append(" pero se encontro ");
                            strbldr.Append(EnumUtils.stringValueOf(CadenaEntrada.ObtenerPrimerTerminal().Componente.Token));
                            strbldr.Append(".");

                            if (term.Equals(Terminal.ElementoFinSentencia()))
                            {
                                //Descarto el ; de la pila pq asumo que simplemente se le olvido
                                strbldr.Append(" Se asume fin de sentencia para continuar con analisis.");
                                
                                
                            }
              
                            throw new ErrorSintacticoException(strbldr.ToString(),
                                    AnalizadorLexico.FilaActual(),
                                    AnalizadorLexico.ColumnaActual(),
                                    true,
                                    false,
                                    false);
                        }
                        else
                        {
                            retorno= AnalizarPila();
                        }
                    }
                }
                else
                {
                    retorno= AnalizarPila();

                }
            }

            return retorno;
        }

        private void ChequearTokensRepetidosEnCadena(Terminal term)
        {
           if (CadenaEntrada.TieneTerminalRepetidoEnPrimerLugarErroneo( cantParentesisAbiertos))
            {

                if (term.Equals(Terminal.ElementoParentesisClausura()))
                {
                    //Le sumo uno asi restauro el equilibrio
                    cantParentesisAbiertos++;
                }
                StringBuilder strbldr = new StringBuilder("Se encontro ");
                strbldr.Append(EnumUtils.stringValueOf(term.Componente.Token));
                strbldr.Append(" repetido.");

                throw new ErrorSintacticoException(strbldr.ToString(),
                        AnalizadorLexico.FilaActual(),
                        AnalizadorLexico.ColumnaActual(),
                        false,
                        true,
                        false);
            }
        }


        private List<PasoAnalizadorSintactico> AnalizarPila()
        {
            List<PasoAnalizadorSintactico> retorno = new List<PasoAnalizadorSintactico>();

            if (Pila.ObtenerTope().GetType() == typeof(NoTerminal))
            {
                
                Terminal t = CadenaEntrada.ObtenerPrimerTerminal();
                NoTerminal nt = (NoTerminal)Pila.ObtenerTope();

                bool generaProdVacia = false;

                //Que es esto??
                if (!PerteneceNoTerminalesNoEscapeables(nt))
                {
                    generaProdVacia = gramatica.NoTerminalGeneraProduccionVacia(nt);
                }

                //Buscar en la tabla arroja excepciones sintacticas si encuentra errores.
                Produccion prod = tabla.BuscarEnTablaProduccion(nt, t, true, generaProdVacia);

                if (prod != null)
                {

                    // flanzani 8/1/2012
                    // Esto es para ver que no se este operando la pila para llegar a un error sintactico, descartando cosas
                    // Si encuentra un problema, devuelve true, y se crea un error sintactico para descartar el tope de la cadena.
                    bool dejarDeOperarPilaYTirarError = ChequearQueNoSeEsteOperandoLaPilaParaUnErrorSintactico(prod);


                    if (dejarDeOperarPilaYTirarError) 
                    {
                        // flanzani 8/1/2012
                        // Este metodo se fija si el estado de la pila es pq falta un token solo, y se fija basandose en que es 
                        // el tope de la pila, para ver que terminal tengo que buscar y decir que falta ese para descartar ese
                        AnalizarLugarDeLaPilaYDescartarHastaTerminalQueCorresponda(nt, t, generaProdVacia);

                        
                    }

                    Pila.TransformarProduccion(prod);
                }
               

            }
            else
            {
                if (!((Terminal)Pila.ObtenerTope()).NoEsLambda())
                {                    
                    Terminal t = Terminal.ElementoVacio();
                    t.Componente.Fila = AnalizadorLexico.FilaActual();
                    t.Componente.Columna = AnalizadorLexico.ColumnaActual();

                    if (HabilitarSemantico)
                    {
                        retorno = ArbolSemantico.CalcularAtributos(t);
                    }


                    Pila.DescartarTope();

                }
            }

            return retorno;
        }

        private void AnalizarLugarDeLaPilaYDescartarHastaTerminalQueCorresponda(NoTerminal nt, Terminal t, bool generaProdVacia)
        {
            Terminal termBuscar = BuscarTerminalApropiadoBasadoEnTopePila(nt);

            //string error = CaptarMensajeErrorApropiado(nt, t);

            Produccion prod = tabla.BuscarEnTablaProduccion(nt, termBuscar, false, generaProdVacia);

            if (prod != null)
            {
                StringBuilder strbldr = new StringBuilder(string.Empty);
                strbldr.Append("Se esperaba ");
                strbldr.Append(termBuscar.Componente.Lexema);

                throw new ErrorSintacticoException(strbldr.ToString(),
                        AnalizadorLexico.FilaActual(),
                        AnalizadorLexico.ColumnaActual(),
                        true,
                        false,
                        true,
                        true,
                        termBuscar
                        );

            }
            else
            {

                StringBuilder strbldr = new StringBuilder(string.Empty);
                strbldr.Append(EnumUtils.stringValueOf(t.Componente.Token));
                strbldr.Append(" no tiene lugar en la sentencia.");

                throw new ErrorSintacticoException(strbldr.ToString(),
                        AnalizadorLexico.FilaActual(),
                        AnalizadorLexico.ColumnaActual(),
                        false,
                        true,
                        false);
            }
        }

        private string CaptarMensajeErrorApropiado(NoTerminal nt, Terminal t)
        {
            string x;

            return string.Empty;
        }

        private Terminal BuscarTerminalApropiadoBasadoEnTopePila(NoTerminal nt)
        {
            Terminal retorno;
            switch (nt.Nombre)
            {
                case "MULT":
                case "MULTS":
                case "EXP":
                case "EXPR":
                    if (cantParentesisAbiertos > 0)
                    {
                        retorno = Terminal.ElementoParentesisClausura();
                    }
                    else
                    {
                        retorno = Terminal.ElementoFinSentencia();
                    }
                    break;

                case "IDASIGN":
                    retorno = Terminal.ElementoFinSentencia();
                    break;

                case "EXPRPRPROC":
                case "EXPRPRPROCED":
                case "EXPRBOOLEANAS":
                case "EXPRBOOLEXTRA":
                   
                    retorno = Terminal.ElementoParentesisClausura();                   
                    break;
                default:
                    retorno = Terminal.ElementoFinSentencia();
                    break;
            }

            return retorno;
        }

        private bool ChequearQueNoSeEsteOperandoLaPilaParaUnErrorSintactico(Produccion prod)
        {
            bool retorno =false;
            bool pararChequeo = false;
            if (prod.ProduceElementoVacio())
            {
                int posPila = 1;
                while (!(CadenaEntrada.EsFinDeCadena() && Pila.EsFinDePila()) && (Pila.Count > posPila) && !pararChequeo)
                {

                    if (Pila.ObtenerPosicion(posPila).GetType() == typeof(Terminal))
                    {
                        Terminal term = (Terminal)Pila.ObtenerPosicion(posPila);

                        if (CadenaEntrada.ObtenerPrimerTerminal().Equals(Pila.ObtenerPosicion(posPila)))
                        {
                            //No hay error pq coincide el terminal, y se va a poder descartar en el proximo paso.
                            retorno = false;
                            pararChequeo = true;
                        }
                        else
                        {
                            if (term.NoEsLambda())
                            {
                                //Hay error pq el terminal no coindiria con el de la cadena de entrada.
                                retorno = true;
                                pararChequeo = true;
                            }
                            
                        }
                    }
                    else
                    {
                        Terminal t = CadenaEntrada.ObtenerPrimerTerminal();
                        NoTerminal nt = (NoTerminal)Pila.ObtenerPosicion(posPila);

                        bool generaProdVacia = false;

                        //Que es esto??
                        if (!PerteneceNoTerminalesNoEscapeables(nt))
                        {
                            generaProdVacia = gramatica.NoTerminalGeneraProduccionVacia(nt);
                        }

                        Produccion prodAux = tabla.BuscarEnTablaProduccion(nt, t, false, generaProdVacia);

                        if (prodAux != null)
                        {
                            if (prodAux.ProduceElementoVacio())
                            {
                                posPila++;
                            }
                            else
                            {
                                //Significa que llegue a algo concreto con el terminal que tengo en el tope, y dejo seguir.
                                retorno = false;
                                pararChequeo = true;
                            }
                        }
                        else
                        {
                            //Significa que en la tabla ni figura, o sea que es un error
                            retorno = true;
                            pararChequeo = true;
                        }
                        
                    }
                }

                if (posPila > Pila.Count)
                {
                    //Hubo error pq el terminal tope no servia para nada de la pila
                    retorno = true;
                }
            }

            return retorno;
        }

        private bool PerteneceNoTerminalesNoEscapeables(NoTerminal nt)
        {
            return (nt.Nombre.Equals("EXPR") || nt.Nombre.Equals("BLQ") || nt.Nombre.Equals("PROCEDIMIENTO") || nt.Nombre.Equals("PROCED"));

        }

        private void RellenarCadenaEntrada()
        {
            if (!finArch)
            {
                Terminal t = Terminal.ElementoVacio();

                while ((CadenaEntrada.Count < cantElementosCadenaEntrada) && (!t.Equals(Terminal.ElementoEOF())))
                {
                    
                    t = new Terminal();
                    t.Componente = AnalizadorLexico.ObtenerProximoToken();

                    //Controlo que no haya un error lexico en el token que acabo de traer.
                    //Si hay error directamente me salteo el paso, no se inserta en la cadena, y no toco la pila.
                    if (t.Equals(Terminal.ElementoError()))
                    {
                        //throw new ErrorLexicoException(string.Format("El caracter {0} no es reconocido por el lenguaje GarGar", t.Componente.Lexema), t.Componente.Fila, t.Componente.Columna);
                        string lexemaError;
                        if (string.IsNullOrEmpty(t.Componente.CaracterErroneo))
                        {
                            lexemaError = t.Componente.Lexema.Split(new char[] { ' ' })[0];
                        }
                        else
                        {
                            lexemaError = t.Componente.CaracterErroneo;
                        }

                        string errorMensaje = string.Format("El caracter {0} es invalido en este contexto", lexemaError);

                        if (t.Componente.Descripcion != null)
                        {
                            errorMensaje = t.Componente.Descripcion;
                        }                            

                        throw new ErrorLexicoException(errorMensaje, t.Componente.Fila, t.Componente.Columna);
                    }

                    if (t.Componente.Token == ComponenteLexico.TokenType.Numero)
                    {
                        if (t.Componente.Lexema.TrimStart()[0] == '-')
                        {

                            Terminal ultimoTerminal = CadenaEntrada.UltimoTerminalInsertado;

                            if (ultimoTerminal != null && TerminalesHelpers.EsTerminalConValor(ultimoTerminal))
                            {
                                //Para que no de error Sintactico, creo este otro token para que parezca que es una operacion negativa -

                                ComponenteLexico comp = new ComponenteLexico();
                                comp.Fila = t.Componente.Fila;
                                comp.Columna = t.Componente.Columna;
                                comp.Lexema = "-";
                                comp.Token = ComponenteLexico.TokenType.RestaEntero;

                                CadenaEntrada.InsertarTerminal(new Terminal() { Componente = comp });

                                //Le sumo uno pq el menos ya no pertenece mas.
                                t.Componente.Columna++;

                                //Le saco el - del lexema
                                t.Componente.Lexema = t.Componente.Lexema.Remove(0, 1);
                            }
                        }
                    }

                   

                    CadenaEntrada.InsertarTerminal(t);                       
                    
                }
                if (t.Equals(Terminal.ElementoEOF()))
                {
                    finArch = true;
                }
            }
        }

     
    }
}
