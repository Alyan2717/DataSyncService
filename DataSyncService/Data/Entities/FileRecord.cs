namespace DataSyncService.Data.Entities
{
    public class FileRecord
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
        public bool Synced { get; set; } = false;
        public DateTime? LastSyncDate { get; set; }
    }
}
