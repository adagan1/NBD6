using NBD6.Models;
using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace NBD6.Data
{
    public static class NBDInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            NBDContext context = applicationBuilder.ApplicationServices.CreateScope()
                .ServiceProvider.GetRequiredService<NBDContext>();
            try
            {
                // Delete and recreate the Database with every restart
                //context.Database.EnsureDeleted();
                //context.Database.EnsureCreated();

                // Seed Addresses
                if (!context.Addresses.Any())
                {
                    Address address = new Address
                    {
                        AddressID = GenerateUniqueID(),
                        Country = "Canada",
                        Province = "Ontario",
                        Postal = "K1A 0B1",
                        Street = "Highland Park",
                    };
                    context.Addresses.Add(address);
                    context.SaveChanges();
                }

                // Seed Clients
                if (!context.Clients.Any())
                {
                    int addressID = context.Addresses.FirstOrDefault()?.AddressID ?? 0; // Retrieve the first AddressID
                    Client client = new Client
                    {
                        CompanyName = "Ravens HQ",
                        ClientName = "Lamar Jackson",
                        ClientContact = "lamarjackson@gmail.com",
                        ClientPhone = "2222223212",
                        AddressID = addressID,
                    };
                    context.Clients.Add(client);
                    context.SaveChanges();
                }

                // Seed Projects
                if (!context.Projects.Any())
                {
                    int clientID = context.Clients.FirstOrDefault()?.ClientID ?? 0; // Retrieve the first ClientID
                    int addressID = context.Addresses.FirstOrDefault()?.AddressID ?? 0; // Retrieve the first AddressID
                    Project project = new Project
                    {
                        ProjectName = "BU Glass Garden",
                        ProjectStartDate = new DateTime(2023, 01, 01),
                        ProjectEndDate = new DateTime(2023, 03, 22),
                        ProjectSite = "Brock University",
                        BidAmount = 21,
                        ClientID = clientID,
                        AddressID = addressID,
                    };
                    context.Projects.Add(project);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetBaseException().Message);
            }
        }

        // Assuming you have a method to generate unique IDs for seeding data
        private static int GenerateUniqueID()
        {
            // Implement your logic to generate unique IDs here
            // You can use a random number generator, GUID, or any other method that ensures uniqueness
            // For simplicity, let's use a simple counter for demonstration purposes
            return new Random().Next(1, 1000); // Generate a random number between 1 and 1000
        }
    }
}