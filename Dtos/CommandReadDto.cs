
namespace commander.Dtos
{
  public class CommandReadDto{
    // create primary key prop/field
    public int id { get; set; }
    // indicate NOT NULL and char length
    public string  howto { get; set; }
    public string command { get; set; }
    public string platform { get; set; }
  }
}