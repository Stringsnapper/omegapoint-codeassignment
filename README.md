# Code Assignment - Omegapoint Senior Developer

This is a simple Player Management system with a user interface built as a React web app and an ASP.NET Core REST API with CRUD capability in the backend.

Data is stored persistently using an SQL Server Express database.

## Tech Stack
- **Backend:** C# - ASP.NET Core
- **Frontend:** TypeScript - Vite React app

## Description

### Frontend
The user interface consists of two main parts:
- A form to create or edit players.
- A list presenting existing players as cards.

#### Player Form
The player form lets users create new players by entering `Name` and `Description`. When the form is submitted the data will be posted to the backend and the Player List gets updated with a new card.
A new player always starts with 0 `XP`. The `Level` is never stored in the database. Instead it is calculated in the service layer of the backend application using the formula `Floor(0.4473 * Sqrt(xp))`. This gives a level progression with increasingly higher XP requirement for each level.

#### Player Card

Each player is represented as a card that contains the following properties:
- Name
- XP
- Level
- Description
- Button - Give XP
- Button - Delete

When pressing "Give XP" a PATCH request is sent to the backend with an updated XP value (+10). The players are then refetched so that the change to `XP` and possibly `Level` becomes visible.

By pressing a player card the card gets selected and the Player Form switches to edit mode. The input fields display the `Name` and `Description` value of the selected Player Card, the Create Player button gets disabled and the Update Player button gets enabled. Pressing "Update Player" sends a PUT request to the backend with a DTO containing the new Name and Description.

By pressing the New Player button the form resets and switches back to the "Create Player" mode.

### Backend

The backend application consists of 3 layers:
- Controller
- Service
- Repository

#### Controller
The controller provides the following endpoints:
- GET players - `api/players`
- GET player - `api/players/<id>`
- POST player - `api/players`
  - Body:
    ``` 
    - name
    - description
    ```
- PUT player - `api/players/<id>`
  - Body:
    ```
    id 
    name
    description
    xp
    ```
- PATCH player - `api/players/<id>/xp`
  - Body:
    ```
    xp
    ```
- DELETE player - `api/players/<id>`

#### Service
The purpose of the Service layer is to transform DTOs to and from database Models. Business logic such as the Level Calculation is handled here rather than the controller layer which keeps the controller layer thin and focused on request and response handling.

#### Repository
The purpose of the Db Context is to read and write to and from the database. Seperating this component from the service layer simplifies testing by allowing us to completely mock the database responses. 

## Security considerations 

### OAuth2 authorization and RBAC - OUT OF SCOPE
This feature would enable fine grained control over who gets access to what feature. Write actions could be limited to signed in users, which would make it less accessible for potential overload or XSS attacks. The requests could also become more traceable which could help in identifying a harmful actor.

### Sanitize form to prevent XSS attacks
This is done using [DOMPurify](https://www.npmjs.com/package/dompurify). Before submitting the form the DOMPurify.sanitize is called on the input values to remove any dirty/harmful HTML.

### Rate limiting to prevent spamming
The backend has a Rate Limiter to prevent too many requests in a short amount of time from the same source IP. The current implementation only sets a global rate limiter that applies to all endpoints. In a future version this could be refined with different rates on different endpoint depending on need and traffic. If OAuth2 is implemented in the future, the User ID or User name can be used instead of the IP address to limit requests from the same user on multiple clients.

### Form validation - OUT OF SCOPE
By defining rules on input format and size we could prevent users from posting too large data values or data that is not in line with the intended use of the platform.

### DTO Validation - OUT OF SCOPE
Validation in the backend will make the application even stricter and only allow data that is of a reasonalbe size and value.

### Validating request origin with API key - OUT OF SCOPE
In the REST API, an API key could be used to validate the request origin. By including the API Key in the request from the frontend, the backend can validate it and verify that the request comes from a trusted source.

### Configuration for connection strings
Connection strings should not be stored directly in the code. For future implementations they should be moved to the configuration files. Currently only localhost is used, which does not present any significant risk.

## Testing
A parameterized Unit Test (XUnit) for the Level Calculator has been implemented. In the future, integration tests for the controller endpoints as well as frontend tests (using JEST or similar) should be added.

