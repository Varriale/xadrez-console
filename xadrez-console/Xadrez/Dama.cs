using tabuleiro;

namespace xadrez
{
    class Dama : Peca
    {
        public Dama(Cor cor, Tabuleiro tabuleiro) : base(cor, tabuleiro)
        {
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];
            Posicao pos = new Posicao(0, 0);

            for (int i = -1; i <= 1; i++)
                for (int j = -1; j <= 1; j++)
                {
                    pos.definePosicao(Posicao.Linha + i, Posicao.Coluna+j);
                    while (Tab.posicaoValida(pos) && podeMover(pos))
                    {
                        mat[pos.Linha, pos.Coluna] = true;
                        if (Tab.Peca(pos) != null && Tab.Peca(pos).Cor != this.Cor)
                        {
                            break;
                        }
                        pos.Linha += i;
                        pos.Coluna += j;
                    }
                }
            return mat;
        }
        public override string ToString()
        {
            return "D";
        }
    }
}
