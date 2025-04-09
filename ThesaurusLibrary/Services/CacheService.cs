using ThesaurusLibrary.IServices;
using ThesaurusLibrary.Models;
using ThesaurusWebAPI.IApiServices;

public class CacheService : ICacheService
{
    private readonly IDataReaderService _readerService;
    private readonly ILoggerService _loggerService;


    public CacheService(IDataReaderService readerService, ILoggerService loggerService)
    {
        _readerService = readerService;
        _loggerService = loggerService;
    }

    #region Caching service functions


    #region Loads cache from json file
    public Dictionary<string, WordNode> LoadCache()
    {
        try
        {
            var rawData = _readerService.ReadData();
            var result = new Dictionary<string, WordNode>();

            foreach (var kvp in rawData)
            {
                if (!result.ContainsKey(kvp.Key))
                    result[kvp.Key] = new WordNode(kvp.Key);

                foreach (var syn in kvp.Value)
                {
                    if (!result.ContainsKey(syn))
                        result[syn] = new WordNode(syn);

                    result[kvp.Key]._neighbors.Add(result[syn]);
                }
            }

            return result;
        }
        catch (Exception ex)
        {
            _loggerService.LogError("Error in loading cache from file", ex);
            return new Dictionary<string, WordNode>();
        }
    }
    #endregion

    #region Save cache data in json file
    public void SaveCache(Dictionary<string, WordNode> data)
    {
        try
        {
            var toSave = new Dictionary<string, string[]>();

            foreach (var kvp in data)
            {
                var synonyms = kvp.Value._neighbors.Select(n => n._word).ToArray();
                toSave[kvp.Key] = synonyms;
            }

            _readerService.WriteData(toSave);
        }
        catch (Exception ex)
        {
            _loggerService.LogError("Error in saving data in file from cache", ex);
        }

    }
    #endregion

    #region Check if word is present in dictionary
    public bool CaheContains(string word)
    {
        try
        {
            var data = LoadCache();
            return data.ContainsKey(word);
        }
        catch (Exception ex)
        {
            _loggerService.LogError("Error in saving data in file from cache", ex);
            return false;
        }
    }
    #endregion

    #endregion
}