using System.Collections.Generic;

namespace endpoints
{

    public class Methods
    {
        public static string Pluralization(int number, string one, string few, string many)
        {
            int modEx = number % 100;
            int mod = number % 10;
            if (modEx >= 11 && modEx <= 14)
            {
                return number + " " + many;
            }
            else if (mod == 1)
            {
                return number + " " + one;
            }
            else if (mod >= 2 && mod <= 4)
            {
                return number + " " + few;
            }
            return number + " " + many;

        }

        public static Dictionary<string, int> FindFrequency(string[] words)
        {
            Dictionary<string, int> wordsFrequency = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (!wordsFrequency.ContainsKey(word))
                {
                    wordsFrequency.Add(word, 1);
                }
                else
                {
                    wordsFrequency[word]++;
                }
            }
            return wordsFrequency;
        }
    }
}