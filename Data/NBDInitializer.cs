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
               
                if (!context.Clients.Any())
                {
                    // Create clients
                    context.Clients.AddRange(
                        new Client
                        {
                            CompanyName = "Ravens HQ",
                            FirstName = "Lamar",
                            MiddleName = "S",
                            LastName = "Jackson",
                            ClientContact = "lamarjackson@gmail.com",
                            ClientPhone = "2222223212",
                            Address = new Address
                            {
                                Country = "Canada",
                                Province = "Ontario",
                                Postal = "K1A 0B1",
                                Street = "Highland Park"
                            }
                        },
                        new Client
                        {
                            CompanyName = "Gators HQ",
                            FirstName = "Kyle",
                            MiddleName = "S",
                            LastName = "Ziz",
                            ClientContact = "kziz@gmail.com",
                            ClientPhone = "2222223222",
                            Address = new Address
                            {
                                Country = "Canada",
                                Province = "Manitoba",
                                Postal = "K2A 0B2",
                                Street = "Wind Street"
                            },
                        },
                        new Client
                        {
                            CompanyName = "Bulls HQ",
                            FirstName = "Evan",
                            MiddleName = "S",
                            LastName = "My",
                            ClientContact = "emy@gmail.com",
                            ClientPhone = "2225223212",
                            Address = new Address
                            {
                                Country = "Canada",
                                Province = "Manitoba",
                                Postal = "K2A 0B8",
                                Street = "Thai Street"
                            }
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
                            Address = new Address
                            {
                                Country = "Canada",
                                Province = "Yukon",
                                Postal = "K5A 0Y8",
                                Street = "Project Street 1"
                            }
                        },
                        new Project
                        {
                            ProjectName = "NC Glass Garden",
                            ProjectStartDate = new DateTime(2023, 01, 01),
                            ProjectEndDate = new DateTime(2023, 03, 22),
                            ProjectSite = "Niagara College",
                            BidAmount = 21,
                            ClientID = 2, // Assuming ClientID associated with this Project
                            Address = new Address
                            {
                                Country = "Canada",
                                Province = "Yukon",
                                Postal = "K8A 0K8",
                                Street = "Project Street 2"
                            }
                        },
                        new Project
                        {
                            ProjectName = "WU Glass Garden",
                            ProjectStartDate = new DateTime(2023, 01, 01),
                            ProjectEndDate = new DateTime(2023, 03, 22),
                            ProjectSite = "Western Union",
                            BidAmount = 21,
                            ClientID = 3, // Assuming ClientID associated with this Project
                            Address = new Address
                            {
                                Country = "Canada",
                                Province = "Yukon",
                                Postal = "K9A 0A8",
                                Street = "Project Street 3"
                            }
                        }
                        // Add other Project entries similarly
                    );
                    context.SaveChanges();
                }               
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetBaseException().Message);
            }
        }
    }
}
