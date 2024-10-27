# Browsely Architecture Overview

Respecting the fact that as little code as possible was needed, the goal was to demonstrate the architecture of a scalable, modular microservice solution using best practices, though it may be overengineered for small tasks. Services are modular, allowing independent scaling and deployment for more complex scenarios as the system grows.

## Quick Start


## Overview
Utilizes CQRS and Domain-Driven Design (DDD) principles. While kept in one solution for simplicity, real setups would have services in separate repositories for independent development, deployment, and scaling.

## Components
### Dispatcher Service:
Manages URL scheduling, reviews, and updates. Divided into API, Application, Domain, and Infrastructure layers, using CQRS to separate queries and commands.
### Node Service:
Handles URL content retrieval via Dockerized services (e.g., Browserless). Manages URL review events with event-driven processing using messaging.

### Payload Service:
- Fetching content using the browserless/chrome image, which is bootstrapped as needed. The Node Service orchestrates this process, running only when an event is received.


## Technologies
- **EF Core** for ORM following the Database per Microservice pattern for database interactions using MSSQL.
- **RabbitMQ** for asynchronous, event-driven communication between services for scalability.
- **Common Packages** which contains common utilities like logging and exception handling for consistency.


## Technologies Overview

### Entity Framework Core (EF Core)
Manages database interactions via an object-oriented API. Simplifies data access and validation, reducing boilerplate code.

### RabbitMQ - Message Broker
Manages asynchronous communication, triggering events (e.g., URL reviews) that other modules respond to, promoting loose coupling.

### Seq (or Similar) - Logging and Monitoring
Collects and analyzes logs across components for system visibility, error detection, and resolution. In real case most likly we will need OpenTelemetry.

### Testing
We have implemented unit testing, but we deferred integration testing due to its complexity. Given our dependencies such as RabbitMQ, MSSQL, and Seq, we would need to use Testcontainers for containerizing these services and the ASP.NET Core Test Host to mock Kestrel effectively. This setup will allows for a realistic simulation of the application environment during integration tests.
