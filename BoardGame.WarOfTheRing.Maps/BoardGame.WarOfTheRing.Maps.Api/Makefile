run-dev:
	docker-compose build
	docker-compose up -d

ef-add-migration:
	dotnet ef migrations add --project ./../BoardGame.WarOfTheRing.Maps.Infrastructure/BoardGame.WarOfTheRing.Maps.Infrastructure.csproj --startup-project BoardGame.WarOfTheRing.Maps.Api.csproj --context BoardGame.WarOfTheRing.Maps.Infrastructure.Persistence.EntityFrameworkCore.MapDbContext --configuration Debug $(NAME) --output-dir Migrations

ef-remove-migration:
	dotnet ef migrations remove --project ./../BoardGame.WarOfTheRing.Maps.Infrastructure/BoardGame.WarOfTheRing.Maps.Infrastructure.csproj --startup-project BoardGame.WarOfTheRing.Maps.Api.csproj --context BoardGame.WarOfTheRing.Maps.Infrastructure.Persistence.EntityFrameworkCore.MapDbContext --configuration Debug --force
	
ef-update-db:
	dotnet ef database update --project ./../BoardGame.WarOfTheRing.Maps.Infrastructure/BoardGame.WarOfTheRing.Maps.Infrastructure.csproj --startup-project BoardGame.WarOfTheRing.Maps.Api.csproj --context BoardGame.WarOfTheRing.Maps.Infrastructure.Persistence.EntityFrameworkCore.MapDbContext --configuration Debug $(TARGET-MIGRATION)

ef-drop-db:
	dotnet ef database drop --project ./../BoardGame.WarOfTheRing.Maps.Infrastructure/BoardGame.WarOfTheRing.Maps.Infrastructure.csproj --startup-project BoardGame.WarOfTheRing.Maps.Api.csproj --context BoardGame.WarOfTheRing.Maps.Infrastructure.Persistence.EntityFrameworkCore.MapDbContext --configuration Debug --force