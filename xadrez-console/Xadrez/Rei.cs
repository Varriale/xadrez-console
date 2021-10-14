using tabuleiro;

namespace xadrez
{
    class Rei : Peca
    {
        public Rei(Cor cor, Tabuleiro tabuleiro) : base(cor, tabuleiro)
        {
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];
            Posicao pos = new Posicao(0, 0);

            for (int i = -1; i <= 1; i++)
                for (int j = -1; j <= 1; j++)
                {
                    pos.definePosicao(Posicao.Linha + i, Posicao.Coluna + j);
                    if (Tab.posicaoValida(pos) && podeMover(pos))
                    {
                        mat[pos.Linha, pos.Coluna] = true;
                    }
                }
            return mat;
        }
        public override string ToString()
        {
            return "R";
        }
    }
}
