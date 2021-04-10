using System.ComponentModel.DataAnnotations;

namespace BasicBilling.Data.Entities
{
  public class Client
  {
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(250)]
    public string Firstname { get; set; } = default!;

    [MaxLength(250)]
    public string? Middlename { get; set; }

    [Required]
    [MaxLength(250)]
    public string Lastname { get; set; } = default!;
  }
}