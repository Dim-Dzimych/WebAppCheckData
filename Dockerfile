﻿FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Telegram_Your_Half/Telegram_Your_Half.csproj", "Telegram_Your_Half/"]
RUN dotnet restore "Telegram_Your_Half/Telegram_Your_Half.csproj"
COPY . .
WORKDIR "/src/Telegram_Your_Half"
RUN dotnet build "Telegram_Your_Half.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Telegram_Your_Half.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Telegram_Your_Half.dll"]
