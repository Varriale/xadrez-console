using tabuleiro;

namespace xadrez
{
    class Rei : Peca
    {
        private PartidaDeXadrez partida;
        public Rei(Cor cor, Tabuleiro tabuleiro, PartidaDeXadrez partida) : base(cor, tabuleiro)
        {
            this.partida = partida;
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

            if (qteMovimentos == 0 && !partida.xeque)//nao importa quem esta em xeque, pois o adv nao pode terminar sua rodada em xeque
            {
                //#jogadaEspecial Roque pequeno
                for(int i = -1; i <= 1; i += 2)
                {
                    pos.definePosicao(Posicao.Linha, Posicao.Coluna + 3*i);
                    Peca peca = Tab.Peca(pos);
                    if (peca!=null && peca is Torre && peca.qteMovimentos == 0 && peca.Cor==Cor)
                    {
                        bool espacoVazio = true;
                        for (int j = 1; j < 3; j++)//verifica se o espaço esta vazio entre os dois
                        {
                            pos.definePosicao(Posicao.Linha, Posicao.Coluna + j * i);
                            if (!(Tab.posicaoValida(pos) && Tab.Peca(pos)==null))
                            {
                                espacoVazio = false;
                            }
                        }
                        if (espacoVazio)
                            mat[Posicao.Linha, Posicao.Coluna + 2 * i] = true;
                    }
                }
                //#jogadaEspecial Roque Grande
                for (int i = -1; i <= 1; i += 2)
                {
                    pos.definePosicao(Posicao.Linha, Posicao.Coluna + 4 * i);
                    Peca peca = Tab.Peca(pos);
                    if (peca != null && peca is Torre && peca.qteMovimentos == 0 && peca.Cor == Cor)
                    {
                        bool espacoVazio = true;
                        for (int j = 1; j < 4; j++)//verifica se o espaço esta vazio entre os dois
                        {
                            pos.definePosicao(Posicao.Linha, Posicao.Coluna + j * i);
                            if (!(Tab.posicaoValida(pos) && Tab.Peca(pos) == null))
                            {
                                espacoVazio = false;
                            }
                        }
                        if (espacoVazio)
                            mat[Posicao.Linha, Posicao.Coluna + 2 * i] = true;
                    }
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
