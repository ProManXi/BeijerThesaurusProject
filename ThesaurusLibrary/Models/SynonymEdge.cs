using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesaurusLibrary.Models
{
    public class SynonymEdge
    {
        public WordNode _from;
        public WordNode _to;
        public int _weight;

        public SynonymEdge(WordNode from, WordNode to, int weight = 1)
        {
            _from = from;
            _to = to;
            _weight = weight;
        }
    }
}
