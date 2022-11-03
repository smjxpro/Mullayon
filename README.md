### The Backend

##### To Create Database Migration
In your project root: ```dotnet ef migrations add <YourMigrationName> -s ./Mullayon.Api -p ./Mullayon.Infrastructure```

##### To Apply Database Migration
In your project root: ```dotnet ef database update -s ./Mullayon.Api -p ./Mullayon.Infrastructure```

##### To Run the API
In your project root: ```dotnet watch run -p ./Mullayon.Api```
