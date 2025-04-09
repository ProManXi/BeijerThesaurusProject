using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesaurusLibrary.Models;

namespace ThesaurusLibrary.IServices
{
    public interface ISupportService
    {
        public void DFS(WordNode node, HashSet<string> visited, List<string> result);
    }
}
