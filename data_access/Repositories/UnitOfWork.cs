using data_access.Data;
using data_access.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_access.Repositories
{
    public class UnitOfWork : IUnitOW
    {
        private SecurePassDBContext context = new();

        private Repository<BankAccount>? bankAccounts;
        private Repository<BaseEntity>? baseEntities;
        private Repository<Category>? categories;
        private Repository<CreditCard>? creditCards;
        private Repository<DataBase>? dataBases;
        private Repository<Email>? emails;
        private Repository<SecureObject>? secureObjects;
        private Repository<Server>? servers;
        private Repository<Universal>? universals;
        private Repository<User>? users;
        private bool disposed = false;

        public IRepository<BankAccount> BankAccounts => bankAccounts ??= new Repository<BankAccount>(context);
        public IRepository<BaseEntity> BaseEntities => baseEntities ??= new Repository<BaseEntity>(context);
        public IRepository<Category> Categories => categories ??= new Repository<Category>(context);
        public IRepository<CreditCard> CreditCards => creditCards ??= new Repository<CreditCard>(context);
        public IRepository<DataBase> DataBases => dataBases ??= new Repository<DataBase>(context);
        public IRepository<Email> Emails => emails ??= new Repository<Email>(context);
        public IRepository<SecureObject> SecureObjects => secureObjects ??= new Repository<SecureObject>(context);
        public IRepository<Server> Servers => servers ??= new Repository<Server>(context);
        public IRepository<Universal> Universals => universals ??= new Repository<Universal>(context);
        public IRepository<User> Users => users ??= new Repository<User>(context);
        public void Save()
        {
            context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
