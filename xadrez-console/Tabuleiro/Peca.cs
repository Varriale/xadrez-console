namespace tabuleiro
{
    class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }

        public int qteMovimentos { get; protected set; }

        public Tabuleiro Tabuleiro { get; set; }

        public Peca( Cor cor, Tabuleiro tabuleiro)
        {
            Posicao = null;
            Cor = cor;
            this.qteMovimentos = 0;
            Tabuleiro = tabuleiro;
        }

        public override string ToString()
        {
            return "G";
        }

    }
}
