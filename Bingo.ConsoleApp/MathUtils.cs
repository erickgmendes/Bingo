using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bingo.ConsoleApp
{
    public static class MathUtils
    {

        public static bool EhPrimo(this int numero)
        {
            if (numero <= 1) return false;
            if (numero == 2) return true;
            if (numero % 2 == 0) return false;
            var limite = (int)Math.Floor(Math.Sqrt(numero));
            for (int i = 3; i <= limite; i += 2) if (numero % i == 0) return false;
            return true;
        }

        public static IList<int> GetNumerosPrimos(int total = 10)
        {
            var lista = new List<int>();
            var i = 1;

            while(lista.Count < total)
            {
                if (i.EhPrimo())
                {
                    lista.Add(0);
                }
            }

            return lista;
        }

    }
}
