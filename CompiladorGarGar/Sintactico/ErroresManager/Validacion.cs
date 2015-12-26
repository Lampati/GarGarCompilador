using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Sintactico.ErroresManager.Errores;

namespace CompiladorGargar.Sintactico.ErroresManager
{
    internal class Validacion
    {
        internal delegate bool ValidacionDelegate(List<Terminal> aValidar, List<Terminal> hastaAhora);

        private List<Terminal> listaTerminalesAValidar;
        private List<Terminal> listaTerminalesHastaAhora;
        private ValidacionDelegate metodoValidacion;
        private MensajeError mensajeError;
        private short importancia;

        private int filaDelError;
        public int FilaDelError
        {
            get { return filaDelError; }
        }

        private int columnaDelError;
        public int ColumnaDelError
        {
            get { return columnaDelError; }
        }

        public short Importancia
        {
            get { return importancia; }
        }
               

        public bool EsValido
        {
            get { return Validar(); }
        }
        

        /// <summary>
        /// Crear una nueva validacion
        /// </summary>
        /// <param name="terminalesAValidar">Lista de terminales con los que se validara en el metodo Validar()</param>
        /// <param name="mens">Mensaje de Error a mostrar si la validacion no se cumple</param>
        /// <param name="importancia">Importancia de la validacion</param>
        /// <param name="metodoVal">Metodo que contiene el codigo a ejecutar de la validacion</param>
        internal Validacion(List<Terminal> terminalesAValidar, MensajeError mens, short p, ValidacionDelegate metodoVal, int f, int c)
            : this( terminalesAValidar, null, mens, p, metodoVal, f, c)
        {
            
        }

        internal Validacion(List<Terminal> terminalesAValidar, List<Terminal> terminalesHastaAhora, MensajeError mens, short p, ValidacionDelegate metodoVal, int f, int c)
        {
            listaTerminalesAValidar = terminalesAValidar;
            listaTerminalesHastaAhora = terminalesHastaAhora;
            mensajeError = mens;
            importancia = p;
            metodoValidacion = metodoVal;

            filaDelError = f;
            columnaDelError = c;
        }

        private bool Validar()
        {
            return metodoValidacion.Invoke(listaTerminalesAValidar, listaTerminalesHastaAhora);
        }

        internal void ArrojarExcepcion()
        {
            throw new ValidacionException(mensajeError, mensajeError.Descripcion) { Fila = filaDelError, Columna = columnaDelError };
        }

        public override string ToString()
        {
            return metodoValidacion.Method.Name;
        }
    }
}
