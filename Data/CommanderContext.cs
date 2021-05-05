using commander.Models;
using Microsoft.EntityFrameworkCore;

namespace commander.Data{
  // MSSQL EF database context
  public class CommanderContext:DbContext{
    public CommanderContext(DbContextOptions<CommanderContext> opt):base(opt)
    {

    }
    // define link between class Command and table Commands
    // NOTE! second name is the name of the table!
    public DbSet<Command> Commands { get; set; }
  }

}