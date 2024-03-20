FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BoardGame.WarOfTheRing.PoliticalTrack.Api/BoardGame.WarOfTheRing.PoliticalTrack.Api.csproj", "BoardGame.WarOfTheRing.PoliticalTrack.Api/"]
RUN dotnet restore "BoardGame.WarOfTheRing.PoliticalTrack.Api/BoardGame.WarOfTheRing.PoliticalTrack.Api.csproj"
COPY . .
WORKDIR "/src/BoardGame.WarOfTheRing.PoliticalTrack.Api"
RUN dotnet build "BoardGame.WarOfTheRing.PoliticalTrack.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BoardGame.WarOfTheRing.PoliticalTrack.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BoardGame.WarOfTheRing.PoliticalTrack.Api.dll"]
