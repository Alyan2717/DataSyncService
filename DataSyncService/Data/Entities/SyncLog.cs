namespace DataSyncService.Data.Entities
{
    public class SyncLog
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";
        public string Message { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; }
    }
}
