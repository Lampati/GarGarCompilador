using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;

namespace CompiladorGargar.Lexicografico
{
    class CharBuffer
    {
        protected int fila;
        public int Fila
        {
            get { return fila; }            
        }

        protected int filaUltChar;
        public int FilaUltChar
        {
            get { return filaUltChar; }
        }

        protected int columna;
        public int Columna
        {
            get { return columna; }            
        }

        protected bool finArchivo = false;
        public bool FinArchivo
        {
            get { return finArchivo; }
        }

        protected bool habiaEspacio;
        public bool HabiaEspacio
        {
            get { return habiaEspacio; }
        }

        protected int offsetArchivo;
        protected char[] charBuffer;
        protected int charBufferMaxSize;
        protected int charBufferTope;

        protected bool ultimoBuffer = false;

        protected int indiceBuffer;

        protected int extraEspaciosEnBuffer;

       
         private string texto;

        public CharBuffer(string texto)
        {
            this.texto = texto;

            this.fila = 1;
            this.columna = 0;
            this.offsetArchivo = 0;
            this.charBufferMaxSize = Convert.ToInt32(CompiladorGargar.Properties.Resources.CapacidadBuffer);
            ultimoBuffer = false;
            finArchivo = false;

            //es para que el look ahead en la ultima posicion del buffer no se pierda al cambiar el buffer
            this.extraEspaciosEnBuffer = Convert.ToInt32(CompiladorGargar.Properties.Resources.ExtraEspaciosEnBuffer);

            this.indiceBuffer = 0;
            this.LlenarBuffer();
        }

        protected  void  LlenarBuffer()
        { 	    
            try
            {
                if (!ultimoBuffer)
                {
                    bool auxUltBuffer = false;
                    this.indiceBuffer = 0;                    

                    //Creo el buffer con la capacidad elegida y unos caracteres mas.
                    charBuffer = new char[this.charBufferMaxSize + this.extraEspaciosEnBuffer];

                    //Le resto la cantidad de espacios extras al tope, asi el offset del archivo 
                    // queda esa cantidad de caracteres retrasados, para volverlos a leer cuando haga el switch
                    // del buffer.

                    if (((offsetArchivo + charBuffer.Length) - this.extraEspaciosEnBuffer) > texto.Length)
                    {
                        charBufferTope = texto.Length - offsetArchivo  ;
                        this.ultimoBuffer = true;

                        charBuffer = this.texto.Substring(offsetArchivo, charBufferTope).ToCharArray().Concat(new char[charBufferMaxSize-charBufferTope]).ToArray();
                    }
                    else
                    {
                        charBufferTope = charBuffer.Length - this.extraEspaciosEnBuffer;
                        charBuffer = this.texto.Substring(offsetArchivo, charBufferTope).ToCharArray();
                    }                  

                    
                    
                    offsetArchivo += this.charBufferTope;
                }
                else
                {
                    this.finArchivo = true;
                }
            }
            catch (Exception ex)
            {
                Utils.Log.AddError(ex.Message);
                throw new Exception("Error al rellenar el buffer");
            }
            //return aux;
        }

        public char ObtenerProximoCaracterValido()
        {
            char x;

            this.habiaEspacio = false;

            int cantFilasSalteadas = 0;

            x = ObtenerProximoChar();

            while (Utilidades.StringUtils.esCaracterSalteable(x))
            {
                habiaEspacio = true;

                if (Utilidades.StringUtils.esRetornoDeCarro(x))
                {
                    x = ObtenerProximoChar();

                    if (Utilidades.StringUtils.esNuevaLinea(x))
                    {
                        cantFilasSalteadas++;
                        fila++;
                        columna = 0;
                    }
                }
                x = ObtenerProximoChar();
            }
            filaUltChar = fila - cantFilasSalteadas;

            return x;
        }

        public char ObtenerProximoChar()
        {
            char x = PeekProximoChar();
            indiceBuffer++;
            columna++;
            return x;
        }

        public char PeekProximoChar()
        {

            if (indiceBuffer >= charBufferTope)
            {
                LlenarBuffer();
            }
            char x = charBuffer[indiceBuffer];
            
            return x;
        }
    }
}
