using Moq;
using ThesaurusLibrary.IServices;
using ThesaurusLibrary.Models;
using ThesaurusLibrary.Services;
using ThesaurusWebAPI.IApiServices;

namespace ThesaurusLibrary.UnitTests
{
    public class SynonymServiceTests
    {
        private ISynonymService _synonymService;
        private ISupportService _helperMockService;
        private Mock<ICacheService> _cacheMockService;
        private Mock<ILoggerService> _loggerMockService;

        [SetUp]
        public void Setup()
        {
            _helperMockService = new SupportService();  // no need to mock as we its important to test functionality
            _cacheMockService = new Mock<ICacheService>();
            _loggerMockService = new Mock<ILoggerService>();
            _cacheMockService.Setup(x => x.LoadCache()).Returns(new Dictionary<string, WordNode>());
            _cacheMockService.Setup(x => x.SaveCache(It.IsAny<Dictionary<string, WordNode>>()));
            _synonymService = new SynonymService(_helperMockService, _cacheMockService.Object, _loggerMockService.Object);
        }

        [Test]
        public void AddWord_Should_Add_Word_With_Synonyms()
        {
            // Arrange
            string word = "lay";
            var synonyms = new List<string> { "laying", "rest" };

            // Act
            _synonymService.AddWord(word, synonyms);
            var result = _synonymService.GetSynonyms(word);

            // Assert
            Assert.IsNotNull(result);
            CollectionAssert.AreEquivalent(synonyms, result);
        }

        [Test]
        public void GetSynonyms_Should_Return_EmptyList_When_Word_Not_Found()
        {
            // Arrange
            string word = "unknown";

            // Act
            var result = _synonymService.GetSynonyms(word);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }

        [Test]
        public void GetAllWords_Should_Return_All_Added_Words()
        {
            // Arrange
            _synonymService.AddWord("fast", new List<string> { "quick", "speedy" });
            _synonymService.AddWord("smart", new List<string> { "intelligent", "clever" });

            // Act
            var allWords = _synonymService.GetAllWords();

            // Assert
            CollectionAssert.Contains(allWords, "fast");
            CollectionAssert.Contains(allWords, "smart");
            Assert.AreEqual(6, allWords.Count);
        }

        [Test]
        public void AddWord_Should_Not_Duplicate_Synonyms()
        {
            // Arrange
            _synonymService.AddWord("happy", new List<string> { "joy" });
            _synonymService.AddWord("happy", new List<string> { "joy" });

            // Act
            var synonyms = _synonymService.GetSynonyms("happy");

            // Assert
            Assert.That(synonyms, Has.Exactly(1).Items); // Should only have "joy" once
            Assert.Contains("joy", synonyms);
        }
    }
}