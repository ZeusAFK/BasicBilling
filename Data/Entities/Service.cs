using System.ComponentModel.DataAnnotations;

namespace BasicBilling.Data.Entities
{
  public class Service
  {
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public string Shortname { get; set; } = default!;

    [Required]
    public string Name { get; set; } = default!;

    public string? Description { get; set; }
  }
}