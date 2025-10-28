using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataSyncService.Data.Entities
{
    public class FileRecord
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string FileName { get; set; }

        [Required]
        [Column(TypeName = "varchar(500)")]
        public string FilePath { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime UploadedAt { get; set; }

        public bool Synced { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? LastSyncDate { get; set; }

    }
}
