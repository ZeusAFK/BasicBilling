using BasicBilling.Data.Abstracts;

namespace BasicBilling.Data.Models
{
  public class PaymentReadDto : AbstractEntityModel
  {
    public BillReadDto Bill { get; set; } = default!;
  }
}