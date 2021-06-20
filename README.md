# NET-CQRS-Microservices
useful stuff:
```sh
docker-compose up -d
docker-compose stop
docker-compose start
docker-compose down

dotnet ef database drop -p Services/Write
dotnet ef migrations add Initial -p Services/Write
dotnet ef database update -p Services/Write

dotnet run -p Services/Write
dotnet watch -p Services/Write run

dotnet run -p Services/Read
dotnet watch -p Services/Read run
```
