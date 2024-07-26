using Azure.Storage.Blobs;
using JobPortal.Application.Contracts.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace JobPortal.Infrastructure.Storage
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName;

        public BlobStorageService(IConfiguration configuration)
        {
            _blobServiceClient = new BlobServiceClient(configuration["AzureBlobStorage:ConnectionString"]);
            _containerName = configuration["AzureBlobStorage:ContainerName"];

        }

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName)
        {
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = blobContainerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(fileStream, true);
            return blobClient.Uri.ToString();
        }

        public async Task DeleteFileAsync(string fileName)
        {
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = blobContainerClient.GetBlobClient(fileName);
            await blobClient.DeleteIfExistsAsync();
        }

        public async Task<byte[]> DownloadFileAsync(string blobUrl)
        {
            var blobClient = new BlobClient(new Uri(blobUrl));
            var response = await blobClient.DownloadAsync();

            using (var memoryStream = new MemoryStream())
            {
                await response.Value.Content.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public async Task<Stream> GetFileStreamAsync(string blobUrl)
        {
            var blobClient = new BlobClient(new Uri(blobUrl));
            var response = await blobClient.DownloadAsync();
            return response.Value.Content;
        }

    }
}