
namespace StringSearchingAlgorithms
{
    static class PrefixFunction
    {

        public static int[] Compute(string str)
        {
            var pi = new int[str.Length];
            pi[0] = 0;

            for (int i = 1, currentPosition = 0; i < str.Length; i++)
            {

                while ((currentPosition > 0) && (str[currentPosition] != str[i]))
                {
                    currentPosition = pi[currentPosition - 1];
                }

                if (str[currentPosition] == str[i])
                {
                    currentPosition++;
                }
                pi[i] = currentPosition;
            }

            return pi;
        }

    }
}