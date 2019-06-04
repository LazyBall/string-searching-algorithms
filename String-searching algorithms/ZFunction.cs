using System;

namespace StringSearchingAlgorithms
{
    static class ZFunction
    {
        public static int[] Compute(string str)
        {
            int[] z = new int[str.Length];
            int left = 0, right = 0;

            for (int i = 1; i < str.Length; i++)
            {
                z[i] = Math.Max(0, Math.Min(right - i, z[i - left]));

                while (i + z[i] < str.Length && str[z[i]] == str[i + z[i]])
                {
                    z[i]++;
                }

                if (i + z[i] > right)
                {
                    left = i;
                    right = i + z[i];
                }
            }

            return z;
        }


        public static int[] BuildFromPrefixFunction(int[] pi)
        {
            int[] z = new int[pi.Length];

            for (int j = 1; j < pi.Length; j++)
            {
                if (pi[j] > 0) z[j - pi[j] + 1] = pi[j];
            }

            z[0] = pi.Length;
            int i = 1;

            while (i < pi.Length)
            {
                int t = i;
                if (z[i] > 0)
                {
                    for (int j = 1; j < z[i]; j++)
                    {
                        if (z[i + j] > z[j]) break;
                        z[i + j] = Math.Min(z[j], z[i] - j);
                        t = i + j;
                    }
                }
                i = t + 1;
            }

            return z;
        }
    }
}