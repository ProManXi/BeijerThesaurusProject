using Moq;
using ThesaurusLibrary.IServices;
using ThesaurusLibrary.Models;
using ThesaurusWebAPI.IApiServices;

namespace ThesaurusLibrary.UnitTests
{
        public class CacheServiceTests
        {
            private Mock<IDataReaderService> _mockReader;
            private CacheService _cacheService;
            private Mock<ILoggerService> _loggerMockService;



        [SetUp]
            public void Setup()
            {
                _mockReader = new Mock<IDataReaderService>();
                _loggerMockService = new Mock<ILoggerService>();
                _cacheService = new CacheService(_mockReader.Object, _loggerMockService.Object);
            }

            [Test]
            public void SaveCache_Should_Serialize_Without_Exception()
            {
                // Arrange
                var nodes = new Dictionary<string, WordNode>
                {
                    ["happy"] = new WordNode("happy")
                };

                // Act & Assert (no exception should be thrown)
                Assert.DoesNotThrow(() => _cacheService.SaveCache(nodes));
            }

            [Test]
            public void LoadCache_Should_Return_Valid_Dictionary()
            {
                // Arrange
                var json = "{\"happy\":{\"_word\":\"happy\",\"_neighbors\":[]}}";
                _mockReader.Setup(x => x.ReadData()).Returns(new Dictionary<string, string[]>
                {
                    {"happy", new[] { "joyful, cheerful"}  }
                });

                // Act
                var result = _cacheService.LoadCache();

                // Assert
                Assert.IsTrue(result.ContainsKey("happy"));
                Assert.AreEqual("happy", result["happy"]._word);
            }
        }
}
