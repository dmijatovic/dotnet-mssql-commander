using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using commander.Data;
using commander.Models;
using AutoMapper;
using commander.Dtos;
using Microsoft.AspNetCore.JsonPatch;

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
    private readonly IMapper _mapper;

    // constructor getting data injected into this class
    // using iCommander interface, as it interface we are
    // sure that it implements required methods
    // then we inject IMapper to use AutoMapper and DTO
    public CommandsController(iCommander database, IMapper mapper)
    {
        _database = database;
        _mapper = mapper;
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
    public ActionResult <IEnumerable<CommandReadDto>> GetAllCommands(){
      var commands = _database.GetAllCommands();
      return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));
    }
    // GET api/v1/commands/{id}
    [HttpGet("{id}")]
    public ActionResult <CommandReadDto> GetCommandById(int id){
      var command = _database.GetCommandById(id);
      if (command != null){
        return Ok(_mapper.Map<CommandReadDto>(command));
      }else{
        return NotFound();
      }
    }
    // POST api/v1/commands
    [HttpPost]
    // returns CommandReadDto object
    public ActionResult <CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto){
      // map data received in Dto class into our model class
      // Note! this mapping need to be defined in Profile\CommandsProfile.cs
      var commandModel = _mapper.Map<Command>(commandCreateDto);
      // create command (sql insert statement?!?)
      _database.CreateCommand(commandModel);
      // commit this to DB
      _database.SaveChanges();
      // map model into read DTO
      var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);
      // return model back to user
      return Ok(commandReadDto);
    }
    // PUT api/v1/commands/1
    [HttpPut("{id}")]
    public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto){
      // check if items exists
      var itemFromDb=_database.GetCommandById(id);
      // if not found return that info
      if(itemFromDb==null){
        return NotFound();
      }
      // update data
      _mapper.Map(commandUpdateDto,itemFromDb);
      // call update (this fn is empty but there might)
      _database.UpdateCommand(itemFromDb);
      // commit changes toDB
      _database.SaveChanges();
      // return OK with no content
      return NoContent();
    }
    // PATCH api/v1/commands/{id}
    [HttpPatch("{id}")]
    public ActionResult PartialUpdate(int id,JsonPatchDocument<CommandUpdateDto> patchItem){
      // check if items exists
      var itemFromDb=_database.GetCommandById(id);
      // if not found return that info
      if(itemFromDb==null){
        return NotFound();
      }
      // for patch map from Command to CompandUpdateDto
      var commandToPatch = _mapper.Map<CommandUpdateDto>(itemFromDb);
      // apply this patch ?!?
      patchItem.ApplyTo(commandToPatch,ModelState);
      if (TryValidateModel(commandToPatch)==false){
        return ValidationProblem(ModelState);
      }
      // update
      _mapper.Map(commandToPatch,itemFromDb);
      // call update (this fn is empty but there might)
      _database.UpdateCommand(itemFromDb);
      // commit changes toDB
      _database.SaveChanges();
      // return OK with no content
      return NoContent();
    }
    // DELETE api/v1/commands/{id}
    [HttpDelete("{id}")]
    public ActionResult DeleteCommand(int id){
       // check if items exists
      var itemFromDb=_database.GetCommandById(id);
      // if not found return that info
      if(itemFromDb==null){
        return NotFound();
      }
      // for patch map from Command to CompandUpdateDto
      var itemDto = _mapper.Map<CommandReadDto>(itemFromDb);
      // call delete
      _database.DeleteCommand(itemFromDb);
      // commit changes toDB
      _database.SaveChanges();
      // return OK with no content
      return Ok(itemDto);
    }
  }
}