using System.ComponentModel.DataAnnotations;
using BasicBilling.Data.Abstracts;

namespace BasicBilling.Data.Entities
{
  public class Payment : AbstractDatabaseEntity
  {
    [Required]
    public Bill Bill { get; set; } = default!;
  }
}