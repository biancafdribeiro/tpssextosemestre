# Controle de Patrimônio (ASP.NET Core + EF Core + SQLite)

## Estrutura
- `Patrimonio.Core` : biblioteca (DLL) com regras de negócio (tombamento e depreciação).
- `Patrimonio.Api`  : Web API + página web (`wwwroot/index.html`) + banco SQLite.
- `Patrimonio.Tests`: testes unitários (DLL) e de integração (API).

## Pré-requisitos
- .NET 8 SDK (https://dotnet.microsoft.com/download)

## Como executar
```bash
cd patrimonio
dotnet build
dotnet run --project Patrimonio.Api
```
Abra no navegador: http://localhost:5000 (ou a porta exibida no terminal).
Documentação Swagger: http://localhost:5000/swagger

O arquivo `patrimonio.db` é criado automaticamente na primeira execução.

## Como testar
```bash
dotnet test
```

## Endpoints
| Método | Rota                | Descrição                          |
|--------|---------------------|------------------------------------|
| GET    | /api/bens           | Lista (filtros: ?categoria=&status=) |
| GET    | /api/bens/{id}      | Obtém um bem                       |
| POST   | /api/bens           | Cadastra (gera o tombamento)       |
| PUT    | /api/bens/{id}      | Atualiza                           |
| DELETE | /api/bens/{id}      | Remove                             |
