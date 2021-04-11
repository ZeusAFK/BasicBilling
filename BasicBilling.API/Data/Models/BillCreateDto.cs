using System.ComponentModel.DataAnnotations;

namespace BasicBilling.Data.Models
{
  public class BillCreateDto
  {
    [Required]
    public int ClientId { get; set; }

    [Required]
    public int period { get; set; }

    [Required]
    public decimal Amount { get; set; } = default!;

    [Required]
    public string category { get; set; } = default!;
  }
}