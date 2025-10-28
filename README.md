# 🗂️ DataSync API Service

A production-grade .NET 8 backend microservice for secure file synchronization between local storage and remote SFTP servers.

## 🚀 Features
- Upload and store files locally
- Automatic & manual SFTP synchronization
- Background jobs with Quartz.NET
- Structured logging using Serilog
- MySQL database with EF Core
- Swagger API documentation

## ⚙️ Tech Stack
- **.NET 8 Web API**
- **Entity Framework Core**
- **Quartz.NET**
- **SSH.NET (SFTP)**
- **Serilog**
- **MySQL**

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

# 🗂️ DataSync API Service

**Live Demo:** [https://datasyncservice.onrender.com/swagger/index.html](https://datasyncservice.onrender.com/swagger/index.html)

![.NET 8](https://img.shields.io/badge/.NET-8.0-blueviolet)
![Status](https://img.shields.io/badge/status-active-success)
![License: MIT](https://img.shields.io/badge/License-MIT-green)
![Deploy](https://img.shields.io/badge/Deployed-Render-blue)
