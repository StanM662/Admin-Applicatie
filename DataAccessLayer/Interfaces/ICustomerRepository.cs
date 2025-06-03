using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface ICustomerRepository
    {
        public IEnumerable<Product> GetAllCustomers();

        public Product? GetCustomerById(int id);

        public void AddCustomer(Product customer);

        public void UpdateCustomer(Product customer);

        public void DeleteCustomer(Product customer);
    }
}
