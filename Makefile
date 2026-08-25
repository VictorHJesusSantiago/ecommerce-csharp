## Build
dotnet restore
dotnet build --no-restore

## Run API
dotnet run --project src/Ecommerce.Api

## Run Web
dotnet run --project src/Ecommerce.Web

## Run Tests
dotnet test

## Docker
docker-compose -f docker/docker-compose.yml up --build

## Database Migration
dotnet ef migrations add InitialCreate --project src/Ecommerce.Infrastructure --startup-project src/Ecommerce.Api
dotnet ef database update --project src/Ecommerce.Infrastructure --startup-project src/Ecommerce.Api

## Publish
dotnet publish src/Ecommerce.Api -c Release -o ./publish/api
dotnet publish src/Ecommerce.Web -c Release -o ./publish/web
