FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BoardGame.WarOfTheRing.Dice.Api/BoardGame.WarOfTheRing.Dice.Api.csproj", "BoardGame.WarOfTheRing.Dice.Api/"]
RUN dotnet restore "BoardGame.WarOfTheRing.Dice.Api/BoardGame.WarOfTheRing.Dice.Api.csproj"
COPY . .
WORKDIR "/src/BoardGame.WarOfTheRing.Dice.Api"
RUN dotnet build "BoardGame.WarOfTheRing.Dice.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BoardGame.WarOfTheRing.Dice.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BoardGame.WarOfTheRing.Dice.Api.dll"]
