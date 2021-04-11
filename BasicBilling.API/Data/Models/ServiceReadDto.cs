using System.ComponentModel.DataAnnotations;
using BasicBilling.Data.Abstracts;

namespace BasicBilling.Data.Models
{
  public class ServiceReadDto : AbstractEntityModel
  {
    [Required]
    public string Shortname { get; set; } = default!;

    [Required]
    public string Name { get; set; } = default!;

    public string? Description { get; set; }
  }
}