using ThesaurusLibrary.IServices;
using ThesaurusLibrary.Models;

namespace ThesaurusLibrary.Services
{
    public class SupportService : ISupportService
    {
        #region Supporting funtions for Thesaurus Library
        public void DFS(WordNode node, HashSet<string> visited, List<string> result)
        {
            foreach (WordNode neighbor in node._neighbors)
            {
                if (visited.Contains(neighbor._word) == false)
                {
                    visited.Add(neighbor._word);
                    result.Add(neighbor._word);
                    DFS(neighbor, visited, result); // backtracking happens here
                }
            }
        }
        #endregion
    }
}
