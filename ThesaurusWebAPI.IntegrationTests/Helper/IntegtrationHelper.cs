using System;

namespace ThesaurusWebAPI.IntegrationTests.Helper
{
    public class IntegtrationHelper
    {
        public static string RandomWord(int length = 6)
        {
            var rand = new Random();
            var word = string.Empty;

            for (int i = 0; i < length; i++)
            {
                char letter = (char)rand.Next('a', 'z' + 1); // picks a random lowercase letter
                word += letter;
            }

            return word;
        }
    }
}
