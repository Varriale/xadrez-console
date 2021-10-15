using System;
using System.Collections.Generic;
using tabuleiro;

namespace xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        public Peca vulneravelEnPassant { get; private set; }
        private bool enPassant;
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
        public bool xeque { get; set; }

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            xeque = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            vulneravelEnPassant = null;
            enPassant = false;
            colocarPecas();
        }
        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }

        private void colocarPecas()
        {
            colocarNovaPeca('a', 1, new Torre(Cor.Branca, tab));
            colocarNovaPeca('h', 1, new Torre(Cor.Branca, tab));
            colocarNovaPeca('d', 1, new Rei(Cor.Branca, tab,this));
            /*colocarNovaPeca('c', 2, new Torre(Cor.Branca, tab));
            colocarNovaPeca('d', 2, new Torre(Cor.Branca, tab));
            colocarNovaPeca('e', 2, new Torre(Cor.Branca, tab));
            colocarNovaPeca('c', 8, new Torre(Cor.Preta, tab));
            colocarNovaPeca('e', 8, new Torre(Cor.Preta, tab));*/
            colocarNovaPeca('d', 8, new Rei(Cor.Preta, tab,this));
            colocarNovaPeca('c', 7, new Torre(Cor.Preta, tab));
            colocarNovaPeca('d', 7, new Torre(Cor.Preta, tab));
            /*colocarNovaPeca('e', 7, new Torre(Cor.Preta, tab));

            colocarNovaPeca('d', 4, new Bispo(Cor.Branca, tab));
            colocarNovaPeca('e', 4, new Cavalo(Cor.Branca, tab));
            colocarNovaPeca('c', 4, new Dama(Cor.Branca, tab));*/
            colocarNovaPeca('e', 4, new Peao(Cor.Preta, tab,this));
            colocarNovaPeca('d', 2, new Peao(Cor.Branca, tab, this));
            colocarNovaPeca('f', 1, new Peao(Cor.Branca, tab, this));
            xeque = estaEmXeque(adversaria(jogadorAtual))|| estaEmXeque(jogadorAtual);
        }

        public Peca executarMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.RetirarPeca(origem);
            Peca pecaCapturada = null;
            enPassant = false;
            if (p != null)
            {
                p.IncrementarQteMovimentos();
                pecaCapturada = tab.RetirarPeca(destino);
                tab.ColocarPeca(p, destino);

                //#jogadaEspecial roque
                if(p is Rei)
                {
                    if (Math.Abs(origem.Coluna - destino.Coluna) == 2)
                    {
                        if(destino.Coluna - origem.Coluna < 0)//para a esquerda
                        {
                            Posicao inicialTorre = new Posicao(origem.Linha, 0);
                            Posicao finalTorre = new Posicao(origem.Linha, origem.Coluna-1);
                            executarMovimento(inicialTorre, finalTorre);
                        }
                        else
                        {
                            Posicao inicialTorre = new Posicao(origem.Linha, tab.Colunas-1);
                            Posicao finalTorre = new Posicao(origem.Linha, origem.Coluna + 1);
                            executarMovimento(inicialTorre, finalTorre);
                        }
                            
                    }
                }

                //#jogadaEspecial En Passant
                if(p is Peao)
                {
                    if (origem.Coluna != destino.Coluna && pecaCapturada == null)
                    {
                        pecaCapturada = tab.RetirarPeca(vulneravelEnPassant.Posicao);
                        enPassant = true;
                    }
                }


                if (pecaCapturada != null)
                    capturadas.Add(pecaCapturada);
            }
            return pecaCapturada;
        }

        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca peca in capturadas)
                if (peca.Cor == cor)
                    aux.Add(peca);
            return aux;
        }

        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca peca in pecas)
                if (peca.Cor == cor)
                    aux.Add(peca);
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }

        public void realizaJogada(Posicao origem, Posicao destino)
        {

            Peca capturada = executarMovimento(origem, destino);

            if (estaEmXeque(jogadorAtual))
            {
                desfazMovimento(origem, destino, capturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            xeque = estaEmXeque(adversaria(jogadorAtual));
            if (testeXequemate(adversaria(jogadorAtual)))
            {
                terminada = true;
            }
            else
            {
                turno++;
                mudaJogador();
            }
            Peca p = tab.Peca(destino);
            if(p is Peao && Math.Abs(destino.Linha - origem.Linha) == 2)
                vulneravelEnPassant = p;
            else
                vulneravelEnPassant = null;
        }

        public void desfazMovimento(Posicao origem, Posicao destino, Peca capturada)
        {
            Peca p = tab.RetirarPeca(destino);
            p.decrementarQteMovimentos();
            if (capturada != null)
            {
                //#jogadaEspecial En Passant
                if (enPassant)
                {
                    Posicao posPass = new Posicao(origem.Linha, destino.Coluna);
                    tab.ColocarPeca(capturada, posPass);
                }
                else
                    tab.ColocarPeca(capturada, destino);
                capturadas.Remove(capturada);
            }
            tab.ColocarPeca(p, origem);
            //#jogadaEspecial roque
            if (p is Rei)
            {
                if (Math.Abs(origem.Coluna - destino.Coluna) == 2)
                {
                    if (destino.Coluna - origem.Coluna < 0)//para a esquerda
                    {
                        Posicao inicialTorre = new Posicao(origem.Linha, 0);
                        Posicao finalTorre = new Posicao(origem.Linha, origem.Coluna - 1);
                        Peca t = tab.RetirarPeca(finalTorre);
                        t.decrementarQteMovimentos();
                        tab.ColocarPeca(t, inicialTorre);
                    }
                    else
                    {
                        Posicao inicialTorre = new Posicao(origem.Linha, tab.Colunas - 1);
                        Posicao finalTorre = new Posicao(origem.Linha, origem.Coluna + 1);
                        Peca t = tab.RetirarPeca(finalTorre);
                        t.decrementarQteMovimentos();
                        tab.ColocarPeca(t, inicialTorre);
                    }

                }
            }

        }

        public void validarPosicaoDeOrigem(Posicao pos)
        {
            tab.validarPosicao(pos);
            if (tab.Peca(pos) == null)
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            if (tab.Peca(pos).Cor != jogadorAtual)
                throw new TabuleiroException("A peça escolhida não é sua!");
            if (!tab.Peca(pos).existeMovimentosPossiveis())
                throw new TabuleiroException("Não existem movimentos possíveis para a peça escolhida!");

        }
        public void validarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            tab.validarPosicao(destino);
            if (!tab.Peca(origem).movimentoPossivel(destino))
                throw new TabuleiroException("A jogada não é válida!");

        }
        private void mudaJogador()
        {
            jogadorAtual = adversaria(jogadorAtual);
        }

        public void Imprimir(bool[,] posicoesPossiveis = null)
        {
            if (!terminada)
            {
                tab.Imprimir(posicoesPossiveis);
                Console.WriteLine();
                ImprimirPecasCapturadas();
                Console.WriteLine();
                Console.WriteLine("Turno: " + turno);
                Console.WriteLine("Vez do jogador com peça " + jogadorAtual);
                if (xeque)
                {
                    Console.WriteLine("XEQUE!");
                }
            }
            else
            {
                tab.Imprimir(posicoesPossiveis);
                Console.WriteLine();
                ImprimirPecasCapturadas();
                Console.WriteLine();
                Console.WriteLine("Turno: " + turno);
                Console.WriteLine("XEQUEMATE!!\nVencedor: "+jogadorAtual);
                Console.WriteLine();
                Console.WriteLine("fim do jogo");
            }
        }

        private Cor adversaria(Cor cor)
        {
            if (cor == Cor.Preta)
                return Cor.Branca;
            else
                return Cor.Preta;
        }

        public bool estaEmXeque(Cor cor)
        {
            Peca R = rei(cor);
            if (R == null)
                throw new TabuleiroException("Rei da cor " + cor + " não existe!");

            foreach (Peca peca in pecasEmJogo(adversaria(cor)))
            {
                if (peca.MovimentosPossiveis()[R.Posicao.Linha, R.Posicao.Coluna])
                    return true;
            }
            return false;
        }

        public bool testeXequemate(Cor cor)
        {
            if (!estaEmXeque(cor))
                return false;

            foreach (Peca peca in pecasEmJogo(cor))
            {

                bool[,] mat = peca.MovimentosPossiveis();
                Posicao posOrig = peca.Posicao;
                for (int i = 0; i < tab.Linhas; i++)
                    for (int j = 0; j < tab.Colunas; j++)
                        if (mat[i, j])
                        {
                            Posicao pos = new Posicao(i, j);
                            Peca capturada = executarMovimento(posOrig, pos);

                            if (!estaEmXeque(cor))
                            {
                                desfazMovimento(posOrig, pos, capturada);
                                return false;
                            }
                            desfazMovimento(posOrig, pos, capturada);
                        }
            }
            return true;
        }

        private Peca rei(Cor cor)
        {
            foreach (Peca peca in pecasEmJogo(cor))
            {
                if (peca is Rei)
                    return peca;
            }
            return null;
        }

        private void ImprimirPecasCapturadas()
        {
            Console.WriteLine("Pecas Capturadas:");
            Console.Write("Brancas:");
            ImprimirConjunto(pecasCapturadas(Cor.Branca));
            Console.Write("Pretas:");
            ImprimirConjunto(pecasCapturadas(Cor.Preta));
        }

        public void ImprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[");
            foreach (Peca peca in conjunto)
                peca.imprimirPeca();
            Console.WriteLine("]");
        }

    }
}
