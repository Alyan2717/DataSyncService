using DataSyncService.Data;
using DataSyncService.Data.Entities;
using DataSyncService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataSyncService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SyncController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly SftpService _sftpService;

        public SyncController(AppDbContext context, SftpService sftpService)
        {
            _context = context;
            _sftpService = sftpService;
        }

        [HttpPost("manual")]
        public async Task<IActionResult> ManualSync()
        {
            var unsyncedFiles = _context.FileRecords.Where(f => !f.Synced).ToList();
            var results = new List<object>();

            foreach (var file in unsyncedFiles)
            {
                bool success = _sftpService.UploadFile(file.FilePath, file.FileName);
                results.Add(new { file.FileName, Status = success ? "Uploaded" : "Failed" });

                if (success)
                {
                    file.Synced = true;
                    file.LastSyncDate = DateTime.UtcNow;
                }

                _context.SyncLogs.Add(new SyncLog
                {
                    FileName = file.FileName,
                    Action = "Manual Upload",
                    Status = success ? "Success" : "Failed",
                    Message = success ? "File manually synced" : "Manual sync failed",
                    CreatedAt = DateTime.UtcNow
                });
            }

            await _context.SaveChangesAsync();
            return Ok(results);
        }

        [HttpGet("logs")]
        public IActionResult GetLogs()
        {
            var logs = _context.SyncLogs.OrderByDescending(l => l.CreatedAt).ToList();
            return Ok(logs);
        }
    }
}
