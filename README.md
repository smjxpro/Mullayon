### The Backend

##### To Create Database Migration
* In your project root: ```dotnet ef migrations add <YourMigrationName> -s ./Mullayon.Api -p ./Mullayon.Infrastructure```

##### To Apply Database Migration
* In your project root: ```dotnet ef database update -s ./Mullayon.Api -p ./Mullayon.Infrastructure```

##### To Run the API
* In your project root: ```dotnet watch run -p ./Mullayon.Api```

#### To Run via Docker

* Create the migration script: ```dotnet ef migrations script -s ./Mullayon.Api -p ./Mullayon.Infrastructure -o ./DbScripts/migration.sql```

* Copy the `sample.env` file then rename to `.env` and make necessary changes. 

* Then run the following commands:```docker compose -f docker-compose.yml --env-file .env up -d --build```
