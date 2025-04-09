using System.Collections.Generic;
using ThesaurusLibrary.Models;

namespace ThesaurusLibrary.IServices
{
    public interface ICacheService
    {
        Dictionary<string, WordNode> LoadCache();
        void SaveCache(Dictionary<string, WordNode> data);
        bool CaheContains(string word);
    }
}
