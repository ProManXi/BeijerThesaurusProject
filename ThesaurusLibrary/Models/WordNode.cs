using System;

namespace ThesaurusLibrary.Models
{
    public class WordNode
    {
        public string _word;
        public List<WordNode> _neighbors;

        public WordNode(string word)
        {
            _word = word;
            _neighbors = new List<WordNode>();
        }
    }
}
