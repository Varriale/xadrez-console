using System;
using tabuleiro;
using xadrez;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PartidaDeXadrez partida = new PartidaDeXadrez();

                while (!partida.terminada)
                {
                    Console.Clear();
                    partida.tab.Imprimir();
                    Console.WriteLine();
                    Console.WriteLine("Origem:");
                    Posicao origem = lerPosicaoXadrez().toPosicao();
                    Console.Clear();
                    bool[,] movPoss = partida.tab.Peca(origem).MovimentosPossiveis();
                    partida.tab.Imprimir(movPoss);


                    Console.WriteLine();
                    Console.WriteLine("Destino:");
                    Posicao destino = lerPosicaoXadrez().toPosicao();
                    partida.executarMovimento(origem, destino);
                }



            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static PosicaoXadrez lerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            return new PosicaoXadrez(s[0], int.Parse(s[1]+""));
        }

    }
}
