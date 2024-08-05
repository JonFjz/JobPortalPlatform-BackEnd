using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Features.JobPostings.Dtos;
using JobPortal.Domain.Entities;
using JobPortal.Infrastructure.Configurations;

public class SearchService : ISearchService
{
    private readonly ElasticsearchClient _client;

    public SearchService()
    {
        var settings = new ElasticsearchClientSettings(new Uri(ElasticSearchConfiguration.ConnectionString))
            .CertificateFingerprint(ElasticSearchConfiguration.SH256Fingerprint)
            .DefaultIndex(ElasticSearchConfiguration.DefaultIndex)
            .Authentication(new BasicAuthentication(ElasticSearchConfiguration.User, ElasticSearchConfiguration.Password));
        _client = new ElasticsearchClient(settings);
    }

    public async Task<bool> Index(JobPostingDto jobPosting)
    {
        var idAsString = jobPosting.Id.ToString();
        var response = await _client.IndexAsync(jobPosting, index: ElasticSearchConfiguration.DefaultIndex, id: idAsString);

        if (response.IsValidResponse)
        {
            Console.WriteLine($"Indexed document with ID {response.Id}.");
            return true;
        }
        return false;
    }


    public async Task<List<JobPostingDto>> Search(string searchTerm)
    {
        var searchResponse = await _client.SearchAsync<JobPostingDto>(s => s
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
        return new List<JobPostingDto>();
    }

    public async Task<bool> UpdateEntry(JobPosting jobPosting)
    {
        var response = await _client.UpdateAsync<JobPosting, JobPosting>
            (ElasticSearchConfiguration.DefaultIndex, jobPosting.Id, u => u
            .Doc(jobPosting));
        if (response.IsValidResponse)
        {
            Console.WriteLine("Update document succeeded.");
            return true;
        }

        return false;
    }

    public async Task<bool> DeleteEntry(int id)
    {
        var response = await _client.DeleteAsync(ElasticSearchConfiguration.DefaultIndex, id);
        return response.IsValidResponse;
    }
    
    public async Task<List<JobPostingDto>> RecommendJobsAsync(JobSeeker jobSeeker)
    {
        var skills = jobSeeker.Skills.Select(s => s.Name).ToArray();
        var experiences = jobSeeker.Experiences.Select(e => e.JobTitle).ToArray();
        var educations = jobSeeker.Educations.Select(e => e.Degree).ToArray();

        var searchResponse = await _client.SearchAsync<JobPostingDto>(s => s
            .Index(ElasticSearchConfiguration.DefaultIndex)
            .Query(q => q
                .Bool(b => b
                    .Should(
                        sh => sh.Match(m => m
                            .Field(f => f.RequiredSkills)
                            .Query(string.Join(" ", skills))),
                        sh => sh.Match(m => m
                            .Field(f => f.Responsibilities)
                            .Query(string.Join(" ", experiences))),
                        sh => sh.Match(m => m
                            .Field(f => f.Description)
                            .Query(string.Join(" ", experiences))),
                        sh => sh.Match(m => m
                            .Field(f => f.Description)
                            .Query(string.Join(" ", educations)))
                    )
                    .MinimumShouldMatch(1)
                )
            )
            .Size(10)
        );
        return searchResponse.IsValidResponse
            ? searchResponse.Documents.ToList()
            : new List<JobPostingDto>();
    }

}