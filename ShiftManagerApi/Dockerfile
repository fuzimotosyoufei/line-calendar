FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# 1. 設計図だけを先にコピーして復元
COPY *.csproj ./
RUN dotnet restore

# 2. ここで全てのファイルを確実にコピーddddd
COPY . .
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out ./

ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000
ENTRYPOINT ["dotnet", "ShiftManagerApi.dll"]