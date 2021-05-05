using System.ComponentModel.DataAnnotations;

// namespace reflects project name and the folder structure
namespace commander.Models
{
  public class Command{
    // create primary key prop/field
    [Key]
    public int id { get; set; }
    // indicate NOT NULL and char length
    [Required]
    [MaxLength(255)]
    public string  howto { get; set; }
    [Required]
    [MaxLength(255)]
    public string command { get; set; }
    [Required]
    [MaxLength(255)]
    public string platform { get; set; }
  }
}