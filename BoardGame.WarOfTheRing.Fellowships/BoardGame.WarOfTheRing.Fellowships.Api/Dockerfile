FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /app
COPY ./ ./

WORKDIR /app/BoardGame.WarOfTheRing.Fellowships/BoardGame.WarOfTheRing.Fellowships.Api
RUN dotnet restore

RUN dotnet build "BoardGame.WarOfTheRing.Fellowships.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BoardGame.WarOfTheRing.Fellowships.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BoardGame.WarOfTheRing.Fellowships.Api.dll"]

