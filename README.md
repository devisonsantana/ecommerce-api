# Ecommerce

Aplicação ASP.NET Core 10 para gerenciar categorias, produtos, marcas e imagens com Entity Framework Core e PostgreSQL.

## Estrutura principal

- `Src/` – projeto ASP.NET Core Web API
- `Src/Web/Program.cs` – ponto de entrada e configuração de serviços
- `Src/Infrastructure/Extensions/DependecyInjection.cs` – configuração de `DbContext` e repositórios
- `Src/Application/Extensions/DependencyInjection.cs` – registro de serviços de domínio
- `Src/Infrastructure/Persistence/Context/AppDbContext.cs` – definição dos `DbSet`
- `Src/Domain/Models/` – entidades do domínio
- `Tests/` – testes unitários do projeto

## Tecnologias

- .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- Npgsql / PostgreSQL
- Swagger / OpenAPI
- FluentResults

## Requisitos

- .NET 10 SDK
- Docker (opcional, usado para PostgreSQL no `docker-compose`)

## Configuração

1. Navegue para a pasta do projeto:

```bash
cd Src
```

2. Crie ou configure a string de conexão PostgreSQL em um arquivo `.env` ou variável de ambiente:

```env
ConnectionStrings__DefaultConnection="Host=db_psql;Port=5432;Username=dotnet;Password=w1AjdyEZrVy2Pg2fNPvE;Database=ecommerce"
```

3. Se quiser usar Docker Compose, execute no diretório `Src`:

```bash
docker compose up -d
```

> O `docker-compose.yaml` expõe o banco PostgreSQL em `db_psql:5432` e a API em `http://localhost:80`.

## Executar a aplicação

Ainda em `Src/`:

```bash
dotnet run
```

A API deve subir no ambiente de desenvolvimento. O Swagger fica disponível em:

```text
http://localhost:<porta>/swagger
```

No código atual, o Swagger está habilitado apenas em `Development`.

## Testes

Execute os testes a partir da raiz do repositório:

```bash
dotnet test
```

## Endpoints principais

A API já possui controller de categorias (`CategoriesController`). Exemplos de rotas:

- `GET /Categories`
- `GET /Categories/{id}`
- `GET /Categories/{name}`
- `POST /Categories`
- `POST /Categories/bulk`
- `PUT /Categories/{id}`
- `DELETE /Categories/{id}`

## Próximos passos sugeridos

- criar entidades de produto, marca e imagem com relacionamentos para o banco
- configurar `DbContext` para mapeamento explícito (se necessário)
- adicionar migrações e aplicar ao PostgreSQL
- expandir endpoints de produtos e marcas

## Observações

- A aplicação já está usando `AddOpenApi()` e `AddSwaggerGen()` para documentação.
- A conexão com PostgreSQL depende do nome da string `DefaultConnection`.
