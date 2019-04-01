* Title:	Getting Started on .NET Core - New database - EF Core | Microsoft Docs
  * Url:	https://docs.microsoft.com/en-us/ef/core/get-started/netcore/new-db-sqlite

Run dotnet ef migrations add InitialCreate to scaffold a migration and create the initial set of tables for the model.
Run dotnet ef database update to apply the new migration to the database. This command creates the database before applying migrations.

```
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run
```