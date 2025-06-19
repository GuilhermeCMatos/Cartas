using System;
using System.Linq;

namespace Cartas
{
    public class ScoreCalculator
    {
        public int ComputeScore(string[] mao)
        {
            int[] valores = new int[6];

            // Etapa 1: valores base
            for (int i = 0; i < mao.Length; i++)
            {
                string carta = mao[i];
                if (carta == null) continue;

                switch (carta)
                {
                    case "Fogo":
                        int totalFogo = mao.Count(c => c == "Fogo");
                        valores[i] = totalFogo > 1 ? 2 : 0;
                        break;

                    case "Terra":
                        if (IsAdjacent(mao, i, "Terra"))
                            valores[i] = 3;
                        break;

                    case "Água":
                    case "Ar":
                        valores[i] = 1;
                        break;

                    case "Luz":
                        if ((i + 1) % 2 == 1)
                            valores[i] = 3;
                        break;

                    case "Trevas":
                        if ((i + 1) % 2 == 0)
                            valores[i] = 3;
                        break;
                }
            }

            // Etapa 2: efeitos de duplicação
            for (int i = 0; i < mao.Length; i++)
            {
                string carta = mao[i];
                if (carta == null) continue;

                if (carta == "Água" && i + 1 < mao.Length && mao[i + 1] != null)
                    valores[i + 1] *= 2;

                if (carta == "Ar" && i - 1 >= 0 && mao[i - 1] != null)
                    valores[i - 1] *= 2;
            }

            // Etapa 3: aplicar penalidades
            for (int i = 0; i < mao.Length; i++)
            {
                string carta = mao[i];
                if (carta == null) continue;

                // Terra ao lado de Fogo → Terra = 0
                if (carta == "Terra")
                {
                    if ((i > 0 && mao[i - 1] == "Fogo") || (i < mao.Length - 1 && mao[i + 1] == "Fogo"))
                    {
                        valores[i] = 0;
                    }
                }

                // Água ao lado de Trevas → carta à esquerda da Água = 0
                if (carta == "Água")
                {
                    if ((i > 0 && mao[i - 1] == "Trevas") || (i < mao.Length - 1 && mao[i + 1] == "Trevas"))
                    {
                        if (i > 0 && mao[i - 1] != null)
                            valores[i - 1] = 0;
                    }
                }

                // Ar ao lado de Luz → carta à direita da Luz = 0
                if (carta == "Luz")
                {
                    if ((i > 0 && mao[i - 1] == "Ar") || (i < mao.Length - 1 && mao[i + 1] == "Ar"))
                    {
                        if (i < mao.Length - 1 && mao[i + 1] != null)
                            valores[i + 1] = 0;
                    }
                }
            }

            int total = valores.Sum();

            // Etapa 4: bônus por cartas iguais ou diferentes
            if (mao.All(c => c != null))
            {
                int distintos = mao.Distinct().Count();
                if (distintos == 1 || distintos == 6)
                    total += 10;
            }

            return total;
        }

        private bool IsAdjacent(string[] mao, int index, string tipo)
        {
            return (index > 0 && mao[index - 1] == tipo) || (index < mao.Length - 1 && mao[index + 1] == tipo);
        }
    }
}
