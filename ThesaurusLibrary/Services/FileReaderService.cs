using Microsoft.Extensions.Configuration;
using System.Text.Json;
using ThesaurusLibrary.IServices;
using ThesaurusWebAPI.IApiServices;

namespace ThesaurusLibrary.Services
{
    public class FileReaderService : IDataReaderService
    {
        private readonly string _filePath;
        private readonly ILoggerService _loggerService;


        public FileReaderService(IConfiguration config, ILoggerService loggerService)
        {
            _filePath = config["ThesaurusData:filepath"];
            _loggerService = loggerService;
        }

        #region File reading/writing logics

        #region Read data from the Json File and convert in json format
        public Dictionary<string, string[]> ReadData()
        {
            try
            {

                if (!File.Exists(_filePath))
                    return new Dictionary<string, string[]>();

                string json = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<Dictionary<string, string[]>>(json)
                       ?? new Dictionary<string, string[]>();
            }
            catch(Exception ex) 
            {
                _loggerService.LogError("Error in reading data from file", ex);
                return new Dictionary<string, string[]>();
            }
        }
        #endregion

        #region Convert data into json and save it in json file
        public void WriteData(Dictionary<string, string[]> data)
        {
            try
            {
                string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                _loggerService.LogError("Error in writing data in  file", ex);
            }
        }
        #endregion

        #endregion
    }
}