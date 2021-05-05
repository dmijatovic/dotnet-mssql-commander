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

### Service Lifetime

When registering services (dependency injection) there are

- AddSingleton: same for every request (one time class initialization)
- AddScoped: once per client request
- Transient: new instance every time

### Tricks

- `prop`: when defining properties of a class in the Models, there is shortcut key that will write longer definition required by C#.
- `ctor`: to scaffold contructor syntax
- `Ctrl + .`: when missing dependecies in the class suggestion can be shown using shortcut `ctrl + .`
- File names: changing the name of class file does not have an impact to application as long as defined class name in the file remains the same. After chaning name of the class in the file there will be errors to point inconsistance.
