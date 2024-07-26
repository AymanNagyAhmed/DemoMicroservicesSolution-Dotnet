# Dotnet 8 webapi Demo MicroService Solution


## This project for educational purposes:
1. DOTNET 8 project API Gateway - Ocelot API Microservice
2. DOTNET 8 project Authentication MicroService
3. Jwt AuthenticationManager (for generating custom JWt token)
4. DOTNET 8 WebAPI Customer Microservice with (SQL Server, Entity Framework Core)
5. DOTNET 8 WebAPI Product Microservice with (MySQL, Entity Framework Core)
6. DOTNET 8 WebAPI Order Microservice with (MongoDB, Entity Framework Core)

This solution is a collection of three microservices with different database (SQL Server, MySQL & MongoDB).
I used [Ocelot API Gateway](https://github.com/ThreeMammals/Ocelot) which communicates with all microservices for doing the Authentication, Authorization and CRUD operations.


## Installation

```
// to start the project run:
docker compose up --detach

// to stop the project run:
docker compose down

```
## Documentations
1. Customer Microservice API Docs
    - http://localhost:8002/swagger/index.html

2. Product Microservice API Docs
    - http://localhost:8003/swagger/index.html

3. Order Microservice API Docs
    - http://localhost:8004/swagger/index.html

## Usage
1. Import postman collection from here [Postman Collection](./postman_collection/DemoMicroservicesSolution.postman_collection.json) 
2. Api Gateway Link will be (http://localhost:8000)

### Security

If you discover any security related issues, please email aymannagy.ahmed@gmail.com instead of using the issue tracker.

## Credits

- [Ayman Nagy Ahmed](https://github.com/AymanNagyAhmed)

## License

The MIT License (MIT). Please see [License File](LICENSE.md) for more information.