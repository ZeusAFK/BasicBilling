using BasicBilling.Data.Abstracts;
using BasicBilling.Data.Enums;

namespace BasicBilling.Data.Models
{
  public class BillReadDto : AbstractEntityModel
  {
    public int Period { get; set; }

    public ClientReadDto Client { get; set; } = default!;

    public ServiceReadDto Service { get; set; } = default!;

    public BillingStatus Status { get; set; } = default!;
  }
}