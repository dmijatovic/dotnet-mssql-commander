using AutoMapper;
using commander.Dtos;
using commander.Models;

namespace commander.Profiles{
  // class inherits from Profile class from AutoMapper
  public class CommandsProfile:Profile{
    public CommandsProfile()
    {
      // map Command class to CommandReadDto
      // this is simple 1-to-1 mapping
      CreateMap<Command,CommandReadDto>();
    }

  }
}