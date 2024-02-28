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
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                if (!context.Addresses.Any())
                {
                    // Create addresses without assigning foreign keys
                    var addresses = new[]
                    {
                        new Address
                        {
                            Country = "Canada",
                            Province = "Ontario",
                            Postal = "K1A 0B1",
                            Street = "Highland Park"
                        }
                        // Add other Address entries similarly
                    };
                    context.Addresses.AddRange(addresses);
                    context.SaveChanges();
                }

                if (!context.Clients.Any())
                {
                    // Create clients
                    context.Clients.AddRange(
                        new Client
                        {
                            CompanyName = "Ravens HQ",
                            ClientName = "Lamar Jackson",
                            ClientContact = "lamarjackson@gmail.com",
                            ClientPhone = "2222223212",
                            AddressID = 1, // Assuming AddressID associated with this Client
                        }
                        // Add other Client entries similarly
                    );
                    context.SaveChanges();
                }

                if (!context.Projects.Any())
                {
                    // Create projects
                    context.Projects.AddRange(
                        new Project
                        {
                            ProjectName = "BU Glass Garden",
                            ProjectStartDate = new DateTime(2023, 01, 01),
                            ProjectEndDate = new DateTime(2023, 03, 22),
                            ProjectSite = "Brock University",
                            BidAmount = 21,
                            ClientID = 1, // Assuming ClientID associated with this Project
                            AddressID = 1, // Assuming AddressID associated with this Project
                        }
                        // Add other Project entries similarly
                    );
                    context.SaveChanges();
                }

                // After creating clients and projects, update the addresses with the corresponding foreign keys
                var client = context.Clients.FirstOrDefault();
                var project = context.Projects.FirstOrDefault();
                var addressesToUpdate = context.Addresses.ToList();
                foreach (var address in addressesToUpdate)
                {
                    address.ClientID = client.ClientID;
                    address.ProjectID = project.ProjectID;
                }
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetBaseException().Message);
            }
        }
    }
}