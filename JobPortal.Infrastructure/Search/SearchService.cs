using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Domain.Entities;
using JobPortal.Infrastructure.Configurations;

public class SearchService : ISearchService
{
    private readonly ElasticsearchClient _client;

    public SearchService()
    {
        var settings = new ElasticsearchClientSettings(new Uri(ElasticSearchConfiguration.ConnectionString))
            .CertificateFingerprint(ElasticSearchConfiguration.SH256Fingerprint)
            .Authentication(new BasicAuthentication(ElasticSearchConfiguration.User, ElasticSearchConfiguration.Password));
        _client = new ElasticsearchClient(settings);
    }

    public async Task<bool> Index(JobPosting jobPosting)
    {
        var response = await _client.IndexAsync(jobPosting, index: ElasticSearchConfiguration.DefaultIndex);

        if (response.IsValidResponse)
        {
            Console.WriteLine($"Index document with ID {response.Id} succeeded.");
            return true;
        }
        return false;
    }
    public async Task<List<JobPosting>> Search(string searchTerm)
    {
        var searchResponse = await _client.SearchAsync<JobPosting>(s => s
        .Index(ElasticSearchConfiguration.DefaultIndex)
        .Query(q => q
            .Bool(b => b
                .Should(
                    sh => sh.Wildcard(w => w
                        .Field(f => f.Title)
                        .Value($"*{searchTerm}*")),
                    sh => sh.Match(m => m
                        .Field(f => f.Description)
                        .Query(searchTerm)),
                    sh => sh.Match(m => m
                        .Field(f => f.Responsibilities)
                        .Query(searchTerm)),
                    sh => sh.Match(m => m
                        .Field(f => f.RequiredSkills)
                        .Query(searchTerm))
                ))));

        if (searchResponse.IsValidResponse)
        {
            return searchResponse.Documents.ToList();
        }
        return new List<JobPosting>();
    }

    public async Task<bool> UpdateEntry(JobPosting jobPosting)
    {
        var response = await _client.UpdateAsync<JobPosting, JobPosting>
            (ElasticSearchConfiguration.DefaultIndex, jobPosting.Id, u => u
            .Doc(jobPosting));

        return response.IsValidResponse;
    }


    public async Task<bool> DeleteEntry(int id)
    {
        var response = await _client.DeleteAsync(ElasticSearchConfiguration.DefaultIndex, id);
        return response.IsValidResponse;
    }
   
}
