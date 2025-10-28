# 🗂️ DataSync API Service

A production-grade .NET 8 backend microservice for secure file synchronization between local storage and remote SFTP servers.

## 🚀 Features
- Upload and store files locally
- Automatic & manual SFTP synchronization
- Background jobs with Quartz.NET
- Structured logging using Serilog
- SQL Server database with EF Core
- Swagger API documentation

## ⚙️ Tech Stack
- **.NET 8 Web API**
- **Entity Framework Core**
- **Quartz.NET**
- **SSH.NET (SFTP)**
- **Serilog**
- **SQL Server**

## 📁 Project Structure
DataSyncService/
├── Controllers/
├── Services/
├── Data/
├── Models/
├── Middleware/
├── Logs/
├── Program.cs
├── appsettings.json

## 🌐 API Endpoints
| Method | Endpoint | Description |
|--------|-----------|-------------|
| POST | `/api/files/upload` | Upload file to local storage |
| GET | `/api/files` | Get all uploaded files |
| POST | `/api/sync/manual` | Trigger manual SFTP sync |
| GET | `/api/sync/logs` | Retrieve sync logs |

[Local API] → [SQL Server DB] ↔ [SFTP Server]
       ↑
(Quartz Job auto-sync every minute)

## 🧭 6️⃣ Final Folder Structure
DataSyncService/
├── Controllers/
│ ├── FileController.cs
│ └── SyncController.cs
├── Services/
│ ├── SftpService.cs
│ └── SyncJob.cs
├── Data/
│ ├── AppDbContext.cs
│ ├── Entities/
│ ├── FileRecord.cs
│ └── SyncLog.cs
├── Models/
│ ├── ApiResponse.cs
├── Middleware/
│ └── ExceptionMiddleware.cs
├── appsettings.json
├── Program.cs
├── README.md
├── Logs/
└── DataSync.db
