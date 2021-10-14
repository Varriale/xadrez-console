using tabuleiro;

namespace xadrez
{
    class Peao : Peca
    {
        public Peao(Cor cor, Tabuleiro tabuleiro) : base(cor, tabuleiro)
        {
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];
            Posicao pos = new Posicao(0, 0);
            int direcao =0;

            if (Cor == Cor.Branca)
                direcao = -1;
            else
                direcao = 1;

            pos.definePosicao(Posicao.Linha+direcao, Posicao.Coluna);
            if (Tab.posicaoValida(pos) && podeMover(pos) && Tab.Peca(pos)==null)
                mat[pos.Linha, pos.Coluna] = true;
            
            if (qteMovimentos == 0)
            {
                pos.definePosicao(Posicao.Linha + 2*direcao, Posicao.Coluna);
                if (Tab.posicaoValida(pos) && podeMover(pos) && Tab.Peca(pos) == null)
                    mat[pos.Linha, pos.Coluna] = true;
            }

            for(int j = -1; j <= 1; j += 2)
            {
                pos.definePosicao(Posicao.Linha + direcao, Posicao.Coluna+j);
                Peca peca = Tab.Peca(pos);
                if(peca!=null)
                    if (Tab.posicaoValida(pos) && podeMover(pos)&&peca.Cor!=Cor)
                        mat[pos.Linha, pos.Coluna] = true;
            }


            return mat;
        }
        public override string ToString()
        {
            return "P";
        }
    }
}
