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
## Adding MicroRabbit
```sh
dotnet add package MassTransit.AspNetCore &&
dotnet add package MassTransit.RabbitMQ
```
Adding endpoint
in `Services/Write/Services/ArticleRepository.cs`
Adding connection string
```json
"EventBusSettings": {
    "HostAddress": "amqp://guest:guest@localhost:5672"
}
```
Disable dotnet watch run from opening browser with swagger
[https://stackoverflow.com/questions/43957547/how-to-disable-browser-launch-when-building-and-running-in-net-core/43958782#43958782](https://stackoverflow.com/questions/43957547/how-to-disable-browser-launch-when-building-and-running-in-net-core/43958782#43958782)