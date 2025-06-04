using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IAccountRepository
    {
        public IEnumerable<Account> GetAllAccounts();

        public Account? GetAccountById(int id);

        public void AddAccount(Account account);

        public void UpdateAccount(Account account);

        public void DeleteAccount(Account account);
    }
}
