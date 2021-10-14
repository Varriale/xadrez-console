using System;
using System.Text;

namespace tabuleiro
{
    class Tabuleiro
    {
        public int Linhas { get; set; }
        public int Colunas { get; set; }
        private Peca[,] pecas;

        public Tabuleiro(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            pecas = new Peca[Linhas, Colunas];
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Linhas; i++)
            {
                sb.Append((8-i)+" ");
                for (int j = 0; j < Colunas; j++)
                {
                    sb.Append((pecas[i, j]?.ToString() ?? "-") + " ");
                }
                sb.AppendLine("");
            }
            sb.Append("  ");
            for (int j = 0; j < Colunas; j++)
                sb.Append((char)('a'+j)+" ");
            sb.AppendLine("");

            return sb.ToString();
        }

        public void Imprimir(bool[,] posicoesPossiveis=null)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;
            for (int i = 0; i < Linhas; i++)
            {
                System.Console.Write((8 - i) + " ");
                for (int j = 0; j < Colunas; j++)
                {
                    if (posicoesPossiveis?[i, j] ?? false)
                    {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    if (pecas[i, j] != null)
                        pecas[i, j].imprimirPeca();
                    else
                        System.Console.Write("- ");

                    Console.BackgroundColor = fundoOriginal;
                }
                System.Console.WriteLine("");
            }
            System.Console.Write("  ");
            for (int j = 0; j < Colunas; j++)
                System.Console.Write((char)('a' + j) + " ");
            System.Console.WriteLine("");

        }

        public Peca Peca(int linha, int coluna)
        {
            if(posicaoValida(new Posicao(linha,coluna)))
                return pecas[linha, coluna];
            return null;
        }
        public Peca Peca(Posicao pos)
        {
            if (posicaoValida(pos))
                return pecas[pos.Linha, pos.Coluna];
            return null;
        }

        public void ColocarPeca(Peca P, Posicao pos)
        {
            if (existePeca(pos))
                throw new TabuleiroException("Posição Ocupada");

            pecas[pos.Linha, pos.Coluna] = P;
            P.Posicao = pos;
        }
        public Peca RetirarPeca(Posicao pos)
        {
            if (existePeca(pos))
            {
                Peca aux = Peca(pos);
                pecas[pos.Linha, pos.Coluna] = null;
                aux.Posicao = null;
                return aux;
            }
            else
            {
                return null;
            }
        }           

        public bool posicaoValida(Posicao pos)
        {
            if (pos.Linha < 0 || pos.Linha >= Linhas || pos.Coluna < 0 || pos.Coluna >= Colunas)
                return false;
            else
                return true;
        }

        public void validarPosicao(Posicao pos)
        {
            if (!posicaoValida(pos))
                throw new TabuleiroException("Posição Inválida");
        }

        public bool existePeca(Posicao pos)
        {
            validarPosicao(pos);
            return Peca(pos) != null;
        }
    }
}
