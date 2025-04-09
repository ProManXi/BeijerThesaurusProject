using System.Collections.Generic;

namespace ThesaurusLibrary.IServices
{
    public interface IDataReaderService
    {
        Dictionary<string, string[]> ReadData();
        void WriteData(Dictionary<string, string[]> data);
    }
}