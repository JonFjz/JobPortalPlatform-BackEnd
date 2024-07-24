namespace JobPortal.Application.Contracts.Infrastructure
{
    public interface IBlobStorageService
    {
        Task<string> UploadFileAsync(Stream fileStream, string fileName);
        Task<byte[]> DownloadFileAsync(string blobUrl);
        Task DeleteFileAsync(string fileUrl);
    }
}
