using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Infrastructure.Configurations;
using Microsoft.Extensions.Options;

namespace JobPortal.Infrastructure.Storage
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly BlobStorageSettings _blobSettings;

        public BlobStorageService(IOptions<BlobStorageSettings> blobSettings)
        {
            _blobSettings = blobSettings.Value;
        }


        #region upload
        private async Task<string> UploadFileAsync(Stream data, string originalFileName, string containerName)
        {
            var blobServiceClient = new BlobServiceClient(_blobSettings.ConnectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            var uniqueBlobName = $"{Guid.NewGuid()}_{originalFileName}";
            var blobClient = containerClient.GetBlobClient(uniqueBlobName);

            await blobClient.UploadAsync(data);

            return uniqueBlobName;
        }

        public Task<string> UploadResumeAsync(Stream data, string fileName)
        {
            return UploadFileAsync(data, fileName, _blobSettings.ResumesContainerName);
        }

        public Task<string> UploadLogoAsync(Stream data, string fileName)
        {
            return UploadFileAsync(data, fileName, _blobSettings.LogosContainerName);
        }

        #endregion


        #region download
        private async Task<byte[]> DownloadFileAsync(string containerName, string blobName)
        {
            var blobServiceClient = new BlobServiceClient(_blobSettings.ConnectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(blobName);

            var sasUrl = GetBlobSasUrl(blobClient.Uri.ToString(), containerName, blobName);
            if (sasUrl == null) throw new Exception("Failed to generate SAS URL");

            var downloadClient = new BlobClient(new Uri(sasUrl));
            var response = await downloadClient.DownloadAsync();

            using (var memoryStream = new MemoryStream())
            {
                await response.Value.Content.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public Task<byte[]> DownloadResumeAsync(string blobName)
        {
            return DownloadFileAsync(_blobSettings.ResumesContainerName, blobName);
        }

        public Task<byte[]> DownloadLogoAsync(string blobName)
        {
            return DownloadFileAsync(_blobSettings.LogosContainerName, blobName);
        }
        #endregion


        #region delete
        private async Task DeleteFileAsync(string blobName, string containerName)
        {
            var blobServiceClient = new BlobServiceClient(_blobSettings.ConnectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(blobName);
            await blobClient.DeleteIfExistsAsync();
        }

        public Task DeleteResumeAsync(string blobName)
        {
            return DeleteFileAsync(blobName, _blobSettings.ResumesContainerName);
        }

        public Task DeletePhotoAsync(string blobName)
        {
            return DeleteFileAsync(blobName, _blobSettings.LogosContainerName);
        }
        #endregion


        public string? GetBlobSasUrl(string? blobUrl, string containerName, string blobName)
        {
            if (blobUrl == null) return null;

            var sasBuilder = new BlobSasBuilder()
            {
                BlobContainerName = containerName,
                Resource = "b",
                StartsOn = DateTimeOffset.UtcNow,
                ExpiresOn = DateTimeOffset.UtcNow.AddMonths(1),
                BlobName = blobName
            };

            sasBuilder.SetPermissions(BlobSasPermissions.Read);

            var blobServiceClient = new BlobServiceClient(_blobSettings.ConnectionString);

            var sasToken = sasBuilder
                .ToSasQueryParameters(new StorageSharedKeyCredential(blobServiceClient.AccountName, _blobSettings.AccountKey))
                .ToString();

            return $"{blobUrl}?{sasToken}";
        }

    }
}
