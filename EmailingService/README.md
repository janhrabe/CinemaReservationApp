# RabbitMQ Consumer Service

## Introduction
The RabbitMQ Consumer Service is responsible for listening to reservation events in the cinema reservation system. It processes messages from RabbitMQ and print messages in console.

## Getting Started
### Installation
1. Clone the repository.
2. Ensure RabbitMQ is running (see Docker section below).
3. Configure the RabbitMQ connection settings in the application.
4. Start the service using:
   ```dotnet run --project RabbitMQConsumer```

### Dependencies
- .NET 8
- RabbitMQ
- Clean Architecture

## Docker
### Running RabbitMQ Container
If you don’t have a running RabbitMQ instance in Docker, you can start one with:
```sh
docker run -d \
  --name rabbitmq_container \
  -e RABBITMQ_DEFAULT_USER=admin \
  -e RABBITMQ_DEFAULT_PASS=admin \
  -p 5672:5672 \
  -p 15672:15672 \
  -v rabbitmq_data:/var/lib/rabbitmq \
  --restart always \
  rabbitmq:3-management
```

## Running the Consumer Service
Ensure RabbitMQ is running, then start the consumer with:
```dotnet run --project RabbitMQConsumer```



