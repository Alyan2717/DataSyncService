using Renci.SshNet;
using Serilog;

namespace DataSyncService.Services
{
    public class SftpService
    {
        private readonly IConfiguration _config;

        public SftpService(IConfiguration config)
        {
            _config = config;
        }

        private SftpClient GetClient()
        {
            var host = _config["SftpConfig:Host"];
            var port = int.Parse(_config["SftpConfig:Port"]);
            var username = _config["SftpConfig:Username"];
            var password = _config["SftpConfig:Password"];
            return new SftpClient(host, port, username, password);
        }

        public bool UploadFile(string localFilePath, string remoteFileName)
        {
            using var client = GetClient();
            try
            {
                var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "upload");
                if (!Directory.Exists(uploadsPath))
                {
                    Directory.CreateDirectory(uploadsPath);
                    Console.WriteLine($"✅ Created uploads directory: {uploadsPath}");
                }

                if (!File.Exists(localFilePath))
                {
                    Console.WriteLine($"❌ File not found: {localFilePath}");
                    return false;
                }

                client.Connect();
                if (!client.IsConnected)
                {
                    Console.WriteLine("❌ Failed to connect to SFTP server.");
                    return false;
                }

                var remotePath = $"{_config["SftpConfig:RemoteFolder"]}/{remoteFileName}";
                using var fileStream = File.OpenRead(localFilePath);
                client.UploadFile(fileStream, remotePath, true);
                client.Disconnect();

                Console.WriteLine($"✅ Uploaded {remoteFileName} successfully!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ SFTP Upload failed: {ex.Message}");
                Log.Error(ex, "SFTP Upload failed: {ErrorMessage}", ex.Message);
                return false;
            }
        }

        public bool DownloadFile(string remoteFileName, string localFolder)
        {
            using var client = GetClient();
            try
            {
                client.Connect();
                if (!client.IsConnected)
                {
                    Console.WriteLine("❌ Failed to connect to SFTP server.");
                    return false;
                }

                if (!Directory.Exists(localFolder))
                    Directory.CreateDirectory(localFolder);

                var localPath = Path.Combine(localFolder, remoteFileName);
                var remotePath = $"{_config["SftpConfig:RemoteFolder"]}/{remoteFileName}";

                using var fileStream = File.Create(localPath);
                client.DownloadFile(remotePath, fileStream);
                client.Disconnect();

                Console.WriteLine($"✅ Downloaded {remoteFileName} to {localPath}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ SFTP Download failed: {ex.Message}");
                Log.Error(ex, "SFTP Download failed: {ErrorMessage}", ex.Message);
                return false;
            }
        }

        public List<string> ListRemoteFiles()
        {
            var files = new List<string>();
            using var client = GetClient();
            try
            {
                client.Connect();
                var entries = client.ListDirectory(_config["SftpConfig:RemoteFolder"]);
                files = entries.Where(f => !f.IsDirectory).Select(f => f.Name).ToList();
                client.Disconnect();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ SFTP list failed: {ex.Message}");
                Log.Error(ex, "SFTP List failed: {ErrorMessage}", ex.Message);
            }
            return files;
        }
    }
}
