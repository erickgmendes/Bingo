using System;
using System.Collections.Generic;
using System.Linq;

namespace Bingo.ConsoleApp
{
    internal class Program
    {

        #region Method Main

        static void Main()
        {
            while ("S".Equals(Continuar()))
            {
                var numCartelas = GetQuantidadeCartelas();

                Console.WriteLine(string.Empty);

                var cartelas = MontarCartelas(numCartelas);
                Jogar(cartelas);
            }

            Despedida();
        }

        #endregion

        #region Private Methods

        private static string Continuar()
        {
            Console.Write("Começar Novo Jogo? (S/N): ");
            return Console.ReadLine().ToUpper();
        }

        private static int GetQuantidadeCartelas()
        {
            var quantidade = 0;

            while (quantidade < 2)
            {
                Console.Write("Informe a quantidade de cartelas: ");
                quantidade = Convert.ToInt32(Console.ReadLine());

                if (quantidade < 2)
                {
                    Console.WriteLine("Quantidade precisa sem maior ou igual a 2");
                }
            }

            return quantidade;
        }

        private static IList<int> MontarPedras
        {
            get
            {
                var pedras = new List<int>(90);
                pedras.AddRange(Enumerable.Range(1, 90));
                return pedras;
            }
        }

        private static List<Cartela> MontarCartelas(int numCartelas)
        {
            var cartelas = new List<Cartela>();

            for (int i = 1; i <= numCartelas; i++)
            {
                var cartelaRepetida = true;

                while (cartelaRepetida)
                {
                    var novaCartela = new Cartela(i);

                    if (cartelas.Any(x => x.Equals(novaCartela)))
                    {
                        cartelaRepetida = true;
                    }
                    else
                    {
                        cartelaRepetida = false;
                        cartelas.Add(novaCartela);
                    }
                }
            }

            cartelas.ForEach(x => x.ShowCartela());
            return cartelas;
        }

        private static void Jogar(IList<Cartela> cartelas)
        {
            var pedras = MontarPedras;
            var random = new Random();
            var bingo = false;
            var pedrasCantadas = new List<int>(90);

            Console.WriteLine("Número da vez: ");
            while (!bingo)
            {
#if !DEBUG
                System.Threading.Thread.Sleep(800);
#endif
                System.Threading.Thread.Sleep(200);
                var indice = random.Next(1, pedras.Count) - 1;
                var numero = pedras[indice];
                pedras.RemoveAt(indice);
                pedrasCantadas.Add(numero);

                Console.Write($"|{numero:D2}");

                if (pedrasCantadas.Count % 10 == 0)
                {
                    Console.WriteLine("|");
                }

                foreach (var cartela in cartelas)
                {
                    if (cartela.Verificar(numero))
                    {
                        bingo = cartela.Bingo(pedrasCantadas);
                    }

                    if (bingo)
                    {
                        if (pedrasCantadas.Count % 10 != 0)
                        {
                            Console.WriteLine("|");
                        }

                        Bingo();

                        MostrarNumerosSorteados(pedrasCantadas);
                        MostrarCartelaVencedora(cartela);

                        return;
                    }
                }
            }
        }

        private static void Bingo()
        {
            //Console.WriteLine("\n\nB I N G O ! ! !\n\n");
            Console.WriteLine(string.Empty);

            Console.WriteLine("______  _                       _  _ ");
            Console.WriteLine("| ___ \\(_)                     | || |");
            Console.WriteLine("| |_/ / _  _ __    __ _   ___  | || |");
            Console.WriteLine("| ___ \\| || '_ \\  / _` | / _ \\ | || |");
            Console.WriteLine("| |_/ /| || | | || (_| || (_) ||_||_|");
            Console.WriteLine("\\____/ |_||_| |_| \\__, | \\___/ (_)(_)");
            Console.WriteLine("                   __/ |");
            Console.WriteLine("                  |___/");


            //Console.WriteLine("BBBBBBBBBBBBBBBBB     iiii                                                          !!!  !!!");
            //Console.WriteLine("B::::::::::::::::B   i::::i                                                        !!:!!!!:!!");
            //Console.WriteLine("B::::::BBBBBB:::::B   iiii                                                         !:::!!:::!");
            //Console.WriteLine("BB:::::B     B:::::B                                                               !:::!!:::!");
            //Console.WriteLine("  B::::B     B:::::Biiiiiii nnnn  nnnnnnnn       ggggggggg   ggggg   ooooooooooo   !:::!!:::!");
            //Console.WriteLine("  B::::B     B:::::Bi:::::i n:::nn::::::::nn    g:::::::::ggg::::g oo:::::::::::oo !:::!!:::!");
            //Console.WriteLine("  B::::BBBBBB:::::B  i::::i n::::::::::::::nn  g:::::::::::::::::go:::::::::::::::o!:::!!:::!");
            //Console.WriteLine("  B:::::::::::::BB   i::::i nn:::::::::::::::ng::::::ggggg::::::ggo:::::ooooo:::::o!:::!!:::!");
            //Console.WriteLine("  B::::BBBBBB:::::B  i::::i   n:::::nnnn:::::ng:::::g     g:::::g o::::o     o::::o!:::!!:::!");
            //Console.WriteLine("  B::::B     B:::::B i::::i   n::::n    n::::ng:::::g     g:::::g o::::o     o::::o!:::!!:::!");
            //Console.WriteLine("  B::::B     B:::::B i::::i   n::::n    n::::ng:::::g     g:::::g o::::o     o::::o!!:!!!!:!!");
            //Console.WriteLine("  B::::B     B:::::B i::::i   n::::n    n::::ng::::::g    g:::::g o::::o     o::::o !!!  !!! ");
            //Console.WriteLine("BB:::::BBBBBB::::::Bi::::::i  n::::n    n::::ng:::::::ggggg:::::g o:::::ooooo:::::o          ");
            //Console.WriteLine("B:::::::::::::::::B i::::::i  n::::n    n::::n g::::::::::::::::g o:::::::::::::::o !!!  !!! ");
            //Console.WriteLine("B::::::::::::::::B  i::::::i  n::::n    n::::n  gg::::::::::::::g  oo:::::::::::oo !!:!!!!:!!");
            //Console.WriteLine("BBBBBBBBBBBBBBBBB   iiiiiiii  nnnnnn    nnnnnn    gggggggg::::::g    ooooooooooo    !!!  !!! ");
            //Console.WriteLine("                                                          g:::::g");
            //Console.WriteLine("                                              gggggg      g:::::g");
            //Console.WriteLine("                                              g:::::gg   gg:::::g");
            //Console.WriteLine("                                               g::::::ggg:::::::g");
            //Console.WriteLine("                                                gg:::::::::::::g");
            //Console.WriteLine("                                                  ggg::::::ggg");
            //Console.WriteLine("                                                     gggggg");

            Console.WriteLine(string.Empty);
        }

        private static void MostrarNumerosSorteados(List<int> pedrasCantadas)
        {
            Console.Write("Números Sorteados: \n");

            var pedras = MontarPedras;

            for (int i = 1; i <= pedras.Count; i++)
            {
                if (pedrasCantadas.Contains(pedras[i - 1]))
                {
                    Console.Write($"|{pedras[i - 1]:D2}");
                }
                else
                {
                    Console.Write("|--");
                }

                if (i % 10 == 0)
                {
                    Console.WriteLine("|");
                }
            }
        }

        private static void MostrarCartelaVencedora(Cartela cartela)
        {
            Console.WriteLine($"\n\nCartela Vencedora: \n");
            cartela.ShowCartela();
        }

        private static void Despedida()
        {
            Console.WriteLine("Tchau...");
            System.Threading.Thread.Sleep(1500);
        }

        #endregion

    }
}
