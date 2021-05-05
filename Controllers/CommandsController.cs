using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using commander.Data;
using commander.Models;

// namespace reflects project name and the folder structure
namespace commander.Controllers{
  // [controller] can be used to inherit name of the class without controller
  // but that means when changin name of the class the route is also changed
  [Route("api/v1/commands")]
  [ApiController]
  public class CommandsController : ControllerBase{
    // define use of mock data directly here (not adviced)
    private readonly MockData _mockDB = new MockData();
    // define private class variable to get injected data
    private readonly iCommander _database;
    // constructor getting data injected into this class
    // using iCommander interface, as it interface we are
    // sure that it implements required methods
    public CommandsController(iCommander database)
    {
        _database = database;
    }

    // GET api/v1/commands/{id}
    [HttpGet]
    // method definition has
    // public/private scope
    // type ActionResult
    // IEnumerable indicates array
    // between <> is object type returned
    // THEN comes the method name GetAllCommands
    // ActionResult method needs to return something
    public ActionResult <IEnumerable<Command>> GetAllCommands(){
      var commands = _database.GetAllCommands();
      return Ok(commands);
    }
    // GET api/v1/commands/{id}
    [HttpGet("{id}")]
    public ActionResult <Command> GetCommandById(int id){
      var command = _database.GetCommandById(id);
      return Ok(command);
    }
  }
}