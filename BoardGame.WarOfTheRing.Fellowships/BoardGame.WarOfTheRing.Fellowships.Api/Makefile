run-dev:
	docker-compose build
	docker-compose up

ef-add-migration:
	dotnet ef migrations add --project ./../BoardGame.WarOfTheRing.Fellowships.Infrastructure/BoardGame.WarOfTheRing.Fellowships.Infrastructure.csproj --startup-project BoardGame.WarOfTheRing.Fellowships.Api.csproj --context BoardGame.WarOfTheRing.Fellowships.Infrastructure.Persistence.EntityFrameworkCore.FellowshipDbContext --configuration Debug $(NAME) --output-dir Migrations

ef-remove-migration:
	dotnet ef migrations remove --project ./../BoardGame.WarOfTheRing.Fellowships.Infrastructure/BoardGame.WarOfTheRing.Fellowships.Infrastructure.csproj --startup-project BoardGame.WarOfTheRing.Fellowships.Api.csproj --context BoardGame.WarOfTheRing.Fellowships.Infrastructure.Persistence.EntityFrameworkCore.FellowshipDbContext --configuration Debug --force
	
ef-update-db:
	dotnet ef database update --project ./../BoardGame.WarOfTheRing.Fellowships.Infrastructure/BoardGame.WarOfTheRing.Fellowships.Infrastructure.csproj --startup-project BoardGame.WarOfTheRing.Fellowships.Api.csproj --context BoardGame.WarOfTheRing.Fellowships.Infrastructure.Persistence.EntityFrameworkCore.FellowshipDbContext --configuration Debug $(TARGET-MIGRATION)

ef-drop-db:
	dotnet ef database drop --project ./../BoardGame.WarOfTheRing.Fellowships.Infrastructure/BoardGame.WarOfTheRing.Fellowships.Infrastructure.csproj --startup-project BoardGame.WarOfTheRing.Fellowships.Api.csproj --context BoardGame.WarOfTheRing.Fellowships.Infrastructure.Persistence.EntityFrameworkCore.FellowshipDbContext --configuration Debug --force