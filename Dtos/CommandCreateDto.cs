using System.ComponentModel.DataAnnotations;

namespace commander.Dtos
{
  public class CommandCreateDto{
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