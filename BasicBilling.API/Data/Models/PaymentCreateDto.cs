using System.ComponentModel.DataAnnotations;

namespace BasicBilling.Data.Models
{
  public class PaymentCreateDto
  {
    [Required]
    public int ClientId { get; set; }

    [Required]
    public int period { get; set; }

    [Required]
    public string category { get; set; } = default!;
  }
}