using tabuleiro;

namespace xadrez
{
    class PosicaoXadrez
    {
        public char Coluna { get; set; }
        public int Linha { get; set; }

        public PosicaoXadrez(char coluna, int linha)
        {
            Coluna = coluna.ToString().ToLower()[0];
            Linha = linha;
        }

        public override string ToString()
        {
            return "" + Coluna + Linha;
        }

        public Posicao toPosicao()
        {
            return new Posicao(8 - Linha, Coluna - 'a');
        }

    }
}
