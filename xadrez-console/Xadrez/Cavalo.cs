using tabuleiro;

namespace xadrez
{
    class Cavalo : Peca
    {
        public Cavalo(Cor cor, Tabuleiro tabuleiro) : base(cor, tabuleiro)
        {
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];
            Posicao pos = new Posicao(0, 0);

            for (int i = -2; i <= 2; i++)
                for (int j = -2; j <= 2; j++)
                {
                    if (i != 0 && j != 0 && i != j && i != -j)
                    {
                        pos.definePosicao(Posicao.Linha + i, Posicao.Coluna + j);
                        if (Tab.posicaoValida(pos) && podeMover(pos))
                        {
                            mat[pos.Linha, pos.Coluna] = true;
                        }
                    }
                }
            return mat;
        }
        public override string ToString()
        {
            return "C";
        }
    }
}
