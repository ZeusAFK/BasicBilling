using BasicBilling.Data.Abstracts;

namespace BasicBilling.Data.Models
{
  public class ServiceReadDto : AbstractEntityModel
  {
    public string Shortname { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
  }
}