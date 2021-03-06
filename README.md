# Commander dotnet C# api demo

This repo is based on [youtube video](https://youtube.com/watch?v=fmvcAzHpsk8) teaching how to build web api with dotnet core.

## Basics dotnet webapi

- Program.cs: file that start app it uses Startup.cs to bootstrap the app
- Startup.cs: bootstrapping file having all 'ingredients' of app defined

### MVC pattern

This is basic structure used in C# apps. For api strcuture the focus is on controllers and models.

- Model: defines properties, usually tables/structures in the database
- Controller: defines the routes and links to data models. It acts as router (route handler)
- View: the output the users interacts with, in case of webapi this the output produces by controller on endpoints

### Interfaces

This is another important paradigm in C# land. These definitions are stored in Data folder (arbitrary). It provides the 'signitures' of operations available in the app. The example implementation of interface is in MockCommander.cs file in the same directory.

### Dependency injection

This concept links interface to actual implementation usign service configuration. It is defined in Startup.cs file, section ConfigureServices. Using this approach there is one place where actual implementation is linked to interface and it can be easily 'swapped' in the future with another implementation.

```C#
// implement mocked data into iCommander interface
iComander -> MockData
```

#### Service Lifetime

When registering services (dependency injection) there are

- AddSingleton: same for every request (one time class initialization)
- AddScoped: once per client request
- Transient: new instance every time

### Microsoft.EntityFrameworkCore

This package is used for connecting to database. In this project we use MSSQL on standard port (via docker)

```bash
# install framework
dotnet add package Microsoft.EntityFrameworkCore -v 3.1.10
# install design
dotnet add package Microsoft.EntityFrameworkCore.Design -v 3.1.10
# install MSSQL server package (or other SQLDB see below)
dotnet add package Microsoft.EntityFrameworkCore.SqlServer -v 3.1.10
# check dependencies are added to csproj file
```

- [PostgreSQL package](https://www.npgsql.org/efcore/#additional-configuration-for-aspnet-core-applications)
- [mySQL package](https://dev.mysql.com/doc/connector-net/en/connector-net-entityframework-core-example.html)

- Dotnet EF tools: for data migrations and setup. These will scaffold DB for you

```bash
dotnet tool install --global dotnet-ef
```

Steps to connect database and models

- Create DB context (CommanderContext.cs): this class is defined in the folder Data. It extends basic DbContext class. It defines DbSet\<Command> with out Command class from Models.
- Define connection in appsettings.json (for develpment there is separate file). Each SQL server has different connection string.

```json
"ConnectionStrings":
```

- Configure connection in the services in Startup.cs file using services.AddDbContext

### DTO (Data Transfer Objects)

Next paradigm in dotnet is DTO. Instead of exposing Models directly to api we use DTO classes to transform model objects into client facing objects. This means that we can rename or remove properties (table fields) from our modal. Not sure if you can make additional calculations but I assume this will be the place for it :-).

Additional packages need to installed

```bash
# install AutoMapper.Extensions.Microsoft.DependencyInjection
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
```

Steps:

- In the Startup.cs class use services.AddAutoMapper() to add it to the project.
- create Dtos folder and add classes for transformation CommandReadDto.cs as example
- create Profiles folder and map Command class to CommandReadDto (see Profile/CommandsProfile.cs)
- update Controllers, see Controllers/CommandController.cs by injecting IMapper into class and returning Dto class instead of model class.

## Extending base setup

In the intital setup only reading (GET) structure is created. Next step is extending this setup with write. There could be diffrent ways to 'skin the cat'. In this training the order followed is:

- Add new method in Data\iComander.cs interface (define new method in the interface). For saving implement SaveChanges() method.
- Implement interface in Data\MsSqlCommander.cs. Use `ctrl + .` for basic scaffold. Two methods are implemented: SaveChanges and CreateCommand.
- Add mapping (if needed) to Profile\CommandsProfile.
- Create Dtos\CommandCreateDto to define required data layout for creating new items.
- Define CreateCommand method that will tie all this together in the Controllers\CommandsController.cs. Here we expose POST method, map received data into model, call database to create new record and return created data back. In addition, we

## PATCH method

Enables you to make partially updates of the object. It has specific format and it requires additional packages to install

```json
[
  {
    "op": "replace",
    "path": "/howto",
    "value": "New value to use"
  },
  {
    "op": "replace",
    "path": "/platform",
    "value": "New platform to use"
  }
]
```

```bash
# install dependecies
dotnet add package Microsoft.AspNetCore.JsonPatch -v 3.1.10
# it needs specific version for dotnet core
dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson -v 3.1.10
```

Then add these services in the Startup.cs class.

`NOTE! By examining csproj file it seems that all installed packages should have manadatory version to v 3.1.* when installing. Otherwise all default to v5 of .NET package.` Maybe is this issue on Linux machines only?

## Tricks

- `prop`: when defining properties of a class in the Models, there is shortcut key that will write longer definition required by C#.
- `ctor`: to scaffold contructor syntax
- `Ctrl + .`: when missing dependecies in the class suggestion can be shown using shortcut `ctrl + .`
- File names: changing the name of class file does not have an impact to application as long as defined class name in the file remains the same. After chaning name of the class in the file there will be errors to point inconsistance.

## Publish / Build application

For building application, as dotnet core, is crossplatform we can `publish` to different OS targets.

For more info about [publish flags see docs](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-publish)

```bash
# target Alpine
dotnet publish ./commander.csproj \
  --runtime linux-musl-x64 \
  -c RELEASE \
  -o out /p:Version=1.0.0 \
  -p:PublishTrimmed=true

# target Debian?!?
dotnet publish ./commander.csproj \
  --runtime linux-x64 \
  -c RELEASE \
  -o out /p:Version=1.0.0 \
  -p:PublishTrimmed=true

```

To build docker

```bash
# build docker image default file
docker build -t commander:0.0.2 .
# build alpine version
docker build -t commander:0.0.3 ./Docker_apline
# run image
docker run -p 5000:5000 commander:1.0.0
```

### Docker compose

I have tried few 'flavours' of docker images with this application. It seem to me that debia is the best choice at the moment of writing. First there are 2 versions of dotnet currently v5 (last version) and v3.1 (LTS version). I used 3.1 LTS version for all development and dockerizing.

- aspnet: mcr.microsoft.com/dotnet/core/aspnet:3.1,this works out-of-the-box and has image size of 218MB
- runtime-deps: mcr.microsoft.com/dotnet/runtime-deps:3.1, works with compiled self-contained approach (Dockefile_debian) and has image size of 215MB.
- alpine: dotnet/runtime-deps:3.1-alpine, does not work. It compiles but when using it errors out when accessing endpoint to return a list of object (Dockerfile_alpine). The image size was about 120MB. It clearly missing some dependencies.

```bash
# run containers in detached mode
docker-compose up -d
# stop
docker-compose stop
# clear
docker-compose down
# clear volumes too
docker-compose down --volumes
```

## Load tests / performance

I was in particulair interested in the performance of dotnet and mssql. Initial test are not impressive, comparing it with performance tests I did with some other tools (node, python, rust, go). This is indeed completely different stack dotnet and MSSQL, but to me this is default MS stack used by lot of large companies.

The performance of load test with 10 connection for 30 seconds was 25k (first test) and then 27k (second test). Same SQL server with node api achieves around 35k requests.

**NOTE! dotnet api is programed for synchornious processing. I have not learned to use async approach. This could be next step for improvement**
