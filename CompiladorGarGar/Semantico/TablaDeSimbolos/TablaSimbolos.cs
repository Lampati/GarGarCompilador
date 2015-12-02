using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Semantico.TablaDeSimbolos;
using System.Diagnostics;
using CompiladorGargar.Auxiliares;
using System.Globalization;

namespace CompiladorGargar.Semantico.TablaDeSimbolos
{
    public class TablaSimbolos
    {
        private List<NodoTipoArreglo> listaTiposArreglos;
        internal List<NodoTipoArreglo> ListaTiposArreglos
        {
            get { return listaTiposArreglos; }
        }

        private List<NodoTablaSimbolos> listaNodos;
        internal List<NodoTablaSimbolos> ListaNodos
        {
            get { return listaNodos; }
        }


        internal TablaSimbolos()
        {
            this.listaNodos = new List<NodoTablaSimbolos>();
            this.listaTiposArreglos = new List<NodoTipoArreglo>();
        }  

        #region Tipos Arreglo

        internal string AgregarTipoArreglo(NodoTablaSimbolos.TipoDeDato tipo, string rango)
        {
            string nombreTipo;

            if (!listaTiposArreglos.Exists(m => m.Rango == rango && m.TipoDato == tipo))
            {
                NodoTipoArreglo t = new NodoTipoArreglo(tipo, rango);
                listaTiposArreglos.Add(t);
                nombreTipo = t.Nombre;
            }
            else
            {
                NodoTipoArreglo t = listaTiposArreglos.Find(m => m.Rango == rango && m.TipoDato == tipo);                
                nombreTipo = t.Nombre;
            }

            return nombreTipo;
        }


        #endregion


        internal void AgregarAuxiliarParaCodIntermedio(string nombre, NodoTablaSimbolos.TipoDeDato tdato)
        {

            this.listaNodos.Add(new NodoTablaSimbolos(nombre, NodoTablaSimbolos.TipoDeEntrada.AuxiliarCodigoIntermedio, tdato,false,NodoTablaSimbolos.TipoContexto.Global,null));
        }

        

        #region Manejo Variables

        internal void AgregarParametroDeProc(string nombre, NodoTablaSimbolos.TipoDeDato tdato, NodoTablaSimbolos.TipoContexto contexto, string nombreProc)
        {

            this.listaNodos.Add(new NodoTablaSimbolos(nombre, NodoTablaSimbolos.TipoDeEntrada.Parametro, tdato,  false, contexto, nombreProc));
        }

        internal void AgregarVariable(string nombre, NodoTablaSimbolos.TipoDeDato tdato, bool esConstante, NodoTablaSimbolos.TipoContexto contexto, string nombreProc)
        {

            this.listaNodos.Add(new NodoTablaSimbolos(nombre, NodoTablaSimbolos.TipoDeEntrada.Variable, tdato, esConstante, contexto, nombreProc));
        }

        internal void AgregarVariable(string nombre, NodoTablaSimbolos.TipoDeDato tdato, bool esConstante, NodoTablaSimbolos.TipoContexto contexto, string nombreProc, double valorInt)
        {

            this.listaNodos.Add(new NodoTablaSimbolos(nombre, NodoTablaSimbolos.TipoDeEntrada.Variable, tdato, esConstante, contexto, nombreProc) { Valor = valorInt} );
        }

        internal void AgregarVariable(string nombre, NodoTablaSimbolos.TipoDeDato tdato, bool esConstante, NodoTablaSimbolos.TipoContexto contexto, string nombreProc, string valor)
        {
            this.listaNodos.Add(new NodoTablaSimbolos(nombre, NodoTablaSimbolos.TipoDeEntrada.Variable, tdato, esConstante, contexto, nombreProc) { ValorTexto = valor });
        }


        internal List<NodoTablaSimbolos> ObtenerVariablesDeclaradasEnProcedimiento(Semantico.TablaDeSimbolos.NodoTablaSimbolos.TipoContexto cont, string nombreProc)
        {
            if (cont == NodoTablaSimbolos.TipoContexto.Global)
            {
                return this.listaNodos.FindAll(x => x.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Variable
                                    && x.Contexto == NodoTablaSimbolos.TipoContexto.Global);
            }
            else
            {
                return this.listaNodos.FindAll(x => x.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Variable
                                    && x.Contexto == NodoTablaSimbolos.TipoContexto.Local
                                    && x.NombreContextoLocal.ToUpper().Equals(nombreProc.ToUpper()));
            }
            
        }


        internal List<NodoTablaSimbolos> ObtenerAuxilairesParaCodIntermedio()
        {
            return this.listaNodos.FindAll(x => x.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.AuxiliarCodigoIntermedio);
        }
        
        //public bool ExisteVariable(string nombre)
        //{
        //    return this.listaNodos.Exists(

        //        delegate(NodoTablaSimbolos _nodo)
        //        {
        //            return (_nodo.Nombre.Equals(nombre) &&
        //                ((_nodo.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Variable) || (_nodo.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Parametro)) 
        //                //&& (_nodo.EsArreglo == false) 
        //                );
        //        }
        //        );
        //}

        
        
        //public bool ExisteVariable(string nombre, NodoTablaSimbolos.TipoContexto contexto, string nombreContexto)
        //{
        //    return this.listaNodos.Exists(

        //        delegate(NodoTablaSimbolos _nodo)
        //        {
        //            return (_nodo.Nombre.Equals(nombre) &&
        //                ((_nodo.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Variable) || (_nodo.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Parametro)) &&
        //                ((_nodo.Contexto == contexto) && 
        //                (_nodo.NombreContextoLocal == nombreContexto))
        //                );
        //        }
        //        );
        //}

        internal bool ExisteVariable(string nombre, NodoTablaSimbolos.TipoContexto contexto, string nombreContexto)
        {
            return ObtenerVariable(nombre, contexto, nombreContexto) != null;
        }

        internal bool ExisteVariableEnEsteContexto(string nombre, NodoTablaSimbolos.TipoContexto contexto, string nombreContexto)
        {
            NodoTablaSimbolos nodo = ObtenerVariable(nombre, contexto, nombreContexto);            

            return (nodo != null && nodo.Contexto == contexto && nodo.NombreContextoLocal == nombreContexto);
        }

        private NodoTablaSimbolos ObtenerVariable(string nombre, NodoTablaSimbolos.TipoContexto contexto, string nombreContexto)
        {
            NodoTablaSimbolos nodo;
            
            nodo = this.listaNodos.Find(

                delegate(NodoTablaSimbolos _nodo)
                {
                    //flanzani 23/10/2010
                    //Faltaba agregar el nombre del contexto local, pq sino puede traer de cualquiera.
                    return (
                            _nodo.Contexto == NodoTablaSimbolos.TipoContexto.Local &&
                            _nodo.Nombre.Equals(nombre,StringComparison.InvariantCultureIgnoreCase) &&
                            _nodo.EsArreglo == false &&
                            ((_nodo.NombreContextoLocal == nombreContexto &&
                            (_nodo.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Variable)) ||
                            (_nodo.NombreContextoLocal == nombreContexto && 
                            _nodo.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Parametro))
                    
                    );
                }
                );

            if (nodo == null)
            {
                nodo = this.listaNodos.Find(

                delegate(NodoTablaSimbolos _nodo)
                {
                    return (_nodo.EsArreglo == false &&
                        _nodo.Contexto == NodoTablaSimbolos.TipoContexto.Global &&
                        _nodo.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Variable && 
                        _nodo.Nombre.Equals(nombre));
                }
                );
            }

            //Debug.Assert(nodo != null, new StringBuilder("La variable ").Append(nombre).Append(" no estaba declarada.").ToString());

            return nodo;
        }

        internal NodoTablaSimbolos.TipoDeDato ObtenerTipoVariable(string nombre, NodoTablaSimbolos.TipoContexto contexto, string nombreContexto)
        {
            NodoTablaSimbolos nodo = this.ObtenerVariable(nombre,contexto,nombreContexto);
            return nodo.TipoDato;
        }

       

        //public NodoTablaSimbolos.TipoContexto ObtenerContextoVariable(string nombre, NodoTablaSimbolos.TipoContexto contexto, string nombreContexto)
        //{
        //    NodoTablaSimbolos nodo = this.ObtenerVariable(nombre, contexto, nombreContexto);
        //    return nodo.Contexto;
        //}

        //public string ObtenerNombreContextoVariable(string nombre, NodoTablaSimbolos.TipoContexto contexto, string nombreContexto)
        //{
        //    NodoTablaSimbolos nodo = this.ObtenerVariable(nombre, contexto, nombreContexto);
        //    return nodo.NombreContextoLocal;
        //}

        internal bool EsModificableValorVarible(string nombre, NodoTablaSimbolos.TipoContexto contexto, string nombreContexto)
        {
            NodoTablaSimbolos nodo = this.ObtenerVariable(nombre,contexto,nombreContexto);
            return !nodo.EsConstante;
        }

        internal double RetornarValorConstante(string nombre, NodoTablaSimbolos.TipoContexto contexto, string nombreContexto)
        {
            NodoTablaSimbolos nodo = this.ObtenerVariable(nombre, contexto, nombreContexto);
            if (nodo.EsConstante)
            {
                return nodo.Valor;
            }
            else
            {
                return int.MinValue;
            }
        }

                

        #endregion


        #region Manejo Arreglos

        internal bool AgregarArreglo(string nombre, NodoTablaSimbolos.TipoDeDato tdato, NodoTablaSimbolos.TipoContexto contexto, string nombreContexto, string rango, bool esConst)
        {
            //Para que no falle pq se puso mal el rango del arrego, si viene en null, se pone en 1
            if (rango == null)
            {
                rango = "1";
            }

            double rangoNum;
            if (!double.TryParse(rango, System.Globalization.NumberStyles.Number, new CultureInfo("es-AR"), out rangoNum))
            {
                NodoTablaSimbolos nodoConstante = this.listaNodos.Find(x => x.Nombre.ToUpper().Equals(rango.ToUpper()) && x.EsConstante);

                rangoNum = nodoConstante.Valor;
            }

            //o sea, que sea entero
            if (Math.Truncate(rangoNum) == rangoNum)
            {



                this.listaNodos.Add(

                        new NodoTablaSimbolos(nombre, NodoTablaSimbolos.TipoDeEntrada.Variable,
                       tdato, true, esConst, contexto,
                       nombreContexto) { Valor = rangoNum }

                       );

                return true;
            }
            else
            {
                //Esto seria un error, no se puede tener un arreglo con rango decimal
                return false;
            }
        }

        internal bool AgregarArregloParametroDeProc(string nombre, NodoTablaSimbolos.TipoDeDato tipoDeDato, NodoTablaSimbolos.TipoContexto tipoContexto, string nombreProc, string rango)
        {
            double rangoNum;
            if (!double.TryParse(rango, System.Globalization.NumberStyles.Number, new CultureInfo("es-AR"), out rangoNum))
            {
                NodoTablaSimbolos nodoConstante = this.listaNodos.Find(x => x.Nombre.ToUpper().Equals(rango.ToUpper()) && x.EsConstante);

                rangoNum = nodoConstante.Valor;
            }

              //o sea, que sea entero
            if (Math.Truncate(rangoNum) == rangoNum)
            {


                this.listaNodos.Add(new NodoTablaSimbolos(nombre, NodoTablaSimbolos.TipoDeEntrada.Parametro, tipoDeDato, true, false, tipoContexto, nombreProc) { Valor = rangoNum });

                return true;
            }
            else
            {
                //Esto seria un error, no se puede tener un arreglo con rango decimal
                return false;
            }
        }


        internal double ObtenerTopeArreglo(string nombre, NodoTablaSimbolos.TipoContexto contexto, string nombreContexto)
        {

            NodoTablaSimbolos arr = ObtenerArreglo(nombre, contexto, nombreContexto);
            return arr.Valor;
        }
        

        //public bool ExisteArreglo(string nombre, NodoTablaSimbolos.TipoContexto contexto, string nombreContexto)
        //{
        //    return this.listaNodos.Exists(

        //        delegate(NodoTablaSimbolos _nodo)
        //        {
        //            return ((_nodo.Nombre.Equals(nombre)) &&
        //                (_nodo.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Variable || _nodo.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Parametro) &&
        //                (_nodo.EsArreglo == true) &&
        //                (_nodo.Contexto == contexto) &&
        //                (_nodo.NombreContextoLocal == nombreContexto) 
                        
        //                );
        //        }
        //        );
        //}

        internal bool ExisteArreglo(string nombre, NodoTablaSimbolos.TipoContexto contexto, string nombreContexto)
        {
            return ObtenerArreglo(nombre, contexto, nombreContexto) != null;

        }

        internal bool ExisteArregloEnEsteContexto(string nombre, NodoTablaSimbolos.TipoContexto contexto, string nombreContexto)
        {
            NodoTablaSimbolos nodo = ObtenerArreglo(nombre, contexto, nombreContexto);

            return (nodo != null && nodo.Contexto == contexto && nodo.NombreContextoLocal == nombreContexto);
        }

        private NodoTablaSimbolos ObtenerArreglo(string nombre, NodoTablaSimbolos.TipoContexto contexto, string nombreContexto)
        {
            NodoTablaSimbolos nodo;

            nodo = this.listaNodos.Find(

                delegate(NodoTablaSimbolos _nodo)
                {
                    //flanzani 23/10/2010
                    //Faltaba agregar el nombre del contexto local, pq sino puede traer de cualquiera.
                    return (
                            _nodo.Contexto == NodoTablaSimbolos.TipoContexto.Local &&
                            _nodo.Nombre.ToUpper().Equals(nombre.ToUpper()) &&
                            _nodo.EsArreglo &&
                            ((_nodo.NombreContextoLocal == nombreContexto &&
                            (_nodo.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Variable)) ||
                            (_nodo.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Parametro))

                    );
                }
                );

            if (nodo == null)
            {
                nodo = this.listaNodos.Find(

                delegate(NodoTablaSimbolos _nodo)
                {
                    return _nodo.EsArreglo &&
                        (_nodo.Contexto == NodoTablaSimbolos.TipoContexto.Global &&
                        _nodo.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Variable && _nodo.Nombre.ToUpper().Equals(nombre.ToUpper()));
                }
                );
            }

            //Debug.Assert(nodo != null, new StringBuilder("La variable ").Append(nombre).Append(" no estaba declarada.").ToString());

            return nodo;
        }


        internal NodoTablaSimbolos.TipoDeDato ObtenerTipoArreglo(string nombre, NodoTablaSimbolos.TipoContexto contexto, string nombreContexto)
        {
            NodoTablaSimbolos nodo = this.ObtenerArreglo(nombre, contexto, nombreContexto);
            return nodo.TipoDato;
            
            //return this.ObtenerTipoVariable(nombre,NodoTablaSimbolos.TipoContexto.Global,string.Empty);            
        }

       

       
        #endregion


        #region Manejo Procedimientos/Funciones

        internal void AgregarProcedimiento(string nombre, List<FirmaProc> firma)
        {
            this.listaNodos.Add(new NodoTablaSimbolos(nombre, NodoTablaSimbolos.TipoDeEntrada.Procedimiento,
                NodoTablaSimbolos.TipoDeDato.Ninguno, firma));
        }

        internal void AgregarFuncion(string nombre, List<FirmaProc> firma, 
            NodoTablaSimbolos.TipoDeDato tdato)
        {
            this.listaNodos.Add(new NodoTablaSimbolos(nombre, NodoTablaSimbolos.TipoDeEntrada.Funcion, tdato, firma));
        }

        internal void AgregarProcedimientoDelFramework(string nombre, List<FirmaProc> firma, string codigoPascal, string nombreFunc)
        {
            this.listaNodos.Add(new NodoTablaSimbolos(nombre, NodoTablaSimbolos.TipoDeEntrada.Procedimiento,
                NodoTablaSimbolos.TipoDeDato.Ninguno, firma) 
                { EsDelFramework = true , CodigoPascalParaElFramework = codigoPascal, NombreFuncionEnPascal = nombreFunc}
            );
        }

        internal void AgregarFuncionDelFramework(string nombre, List<FirmaProc> firma,
            NodoTablaSimbolos.TipoDeDato tdato, string codigoPascal, string nombreFunc)
        {
            this.listaNodos.Add(new NodoTablaSimbolos(nombre, NodoTablaSimbolos.TipoDeEntrada.Funcion, tdato, firma) 
            { EsDelFramework = true, CodigoPascalParaElFramework = codigoPascal , NombreFuncionEnPascal = nombreFunc}
            );
        }

        internal bool ExisteProcedimiento(string nombre)
        {
            return this.listaNodos.Exists(

                delegate(NodoTablaSimbolos _nodo)
                {
                    return (_nodo.Nombre.ToUpper().Equals(nombre.ToUpper()) && (_nodo.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Procedimiento));
                }
                );
        }

        internal bool ExisteFuncion(string nombre)
        {
            return this.listaNodos.Exists(

                delegate(NodoTablaSimbolos _nodo)
                {
                    return (_nodo.Nombre.ToUpper().Equals(nombre.ToUpper()) && (_nodo.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Funcion));
                }
                );
        }

        private NodoTablaSimbolos ObtenerRutina(string nombre, NodoTablaSimbolos.TipoDeEntrada tipo)
        {
            NodoTablaSimbolos nodo;

            nodo = this.listaNodos.Find(

                delegate(NodoTablaSimbolos _nodo)
                {
                    return (_nodo.TipoEntrada == tipo &&
                         _nodo.Nombre.ToUpper().Equals(nombre.ToUpper()));
                }
                );

            Debug.Assert(nodo != null, new StringBuilder("La variable ").Append(nombre).Append(" no estaba declarada.").ToString());

            return nodo;
        }

        internal List<FirmaProc> ObtenerFirma(string nombre, NodoTablaSimbolos.TipoDeEntrada tipo)
        {
            NodoTablaSimbolos nodo = this.ObtenerRutina(nombre, tipo);

            return nodo.Firma;
        }

        //internal List<string> ObtenerParametros(string nombre)
        //{
        //    List<NodoTablaSimbolos> nodos = this.listaNodos.FindAll(
        //        delegate(NodoTablaSimbolos _nodo) 
        //        {
        //            return (_nodo.NombreContextoLocal.Equals(nombre) && (_nodo.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Parametro));
        //        }                   
        //    );

        //    List<string> retorno = new List<string>();

        //    foreach (NodoTablaSimbolos nodo in nodos)
        //    {
        //        retorno.Add(nodo.Nombre);
        //    }

        //    return retorno;
        //}

        internal NodoTablaSimbolos.TipoDeDato ObtenerTipoFuncion(string nombre)
        {
            NodoTablaSimbolos nodo = this.ObtenerRutina(nombre, NodoTablaSimbolos.TipoDeEntrada.Funcion);

            return nodo.TipoDato;
        }

        internal NodoTablaSimbolos.TipoDeDato ObtenerTipoProcedimiento(string nombre)
        {
            NodoTablaSimbolos nodo = this.ObtenerRutina(nombre, NodoTablaSimbolos.TipoDeEntrada.Procedimiento);

            return nodo.TipoDato;
        }


        #endregion






        internal bool EsParametroDeEsteProc(string nombre, NodoTablaSimbolos.TipoContexto tipoContexto, string nombreCont)
        {

            NodoTablaSimbolos nodo = ObtenerVariable(nombre, tipoContexto, nombreCont);

            return nodo != null
                && nodo.Nombre.ToUpper().Equals(nombre.ToUpper()) 
                && nodo.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Parametro
                && nodo.Contexto == tipoContexto
                && nodo.NombreContextoLocal == nombreCont;
            
        }

        internal void AgregarConstante(string nombre, NodoTablaSimbolos.TipoDeDato tipo, NodoTablaSimbolos.TipoContexto tipoContexto, string nombreProc, double valorInt)
        {            
            AgregarVariable(nombre, tipo, true, tipoContexto, nombreProc, valorInt); 
        }

        internal void AgregarConstante(string nombre, NodoTablaSimbolos.TipoDeDato tipo, NodoTablaSimbolos.TipoContexto tipoContexto, string nombreProc, string valorTexto)
        {
            AgregarVariable(nombre, tipo, true, tipoContexto, nombreProc, valorTexto);
        }

        internal bool EsVariableGlobal(string nombre, NodoTablaSimbolos.TipoContexto tipoContexto, string nombreCont)
        {
            NodoTablaSimbolos nodo = ObtenerVariable(nombre, tipoContexto, nombreCont);

            return nodo != null
                && nodo.Nombre.Equals(nombre)
                && nodo.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Variable
                && nodo.Contexto == NodoTablaSimbolos.TipoContexto.Global;

        }


        #region Metodos para obtener desde afuera el contenido 

        public List<NodoTablaSimbolos> ObtenerVariablesGlobales()
        {
            return listaNodos.FindAll(x => x.Contexto == NodoTablaSimbolos.TipoContexto.Global
                                      && !x.EsConstante
                                      && x.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Variable
                    );
        }

        public List<NodoTablaSimbolos> ObtenerVariablesDelProcPrincipal()
        {
            return listaNodos.FindAll(x => x.Contexto == NodoTablaSimbolos.TipoContexto.Local
                                      && !x.EsConstante
                                      && x.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Variable
                                      && x.NombreContextoLocal.ToUpper().Equals("PRINCIPAL")
                    );
        }

        public List<NodoTablaSimbolos> ObtenerParametrosDelProcSalida()
        {
            return listaNodos.FindAll(x => x.Contexto == NodoTablaSimbolos.TipoContexto.Local
                                      && !x.EsConstante
                                      && x.TipoEntrada == NodoTablaSimbolos.TipoDeEntrada.Parametro
                                      && x.NombreContextoLocal.ToUpper().Equals("SALIDA")
                    );
        }

        #endregion

        internal bool EsNombreFuncionFramework(string p)
        {
            return this.listaNodos.Exists(x => x.EsDelFramework && x.Nombre.ToUpper().Equals(p.ToUpper()));
        }    

        internal string ObtenerNombrePascalFuncionFramework(string p)
        {
            return this.listaNodos.Find(x => x.EsDelFramework && x.Nombre.ToUpper().Equals(p.ToUpper())).NombreFuncionEnPascal;
        }
    }
}
