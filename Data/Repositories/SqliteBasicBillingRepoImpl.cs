using BasicBilling.Data.Contexts;
using BasicBilling.Data.Interfaces;

namespace BasicBilling.Data.Repositories
{
  public class SqliteBasicBillingRepoImpl : IBasicBillingRepo
  {
    private readonly BasicBillingContext context = default!;

    public SqliteBasicBillingRepoImpl(BasicBillingContext context)
    {
      this.context = context;
    }

    public bool SaveChanges()
    {
      return context.SaveChanges() >= 0;
    }
  }
}