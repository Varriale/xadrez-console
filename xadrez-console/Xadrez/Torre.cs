using tabuleiro;

namespace xadrez
{
    class Torre : Peca
    {
        public Torre(Cor cor, Tabuleiro tabuleiro) : base(cor, tabuleiro)
        {
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];
            Posicao pos = new Posicao(0, 0);

            for (int i = -1; i <= 1; i=i+2)
            {
                pos.definePosicao(Posicao.Linha+i, Posicao.Coluna);
                while (Tab.posicaoValida(pos) && podeMover(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                    if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != this.Cor)
                    {
                        break;
                    }
                    pos.Linha = pos.Linha + i;
                }
            }
            for (int i = -1; i <= 1; i = i + 2)
            {
                pos.definePosicao(Posicao.Linha, Posicao.Coluna + i);
                while (Tab.posicaoValida(pos) && podeMover(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                    if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != this.Cor)
                    {
                        break;
                    }
                    pos.Coluna = pos.Coluna + i;
                }
            }

            return mat;
        }

        public override string ToString()
        {
            return "T";
        }

    }
}
