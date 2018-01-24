## Database Migration

Cause the `DbContext` files is in the SamuraiAppCore.Data project, and the Configuration is in the SamuraiWebApi project, so should specify the --startup-project tag in the command, aka `dotnet ef --startup-project "..\SamuraiWebApi" migrations add <name>` to generate the migration file, and `dotnet ef --startup-project "..\SamuraiWebApi" database update` to update the database schema.
