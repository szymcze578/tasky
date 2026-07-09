# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Overview

Tasky is an ASP.NET Core Web API (.NET 10) for managing task boards. It follows a layered Clean Architecture with four projects and uses Entity Framework Core over PostgreSQL.

## Commands

```bash
# Build the whole solution
dotnet build Tasky.sln

# Run the API (from Tasky.API; uses launchSettings profiles)
dotnet run --project Tasky.API                 # default profile
dotnet run --project Tasky.API --launch-profile https

# API listens on http://localhost:6060 and https://localhost:6066 (Development).
# Swagger UI is served at /swagger only in the Development environment.

# EF Core migrations — Tasky.Infrastructure holds the DbContext, Tasky.API is the startup project
dotnet ef migrations add <Name> --project Tasky.Infrastructure --startup-project Tasky.API
dotnet ef database update --project Tasky.Infrastructure --startup-project Tasky.API
```

There is currently no test project, linter config, or CI in the repo.

## Architecture

Dependency flow is strictly inward: `API → Application → Domain` and `Infrastructure → Application → Domain`. The API references both Application and Infrastructure only to wire up DI at startup; business logic never depends on Infrastructure directly.

- **Tasky.Domain** — POCO entities (`UserEntity`, `BoardEntity`, `TaskEntity`, owned `Address`) and the `Status` enum. No dependencies.
- **Tasky.Application** — service interfaces (`Interfaces/Services`), repository interfaces (`Interfaces/Infrastructure`), service implementations, DTOs (`Model`), and DTO↔entity mappers. Defines the contracts that Infrastructure implements.
- **Tasky.Infrastructure** — `BoardDbContext`, repository implementations, and EF migrations. Implements the repository interfaces declared in Application.
- **Tasky.API** — controllers, ViewModels, and ViewModel↔DTO mappers. The HTTP boundary.

### Request flow

A request crosses two mapping boundaries: `Controller (ViewModel) → API.Mappers → Service (DTO) → Application.Mappers → Repository (Entity)`. There are two distinct `UserMapper` classes — `Tasky.API.Mappers.UserMapper` (ViewModel↔DTO) and `Tasky.Application.Mappers.UserMapper` (DTO→Entity). Keep mapping in the correct layer; do not let entities leak into controllers or ViewModels into services.

### Dependency injection

Each layer exposes a `ServiceCollectionExtension` so wiring stays local to its project:
- `AddInfrastructure(config)` registers `BoardDbContext` (Npgsql) and the repositories.
- `AddServices()` registers the application services.
Both are called from `Tasky.API/Program.cs`. When adding a new service or repository, register it in the corresponding extension, not in `Program.cs`.

### Persistence

`BoardDbContext` exposes `DbSet`s as `internal` (only Infrastructure repositories touch them). Entity configuration is done in `OnModelCreating`; `Address` is configured as an EF owned type on `UserEntity`. The connection string lives under `ConnectionStrings:DefaultConnection` in appsettings.

## Conventions

- C# 12 primary constructors are used for services and controllers (e.g. `TaskService(ITaskRepository taskRepository)`); newer files prefer this over explicit constructor fields, though some repositories still use classic constructors.
- `ImplicitUsings` and `Nullable` are enabled across all projects.
- Repositories are async (`Task<...>`), but note some service code currently bridges async-to-sync via `.Result` (e.g. `TaskService.GetTasksByBoardId`) — prefer making the call chain async end-to-end when touching this code.

## State of the codebase

This is an early-stage project. `BoardController.GetBoardsForUser` and `UserController.GetUsers` return placeholder strings rather than real data, and several entities (boards, full task CRUD) are only partially wired through the layers. Expect to complete vertical slices (controller → service → repository → migration) rather than editing finished features.
