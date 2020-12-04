FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /source

COPY . .

RUN dotnet publish ./QyonAdventureWorks.Api/QyonAdventureWorks.Api.csproj -c release -o /app

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build /app .

EXPOSE 8080
ENTRYPOINT ["dotnet", "QyonAdventureWorks.Api.dll"]