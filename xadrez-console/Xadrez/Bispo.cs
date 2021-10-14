using tabuleiro;

namespace xadrez
{
    class Bispo : Peca
    {
        public Bispo(Cor cor, Tabuleiro tabuleiro) : base(cor, tabuleiro)
        {
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];
            Posicao pos = new Posicao(0, 0);

            for (int i = -1; i <= 1; i=i+2)
            {
                for (int j = -1; j <= 1; j = j + 2)
                {
                    pos.definePosicao(Posicao.Linha + i, Posicao.Coluna+j);
                    while (Tab.posicaoValida(pos) && podeMover(pos))
                    {
                        mat[pos.Linha, pos.Coluna] = true;
                        if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != this.Cor)
                        {
                            break;
                        }
                        pos.Linha +=  i;
                        pos.Coluna += j;
                    }
                }
            }

            return mat;
        }

        public override string ToString()
        {
            return "B";
        }

    }
}
