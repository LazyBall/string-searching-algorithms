using System;

namespace StringSearchingAlgorithms
{
    static class PrefixFunction
    {
        public static int[] Compute(string str)
        {
            var pi = new int[str.Length]; // значения префикс-функции
            pi[0] = 0; // для префикса из одного символа функция равна нулю
            int k = 0;

            for (int q = 1; q < str.Length; q++)
            {

                while ((k > 0) && (str[k] != str[q]))
                {
                    k = pi[k - 1];
                }

                if (str[k] == str[q])
                {
                    k++;
                }
                pi[q] = k;
            }

            return pi;
        }

        public static int[] BuildFromZFunction(int[] z)
        {
            var pi = new int[z.Length];

            for (int i = 1; i < z.Length; i++)
            {

                for (int j = z[i] - 1; j >= 0; j--)
                {
                    if (pi[i + j] > 0) break;
                    else pi[i + j] = j + 1;
                }

            }

            return pi;
        }
    }
}