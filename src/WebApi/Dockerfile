﻿FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["../src/WebApi/WebApi.csproj", "src/WebApi/"]
COPY ["../src/Application/Application.csproj", "src/Application/"]
COPY ["../src/Domain/Domain.csproj", "src/Domain/"]
COPY ["../src/Infrastructure/Infrastructure.csproj", "src/Infrastructure/"]
COPY ["./Directory.Build.props", "."]
COPY ["./Directory.Packages.props", "."]
COPY ["./LightsOn.sln", "."]
RUN dotnet restore "src/WebApi/WebApi.csproj"
COPY . .
WORKDIR "/src/src/WebApi"
RUN dotnet build "WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LightsOn.WebApi.dll"]
