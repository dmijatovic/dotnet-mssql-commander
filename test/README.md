# Load test with autocannon

This is load test for dotnet commander application. The application is dotnet C# api connected to MSSQL 2019. We test performance of this combination here.

## Dependencies

```bash
# save dependencies
npm i -D autocannon lowdb
```

After cloning the project run `npm install` to install dependencies.

## Run load test

First you need to start application using docker-compose. Then you can run `npm run loadtest`

The report will be shown in the console and saved in `report\db.json`
