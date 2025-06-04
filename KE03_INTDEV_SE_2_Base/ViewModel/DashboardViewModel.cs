using DataAccessLayer.Models;

namespace KE03_INTDEV_SE_2_Base.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalCustomers { get; set; }
        public int TotalOrders { get; set; }
        public int TotalProducts { get; set; }
        public int TotalParts { get; set; }

        public List<Customer> Customers { get; set; }
        public List<Order> Orders { get; set; }
        public List<Product> Products { get; set; }
        public List<Part> Parts { get; set; }
    }
}