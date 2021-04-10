using System.ComponentModel.DataAnnotations;
using BasicBilling.Data.Abstracts;
using BasicBilling.Data.Enums;

namespace BasicBilling.Data.Entities
{
  public class Bill : AbstractDatabaseEntity
  {
    [Required]
    public int Period { get; set; }

    public Service Service { get; set; } = default!;

    public BillingStatus Status { get; set; } = default!;
  }
}