using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using ThesaurusWebAPI.IntegrationTests.Helper;

public class ThesaurusApiIntegrationTests
{
    private  WebApplicationFactory<Program> _factory;
    private  HttpClient _httpClient;

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

        var response = await _httpClient.PostAsync("/api/thesaurus/addword", content);
        response.EnsureSuccessStatusCode();

        var inputword = JsonContent.Create(new
        {
            word = uniqueWord
        }); 

        var getResponse = await _httpClient.PostAsync("/api/thesaurus/getsyn", inputword);
        var json = await getResponse.Content.ReadAsStringAsync();

        Assert.That(json, Does.Contain("poor"));
        Assert.That(json, Does.Contain("substandard"));
    }


    [Test]
    public async Task GetSynonyms_ShouldReturnExpectedSynonyms()
    {
        var inputword = "joy";

        var response = await _httpClient.GetAsync("/api/thesaurus/getsyn/ " + inputword);
        var result = await response.Content.ReadAsStringAsync();

        Assert.That(result, Does.Contain("happy"));
        Assert.That(result, Does.Contain("cheerful"));
    }

    [Test]
    public async Task GetAllWords_ShouldReturnOk()
    {
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
