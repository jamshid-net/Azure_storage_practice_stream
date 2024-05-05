#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["angularapp2.client/nuget.config", "angularapp2.client/"]
COPY ["AngularApp2.Server/AngularApp2.Server.csproj", "AngularApp2.Server/"]
COPY ["angularapp2.client/angularapp2.client.esproj", "angularapp2.client/"]
RUN dotnet restore "./AngularApp2.Server/./AngularApp2.Server.csproj"
COPY . .
WORKDIR "/src/AngularApp2.Server"
RUN dotnet build "./AngularApp2.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AngularApp2.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AngularApp2.Server.dll"]