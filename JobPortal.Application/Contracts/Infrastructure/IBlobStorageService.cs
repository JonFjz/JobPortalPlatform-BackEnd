namespace JobPortal.Application.Contracts.Infrastructure
{
    public interface IBlobStorageService
    {
        Task<string> UploadResumeAsync(Stream fileStream, string fileName);
        Task<string> UploadLogoAsync(Stream fileStream, string fileName);
        Task DeleteResumeAsync(string blobName);
        Task DeletePhotoAsync(string blobName);
        Task<byte[]> DownloadResumeAsync(string blobName);
        Task<byte[]> DownloadLogoAsync(string blobName);
    }
}
