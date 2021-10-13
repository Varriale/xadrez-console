﻿using System.Text;

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
            pecas[1, 1] = new Peca(new Posicao(0, 0), Cor.Branca, this);
            pecas[2, 4] = new Peca(new Posicao(1, 3), Cor.Branca, this);

        }

        public override string ToString()
        {
            StringBuilder sb=new StringBuilder();
            for(int i=0;i<Linhas;i++)
            {
                for(int j = 0; j < Colunas; j++)
                {
                    sb.Append((pecas[i,j]?.ToString() ?? "-") + " ");
                }
                sb.AppendLine("");
            }


            return sb.ToString();
        }

        public Peca Peca(int linha, int coluna)
        {
            return pecas[linha, coluna];
        }
    }
}