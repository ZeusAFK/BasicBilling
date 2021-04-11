using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BasicBilling.Data.Abstracts;
using BasicBilling.Data.Enums;

namespace BasicBilling.Data.Entities
{
  public class Bill : AbstractDatabaseEntity
  {
    [Required]
    public int Period { get; set; }

    [Required]
    public Client Client { get; set; } = default!;

    [Required]
    public Service Service { get; set; } = default!;

    [Column(TypeName = "money")]
    public decimal Amount { get; set; }

    [Required]
    public BillingStatus Status { get; set; } = default!;
  }
}