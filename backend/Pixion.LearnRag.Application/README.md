## Application Project

The ``Application Project`` contains the core business logic, organized following the CQRS (Command Query Responsibility
Segregation) pattern. Each strategy manages its respective endpoints independently.

To simplify the application design, most Queries and Commands also function as DTOs for the `API Project`. Validation
for
all Queries and Commands is handled using **FluentValidation**, integrated via **MediatR** middleware.

