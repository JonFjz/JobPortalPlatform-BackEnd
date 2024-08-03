FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["JobPortal.API/JobPortal.API.csproj", "JobPortal.API/"]
COPY ["JobPortal.Application/JobPortal.Application.csproj", "JobPortal.Application/"]
COPY ["JobPortal.Common/JobPortal.Common.csproj", "JobPortal.Common/"]
COPY ["JobPortal.Domain/JobPortal.Domain.csproj", "JobPortal.Domain/"]
COPY ["JobPortal.Infrastructure/JobPortal.Infrastructure.csproj", "JobPortal.Infrastructure/"]
COPY ["JobPortal.Persistence/JobPortal.Persistence.csproj", "JobPortal.Persistence/"]
COPY ["JobPortal.Worker/JobPortal.Worker.csproj", "JobPortal.Worker/"]
RUN dotnet restore "./JobPortal.API/JobPortal.API.csproj"
COPY . .
WORKDIR "/src/JobPortal.API"
RUN dotnet build "./JobPortal.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./JobPortal.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JobPortal.API.dll"]