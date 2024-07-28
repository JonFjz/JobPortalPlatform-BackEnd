namespace JobPortal.Infrastructure.Configurations
{
    public class BlobStorageSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string ResumesContainerName { get; set; } = null!;
        public string LogosContainerName { get; set; } = null!;
        public string AccountKey { get; set; } = null!;
    }
}
