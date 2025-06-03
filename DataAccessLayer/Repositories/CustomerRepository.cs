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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly MatrixIncDbContext _context;

        public CustomerRepository(MatrixIncDbContext context)
        {
            _context = context;
        }

        public void AddCustomer(Product customer)
        {
            _context.Products.Add(customer);
            _context.SaveChanges();
        }

        public void DeleteCustomer(Product customer)
        {
            _context.Products.Remove(customer);
            _context.SaveChanges();
        }

        public IEnumerable<Product> GetAllCustomers()
        {
            return _context.Products.Include(c => c.Orders);
        }

        public Product? GetCustomerById(int id)
        {
            return _context.Products.Include(c => c.Orders).FirstOrDefault(c => c.Id == id);
        }

        public void UpdateCustomer(Product customer)
        {
            _context.Products.Update(customer);
            _context.SaveChanges();
        }
    }
}
