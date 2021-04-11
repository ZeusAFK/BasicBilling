using System.Collections.Generic;
using BasicBilling.Data.Entities;

namespace BasicBilling.Data.Interfaces
{
  public interface IBasicBillingRepo
  {
    bool SaveChanges();
    
    #region Clients
    IEnumerable<Client> GetAllClients();
    Client GetClientById(int id);
    void CreateClient(Client client);
    void UpdateClient(Client client);
    #endregion

    #region Services
    Service GetServiceByShortname(string shortname);
    #endregion

    #region Bills
    void CreateBill(Bill bill);
    Bill GetBillById(int id);
    #endregion
  }
}