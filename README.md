# Internship-4-OOP2 - Users & Companies Managment API

Technologies:
- ASP:NET Core Web API
- Entity Framework Core
- Dapper
- PostgreSQL
- Clean Arhitecture

Functionalities:
- CRUD operations for users
- CRUD operations for companies
- Automatized import of users from external API
- Cache that blocks import within the same day
- Validation for email, username, URL + domain rules
- GUID for password when generating users

Arhitecture:
- Domain -> entities, value objects, validation rules
- Application -> services, logic and DTOs
- Infrastructure -> base contexts, repository pattern, Dapper + EF
- API -> Swagger endpoints, controllers

Database:
- clean-1 for users and clean-2 for companies
- import writes company just once and skips duplicates
- SQL for creating tables:
  
  CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    username VARCHAR(100) NOT NULL UNIQUE,
    email VARCHAR(255) NOT NULL UNIQUE,
    address_street VARCHAR(150),
    address_city VARCHAR(100),
    geo_lat DECIMAL(10,6),
    geo_lng DECIMAL(10,6),
    website VARCHAR(100),
    password VARCHAR(100) NOT NULL,
    created_at TIMESTAMP DEFAULT NOW(),
    updated_at TIMESTAMP DEFAULT NOW(),
    is_active BOOLEAN DEFAULT TRUE
);

CREATE TABLE companies (
    id SERIAL PRIMARY KEY,
    name VARCHAR(150) NOT NULL UNIQUE
);

CREATE TABLE externalimportinfo (
    id SERIAL PRIMARY KEY,
    lastimportedat TIMESTAMP NOT NULL
);

External API:
- https://jsonplaceholder.typicode.com/users





    
