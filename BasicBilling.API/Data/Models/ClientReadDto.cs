using BasicBilling.Data.Abstracts;

namespace BasicBilling.Data.Models
{
  public class ClientReadDto : AbstractEntityModel
  {
    public string Firstname { get; set; } = default!;
    public string? Middlename { get; set; }
    public string Lastname { get; set; } = default!;

    public string Document { get; set; } = default!;
  }
}