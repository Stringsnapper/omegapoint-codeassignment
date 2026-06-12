# Code Assignment - Omegapoint Senior Developer

This is a web application with CRUD capability and a client to present the content.
## Tech Stack
- **Backend:** ASP .NET Core
- **Frontend:** Vite React app with TypeScript

## Description

## Security considerations 
- OAuth2 authorization for all write actions
  - Setup a Keycloak server
- Sanitize form to prevent XSS attacks
  - could possibly use DOMPurify
- Prevent form spamming
  - Rate limit in both frontend and backend
  - Form validation
- In REST API, validate request dto before transforming and storing.
- In REST API, use API key to validate the request origin 