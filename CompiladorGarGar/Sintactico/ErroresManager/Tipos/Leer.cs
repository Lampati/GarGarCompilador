using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompiladorGargar.Sintactico.Gramatica;
using CompiladorGargar.Sintactico.ErroresManager.Errores;

namespace CompiladorGargar.Sintactico.ErroresManager.Tipos
{
    class Leer : TipoBase
    {
        public Leer(List<Terminal> listaEntera, List<Terminal> listaHastaAhora, int fila, int col)
            : base(listaEntera, listaHastaAhora, fila, col)
        {         


            AgregarValidacionLeerRepetido();
            AgregarValidacionLeerSolo();
            AgregarValidacionLeerNoIdentificador();

            AgregarValidacionPorDefault();
        }

        private void AgregarValidacionLeerRepetido()
        {
            MensajeError mensajeError = new ErrorLeerRepetido();
            short importancia = 10;

            Validacion valRep = new Validacion(listaLineaEntera, mensajeError, importancia, ValidacionesFactory.LeerRepetido, FilaDelError, ColumnaDelError);

            listaValidaciones.Add(valRep);
        }

        private void AgregarValidacionLeerSolo()
        {
            MensajeError mensajeError = new ErrorLeerSolo();
            short importancia = 9;

            Validacion valRep = new Validacion(listaLineaEntera, mensajeError, importancia, ValidacionesFactory.LeerSolo, FilaDelError, ColumnaDelError);

            listaValidaciones.Add(valRep);
        }

        private void AgregarValidacionLeerNoIdentificador()
        {
            MensajeError mensajeError = new ErrorLeerNoIdentificador();
            short importancia = 8;

            List<Terminal> final = ArmarSubListaDerechaDe(listaLineaEntera, Lexicografico.ComponenteLexico.TokenType.Leer);

            Validacion valRep = new Validacion(final, mensajeError, importancia, ValidacionesFactory.LeerNoIdentificador, FilaDelError, ColumnaDelError);

            listaValidaciones.Add(valRep);
        }

        private void AgregarValidacionPorDefault()
        {
            MensajeError mensajeError = new ErrorLeerValidacionPorDefault();
            short importancia = 1;


            Validacion valRep = new Validacion(listaLineaEntera, mensajeError, importancia, ValidacionesFactory.ForzarFalso, FilaDelError, ColumnaDelError);
            listaValidaciones.Add(valRep);
        }
    }
}
