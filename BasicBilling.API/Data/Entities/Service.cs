using System.ComponentModel.DataAnnotations;
using BasicBilling.Data.Abstracts;

namespace BasicBilling.Data.Entities
{
  public class Service : AbstractDatabaseEntity
  {
    [Required]
    public string Shortname { get; set; } = default!;

    [Required]
    public string Name { get; set; } = default!;

    public string? Description { get; set; }
  }
}