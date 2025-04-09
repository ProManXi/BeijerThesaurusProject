using ThesaurusLibrary.IServices;
using ThesaurusLibrary.Models;
using ThesaurusWebAPI.IApiServices;

namespace ThesaurusLibrary.Services
{
    public class SynonymService : ISynonymService
    {
        private readonly ISupportService _helperService;
        private readonly ICacheService _cacheService;
        private Dictionary<string, WordNode> _nodes;
        private readonly ILoggerService _loggerService;


        public SynonymService(ISupportService helperService, ICacheService cacheService, ILoggerService loggingService)
        {
            _helperService = helperService;
            _cacheService = cacheService;
            _nodes = _cacheService.LoadCache() ?? new Dictionary<string, WordNode>(); // Load data from file via injected reader
            _loggerService = loggingService;
        }

        #region Functionalities for our Thesaurus Dictionary

        #region Add word and its opposite in the neighour of node in Dictionary to follow Graph pattern
        public void AddWord(string word, List<string> synonyms)
        {
            try
            {
                if (!_nodes.ContainsKey(word))
                {
                    _nodes[word] = new WordNode(word);
                }

                foreach (string synonym in synonyms)
                {
                    if (!_nodes.ContainsKey(synonym))
                    {
                        _nodes[synonym] = new WordNode(synonym);
                    }

                    _nodes[word]._neighbors.Add(_nodes[synonym]);
                    _nodes[synonym]._neighbors.Add(_nodes[word]);
                }

                _cacheService.SaveCache(_nodes); // Save updated data to file
            }
            catch (Exception ex)
            {
                _loggerService.LogError("Error in adding a word and it's synonyms", ex);
            }
        }
        #endregion

        #region Get synonyms of a word while Avoiding circular search and using DFS search
        public List<string> GetSynonyms(string word)
        {
            try
            {
                List<string> result = new List<string>();
                HashSet<string> visited = new HashSet<string>();

                if (!_nodes.ContainsKey(word))
                    return result;

                visited.Add(word);
                _helperService.DFS(_nodes[word], visited, result);
                return result;

            }
            catch (Exception ex)
            {
                _loggerService.LogError("Error in fetching synonyms for: " + word, ex);
                return new List<string>();
            }

        }
        #endregion

        #region Get all words in Dictionary
        public List<string> GetAllWords()
        {
            try
            {
                return _nodes.Keys.ToList();
            }
            catch(Exception ex)
            {
                _loggerService.LogError("Error in fetching all words", ex);
                return new List<string>();
            }
        }
        #endregion

        #endregion
    }
}