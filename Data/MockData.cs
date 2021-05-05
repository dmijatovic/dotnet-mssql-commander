using System.Collections.Generic;
using commander.Models;

// namespace reflects project name and the folder structure
namespace commander.Data{
  // define class that uses iCommander interface
  // use ctrl + . to automatically write basic placeholders
  public class MockData:iCommander
  {
    public bool SaveChanges()
    {
      throw new System.NotImplementedException();
    }
    public void CreateCommand(Command cmd)
    {
      throw new System.NotImplementedException();
    }

    public IEnumerable<Command> GetAllCommands()
    {
      // define static list of commands
      var commands = new List<Command>{
        new Command{id=1,howto="Delete file",command="rm <filename>",platform="linux:bash"},
        new Command{id=1,howto="Move file",command="mv <filename> <destination>",platform="linux:bash"},
        new Command{id=1,howto="Copy file",command="cp <filename> <destination>",platform="linux:bash"}
      };
      return commands;
    }

    public Command GetCommandById(int id)
    {
      // just return hardcoded value
      return new Command{
        id=1,howto="Delete file",command="rm <filename>",platform="linux:bash"
      };
    }

    public void UpdateCommand(Command cmd)
    {
      throw new System.NotImplementedException();
    }
  }
}