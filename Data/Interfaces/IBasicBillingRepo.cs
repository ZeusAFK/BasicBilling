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
    IEnumerable<Service> GetAllServices();
    Service GetServiceByShortname(string shortname);
    Service GetServiceById(int id);
    void CreateService(Service service);
    void UpdateService(Service service);
    #endregion

    #region Bills
    void CreateBill(Bill bill);
    Bill GetBillById(int id);
    #endregion
  }
}