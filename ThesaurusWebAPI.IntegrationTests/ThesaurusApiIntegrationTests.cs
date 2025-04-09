using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using ThesaurusWebAPI.IntegrationTests.Helper;

public class ThesaurusApiIntegrationTests
{
    private WebApplicationFactory<Program> _factory;
    private HttpClient _httpClient;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        _factory = new WebApplicationFactory<Program>();
        _httpClient = _factory.CreateClient();
    }

    [Test]
    public async Task AddWord_And_GetSynonyms_ReturnsExpectedResult()
    {
        string uniqueWord = IntegtrationHelper.RandomWord();
        var content = JsonContent.Create(new
        {
            word = uniqueWord,
            synonyms = new[] { "poor", "substandard" }
        });

        // POST: /api/thesaurus/addword
        var response = await _httpClient.PostAsync("/api/thesaurus/addword", content);
        response.EnsureSuccessStatusCode();

        // GET: /api/thesaurus/getsynonyms?word=...
        var getResponse = await _httpClient.GetAsync($"/api/thesaurus/getsynonyms?word={uniqueWord}");
        var json = await getResponse.Content.ReadAsStringAsync();

        Assert.That(json, Does.Contain("poor"));
        Assert.That(json, Does.Contain("substandard"));
    }

    [Test]
    public async Task GetSynonyms_ShouldReturnExpectedSynonyms()
    {
        var inputWord = "joy";

        // GET: /api/thesaurus/getsynonyms?word=joy
        var response = await _httpClient.GetAsync($"/api/thesaurus/getsynonyms?word={inputWord}");
        var result = await response.Content.ReadAsStringAsync();

        Assert.That(result, Does.Contain("happy"));
        Assert.That(result, Does.Contain("cheerful"));
    }

    [Test]
    public async Task GetAllWords_ShouldReturnOk()
    {
        // GET: /api/thesaurus
        var response = await _httpClient.GetAsync("/api/thesaurus");
        Assert.That(response.IsSuccessStatusCode, Is.True);
    }

    [OneTimeTearDown]
    public void Dispose()
    {
        _httpClient?.Dispose();
        _factory?.Dispose();
    }
}
