using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.VisualBasic;

namespace Cartas
{
    public partial class MainWindow : Window
    {
        List<string> pilha = new List<string> { "Fogo", "Água", "Terra", "Ar", "Luz", "Trevas" };
        string[] mao = new string[6];
        bool[] slotsOcupados = new bool[6];
        int exclusoesRestantes = 3;
        Random rnd = new Random();

        public MainWindow() { InitializeComponent(); }

        private void comprar_Click(object sender, RoutedEventArgs e)
        {
            int slot = Array.FindIndex(slotsOcupados, oc => !oc);
            if (slot == -1) { MessageBox.Show("Mão cheia!"); return; }

            string carta = pilha[rnd.Next(pilha.Count)];
            mao[slot] = carta; slotsOcupados[slot] = true;

            // Cria visual da carta (igual antes) ...
            TextBlock txt = new TextBlock { Text = carta, FontWeight = FontWeights.Bold, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
            Border bc = new Border { BorderThickness = new Thickness(3), Padding = new Thickness(10), Margin = new Thickness(5), CornerRadius = new CornerRadius(5), Child = txt };
            // define cor por switch...
            switch (carta)
            {
                case "Fogo": bc.BorderBrush = Brushes.Red; bc.Background = Brushes.MistyRose; break;
                case "Água": bc.BorderBrush = Brushes.Blue; bc.Background = Brushes.LightBlue; break;
                case "Terra": bc.BorderBrush = Brushes.SaddleBrown; bc.Background = Brushes.BurlyWood; break;
                case "Ar": bc.BorderBrush = Brushes.LightGray; bc.Background = Brushes.WhiteSmoke; break;
                case "Luz": bc.BorderBrush = Brushes.Gold; bc.Background = Brushes.LightYellow; break;
                case "Trevas": bc.BorderBrush = Brushes.Purple; bc.Background = Brushes.MediumPurple; break;
                default: bc.BorderBrush = Brushes.Black; bc.Background = Brushes.LightGray; break;
            }

            // Insere dentro do slot fixo
            foreach (UIElement el in grid_deck.Children)
            {
                if (Grid.GetRow(el) == 2 && Grid.GetColumn(el) == slot && el is Border s && s.Child == null)
                {
                    s.Child = bc;
                    break;
                }
            }
        }

        private void remover_Click(object sender, RoutedEventArgs e)
        {
            if (exclusoesRestantes <= 0)
            {
                MessageBox.Show("💔 Sem remoções!");
                return;
            }

            string inpt = Interaction.InputBox("Posição (1–6):", "Remover Carta", "1");
            if (!int.TryParse(inpt, out int pos) || pos < 1 || pos > 6)
            {
                MessageBox.Show("Entrada inválida."); return;
            }
            int idx = pos - 1;
            if (!slotsOcupados[idx])
            {
                MessageBox.Show("Não tem carta nesse espaço."); return;
            }

            // Limpa o Child do slot
            foreach (UIElement el in grid_deck.Children)
            {
                if (Grid.GetRow(el) == 2 && Grid.GetColumn(el) == idx && el is Border slot && slot.Child is Border cartaB)
                {
                    slot.Child = null;
                    break;
                }
            }
            mao[idx] = null; slotsOcupados[idx] = false;
            exclusoesRestantes--;

            // Atualiza texto do botão
            if (exclusoesRestantes > 0)
                txtRemover.Text = $"Remover Carta: {exclusoesRestantes}";
            else
            {
                txtRemover.Text = "💔 Sem remoções";
                remover.IsEnabled = false;
            }

            MessageBox.Show("Carta removida!");
        }

        private void finalizar_Click(object sender, RoutedEventArgs e)
        {
            var calculator = new ScoreCalculator();
            int score = calculator.ComputeScore(mao);
            MessageBox.Show($"Sua pontuação final foi: {score}", "Fim de Jogo");
            limpar_Click(null, null);
        }

        private void MostrarRegras(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Regras.Texto, "Regras do Jogo");
        }

        private void limpar_Click(object sender, RoutedEventArgs e)
        {
            // Limpa todos os slots
            foreach (UIElement el in grid_deck.Children)
                if (Grid.GetRow(el) == 2 && el is Border slot)
                    slot.Child = null;

            // Reseta estado
            for (int i = 0; i < 6; i++) { mao[i] = null; slotsOcupados[i] = false; }
            exclusoesRestantes = 3;
            remover.IsEnabled = true;
            txtRemover.Text = $"Remover Carta: {exclusoesRestantes}";

            MessageBox.Show("Jogo reiniciado!");
        }
        private void fechar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
