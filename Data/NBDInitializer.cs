using NBD6.Models;
using System.Diagnostics;

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
                            CompanyName = "Company One",
                            FirstName = "Joe",
                            MiddleName = "S",
                            LastName = "Jackson",
                            ClientContact = "Joejackson@gmail.com",
                            ClientPhone = "2222223212",
                            Address = new Address
                            {
                                Country = "Canada",
                                Province = "Ontario",
                                City = "Niagara Falls",
                                Postal = "A1A 1A1",
                                Street = "10 North Street"
                            }
                        },
                        new Client
                        {
                            CompanyName = "Company Two",
                            FirstName = "Alice",
                            MiddleName = "M",
                            LastName = "Johnson",
                            ClientContact = "alicejohnson@gmail.com",
                            ClientPhone = "3333334321",
                            Address = new Address
                            {
                                Country = "Canada",
                                Province = "Ontario",
                                City = "Brampton",
                                Postal = "B2B 2B2",
                                Street = "20 South Street"
                            }
                        },
                        new Client
                        {
                            CompanyName = "Company Three",
                            FirstName = "Emily",
                            MiddleName = "R",
                            LastName = "Smith",
                            ClientContact = "emilysmith@gmail.com",
                            ClientPhone = "4444445432",
                            Address = new Address
                            {
                                Country = "Canada",
                                Province = "Quebec",
                                City = "Quebec City",
                                Postal = "C3C 3C3",
                                Street = "30 East Street"
                            }
                        },
                        new Client
                        {
                            CompanyName = "Company Four",
                            FirstName = "Michael",
                            MiddleName = "A",
                            LastName = "Brown",
                            ClientContact = "michaelbrown@gmail.com",
                            ClientPhone = "5555556543",
                            Address = new Address
                            {
                                Country = "Canada",
                                Province = "British Columbia",
                                City = "Vancouver",
                                Postal = "D4D 4D4",
                                Street = "40 West Street"
                            }
                        },
                        new Client
                        {
                            CompanyName = "Company Five",
                            FirstName = "Sarah",
                            MiddleName = "L",
                            LastName = "Anderson",
                            ClientContact = "sarahanderson@gmail.com",
                            ClientPhone = "6666667654",
                            Address = new Address
                            {
                                Country = "Canada",
                                Province = "Alberta",
                                City = "Edmonton",
                                Postal = "E5E 5E5",
                                Street = "50 Central Street"
                            }
                        });
                   context.SaveChanges();
                }

                if (!context.Projects.Any())
                {
                    // Create projects
                    context.Projects.AddRange(
                        new Project
                        {
                            ProjectName = "Project One",
                            ProjectStartDate = new DateTime(2023, 01, 01),
                            ProjectEndDate = new DateTime(2023, 03, 22),
                            ProjectSite = "Brock University",
                            BidAmount = "2000",
                            ClientID = 1,
                            Address = new Address
                            {
                                Country = "Canada",
                                Province = "Yukon",
                                City = "Watson Lake",
                                Postal = "K5A 0Y8",
                                Street = "1 Project Street "
                            }
                        },
                        new Project
                        {
                            ProjectName = "Project Two",
                            ProjectStartDate = new DateTime(2023, 04, 15),
                            ProjectEndDate = new DateTime(2023, 06, 30),
                            ProjectSite = "University of Toronto",
                            BidAmount = "2500",
                            ClientID = 2,
                            Address = new Address
                            {
                                Country = "Canada",
                                Province = "Ontario",
                                City = "Toronto",
                                Postal = "M5S 1A5",
                                Street = "2 Project Street"
                            }
                        },
                        new Project
                        {
                            ProjectName = "Project Three",
                            ProjectStartDate = new DateTime(2023, 07, 10),
                            ProjectEndDate = new DateTime(2023, 09, 20),
                            ProjectSite = "McMaster University",
                            BidAmount = "3000",
                            ClientID = 3,
                            Address = new Address
                            {
                                Country = "Canada",
                                Province = "Ontario",
                                City = "London",
                                Postal = "L8S 4L8",
                                Street = "3 Project Street"
                            }
                        }, new Project
                        {
                            ProjectName = "Project Four",
                            ProjectStartDate = new DateTime(2023, 10, 05),
                            ProjectEndDate = new DateTime(2023, 12, 15),
                            ProjectSite = "University of British Columbia",
                            BidAmount = "2800",
                            ClientID = 4,
                            Address = new Address
                            {
                                Country = "Canada",
                                Province = "British Columbia",
                                City = "Victoria",
                                Postal = "V6T 1Z4",
                                Street = "4 Project Street"
                            }
                        },
                        new Project
                        {
                            ProjectName = "Project Five",
                            ProjectStartDate = new DateTime(2024, 01, 10),
                            ProjectEndDate = new DateTime(2024, 03, 25),
                            ProjectSite = "Simon Fraser University",
                            BidAmount = "3200",
                            ClientID = 5,
                            Address = new Address
                            {
                                Country = "Canada",
                                Province = "British Columbia",
                                City = "Parksville",
                                Postal = "V5A 1S6",
                                Street = "5 Project Street"
                            }
                        },
                        new Project
                        {
                            ProjectName = "Project Six",
                            ProjectStartDate = new DateTime(2024, 04, 01),
                            ProjectEndDate = new DateTime(2024, 06, 10),
                            ProjectSite = "University of Alberta",
                            BidAmount = "2700",
                            ClientID = 1,
                            Address = new Address
                            {
                                Country = "Canada",
                                Province = "Alberta",
                                City = "Calgary",
                                Postal = "T6G 2R3",
                                Street = "6 Project Street"
                            }
                        },
                        new Project
                        {
                            ProjectName = "Project Seven",
                            ProjectStartDate = new DateTime(2024, 07, 15),
                            ProjectEndDate = new DateTime(2024, 09, 30),
                            ProjectSite = "University of Calgary",
                            BidAmount = "3100",
                            ClientID = 2,
                            Address = new Address
                            {
                                Country = "Canada",
                                Province = "Alberta",
                                City = "Red Deer",
                                Postal = "T2N 1N4",
                                Street = "7 Project Street"
                            }
                        },
                        new Project
                        {
                            ProjectName = "Project Eight",
                            ProjectStartDate = new DateTime(2024, 10, 10),
                            ProjectEndDate = new DateTime(2024, 12, 20),
                            ProjectSite = "University of Waterloo",
                            BidAmount = "2900",
                            ClientID = 3,
                            Address = new Address
                            {
                                Country = "Canada",
                                Province = "Ontario",
                                City = "Windsor",
                                Postal = "N2L 3G1",
                                Street = "8 Project Street"
                            }
                        },
                        new Project
                        {
                            ProjectName = "Project Nine",
                            ProjectStartDate = new DateTime(2025, 01, 05),
                            ProjectEndDate = new DateTime(2025, 03, 15),
                            ProjectSite = "Queen's University",
                            BidAmount = "3300",
                            ClientID = 4,
                            Address = new Address
                            {
                                Country = "Canada",
                                Province = "Ontario",
                                City = "Etobicoke",
                                Postal = "K7L 3N6",
                                Street = "9 Project Street"
                            }
                        },
                        new Project
                        {
                            ProjectName = "Project Ten",
                            ProjectStartDate = new DateTime(2025, 04, 01),
                            ProjectEndDate = new DateTime(2025, 06, 10),
                            ProjectSite = "University of Ottawa",
                            BidAmount = "2600",
                            ClientID = 5,
                            Address = new Address
                            {
                                Country = "Canada",
                                Province = "Ontario",
                                City = "Oshawa",
                                Postal = "K1N 6N5",
                                Street = "10 Project Street"
                            }
                        }
                    );
                    context.SaveChanges();
                }

                if (!context.Staffs.Any())
                {
                    // Create staff members
                    context.Staffs.AddRange(
                        new Staff
                        {
                            FirstName = "John",
                            LastName = "Doe",
                            StaffPhone = "3333333333",
                            StaffPosition = "Designer"
                        },
                        new Staff
                        {
                            FirstName = "Jane",
                            LastName = "Smith",
                            StaffPhone = "4444444444",
                            StaffPosition = "Designer"
                        },
                        new Staff
                        {
                            FirstName = "Michael",
                            LastName = "Johnson",
                            StaffPhone = "5555555555",
                            StaffPosition = "Designer"
                        },
                        new Staff
                        {
                            FirstName = "Emily",
                            LastName = "Brown",
                            StaffPhone = "6666666666",
                            StaffPosition = "Designer"
                        },
                        new Staff
                        {
                            FirstName = "David",
                            LastName = "Williams",
                            StaffPhone = "7777777777",
                            StaffPosition = "Designer"
                        },
                        new Staff
                        {
                            FirstName = "Alice",
                            LastName = "Jones",
                            StaffPhone = "8888888888",
                            StaffPosition = "Designer"
                        },
                        new Staff
                        {
                            FirstName = "Robert",
                            LastName = "Davis",
                            StaffPhone = "9999999999",
                            StaffPosition = "Designer"
                        },
                        new Staff
                        {
                            FirstName = "Sarah",
                            LastName = "Taylor",
                            StaffPhone = "1010101010",
                            StaffPosition = "Designer"
                        },
                        new Staff
                        {
                            FirstName = "Daniel",
                            LastName = "Martinez",
                            StaffPhone = "1111111111",
                            StaffPosition = "Designer"
                        },
                        new Staff
                        {
                            FirstName = "Sophia",
                            LastName = "Wilson",
                            StaffPhone = "1212121212",
                            StaffPosition = "Designer"
                        }
                    );
                    context.SaveChanges();
                }

                if (!context.Bids.Any())
                {
                // Create bids
                context.Bids.AddRange(
                    new Bid
                    {
                        BidName = "Landscaping with Roses Bid",
                        BidStart = new DateTime(2024, 01, 01),
                        BidEnd = new DateTime(2024, 01, 15),
                        ProjectID = 1,
                        Materials = new List<Material>
                    {
                        new Material
                        {
                            MaterialType = "Plants",
                            MaterialQuantity = 100,
                            MaterialDescription = "Rose",
                            MaterialSize = "5 gal",
                            MaterialPrice = 5.00m
                        }
                    },
                    Labours = new List<Labour>
                    {
                        new Labour
                        {
                            LabourHours = 40,
                            LabourDescription = "Production Worker",
                            LabourPrice = 30.00m
                        }
                    }
                    },
                    new Bid
                    {
                        BidName = "Tulip Arrangement Project",
                        BidStart = new DateTime(2024, 02, 01),
                        BidEnd = new DateTime(2024, 02, 15),
                        ProjectID = 2,
                        Materials = new List<Material>
                    {
                        new Material
                        {
                            MaterialType = "Plants",
                            MaterialQuantity = 150,
                            MaterialDescription = "Tulip",
                            MaterialSize = "3 gal",
                            MaterialPrice = 3.00m
                        }
                    },
                    Labours = new List<Labour>
                    {
                        new Labour
                        {
                            LabourHours = 60,
                            LabourDescription = "Designer",
                            LabourPrice = 65.00m
                        }
                    }   
                    },
                    new Bid
                    {
                        BidName = "Lavender Field Development",
                        BidStart = new DateTime(2024, 03, 01),
                        BidEnd = new DateTime(2024, 03, 15),
                        ProjectID = 3,
                        Materials = new List<Material>
                    {
                        new Material
                        {
                            MaterialType = "Plants",
                            MaterialQuantity = 200,
                            MaterialDescription = "Lavender",
                            MaterialSize = "4 gal",
                            MaterialPrice = 4.00m
                        }
                    },
                    Labours = new List<Labour>
                    {
                        new Labour
                        {
                            LabourHours = 80,
                            LabourDescription = "Equipment Operator",
                            LabourPrice = 55.00m
                        }
                    }
                    },
                    new Bid
                    {
                        BidName = "Orchid Greenhouse Setup",
                        BidStart = new DateTime(2024, 04, 01),
                        BidEnd = new DateTime(2024, 04, 15),
                        ProjectID = 4,
                        Materials = new List<Material>
                    {
                        new Material
                        {
                            MaterialType = "Plants",
                            MaterialQuantity = 75,
                            MaterialDescription = "Orchid",
                            MaterialSize = "6 gal",
                            MaterialPrice = 7.00m
                        }
                    },
                    Labours = new List<Labour>
                    {
                        new Labour
                        {
                            LabourHours = 50,
                            LabourDescription = "Production Worker",
                            LabourPrice = 30.00m
                        }
                    }
                    },
                    new Bid
                    {
                        BidName = "Sunflower Cultivation Bid",
                        BidStart = new DateTime(2024, 05, 01),
                        BidEnd = new DateTime(2024, 05, 15),
                        ProjectID = 5,
                        Materials = new List<Material>
                    {
                        new Material
                        {
                            MaterialType = "Plants",
                            MaterialQuantity = 300,
                            MaterialDescription = "Sunflower",
                            MaterialSize = "2 gal",
                            MaterialPrice = 2.00m
                        }
                    },
                    Labours = new List<Labour>
                    {
                        new Labour
                        {
                            LabourHours = 45,
                            LabourDescription = "Designer",
                            LabourPrice = 65.00m
                        }
                    }
                    },
                    new Bid
                    {
                        BidName = "Granite Fountain Installation",
                        BidStart = new DateTime(2024, 06, 01),
                        BidEnd = new DateTime(2024, 06, 15),
                        ProjectID = 6,
                        Materials = new List<Material>
                    {
                        new Material
                        {
                            MaterialType = "Pottery",
                            MaterialQuantity = 5,
                            MaterialDescription = "Granite Fountain",
                            MaterialSize = "36 in",
                            MaterialPrice = 200.00m
                        }
                    },
                    Labours = new List<Labour>
                    {
                        new Labour
                        {
                            LabourHours = 55,
                            LabourDescription = "Production Worker",
                            LabourPrice = 30.00m
                        }
                    }
                    },
                    new Bid
                    {
                        BidName = "Clay Vase Crafting Bid",
                        BidStart = new DateTime(2024, 07, 01),
                        BidEnd = new DateTime(2024, 07, 15),
                        ProjectID = 7,
                        Materials = new List<Material>
                    {
                        new Material
                        {
                            MaterialType = "Pottery",
                            MaterialQuantity = 40,
                            MaterialDescription = "Clay Vase",
                            MaterialSize = "12 in",
                            MaterialPrice = 20.00m
                        }
                    },
                    Labours = new List<Labour>
                    {
                        new Labour
                        {
                            LabourHours = 60,
                            LabourDescription = "Production Worker",
                            LabourPrice = 30.00m
                        }
                    }
                    },
                    new Bid
                    {
                        BidName = "Porcelain Bowl Manufacturing Bid",
                        BidStart = new DateTime(2024, 08, 01),
                        BidEnd = new DateTime(2024, 08, 15),
                        ProjectID = 8,
                        Materials = new List<Material>
                    {
                        new Material
                        {
                            MaterialType = "Pottery",
                            MaterialQuantity = 25,
                            MaterialDescription = "Porcelain Bowl",
                            MaterialSize = "12 in",
                            MaterialPrice = 30.00m
                        }
                    },
                    Labours = new List<Labour>
                    {
                        new Labour
                        {
                            LabourHours = 40,
                            LabourDescription = "Botanist",
                            LabourPrice = 75.00m
                        }
                    }
                    },
                    new Bid
                    {
                        BidName = "Terracotta Planter Sale Bid",
                        BidStart = new DateTime(2024, 09, 01),
                        BidEnd = new DateTime(2024, 09, 15),
                        ProjectID = 9,
                        Materials = new List<Material>
                    {
                        new Material
                        {
                            MaterialType = "Pottery",
                            MaterialQuantity = 50,
                            MaterialDescription = "Terracotta Planter",
                            MaterialSize = "6 in",
                            MaterialPrice = 25.00m
                        }
                    },
                    Labours = new List<Labour>
                    {
                        new Labour
                        {
                            LabourHours = 45,
                            LabourDescription = "Designer",
                            LabourPrice = 65.00m
                        }
                    }
                    },
                    new Bid
                    {
                        BidName = "Decorative Stone Landscaping Bid",
                        BidStart = new DateTime(2024, 10, 01),
                        BidEnd = new DateTime(2024, 10, 15),
                        ProjectID = 10,
                        Materials = new List<Material>
                    {
                        new Material
                        {
                            MaterialType = "Materials",
                            MaterialQuantity = 50,
                            MaterialDescription = "River Stones",
                            MaterialSize = "26 yd",
                            MaterialPrice = 20.00m
                        }
                    },
                    Labours = new List<Labour>
                    {
                        new Labour
                        {
                            LabourHours = 55,
                            LabourDescription = "Equipment Operator",
                            LabourPrice = 55.00m
                        }
                    }
                    }
                    // End of bids
                    );
                    context.SaveChanges();
                }


                if (!context.StaffBids.Any())
                {
                    context.StaffBids.AddRange(                     
                        new StaffBid
                        {
                            StaffID = context.Staffs.FirstOrDefault(s => s.FirstName == "John").StaffID,
                            BidID = context.Bids.FirstOrDefault(b => b.BidName == "Landscaping with Roses Bid").BidID
                        },
                        new StaffBid
                        {
                            StaffID = context.Staffs.FirstOrDefault(s => s.FirstName == "Jane").StaffID,
                            BidID = context.Bids.FirstOrDefault(b => b.BidName == "Tulip Arrangement Project").BidID
                        },
                        new StaffBid
                        {
                            StaffID = context.Staffs.FirstOrDefault(s => s.FirstName == "Michael").StaffID,
                            BidID = context.Bids.FirstOrDefault(b => b.BidName == "Lavender Field Development").BidID
                        },                      
                        new StaffBid
                        {
                            StaffID = context.Staffs.FirstOrDefault(s => s.FirstName == "Emily").StaffID,
                            BidID = context.Bids.FirstOrDefault(b => b.BidName == "Orchid Greenhouse Setup").BidID
                        },
                        new StaffBid
                        {
                            StaffID = context.Staffs.FirstOrDefault(s => s.FirstName == "David").StaffID,
                            BidID = context.Bids.FirstOrDefault(b => b.BidName == "Sunflower Cultivation Bid").BidID
                        },
                        new StaffBid
                        {
                            StaffID = context.Staffs.FirstOrDefault(s => s.FirstName == "Alice").StaffID,
                            BidID = context.Bids.FirstOrDefault(b => b.BidName == "Granite Fountain Installation").BidID
                        },
                        new StaffBid
                        {
                            StaffID = context.Staffs.FirstOrDefault(s => s.FirstName == "Robert").StaffID,
                            BidID = context.Bids.FirstOrDefault(b => b.BidName == "Clay Vase Crafting Bid").BidID
                        },
                        new StaffBid
                        {
                            StaffID = context.Staffs.FirstOrDefault(s => s.FirstName == "Sarah").StaffID,
                            BidID = context.Bids.FirstOrDefault(b => b.BidName == "Porcelain Bowl Manufacturing Bid").BidID
                        },
                        new StaffBid
                        {
                            StaffID = context.Staffs.FirstOrDefault(s => s.FirstName == "Daniel").StaffID,
                            BidID = context.Bids.FirstOrDefault(b => b.BidName == "Terracotta Planter Sale Bid").BidID
                        },
                        new StaffBid
                        {
                            StaffID = context.Staffs.FirstOrDefault(s => s.FirstName == "Sophia").StaffID,
                            BidID = context.Bids.FirstOrDefault(b => b.BidName == "Decorative Stone Landscaping Bid").BidID
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