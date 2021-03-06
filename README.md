# base-webapi-accounts-network
A webapi with a basic framework who manage accounts and handler generic entities

## Credits  
I have used the repository https://github.com/evgomes/jwt-api for the token system.  

## The idea behind the project  
![Diagram](https://github.com/Magicianred/base-webapi-accounts-network/blob/develop/Project/Diagrams/idea.png)  
[drawio](https://github.com/Magicianred/base-webapi-accounts-network/blob/develop/Project/Diagrams/idea.drawio)  
[png](https://github.com/Magicianred/base-webapi-accounts-network/blob/develop/Project/Diagrams/idea.png)  

## Entities of the project  
![Diagram](https://github.com/Magicianred/base-webapi-accounts-network/blob/develop/Project/Diagrams/entities.png)  
[drawio](https://github.com/Magicianred/base-webapi-accounts-network/blob/develop/Project/Diagrams/entities.drawio)  
[png](https://github.com/Magicianred/base-webapi-accounts-network/blob/develop/Project/Diagrams/entities.png)  

## To Try Application
1. Use default settings (database inMemory and some data examples)  
```json
  "StorageType": "entityframework.mssql",
  "UseInMemoryStore": true,
  "UseDataSeed": true,
  "ConnectionName": "MyDatabase",
  "ConnectionStrings": {
    "MyDatabase": "YOUR_CONNECTION_STRING"
  }
```
2. Launch app from Visual Studio  
3. Use Swagger UI for testing application  
- /Auth/ValidationAuth - it checks if an valid user is connect  
- /Auth/SecureEndpoint - it is an endpoint protect by password  
- /api/login - it makes the authentication  
```json
{
  "email": "common@common.com",
  "password": "12345678"
}
```