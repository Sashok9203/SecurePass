using data_access.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_access.Repositories
{
    public interface IUnitOW : IDisposable
    {
        IRepository<BankAccount> BankAccounts { get; }
        IRepository<BaseEntity> BaseEntities { get; }
        IRepository<Category> Categories { get; }
        IRepository<CreditCard> CreditCards { get; }
        IRepository<DataBase> DataBases { get; }
        IRepository<Email> Emails { get; }
        IRepository<SecureObject> SecureObjects { get; }
        IRepository<Server> Servers { get; }
        IRepository<Universal> Universals { get; }
        IRepository<User> Users { get; }
        IRepository<WiFi> WiFis { get; }
        IRepository<Contact> Contacts { get; }
        void Save();
        Task SaveAsync();
    }
}
