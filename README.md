## Instructions

1.  Install and run the applications
2.  Open UI application and login using following credentials
    - Username: user@mail.com
    - Password: 123456
3.  Visit products page and click the add button to add products into your cart
4.  Click cart icon at the top-right of the page to go to cart page
5.  Click Create Order button to create new order from selected products
6.  Created orders if not completed (submitted), they can be submit later using `Complete Order` button at the orders page

## Architecture

1.  DDD (Domain Driven Design) used and following layers has been created
    * Shopping.API
    * Shopping.Application
    * Shopping.Domain
    * Shopping.Domain.Shared
    * Shopping.EntityFrameworkCore
      * Application Layers: Shopping.API, Shopping.Application
      * Domain Layers: Shopping.Domain, Shopping.Domain.Shared
      * Infrastructure Layer: Shopping.EntityFrameworkCore
2.  PostgreSQL relational database used.
3.  Entity Framework ORM used for abstracting database operations
4.  Automapper used to map entities into resources

## Manual Installation

1.  Clone repository via git cli `git clone git@github.com:hsntngr/shopping-api.git`
2.  Install nuget packages via `dotnet restore`
3.  Run the application via `dotnet run -p Shopping.API`
4.  When the application ready visit the [https://localhost:7187](https://localhost:7187) for swagger documentation

## Docker

1.  Create an image via `docker build -t [authority]/shopping-api`
2.  Create a container from image via `` `docker run -d --restart always --name shopping-api-instance -p [PORT]:80 [authority]/shopping-api` ``
3.  When the application ready visit the [http://localhost:\[PORT\]](http://localhost:[PORT]) for the swagger the document

## Testing (Unit Tests)

1.  `Nunit` used as a testing framework
2.  `NBuilder` used to create mock data
3.  Number of Tests: `21`
4.  Test Coverage: `100%`
5.  Written tests does not coverage all application, only repositories and order service