# 1C Backup Manager

Windows desktop application for managing 1C backup jobs.

## Version 0.1.0

This first build establishes the Russian-language WPF shell and a Windows background service host. It does **not yet perform 1C exports, SSH/RAC operations, scheduling, or storage replication**. Those features will be added and tested separately before production use.

## Target
- Windows Server 2019 x64
- Self-contained .NET 10 deployment
- WPF desktop UI + Windows Service worker
- Inno Setup installer

## Build
GitHub Actions builds `Setup.exe` on `windows-2022` when code is pushed to `main`, on version tags (`v*`), or manually via Actions. The installer is uploaded as a workflow artifact; tagged builds are also published as a GitHub Release.
