
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
    }
}