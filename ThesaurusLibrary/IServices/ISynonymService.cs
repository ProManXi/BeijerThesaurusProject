using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesaurusLibrary.IServices
{
    public interface ISynonymService
    {
        public void AddWord(string word, List<string> synonyms);
        public List<string> GetSynonyms(string word);
        public List<string> GetAllWords();
    }
}
