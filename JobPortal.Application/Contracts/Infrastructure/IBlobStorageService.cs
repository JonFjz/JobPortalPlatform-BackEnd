namespace JobPortal.Application.Contracts.Infrastructure
{
    public interface IBlobStorageService
    {
        Task<string> UploadFileAsync(Stream fileStream, string fileName);
        Task DeleteFileAsync(string fileUrl);
        Task<byte[]> DownloadFileAsync(string blobUrl);
    }
}
