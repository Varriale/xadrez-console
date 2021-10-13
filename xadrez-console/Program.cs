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
                Tabuleiro tab = new Tabuleiro(8, 8);
                tab.ColocarPeca(new Torre(Cor.Branca, tab), new Posicao(0, 0));
                tab.ColocarPeca(new Torre(Cor.Branca, tab), new Posicao(1, 3));
                tab.ColocarPeca(new Rei(Cor.Branca, tab), new Posicao(0, 2));
                Console.WriteLine(tab);
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
