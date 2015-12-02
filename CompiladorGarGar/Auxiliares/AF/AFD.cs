using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using CompiladorGargar.Lexicografico;

namespace CompiladorGargar.Auxiliares.AF
{
    class AFD
    {
        private List<Estado> estados;
        private List<FuncionTransicion> transiciones;

        private String alfabeto;
        public String Alfabeto
        {
            get { return alfabeto; }
        }

        private Estado estadoActual;
        public Estado EstadoActual
        {
            get { return estadoActual; }
        }

      
        

        public AFD()
        {

            this.estados = new List<Estado>();
            this.transiciones = new List<FuncionTransicion>();

            try
            {
                this.ArmarAFD();
                this.ResetearAFD();
            }
            catch (Exception ex)
            {
                Utils.Log.AddError(ex.Message);
                throw new Exception("No se pudo armar el AFD" + "\r\n" + ex.Message);
            }
        }

        public void ResetearAFD()
        {
            this.estadoActual = this.estados.Find(delegate(Estado _e) { if (_e.EsInicial == true) return true; else return false; });
        }

        public void Avanzar(char x)
        {
            x = Convert.ToChar(x.ToString().ToLower());
            FuncionTransicion auxTran = this.transiciones.Find(delegate(FuncionTransicion _t) { if ((_t.EstadoInicial == this.estadoActual.Nombre)&&(_t.Caracter == x)) return true; else return false; });

            if (auxTran != null)
            {
                this.estadoActual = this.estados.Find(delegate(Estado _e) { if (_e.Nombre == auxTran.EstadoFinal) return true; else return false; });
            }
        }

        public bool TryAvanzar(char x)
        {
            x = Convert.ToChar(x.ToString().ToLower());
            return this.transiciones.Exists(delegate(FuncionTransicion _t) { if ((_t.EstadoInicial == this.estadoActual.Nombre) && (_t.Caracter == x)) return true; else return false; });
        }

        #region Construccion AFD
        private void ArmarAFD()
        {
            try
            {
                StreamReader arch;
                
                //byte[] a = System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(CompiladorGargar.Properties.Resources.AFD_GARGAR);
                byte[] a = Encoding.UTF8.GetBytes(CompiladorGargar.Properties.Resources.AFD_GARGAR);
                System.IO.MemoryStream m = new System.IO.MemoryStream(a);
                arch = new StreamReader(m);
                    
               

                try
                {
                    short i,lineas,lineasTotal;
                    short contLineas = 0;

                    lineasTotal = Convert.ToInt16(arch.ReadLine());
                    
                    lineas = Convert.ToInt16(arch.ReadLine());
                    contLineas++;
                    for (i = 0; i < lineas; i++)
                    {
                        String estados = arch.ReadLine();
                        this.CargarEstados(estados);
                        contLineas++;
                    }

                    lineas = Convert.ToInt16(arch.ReadLine());
                    contLineas++;
                    for (i = 0; i < lineas; i++)
                    {
                        String alfabeto = arch.ReadLine();                        
                        this.CargarAlfabeto(alfabeto);
                        contLineas++;
                    }

                    lineas = Convert.ToInt16(arch.ReadLine());
                    contLineas++;
                    for (i = 0; i < lineas; i++)
                    {
                        String transiciones = arch.ReadLine();
                        try
                        {
                            this.CargarTransiciones(transiciones);
                            contLineas++;
                        }
                        catch (Exception)
                        {
                            
                            throw;
                        }
                        
                    }

                    lineas = Convert.ToInt16(arch.ReadLine());
                    contLineas++;
                    for (i = 0; i < lineas; i++)
                    {
                        String inicial = arch.ReadLine();
                        this.CargarEstadoInicial(inicial);
                        contLineas++;
                    }

                    lineas = Convert.ToInt16(arch.ReadLine());
                    contLineas++;
                    for (i = 0; i < lineas; i++)
                    {
                        String finales = arch.ReadLine();
                        try
                        {
                            this.CargarEstadosFinales(finales);
                            contLineas++;
                        }                       
                        
                        catch (Exception)
                        {
                            
                            throw;
                        }
                    }

                    if (contLineas != lineasTotal)
                    {
                        throw new Exception("La cantidad de lineas totales no coincide con la cantidad de lineas leidas");
                    }
                    

                }
                catch (Exception ex)
                {
                    Utils.Log.AddError(ex.Message);
                    throw new Exception("No se pudo armar el AFD por errores de formato en el archivo" + "\r\n" + ex.Message);
                }
                finally
                {
                    arch.Close();
                }
            }
            catch (Exception ex)
            {
                Utils.Log.AddError(ex.Message);
                throw new Exception("No se pudo armar el AFD por errores al intentar abrir el archivo" + "\r\n" + ex.Message);
            }
        }

        private void CargarEstadosFinales(String cadena)
        {
            try
            {
                string[] auxEstadosFinales = cadena.Split(new string[] { "%%" }, StringSplitOptions.None);

                foreach (String duplas in auxEstadosFinales)
                {
                    string[] aux = duplas.Split(new string[] { "~~" }, StringSplitOptions.None);

                    Estado auxEstado = this.estados.Find(delegate(Estado _e) { if (_e.Nombre == aux[0]) return true; else return false; });
                    if (auxEstado != null)
                    {

                        auxEstado.EsFinal = true;
                        try
                        {
                            auxEstado.Token = (ComponenteLexico.TokenType)EnumUtils.enumValueOf(aux[1].ToString(), typeof(ComponenteLexico.TokenType));
                        }
                        catch (Exception ex)
                        {
                            Utils.Log.AddError(ex.Message);
                            throw new Exception("Al estado final " + auxEstado.Nombre + " no se le pudo asignar el TokenType con valor "+ aux[1].ToString());
                        }
                        
                    }
                    else
                    {
                        throw new Exception("El estado final " + aux[0] + " ingresado en el archivo no existia entre los estados definidos");
                    }                    
                }
            }
            catch (Exception ex)
            {
                Utils.Log.AddError(ex.Message);
                throw new Exception("Error al intentar asignar los estados finales",ex);
            }
        }

        private void CargarEstadoInicial(String cadena)
        {
            try
            {
                Estado aux = this.estados.Find(delegate(Estado _e) { if (_e.Nombre == cadena) return true; else return false; });
                if (aux != null)
                {
                    aux.EsInicial = true;
                }
                else
                {
                    throw new Exception("El estado inicial " + cadena + " ingresado en el archivo no existia entre los estados definidos");
                }
            }
            catch (Exception ex)
            {
                Utils.Log.AddError(ex.Message);
                throw new Exception("Error al intentar asignar el estado inicial");
            }
        }

        private void CargarTransiciones(String cadena)
        {
            try
            {
                string[] auxTransiciones = cadena.Split(new string[] { "%%" }, StringSplitOptions.None);

                foreach (String trans in auxTransiciones)
                {
                    string[] aux = trans.Split(new string[] { "~~" }, StringSplitOptions.None);

                    //Esto es para escapar la , y el ; del lenguaje en la carga del AFD
                    //string simbolo = (aux[1] == "coma") ? "," : ((aux[1] == "puntoycoma") ? ";" : aux[1]);
                    string simbolo = aux[1];

                    if (this.ExisteEstado(aux[0]) && this.ExisteEstado(aux[2]) && this.ExisteSimbolo(simbolo))
                    {
                        this.transiciones.Add(new FuncionTransicion(aux[0], char.Parse(simbolo), aux[2]));
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.Log.AddError(ex.Message);
                throw new Exception("Error al intentar asignar las transiciones");
            }
        }

        private bool ExisteSimbolo(string p)
        {
            return alfabeto.Contains(p);
        }

        private bool ExisteEstado(string p)
        {
            return this.estados.Exists(delegate(Estado _e) { if (_e.Nombre == p) return true; else return false; });
        }

        private void CargarAlfabeto(String cadena)
        {
            this.alfabeto += cadena;
        }

        private void CargarEstados(String cadena)
        {
            try
            {
                string[] aux = cadena.Split(new string[] { "~~" }, StringSplitOptions.None);

                foreach (String estado in aux)
                {
                    try
                    {

                        this.estados.Add(new Estado(estado));
                    }
                    catch (Exception ex)
                    {
                        Utils.Log.AddError(ex.Message);
                        throw new Exception("Error al intentar cargar el estado "+estado);
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.Log.AddError(ex.Message);
                throw new Exception("Error al intentar cargar los estados");
            }
        }

        #endregion
    }
}
