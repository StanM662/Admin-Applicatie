using DataAccessLayer.Models;

namespace KE03_INTDEV_SE_2_Base.ViewModels
{
    public class DashboardViewModel
    {
        // Totalen voor de dashboard
        public int TotalCustomers { get; set; }
        public int TotalOrders { get; set; }
        public int TotalProducts { get; set; }
        public int TotalParts { get; set; }

        public List<Customer> Customers { get; set; }
        public List<Order> Orders { get; set; }
        public List<Product> Products { get; set; }
        public List<Part> Parts { get; set; }

        // bestellingen per dag grafiek
        public Dictionary<string, int> OrdersPerDay { get; set; } = new();

        // klanten per dag grafiek
        public Dictionary<string, int> CustomersPerDay { get; set; } = new();
    }
}