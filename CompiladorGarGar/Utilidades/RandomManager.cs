using System;
using System.Text;

namespace Utilidades
{
    public static class RandomManager
    {
        /*Inicializamos el random acá y no cada vez que se invocan los métodos, porque ganamos aleatoriedad.
         Si lo inicializamos cada vez que invocamos el método, no sólo perdemos eficiencia, sino que aumentamos
         la posibilidad de generar una secuencia previsible de números.*/
        private static FastRandom random = new FastRandom();

        public static string RandomStringConPrefijo(string prefijo, int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder(prefijo);
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

      
    }
}
