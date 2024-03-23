run-dev:
	docker-compose build
	docker-compose up

ef-add-migration:
	dotnet ef migrations add --project ./../BoardGame.WarOfTheRing.PoliticalTrack.Infrastructure/BoardGame.WarOfTheRing.PoliticalTrack.Infrastructure.csproj --startup-project BoardGame.WarOfTheRing.PoliticalTrack.Api.csproj --context BoardGame.WarOfTheRing.PoliticalTrack.Infrastructure.Persistence.EntityFrameworkCore.PoliticalTrackDbContext --configuration Debug $(NAME) --output-dir Migrations

ef-remove-migration:
	dotnet ef migrations remove --project ./../BoardGame.WarOfTheRing.PoliticalTrack.Infrastructure/BoardGame.WarOfTheRing.PoliticalTrack.Infrastructure.csproj --startup-project BoardGame.WarOfTheRing.PoliticalTrack.Api.csproj --context BoardGame.WarOfTheRing.PoliticalTrack.Infrastructure.Persistence.EntityFrameworkCore.PoliticalTrackDbContext --configuration Debug --force
	
ef-update-db:
	dotnet ef database update --project ./../BoardGame.WarOfTheRing.PoliticalTrack.Infrastructure/BoardGame.WarOfTheRing.PoliticalTrack.Infrastructure.csproj --startup-project BoardGame.WarOfTheRing.PoliticalTrack.Api.csproj --context BoardGame.WarOfTheRing.PoliticalTrack.Infrastructure.Persistence.EntityFrameworkCore.PoliticalTrackDbContext --configuration Debug $(TARGET-MIGRATION)

ef-drop-db:
	dotnet ef database drop --project ./../BoardGame.WarOfTheRing.PoliticalTrack.Infrastructure/BoardGame.WarOfTheRing.PoliticalTrack.Infrastructure.csproj --startup-project BoardGame.WarOfTheRing.PoliticalTrack.Api.csproj --context BoardGame.WarOfTheRing.PoliticalTrack.Infrastructure.Persistence.EntityFrameworkCore.PoliticalTrackDbContext --configuration Debug --force