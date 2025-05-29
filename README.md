# User API

API de gerenciamento de usuários construída com .NET 9, Clean Architecture, CQRS, PostgreSQL e Swagger.

## Pré-requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [PostgreSQL](https://www.postgresql.org/download/)
- [Git](https://git-scm.com/downloads)

## Configuração do Banco de Dados

1. Instale o PostgreSQL em sua máquina
2. Crie um banco de dados chamado `userdb`:
   ```sql
   CREATE DATABASE userdb;
   ```
3. Crie um usuário para a aplicação:
   ```sql
   CREATE USER api_user WITH PASSWORD 'ApiUserPass';
   ```
4. Conceda as permissões necessárias:
   ```sql
   GRANT ALL PRIVILEGES ON DATABASE userdb TO api_user;
   ```

## Configuração do Projeto

1. Clone o repositório:
   ```bash
   git clone [URL_DO_REPOSITÓRIO]
   cd UserAPI
   ```

2. Atualize a string de conexão no arquivo `UserAPI.Api/appsettings.json` se necessário:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Port=5432;Database=userdb;Username=api_user;Password=ApiUserPass;Pooling=true;Trust Server Certificate=true"
     }
   }
   ```

3. Restaure os pacotes:
   ```bash
   dotnet restore
   ```

4. Execute as migrations:
   ```bash
   dotnet ef migrations add InitialCreate --project UserAPI.Infrastructure --startup-project UserAPI.Api
   dotnet ef database update --project UserAPI.Infrastructure --startup-project UserAPI.Api
   ```

## Executando a Aplicação

1. Inicie a API:
   ```bash
   dotnet run --project UserAPI.Api
   ```

2. Acesse o Swagger UI:
   - http://localhost:5000

## Endpoints Disponíveis

- GET /api/users - Lista todos os usuários
- POST /api/users - Cria um novo usuário
- GET /api/users/{id} - Obtém um usuário específico
- PUT /api/users/{id} - Atualiza um usuário
- DELETE /api/users/{id} - Remove um usuário

## Estrutura do Projeto

- **UserAPI.Core**: Entidades de domínio
- **UserAPI.Application**: CQRS, Handlers, Validators
- **UserAPI.Infrastructure**: DbContext, Migrations
- **UserAPI.Api**: Controllers, Program.cs, configurações

## Tecnologias Utilizadas

- .NET 9
- CQRS com MediatR
- Entity Framework Core
- PostgreSQL
- Swagger/OpenAPI
- FluentValidation 