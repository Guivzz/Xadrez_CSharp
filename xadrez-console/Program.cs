using System;
using tabuleiro;
using xadrez;
using System.Threading;
namespace xadrez_console {
    class Program {       
        static void Main(string[] args) {
            try {
                PartidaDeXadrez partida = new PartidaDeXadrez();
                while (!partida.terminada) {
                    try {                       
                        Console.Clear();
                        Tela.imprimirPartida(partida);
                        Console.Write("Origem: ");
                        Posicao origem = Tela.lerPosicaoXadrez().toPosicao();
                        partida.validarPosicaoDeOrigem(origem);
                        bool[,] posicoesPossiveis = partida.tab.peca(origem).movimentosPossiveis();
                        Console.Clear();
                        Tela.imprimirTabuleiro(partida.tab, posicoesPossiveis);
                        Console.Write("Destino: ");
                        Posicao destino = Tela.lerPosicaoXadrez().toPosicao();
                        partida.validarPosicaoDeDestino(origem, destino);
                        partida.realizaJogada(origem, destino);
                    }
                    catch (TabuleiroException e) {
                        Console.WriteLine(e.Message);
                        Console.WriteLine();
                        Console.WriteLine("Reiniciando em 5 segundos...");
                        DateTime horaInicio = DateTime.Now;
                        DateTime horaTermino = horaInicio.AddSeconds(5);
                        int segundos = 0;
                        
                        while (DateTime.Now < horaTermino) {
                            
                            segundos++;
                            
                            Console.Write("\rFaltam " + segundos + " Segundos... Para reiniciar.");
                            // Esperar um pouco para não sobrecarregar o processador
                            System.Threading.Thread.Sleep(1000);
                        }
                        

                    }
                }
                Console.Clear();
                Tela.imprimirPartida(partida);
            }
            catch (TabuleiroException e) {
                Console.WriteLine(e.Message);
            }
        }
    }
}





