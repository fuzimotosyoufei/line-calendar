# 1. 開発用のSDKを使ってアプリをビルドする
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# プロジェクトファイルをコピーして依存関係を復元
COPY *.csproj ./
RUN dotnet restore

# すべてのソースコードをコピーしてビルド
COPY . ./
RUN dotnet publish -c Release -o out

# 2. 実行用の軽量な環境にビルド成果物を移す
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Renderなどの環境でポートを動的に合わせるための設定iiiiii
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "ShiftManagerApi.dll"]