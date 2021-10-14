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
                    try
                    {
                        Console.Clear();
                        partida.Imprimir();

                        Console.WriteLine();
                        Console.WriteLine("Origem:");
                        Posicao origem = lerPosicaoXadrez().toPosicao();
                        partida.validarPosicaoDeOrigem(origem);


                        Console.Clear();
                        bool[,] movPoss = partida.tab.Peca(origem).MovimentosPossiveis();
                        partida.Imprimir(movPoss);


                        Console.WriteLine();
                        Console.WriteLine("Destino:");
                        Posicao destino = lerPosicaoXadrez().toPosicao();
                        partida.validarPosicaoDeDestino(origem,destino);
                        partida.realizaJogada(origem, destino);
                    }
                    catch (TabuleiroException e)
                    {
                        Console.WriteLine(e.Message); ;
                        Console.ReadLine();
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine("Escreva uma posição válida no formato 'a1'");
                        Console.ReadLine();
                    }
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
            return new PosicaoXadrez(s[0], int.Parse(s[1] + ""));
        }

    }
}
