using System.Collections.Generic;
using commander.Models;

// namespace reflects project name and the folder structure
namespace commander.Data
{
  public interface iCommander{
    // define interface for getting all commands
    IEnumerable<Command> GetAllCommands();
    // define iterface for getting specific command
    Command GetCommandById(int id);
    // define interface for adding new items
    bool SaveChanges();
    void CreateCommand(Command cmd);
    // define interface for updating existing items
    void UpdateCommand(Command cmd);
    // defined interface for deleting
    void DeleteCommand(Command cmd);
  }
}