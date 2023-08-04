using System;
using tabuleiro;
using xadrez;
using xadrez_console;
using System.Collections.Generic;

namespace xadrez {
    internal class PartidaDeXadrez {
        public Tabuleiro tab { get; set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;

        public PartidaDeXadrez() {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();   
            colocarPecas();
        }
        public void executaMovimento(Posicao origem, Posicao destino) {
            Peca p = tab.retirarPeca(origem);
            p.incrementarQteMovimentos();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
            if (pecaCapturada != null) {
                capturadas.Add(pecaCapturada);
            }

        }
        public void realizaJogada(Posicao origem, Posicao destino) { 
            executaMovimento(origem, destino);
            turno++;
            mudaJogador();
        }

        private void mudaJogador() {
            if(jogadorAtual == Cor.Branca) {
                jogadorAtual = Cor.Preta;
            } else {
                jogadorAtual = Cor.Branca;
            }
        }

        public HashSet<Peca> pecasCapturadas(Cor cor) {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in capturadas) {
                if (x.cor == cor) {
                    aux.Add(x);
                }
            }
            return aux;
        }
        public HashSet<Peca> pecasEmJogo(Cor cor) {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas) {
                if (x.cor == cor) {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }
        public void colocarNovaPeca(char coluna, int linha, Peca peca) {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
        }

        private void colocarPecas() {
            colocarNovaPeca('a', 1, new Torre(tab, Cor.Branca));
            //colocarNovaPeca('b', 1, new Cavalo(tab, Cor.Branca));
            //colocarNovaPeca('c', 1, new Bispo(tab, Cor.Branca));
            //colocarNovaPeca('d', 1, new Dama(tab, Cor.Branca));
            colocarNovaPeca('e', 1, new Rei(tab, Cor.Branca));
            //colocarNovaPeca('f', 1, new Bispo(tab, Cor.Branca));
            //colocarNovaPeca('g', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeca('h', 1, new Torre(tab, Cor.Branca));
            //colocarNovaPeca('a', 2, new Peao(tab, Cor.Branca, this));
            //colocarNovaPeca('b', 2, new Peao(tab, Cor.Branca, this));
            //colocarNovaPeca('c', 2, new Peao(tab, Cor.Branca, this));
            //colocarNovaPeca('d', 2, new Peao(tab, Cor.Branca, this));
            //colocarNovaPeca('e', 2, new Peao(tab, Cor.Branca, this));
            //colocarNovaPeca('f', 2, new Peao(tab, Cor.Branca, this));
            //colocarNovaPeca('g', 2, new Peao(tab, Cor.Branca, this));
            //colocarNovaPeca('h', 2, new Peao(tab, Cor.Branca, this));

            colocarNovaPeca('a', 8, new Torre(tab, Cor.Preta));
            //colocarNovaPeca('b', 8, new Cavalo(tab, Cor.Preta));
            //colocarNovaPeca('c', 8, new Bispo(tab, Cor.Preta));
            ///colocarNovaPeca('d', 8, new Dama(tab, Cor.Preta));
            colocarNovaPeca('e', 8, new Rei(tab, Cor.Preta));
            //colocarNovaPeca('f', 8, new Bispo(tab, Cor.Preta));
            //colocarNovaPeca('g', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeca('h', 8, new Torre(tab, Cor.Preta));
            //colocarNovaPeca('a', 7, new Peao(tab, Cor.Preta, this));
            //colocarNovaPeca('b', 7, new Peao(tab, Cor.Preta, this));
            //colocarNovaPeca('c', 7, new Peao(tab, Cor.Preta, this));
            //colocarNovaPeca('d', 7, new Peao(tab, Cor.Preta, this));
           // colocarNovaPeca('e', 7, new Peao(tab, Cor.Preta, this));
           // colocarNovaPeca('f', 7, new Peao(tab, Cor.Preta, this));
           // colocarNovaPeca('g', 7, new Peao(tab, Cor.Preta, this));
           // colocarNovaPeca('h', 7, new Peao(tab, Cor.Preta, this));


        }

        //Tratamento de erros ->

        public void validarPosicaoDeOrigem(Posicao pos) {
            if (tab.peca(pos) == null) {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if(jogadorAtual != tab.peca(pos).cor) {
                throw new TabuleiroException("Essa peça não é sua!");
            }
            if (!tab.peca(pos).existeMovimentosPossiveis()) {
                throw new TabuleiroException(" Não há movimentos possiveis para a peça de origem escolhoda!");
            }
        }

        public void validarPosicaoDeDestino(Posicao origem, Posicao destino) {
            if (!tab.peca(origem).podeMoverPara(destino)) {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }

    }
}
