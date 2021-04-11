using System.ComponentModel.DataAnnotations;

namespace BasicBilling.Data.Models
{
  public class ClientUpdateDto
  {
    [Required]
    [MaxLength(250)]
    public string Firstname { get; set; } = default!;
    
    [MaxLength(250)]
    public string? Middlename { get; set; }
    
    [Required]
    [MaxLength(250)]
    public string Lastname { get; set; } = default!;

    [Required]
    [MaxLength(50)]
    public string Document { get; set; } = default!;
  }
}