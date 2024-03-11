﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NBD6.Models;
using System;
using System.Diagnostics;
using System.Linq;

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
                                Street = "22 Highland Park"
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
                                Street = "32 Wind Street"
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
                                Street = "16 Thai Street"
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
                            ClientID = 1, // Assuming ClientID associated with this Project
                            Address = new Address
                            {
                                Country = "Canada",
                                Province = "Yukon",
                                Postal = "K5A 0Y8",
                                Street = "1 Project Street "
                            }
                        },
                        new Project
                        {
                            ProjectName = "NC Glass Garden",
                            ProjectStartDate = new DateTime(2023, 01, 01),
                            ProjectEndDate = new DateTime(2023, 03, 22),
                            ProjectSite = "Niagara College",
                            ClientID = 2, // Assuming ClientID associated with this Project
                            Address = new Address
                            {
                                Country = "Canada",
                                Province = "Yukon",
                                Postal = "K8A 0K8",
                                Street = "2 Project Street"
                            }
                        },
                        new Project
                        {
                            ProjectName = "WU Glass Garden",
                            ProjectStartDate = new DateTime(2023, 01, 01),
                            ProjectEndDate = new DateTime(2023, 03, 22),
                            ProjectSite = "Western Union",
                            ClientID = 3, // Assuming ClientID associated with this Project
                            Address = new Address
                            {
                                Country = "Canada",
                                Province = "Yukon",
                                Postal = "K9A 0A8",
                                Street = "3 Project Street"
                            }
                        }
                        // Add other Project entries similarly
                    );
                    context.SaveChanges();
                }

                if (!context.Bids.Any())
                {
                    // Create bids
                    context.Bids.AddRange(
                        new Bid
                        {
                            BidName = "Glass Material Bid",
                            BidStart = new DateTime(2024, 01, 01),
                            BidEnd = new DateTime(2024, 01, 15),                           
                            ProjectID = 1 // Assuming ProjectID associated with this Bid
                        },
                        new Bid
                        {
                            BidName = "Steel Material Bid",
                            BidStart = new DateTime(2024, 02, 01),
                            BidEnd = new DateTime(2024, 02, 15),                          
                            ProjectID = 2 // Assuming ProjectID associated with this Bid
                        },
                        new Bid
                        {
                            BidName = "Concrete Material Bid",
                            BidStart = new DateTime(2024, 03, 01),
                            BidEnd = new DateTime(2024, 03, 15),
                            ProjectID = 3 // Assuming ProjectID associated with this Bid
                        }
                    );
                    context.SaveChanges();
                }
                if (!context.Labours.Any())
                {
                    context.Labours.AddRange(
                    new Labour
                    {
                        LabourHours = 40,
                        LabourDescription = "Installation of Glass Panels",
                        LabourPrice = 25.00m,
                        BidID = 1
                    },
                    new Labour
                    {
                        LabourHours = 60,
                        LabourDescription = "Welding and Fabrication",
                        LabourPrice = 30.00m,
                        BidID = 2
                    },
                    new Labour
                    {
                        LabourHours = 80,
                        LabourDescription = "Pouring and Finishing",
                        LabourPrice = 35.00m,
                        BidID = 3
                    }
                    );
                    context.SaveChanges();
                }
                if (!context.Materials.Any())
                {
                    context.Materials.AddRange(
                    new Material
                    {
                        MaterialType = "Concrete",
                        MaterialQuantity = 500,
                        MaterialDescription = "Reinforced Concrete Slabs",
                        MaterialSize = "20 cm",
                        MaterialPrice = 75.00m,
                        BidID = 1
                    },
                    new Material
                    {
                        MaterialType = "Steel",
                        MaterialQuantity = 200,
                        MaterialDescription = "Structural Steel Beams",
                        MaterialSize = "15 m",
                        MaterialPrice = 100.00m,
                        BidID = 2
                    },
                    new Material
                    {
                        MaterialType = "Glass",
                        MaterialQuantity = 100,
                        MaterialDescription = "Tempered Glass Panels",
                        MaterialSize = "10 cm",
                        MaterialPrice = 50.00m,
                        BidID = 3
                    }
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