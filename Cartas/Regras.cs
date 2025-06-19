namespace Cartas
{
    public static class Regras
    {
        public static string Texto =>
@"REGRAS DO JOGO

➤ COMPRAR CARTA:
Você pode comprar cartas até completar os 6 espaços da sua mão.
As cartas vêm de um baralho infinito, podendo haver repetições.
As cartas compradas aparecem na casa desocupada mais a esquerda.

➤ REMOVER CARTA:
Você pode remover até 3 cartas por partida.
Clique no botão 'Remover Carta' e selecione a posição (1 a 6).
Após 3 remoções, o botão se desativará.

➤ FINALIZAR:
Após preencher todas as 6 casas, clique em 'Finalizar' para ver sua pontuação.
Clicar em finalizar limpa sua mão e recomeça o jogo.

➤ PONTUAÇÃO:
- Fogo: vale 0, mas ganha 2 pontos para cada outro Fogo.
- Terra: vale 3 se estiver ao lado de outra Terra.
- Água: vale 1 e duplica o valor da carta à direita.
- Ar: vale 1 e duplica o valor da carta à esquerda.
- Luz: vale 3 se estiver em posição ímpar (1, 3, 5).
- Trevas: vale 3 se estiver em posição par (2, 4, 6).

➤ BÔNUS:
+10 pontos se TODAS as cartas forem diferentes OU todas forem iguais.

➤ PENALIDADES:
- Terra ao lado de Fogo: Terra vale 0.
- Água ao lado de Trevas: carta à esquerda da Água vale 0.
- Luz ao lado de Ar: carta à direita da Luz vale 0.

➤ LIMPAR
Limpar reseta o jogo a qualquer momento sem mostrar sua pontuação.
";
    }
}
