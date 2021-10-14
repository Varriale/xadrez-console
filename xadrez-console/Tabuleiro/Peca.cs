using System;

namespace tabuleiro
{
    abstract class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }

        public int qteMovimentos { get; protected set; }

        public Tabuleiro Tab { get; set; }

        public Peca(Cor cor, Tabuleiro tabuleiro)
        {
            Posicao = null;
            Cor = cor;
            this.qteMovimentos = 0;
            Tab = tabuleiro;
        }
        protected bool podeMover(Posicao pos)
        {
            Peca p = Tab.Peca(pos);
            return p == null || p.Cor != this.Cor;
        }

        public override string ToString()
        {
            return "G";
        }
        public void imprimirPeca()
        {
            if (Cor == Cor.Branca)
            {
                System.Console.Write(this + " ");
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                System.Console.Write(this + " ");
                Console.ForegroundColor = aux;
            }
        }
        public void IncrementarQteMovimentos()
        {
            qteMovimentos++;
        }

        public bool existeMovimentosPossiveis()
        {
            bool[,] mat = MovimentosPossiveis();
            for (int i = 0; i < Tab.Linhas; i++)
                for (int j = 0; j < Tab.Colunas; j++)
                    if (mat[i, j])
                        return true;
            return false;
        }
        public bool movimentoPossivel(Posicao pos)
        {
            return MovimentosPossiveis()[pos.Linha, pos.Coluna];
        }

        public abstract bool[,] MovimentosPossiveis();
    }
}
