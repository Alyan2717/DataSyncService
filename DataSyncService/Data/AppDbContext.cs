using DataSyncService.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DataSyncService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<FileRecord> FileRecords { get; set; }
        public DbSet<SyncLog> SyncLogs { get; set; }
    }
}
