using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public static class MatrixIncDbInitializer
    {
        public static void Initialize(MatrixIncDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Products.Any())
            {
                return; // DB has been seeded
            }

            var customers = new Customer[]
            {
        new Customer { Name = "Neo", Address = "123 Elm St", Active = true },
        new Customer { Name = "Morpheus", Address = "456 Oak St", Active = true },
        new Customer { Name = "Trinity", Address = "789 Pine St", Active = true }
            };
            context.Customers.AddRange(customers);
            context.SaveChanges();

            var orders = new Order[]
            {
        new Order { CustomerId = customers[0].Id, OrderDate = DateTime.Parse("2021-01-01") },
        new Order { CustomerId = customers[0].Id, OrderDate = DateTime.Parse("2021-02-01") },
        new Order { CustomerId = customers[1].Id, OrderDate = DateTime.Parse("2021-02-01") },
        new Order { CustomerId = customers[2].Id, OrderDate = DateTime.Parse("2021-03-01") }
            };
            context.Orders.AddRange(orders);

            var products = new Product[]
            {
        new Product { Name = "Nebuchadnezzar", Description = "Het schip waarop Neo voor het eerst de echte wereld leert kennen", Price = 10000.00m },
        new Product { Name = "Jack-in Chair", Description = "Stoel met een rugsteun en metalen armen waarin mensen zitten om ingeplugd te worden in de Matrix via een kabel in de nekpoort", Price = 500.50m },
        new Product { Name = "EMP (Electro-Magnetic Pulse) Device", Description = "Wapentuig op de schepen van Zion", Price = 129.99m }
            };
            context.Products.AddRange(products);

            var parts = new Part[]
            {
        new Part { Name = "Tandwiel", Description = "Overdracht van rotatie in bijvoorbeeld de motor of luikmechanismen" },
        new Part { Name = "M5 Boutje", Description = "Bevestiging van panelen, buizen of interne modules" },
        new Part { Name = "Hydraulische cilinder", Description = "Openen/sluiten van zware luchtsluizen of bewegende onderdelen" },
        new Part { Name = "Koelvloeistofpomp", Description = "Koeling van de motor of elektronische systemen." }
            };
            context.Parts.AddRange(parts);

            var accounts = new Account[]
            {
        new Account { Name = "Stan", Password = "Stan123" },
        new Account { Name = "Rick", Password = "Rick123" },
        new Account { Name = "Sander", Password = "Sander123" },
        new Account { Name = "Guest", Password = "Guest123" }
            };
            context.Accounts.AddRange(accounts);

            context.SaveChanges();

            
        }

    }
}
