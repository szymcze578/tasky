# ---- Build stage ----
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

# Copy csproj files first and restore, so this layer is cached unless deps change.
COPY "Tasky.API/Tasky.API.csproj", "Tasky.API/"
COPY "Tasky.Application/Tasky.Application.csproj", "Tasky.Application/"
COPY "Tasky.Domain/Tasky.Domain.csproj", "Tasky.Domain/"
COPY "Tasky.Infrastructure/Tasky.Infrastructure.csproj", "Tasky.Infrastructure/"

RUN dotnet restore "Tasky.API/Tasky.API.csproj"

# Copy the rest of the source and publish.
COPY . .

RUN dotnet publish "Tasky.API/Tasky.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ---- Runtime stage ----
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "Tasky.API.dll"]
