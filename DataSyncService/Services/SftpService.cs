using Renci.SshNet;

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
                client.Connect();
                using var fileStream = File.OpenRead(localFilePath);
                client.UploadFile(fileStream, $"{_config["SftpConfig:RemoteFolder"]}/{remoteFileName}", true);
                client.Disconnect();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SFTP Upload failed: {ex.Message}");
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
                Console.WriteLine($"List failed: {ex.Message}");
            }
            return files;
        }
    }
}
