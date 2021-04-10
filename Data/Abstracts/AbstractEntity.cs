using BasicBilling.Data.Interfaces;

namespace BasicBilling.Data.Abstracts
{
  public abstract class AbstractEntity : HasId
  {
    public int getId()
    {
      return 0;
    }
  }
}