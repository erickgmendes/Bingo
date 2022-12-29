using System;
using System.Collections.Generic;
using System.Linq;

namespace Bingo.ConsoleApp
{
    public class Cartela
    {

        #region Attributes

        private readonly int _numero;
        private readonly Random _random = new();

        #endregion

        #region Properties

        public readonly IList<int> Linha1 = new List<int>(5);
        public readonly IList<int> Linha2 = new List<int>(5);
        public readonly IList<int> Linha3 = new List<int>(4);
        public readonly IList<int> Linha4 = new List<int>(5);
        public readonly IList<int> Linha5 = new List<int>(5);

        #endregion

        public Cartela(int numero)
        {
            _numero = numero;
            MontarCartela();
        }

        #region Private Methods

        private void MontarCartela()
        {
            var itensCartela = new List<int>();

            while (itensCartela.Count < 24)
            {
                var numero = _random.Next(1, 90);
                if (!itensCartela.Contains(numero))
                {
                    itensCartela.Add(numero);
                }
            }

            //itensCartela = itensCartela.OrderBy(x => x).ToList();
            itensCartela.Sort();

            for (int i = 0; i < itensCartela.Count - 1; i++)
            {
                Linha1.Add(itensCartela[i]);
                Linha2.Add(itensCartela[++i]);
                if (i != 11)
                {
                    Linha3.Add(itensCartela[++i]);
                }
                Linha4.Add(itensCartela[++i]);
                Linha5.Add(itensCartela[++i]);
            }
        }

        private static bool LinhaConcluida(IList<int> pedras, IList<int> Linha)
        {
            foreach (var item in Linha)
            {
                if (!pedras.Contains(item))
                {
                    return false;
                }
            }

            return true;
        }

        private static void EscreverLinha(IList<int> linha)
        {
            linha.ToList().ForEach(x => Console.Write($"|{x:D2}"));
            Console.WriteLine("|");
        }

        private static void EscreverLinhaEspecial(IList<int> linha)
        {
            for (int i = 0; i < linha.Count; i++)
            {
                if (i == 2)
                {
                    Console.Write("|**");
                }
                Console.Write($"|{linha[i]:D2}");
            }
            Console.WriteLine("|");
        }

        private static bool ConferirLinha(IList<int> linha, IList<int> linhaOutraCartela)
        {
            for (int i = 0; i < linha.Count; i++)
            {
                if (linha[i] != linhaOutraCartela[i])
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region Public Methods

        public void ShowCartela()
        {
            Console.WriteLine($"Cartela #{_numero:D3}");

            EscreverLinha(Linha1);
            EscreverLinha(Linha2);
            EscreverLinhaEspecial(Linha3);
            EscreverLinha(Linha4);
            EscreverLinha(Linha5);

            Console.WriteLine(string.Empty);
        }

        public bool Verificar(int numero)
        {
            if (Linha1.Any(x => x == numero)) return true;
            if (Linha2.Any(x => x == numero)) return true;
            if (Linha3.Any(x => x == numero)) return true;
            if (Linha4.Any(x => x == numero)) return true;
            if (Linha5.Any(x => x == numero)) return true;

            return false;
        }

        public bool Bingo(IList<int> pedras)
        {
            return LinhaConcluida(pedras, Linha1)
                && LinhaConcluida(pedras, Linha2)
                && LinhaConcluida(pedras, Linha3)
                && LinhaConcluida(pedras, Linha4)
                && LinhaConcluida(pedras, Linha5);
        }

        #endregion

        #region Override Methods

        public override string ToString()
        {
            return _numero.ToString();
        }

        // override object.Equals
        public override bool Equals(object cartela)
        {
            var outraCartela = (Cartela)cartela;

            if (outraCartela == null || GetType() != outraCartela.GetType())
            {
                return false;
            }

            var OkLinha1 = ConferirLinha(Linha1, outraCartela.Linha1);
            var OkLinha2 = ConferirLinha(Linha2, outraCartela.Linha2);
            var OkLinha3 = ConferirLinha(Linha3, outraCartela.Linha3);
            var OkLinha4 = ConferirLinha(Linha4, outraCartela.Linha4);
            var OkLinha5 = ConferirLinha(Linha5, outraCartela.Linha5);

            return OkLinha1 && OkLinha2 && OkLinha3 && OkLinha4 && OkLinha5;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            var hash = 0;

            for (int i = 1; i < Linha3.Count; i++)
            {
                hash += Linha1[i - 1] * GetRandomNumeroPrimo()
                      + Linha2[i - 1] * GetRandomNumeroPrimo()
                      + Linha3[i - 1] * GetRandomNumeroPrimo()
                      + Linha4[i - 1] * GetRandomNumeroPrimo()
                      + Linha5[i - 1] * GetRandomNumeroPrimo();
            }

            hash += Linha1[4] * GetRandomNumeroPrimo()
                  + Linha2[4] * GetRandomNumeroPrimo()
                  + Linha4[4] * GetRandomNumeroPrimo()
                  + Linha5[4] * GetRandomNumeroPrimo();

            return hash;
        }

        private int GetRandomNumeroPrimo()
        {
            var numerosPrimos = MathUtils.GetNumerosPrimos(1000);

            var j = _random.Next(1, numerosPrimos.Count) - 1;
            var numeroPrimo = numerosPrimos[j];
            numerosPrimos.RemoveAt(j);
            return numeroPrimo;
        }

        #endregion

    }
}
