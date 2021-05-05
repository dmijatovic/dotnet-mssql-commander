using System;
using System.Collections.Generic;
// used for command ToList()
using System.Linq;
using commander.Models;

namespace commander.Data{
  public class MsSqlCommander : iCommander
  {
    // generated read-only field using ctrl + .
    private readonly CommanderContext _context;

    // inject CommanderContext into this class
    public MsSqlCommander(CommanderContext context)
    {
      // copy injected info to local property
      _context = context;
    }

    public IEnumerable<Command> GetAllCommands()
    {
      return _context.Commands.ToList();
    }

    public Command GetCommandById(int id)
    {
      return _context.Commands.FirstOrDefault(p=>p.id==id);
    }
    // code to support adding new commands
    public bool SaveChanges()
    {
      // this methods need to be called
      // to actually save data to databse
      // probably because possible rollbacks
      return (_context.SaveChanges() >= 0);
    }
    public void CreateCommand(Command cmd)
    {
      if (cmd == null){
        throw new ArgumentNullException(nameof(cmd));
      }
      _context.Commands.Add(cmd);
    }
  }
}