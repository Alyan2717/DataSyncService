using DataSyncService.Data;
using DataSyncService.Data.Entities;
using Quartz;

namespace DataSyncService.Services
{
    public class SyncJob : IJob
    {
        private readonly AppDbContext _context;
        private readonly SftpService _sftpService;

        public SyncJob(AppDbContext context, SftpService sftpService)
        {
            _context = context;
            _sftpService = sftpService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("🔄 Running Sync Job...");

            var unsyncedFiles = _context.FileRecords.Where(f => !f.Synced).ToList();

            foreach (var file in unsyncedFiles)
            {
                bool success = _sftpService.UploadFile(file.FilePath, file.FileName);

                var log = new SyncLog
                {
                    FileName = file.FileName,
                    Action = "Upload",
                    Status = success ? "Success" : "Failed",
                    Message = success ? "File synced successfully" : "Upload failed",
                    CreatedAt = DateTime.UtcNow
                };

                if (success)
                {
                    file.Synced = true;
                    file.LastSyncDate = DateTime.UtcNow;
                }

                _context.SyncLogs.Add(log);
            }

            await _context.SaveChangesAsync();
            Console.WriteLine("✅ Sync Job completed.");
        }

    }
}
