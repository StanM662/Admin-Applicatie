using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly MatrixIncDbContext _context;

        public AccountRepository(MatrixIncDbContext context)
        {
            _context = context;
        }

        public void AddAccount(Account account)
        {
            _context.Accounts.Add(account);
            _context.SaveChanges();
        }

        public void DeleteAccount(Account account)
        {
            _context.Accounts.Remove(account);
            _context.SaveChanges();
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            return _context.Accounts.Include(c => c.Accounts);
        }

        public Account? GetAccountById(int id)
        {
            return _context.Accounts.Include(c => c.Accounts).FirstOrDefault(c => c.Id == id);
        }

        public void UpdateAccount(Account account)
        {
            _context.Accounts.Update(account);
            _context.SaveChanges();
        }
    }
}

