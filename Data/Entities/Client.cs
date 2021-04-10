using System.ComponentModel.DataAnnotations;
using BasicBilling.Data.Abstracts;

namespace BasicBilling.Data.Entities
{
  public class Client : AbstractDatabaseEntity
  {
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