using System.ComponentModel.DataAnnotations;
// NOTE! This class is identical to CommandCreateDto
// it might be merged or reused
namespace commander.Dtos
{
  public class CommandUpdateDto{
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