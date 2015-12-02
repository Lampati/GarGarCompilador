using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Lexicografico;
using CompiladorGargar.Sintactico.TablaGramatica;
using CompiladorGargar.Sintactico.TablaPrimerosSiguientes;
using System.Diagnostics;
using System.IO;

namespace CompiladorGargar.Sintactico.Gramatica
{
    class Gramatica
    {
        private List<NoTerminal> noTerminales;
        public List<NoTerminal> NoTerminales
        {
            get { return noTerminales; }
        }

        private List<Terminal> terminales;
        public List<Terminal> Terminales
        {
            get { return terminales; }
        }
        private List<Produccion> producciones;

        private TablaPrimeros tablaPrimeros;

        private NoTerminal simboloInicial;
        public NoTerminal SimboloInicial
        {
            get { return simboloInicial; }
            set { simboloInicial = value; }
        }

        public Gramatica(string pathArch)
        {
            try
            {
                this.noTerminales = new List<NoTerminal>();
                this.terminales = new List<Terminal>();
                this.producciones = new List<Produccion>();

                try
                {
                    Debug.Assert(pathArch != null, "El archivo era null");
                    this.CargarDeArchivo(pathArch);
                }
                catch (Exception ex)
                {
                    Utils.Log.AddError(ex.Message);
                    throw new Exception("Error al cargar la gramatica desde el archivo: "+pathArch);
                }

                try
                {
                    this.tablaPrimeros = this.ArmarTablaPrimeros();
                    this.tablaPrimeros.CompletarTabla();
                }
                catch (Exception ex)
                {
                    Utils.Log.AddError(ex.Message);
                    throw new Exception("Error en la tablaPrimeros");
                }
            }
            catch (Exception ex)
            {
                Utils.Log.AddError(ex.Message);
                throw new Exception("Error al crear la Gramatica");
            }
        }

        private void CargarDeArchivo(string path)
        {
            try
            {

                Terminal t;

                foreach (ComponenteLexico.TokenType tokn in Enum.GetValues(typeof(ComponenteLexico.TokenType)))
                {
                    t = new Terminal();
                    t.Componente = new ComponenteLexico();
                    t.Componente.Token = tokn;
                    this.terminales.Add(t);
                }
               

                this.ParsearXML(path);

                #region CodigoParaArmarGramaticaSimple
                /*
                this.noTerminales.Add(new NoTerminal("E"));
                this.noTerminales.Add(new NoTerminal("E'"));
                this.noTerminales.Add(new NoTerminal("T"));
                this.noTerminales.Add(new NoTerminal("T'"));
                this.noTerminales.Add(new NoTerminal("F"));

                this.simboloInicial = this.EncontrarNoTerminal("E");
                
                Produccion prod = new Produccion();
                prod.Izq = this.EncontrarNoTerminal("E");
                prod.Der.Add(this.EncontrarNoTerminal("T"));
                prod.Der.Add(this.EncontrarNoTerminal("E'"));
                this.producciones.Add(prod);

                prod = new Produccion();
                prod.Izq = this.EncontrarNoTerminal("E'");
                t = new Terminal();
                t.Componente = new ComponenteLexico();
                t.Componente.Lexema = "+";
                t.Componente.Token = ComponenteLexico.TokenType.SumaEntero;
                prod.Der.Add(t);
                prod.Der.Add(this.EncontrarNoTerminal("T"));
                prod.Der.Add(this.EncontrarNoTerminal("E'"));
                this.producciones.Add(prod);

                prod = new Produccion();
                prod.Izq = this.EncontrarNoTerminal("E'");
                prod.Der = null;
                this.producciones.Add(prod);

                prod = new Produccion();
                prod.Izq = this.EncontrarNoTerminal("T");
                prod.Der.Add(this.EncontrarNoTerminal("F"));
                prod.Der.Add(this.EncontrarNoTerminal("T'"));
                this.producciones.Add(prod);

                prod = new Produccion();
                prod.Izq = this.EncontrarNoTerminal("T'");
                t = new Terminal();
                t.Componente = new ComponenteLexico();
                t.Componente.Lexema = "*";
                t.Componente.Token = ComponenteLexico.TokenType.MultiplicacionEntero;
                prod.Der.Add(t);
                prod.Der.Add(this.EncontrarNoTerminal("F"));
                prod.Der.Add(this.EncontrarNoTerminal("T'"));
                this.producciones.Add(prod);

                prod = new Produccion();
                prod.Izq = this.EncontrarNoTerminal("T'");
                prod.Der = null;
                this.producciones.Add(prod);

                prod = new Produccion();
                prod.Izq = this.EncontrarNoTerminal("F");
                t = new Terminal();
                t.Componente = new ComponenteLexico();
                t.Componente.Lexema = "id";
                t.Componente.Token = ComponenteLexico.TokenType.Identificador;
                prod.Der.Add(t);
                this.producciones.Add(prod);

                prod = new Produccion();
                prod.Izq = this.EncontrarNoTerminal("F");
                t = new Terminal();
                t.Componente = new ComponenteLexico();
                t.Componente.Lexema = "(";
                t.Componente.Token = ComponenteLexico.TokenType.ParentesisApertura;
                prod.Der.Add(t);
                prod.Der.Add(this.EncontrarNoTerminal("E"));
                t = new Terminal();
                t.Componente = new ComponenteLexico();
                t.Componente.Lexema = ")";
                t.Componente.Token = ComponenteLexico.TokenType.ParentesisClausura;
                prod.Der.Add(t);
                this.producciones.Add(prod);
                 */
                #endregion

            }
            catch (Exception ex)
            {
                Utils.Log.AddError(ex.Message);
                throw new Exception("Error al crear la gramatica a partir del xml");
            }
        }

        private void ParsearXML(string filePath)
        {
            //if (!System.IO.File.Exists(filePath))
            //    throw new System.IO.FileNotFoundException(String.Format("El archivo \"{0}\" no existe!", filePath));

            try
            {
            //List<Produccion> prods = new List<Produccion>();
                //using (System.Xml.XmlTextReader reader = new System.Xml.XmlTextReader(filePath))
                using (System.Xml.XmlReader reader = System.Xml.XmlReader.Create(new StringReader(CompiladorGargar.Properties.Resources.Gramatica)))
                
                {
                    while (reader.Read())
                    {
                        if (reader.NodeType == System.Xml.XmlNodeType.Element)
                        {
                            if (reader.Name.Equals("Produccion"))
                            {
                                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                                System.Xml.XmlNode node = doc.ReadNode(reader);
                                /*Armado de produccion*/
                                string izquierda = node["Izq"].InnerText;
                                foreach (System.Xml.XmlNode nd in node["Ders"].ChildNodes)
                                {
                                    if (nd.NodeType == System.Xml.XmlNodeType.Element)
                                    {
                                        Produccion p = new Produccion();
                                        p.Izq = new NoTerminal(izquierda);
                                        string[] elementos = nd.InnerText.Split(new char[] { ' ' });
                                        foreach (string el in elementos)
                                        {
                                            try
                                            {
                                                if (esTerminal(el.Trim()))
                                                {
                                                    p.Der.Add(new Terminal(el.Trim()));
                                                }
                                                else
                                                {
                                                    p.Der.Add(new NoTerminal(el.Trim()));
                                                }
                                            }
                                            catch (Exception)
                                            {

                                                throw;
                                            }
                                        }
                                        this.producciones.Add(p);
                                    }
                                }
                            }
                            if (reader.Name.Equals("NoTerminales"))
                            {
                                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                                System.Xml.XmlNode node = doc.ReadNode(reader);
                                foreach (System.Xml.XmlNode nd in node.ChildNodes)
                                {
                                    if (nd.NodeType == System.Xml.XmlNodeType.Element)
                                    {
                                        string nt = nd.InnerText;
                                        this.noTerminales.Add(new NoTerminal(nt));
                                    }
                                }
                            }
                            if (reader.Name.Equals("SimboloInicial"))
                            {
                                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                                System.Xml.XmlNode node = doc.ReadNode(reader);
                                string nt = node["NoTerminal"].InnerText;
                                this.simboloInicial = this.EncontrarNoTerminal(nt);

                            }
                        }
                    }
                }
            int count = this.producciones.Count;

            }
            catch (Exception)
            {
                
                throw;
            }
        }

        bool esTerminal(string str)
        {
            /*si encontramos al menos 1 caracter en minúscula es un terminal*/
            for (int i = 0; i < str.Length; i++)
                if (!Char.IsUpper(str[i]))
                    return true;

            /*Si llegamos hasta acá es porque la cadena contiene todas mayúsculas*/
            return false;
        }

        private NoTerminal EncontrarNoTerminal(string p)
        {
            NoTerminal aux = new NoTerminal(p);

            return this.noTerminales.Find(

                delegate(NoTerminal _nt)
                {
                    if (_nt.Equals(aux))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                );
        }

        //private List<Terminal> Primeros(NoTerminal nt)
        //{
        //    List<Terminal> terminales = new List<Terminal>();

        //    List<Produccion> listaIteracion = new List<Produccion>();

        //    List<Produccion> subLista = this.ObtenerListaProduccionesParaUnNoTerminal(nt);

        //    bool primerVuelta = true;

        //    while (primerVuelta || (listaIteracion.Count > 0))
        //    {
        //        if (primerVuelta)
        //        {
        //            primerVuelta = false;
        //        }
        //        else
        //        {
        //            subLista = new List<Produccion>(listaIteracion);
        //        }

        //        foreach (Produccion prod in subLista)
        //        {
        //            listaIteracion = new List<Produccion>();

        //            if (prod.Der != null)
        //            {
        //                ElementoGramatica elem = prod.Der.First();                        

        //                if (elem.GetType() == typeof(Terminal))
        //                {
        //                    terminales.Add((Terminal)elem);
        //                }
        //                else
        //                {
        //                    listaIteracion.AddRange(this.ObtenerListaProduccionesParaUnNoTerminal((NoTerminal)elem));
      
        //                }

                        
        //            }
        //            else
        //            {
        //                Terminal t = new Terminal();
        //                t.Componente = new ComponenteLexico();
        //                t.Componente.Token = ComponenteLexico.TokenType.Ninguno;
        //                terminales.Add(t);
        //            }

        //        }
        //    }



        //    return terminales;
        //}

        private List<Terminal> Primeros(NoTerminal nt,Produccion p)
        {
            List<Terminal> terminales = new List<Terminal>();
            List<Produccion> listaIteracion = new List<Produccion>();
            List<Produccion> subLista = new List<Produccion>();
            subLista.Add(p);

            bool primerVuelta = true;

            while (primerVuelta || (listaIteracion.Count > 0))
            {
                if (primerVuelta)
                {
                    primerVuelta = false;
                }
                else
                {
                    subLista = new List<Produccion>(listaIteracion);
                }

                foreach (Produccion prod in subLista)
                {
                    listaIteracion = new List<Produccion>();

                    if (prod.Der != null)
                    {
                        ElementoGramatica elem = prod.Der.First();

                        if (elem.GetType() == typeof(Terminal))
                        {
                            terminales.Add((Terminal)elem);
                        }
                        else
                        {
                            listaIteracion.AddRange(this.ObtenerListaProduccionesParaUnNoTerminal((NoTerminal)elem));
                        }
                    }
                    else
                    {
                        Terminal t = new Terminal();
                        t.Componente = new ComponenteLexico();
                        t.Componente.Token = ComponenteLexico.TokenType.Ninguno;
                        terminales.Add(t);
                    }
                }
            }
            return terminales;
        }

        private List<Produccion> ObtenerListaProduccionesParaUnNoTerminal(NoTerminal nt)
        {
            return this.producciones.FindAll(

                delegate(Produccion _prod)
                {
                    if (_prod.Izq.Equals(nt))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                );
        }

        private bool TieneProduccionLambda(NoTerminal nt)
        {
            return this.producciones.Exists(

                delegate(Produccion _prod)
                {
                    if (_prod.Der == null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                );
        }

        private List<Terminal> Siguientes(NoTerminal nt)
        {
            //if (nt.Nombre == "NUMERO")
            //{
            //    Debugger.Break();
            //}

            List<Terminal> terminales = new List<Terminal>();
            List<NoTerminal> listaIteracion = new List<NoTerminal>();  
          
            List<ElementoGramatica> subLista = this.ObtenerProximoEnParteDerechaDe(nt);
            
            bool primerVuelta = true;

            while (primerVuelta || (listaIteracion.Count > 0))
            {
                if (primerVuelta)
                {
                    primerVuelta = false;
                }
                else
                {
                    subLista = new List<ElementoGramatica>();
                    foreach (NoTerminal noTerminales in listaIteracion)
                    {                        
                        subLista.AddRange(this.ObtenerProximoEnParteDerechaDe(noTerminales));
                    }
                   
                }

                listaIteracion = new List<NoTerminal>();

                foreach (ElementoGramatica elem in subLista)
                {
                    if (elem.GetType() == typeof(Terminal))
                    {
                        if (!terminales.Contains((Terminal)elem))
                        {
                            terminales.Add((Terminal)elem);
                        }
                        //terminales.Add((Terminal)elem);
                    }
                    else
                    {
                        foreach (Produccion prod in this.ObtenerListaProduccionesParaUnNoTerminal((NoTerminal)elem))
                        {
                            if (prod.Der != null)
                            {
                                //terminales.AddRange(this.Primeros((NoTerminal)elem, prod));

                                RetornoPrimeros retorno = (this.tablaPrimeros.Primeros((NoTerminal)elem, prod,true));
                                
                                //RetornoPrimeros retorno = (this.tablaPrimeros.Primeros((NoTerminal)elem, prod, false));


                                //terminales.AddRange(retorno.Terminales);

                                foreach (Terminal t in retorno.Terminales)
                                {
                                    if (!terminales.Contains(t))
                                    {
                                        terminales.Add(t);
                                    }
                                }


                                if (retorno.EsNecesarioSiguiente)
                                {
                                    if (!listaIteracion.Contains((NoTerminal)retorno.NoTerminal))
                                    {
                                        listaIteracion.Add(retorno.NoTerminal);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            //Saco todos los lambda
            terminales.RemoveAll(
                delegate(Terminal _t) 
                { 
                    return _t.Equals(Terminal.ElementoVacio()); 
                }
                
                );

            return terminales;
        }

        private List<ElementoGramatica> ObtenerProximoEnParteDerechaDe(NoTerminal nt)
        {
            List<ElementoGramatica> listaElementos = new List<ElementoGramatica>();

            List<NoTerminal> listaIteracion = new List<NoTerminal>();    
            List<NoTerminal> subLista = new List<NoTerminal>();
            subLista.Add(nt);

            bool primerVuelta = true;

            while (primerVuelta || (listaIteracion.Count > 0))
            {
                if (primerVuelta)
                {
                    primerVuelta = false;
                }
                else
                {
                    subLista = listaIteracion;
                }

                foreach (NoTerminal noTerminal in subLista)
                {
                    listaIteracion = new List<NoTerminal>();
                    //Si es el simbolo inicial, agrego el terminal EOF
                    if (this.simboloInicial.Equals(noTerminal))
                    {

                        listaElementos.Add(Terminal.ElementoEOF());
                    }

                    List<Produccion> listaProds = ObtenerProduccionesConNoTerminalEnParteDerecha(noTerminal);

                    if (listaProds.Count > 0)
                    {
                        foreach (Produccion prod in listaProds)
                        {
                            ElementoGramatica elem = prod.ObtenerSiguienteDe(noTerminal);

                            if (elem != null)
                            {
                                if (!listaElementos.Contains(elem))
                                {
                                    listaElementos.Add(elem);
                                }
                            }
                            else
                            {
                                if (prod.Der != null)
                                {
                                    if (prod.Der.Last().GetType() == typeof(NoTerminal))
                                    {
                                        if (!noTerminal.Equals(prod.Izq))
                                        {
                                            if (!listaIteracion.Contains(noTerminal))
                                            {
                                                listaIteracion.Add(prod.Izq);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return listaElementos;
        }

        private List<Produccion> ObtenerProduccionesConNoTerminalEnParteDerecha(NoTerminal nt)
        {
             return this.producciones.FindAll(

                    delegate(Produccion _prod)
                    {
                        if (_prod.ApareceEnParteDerecha(nt))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
            );

        }



        private TablaPrimeros ArmarTablaPrimeros()
        {
            TablaPrimeros tabla = new TablaPrimeros();

            List<Terminal> termsPrim = new List<Terminal>();

            foreach (NoTerminal nt in noTerminales)
            {
                //if (nt.Nombre == "CONSTANTES")
                //{
                //    Debugger.Break();
                //}

                foreach (Produccion prod in this.ObtenerListaProduccionesParaUnNoTerminal(nt))
                {
                    termsPrim = new List<Terminal>();


                    termsPrim.AddRange(this.Primeros(nt, prod));
                    tabla.AgregarNodo(nt, prod, termsPrim);

                }
            }
            return tabla;

        }

        public TablaAnalisisGramatica ArmarTablaAnalisis()
        {
            try
            {
                TablaAnalisisGramatica tabla = new TablaAnalisisGramatica();



                List<Terminal> termsPrim = new List<Terminal>();
                List<Terminal> termsSig = new List<Terminal>();

                foreach (NoTerminal nt in noTerminales)
                {
                    foreach (Produccion prod in this.ObtenerListaProduccionesParaUnNoTerminal(nt))
                    {
                        termsPrim = new List<Terminal>();
                        termsSig = new List<Terminal>();

                        //if (nt.Nombre == "CONSTANTES")
                        //{
                        //    Debugger.Break();
                        //}

                        //termsPrim.AddRange(this.Primeros(nt,prod));
                        RetornoPrimeros rt = this.tablaPrimeros.Primeros(nt, prod,false);
                        termsPrim.AddRange(rt.Terminales);
                        tabla.AgregarPrimeros(nt, termsPrim, prod);

                        bool existeLamba = termsPrim.Exists(

                            delegate(Terminal _t)
                            {
                                if (_t.Componente.Token == ComponenteLexico.TokenType.Ninguno)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                        );                                          

                        if (existeLamba)
                        {
                            termsSig.AddRange(this.Siguientes(nt));    
                            tabla.AgregarSiguientes(nt, termsSig);
                        }

                    }

                    //if (nt.Nombre == "CON")
                    //{
                    //    if (Debugger.IsAttached)
                    //    {
                    //        Debugger.Break();
                    //    }
                    //}

                    termsSig = new List<Terminal>();
                    termsSig.AddRange(this.Siguientes(nt));
                    tabla.AgregarSincronizacion(nt, termsSig);
                }
                return tabla;
            }
            catch (Exception ex)
            {
                Utils.Log.AddError(ex.Message);
                throw new Exception("Error al crear la tabla de analisis");
            }
        }



        internal bool ContieneElementoFinSentencia(NoTerminal nt)
        {
            List<ElementoGramatica> listaElementos = new List<ElementoGramatica>();

            List<NoTerminal> listaIteracion = new List<NoTerminal>();
            List<NoTerminal> subLista = new List<NoTerminal>();
            subLista.Add(nt);

            bool primerVuelta = true;

            while (primerVuelta || (listaIteracion.Count > 0))
            {
                if (primerVuelta)
                {
                    primerVuelta = false;
                }
                else
                {
                    subLista = new List<NoTerminal>(listaIteracion);
                }

                listaIteracion = new List<NoTerminal>();

                foreach (NoTerminal noTerminal in subLista)
                {                  

                    List<Produccion> listaProds = this.ObtenerListaProduccionesParaUnNoTerminal(noTerminal);

                    if (listaProds.Count > 0)
                    {
                        foreach (Produccion prod in listaProds)
                        {
                            if (prod.Der != null)
                            {
                                if (prod.Der.Contains(Terminal.ElementoFinSentencia()))
                                {
                                    return true;
                                }
                                else
                                {
                                    for (int i = 0; i < prod.Der.Count; i++)
                                    {
                                        if (prod.Der[i].GetType() == typeof(NoTerminal) && !prod.Der.Contains(noTerminal) && !prod.Der.Contains(nt))
                                        {
                                            if (!listaIteracion.Contains((NoTerminal)prod.Der[i]))
                                            {
                                                listaIteracion.Add((NoTerminal)prod.Der[i]);
                                            }
                                        }

                                    }
                                }
                            }                            
                        }
                    }
                }
            }

            return false;
        }

        internal List<Produccion> ObtenerProduccionesParaSalvarError(NoTerminal nt)
        {
            List<Produccion> listaProducciones = new List<Produccion>();

            List<NoTerminal> listaIteracion = new List<NoTerminal>();
            List<NoTerminal> subLista = new List<NoTerminal>();
            subLista.Add(nt);

            bool primerVuelta = true;

            while (primerVuelta || (listaIteracion.Count > 0))
            {
                if (primerVuelta)
                {
                    primerVuelta = false;
                }
                else
                {
                    subLista = new List<NoTerminal>(listaIteracion);
                }

                listaIteracion = new List<NoTerminal>();

                foreach (NoTerminal noTerminal in subLista)
                {
                    bool encontrado = false;

                    List<Produccion> listaProds = this.ObtenerListaProduccionesParaUnNoTerminal(noTerminal);

                    if (listaProds.Count > 0)
                    {
                        foreach (Produccion prod in listaProds)
                        {
                            
                            if (prod.Der != null)
                            {
                                for (int i = 0; i < prod.Der.Count; i++)
                                {
                                    if (prod.Der[i].GetType() == typeof(NoTerminal))
                                    {
                                        if (prod.Der.Contains(Terminal.ElementoFinSentencia())
                                            || 
                                            ContieneElementoFinSentencia((NoTerminal)prod.Der[i])                                             
                                            )
                                        {
                                            listaProducciones.Add(prod);
                                            if (!prod.Der.Contains(Terminal.ElementoFinSentencia()))
                                            {
                                                listaIteracion.Add((NoTerminal)prod.Der[i]);
                                            }
                                            encontrado = true;
                                            break;
                                        }
                                    }

                                }
                                
                            }
                            if (encontrado)
                            {
                                break;
                            }
                        }
                    }
                    if (encontrado)
                    {
                        break;
                    }
                }
            }

            return listaProducciones;
        }

        internal bool NoTerminalGeneraProduccionVacia(NoTerminal nt)
        {
            return this.producciones.Exists(

                    delegate(Produccion _prod)
                    {
                        if (_prod.Izq.Equals(nt) && _prod.ProduceElementoVacio())
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
            );
        }
    }
}
