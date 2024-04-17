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
                            BidName = "Glass Material Bid",
                            BidStart = new DateTime(2024, 01, 01),
                            BidEnd = new DateTime(2024, 01, 15),
                            ProjectID = 1,

                            Materials = new List<Material>
                            {
                                new Material
                                {
                                    MaterialType = "Material",
                                    MaterialQuantity = 500,
                                    MaterialDescription = "Reinforced Concrete Slabs",
                                    MaterialSize = "20 cm",
                                    MaterialPrice = 75.00m
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
                            BidName = "Steel Material Bid",
                            BidStart = new DateTime(2024, 02, 01),
                            BidEnd = new DateTime(2024, 02, 15),
                            ProjectID = 2,
                            Materials = new List<Material>
                            {
                                new Material
                                {
                                    MaterialType = "Steel",
                                    MaterialQuantity = 200,
                                    MaterialDescription = "Structural Steel Beams",
                                    MaterialSize = "15 m",
                                    MaterialPrice = 100.00m
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
                            BidName = "Concrete Material Bid",
                            BidStart = new DateTime(2024, 03, 01),
                            BidEnd = new DateTime(2024, 03, 15),
                            ProjectID = 3,
                            Materials = new List<Material>
                            {
                                new Material
                                {
                                    MaterialType = "Glass",
                                    MaterialQuantity = 100,
                                    MaterialDescription = "Tempered Glass Panels",
                                    MaterialSize = "10 cm",
                                    MaterialPrice = 50.00m
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
                        }, new Bid
                        {
                            BidName = "Electrical Work Bid",
                            BidStart = new DateTime(2024, 04, 01),
                            BidEnd = new DateTime(2024, 04, 15),
                            ProjectID = 4,
                            Materials = new List<Material>
                            {
                                new Material
                                {
                                    MaterialType = "Electrical Components",
                                    MaterialQuantity = 50,
                                    MaterialDescription = "Wiring and Cables",
                                    MaterialSize = "Various",
                                    MaterialPrice = 80.00m
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
                            BidName = "Plumbing Material Bid",
                            BidStart = new DateTime(2024, 05, 01),
                            BidEnd = new DateTime(2024, 05, 15),
                            ProjectID = 5,
                            Materials = new List<Material>
                            {
                                new Material
                                {
                                    MaterialType = "Plumbing Fixtures",
                                    MaterialQuantity = 30,
                                    MaterialDescription = "Sinks, Toilets, and Faucets",
                                    MaterialSize = "Various",
                                    MaterialPrice = 120.00m
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
                            BidName = "Roofing Material Bid",
                            BidStart = new DateTime(2024, 06, 01),
                            BidEnd = new DateTime(2024, 06, 15),
                            ProjectID = 6,
                            Materials = new List<Material>
                            {
                                new Material
                                {
                                    MaterialType = "Roofing Shingles",
                                    MaterialQuantity = 1500,
                                    MaterialDescription = "Asphalt Roofing Shingles",
                                    MaterialSize = "30 cm x 30 cm",
                                    MaterialPrice = 90.00m
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
                            BidName = "Painting Bid",
                            BidStart = new DateTime(2024, 05, 01),
                            BidEnd = new DateTime(2024, 05, 15),
                            ProjectID = 5,
                            Materials = new List<Material>
                            {
                                new Material
                                {
                                    MaterialType = "Paint",
                                    MaterialQuantity = 30,
                                    MaterialDescription = "Interior and Exterior Paint",
                                    MaterialSize = "Various",
                                    MaterialPrice = 120.00m
                                }
                            },
                            Labours = new List<Labour>
                            {
                                new Labour
                                {
                                    LabourHours = 45,
                                    LabourDescription = "Production Worker",
                                    LabourPrice = 30.00m
                                }
                            }
                        },
                        new Bid
                        {
                            BidName = "Flooring Bid",
                            BidStart = new DateTime(2024, 06, 01),
                            BidEnd = new DateTime(2024, 06, 15),
                            ProjectID = 6,
                            Materials = new List<Material>
                            {
                                new Material
                                {
                                    MaterialType = "Flooring",
                                    MaterialQuantity = 1500,
                                    MaterialDescription = "Hardwood Flooring",
                                    MaterialSize = "15 cm x 15 cm",
                                    MaterialPrice = 90.00m
                                }
                            },
                            Labours = new List<Labour>
                            {
                                new Labour
                                {
                                    LabourHours = 55,
                                    LabourDescription = "Designer",
                                    LabourPrice = 65.00m
                                }
                            }
                        }, 
                        new Bid
                        {
                            BidName = "Landscaping Bid",
                            BidStart = new DateTime(2024, 08, 01),
                            BidEnd = new DateTime(2024, 08, 15),
                            ProjectID = 8,
                            Materials = new List<Material>
                            {
                                new Material
                                {
                                    MaterialType = "Plants",
                                    MaterialQuantity = 100,
                                    MaterialDescription = "Assorted Flowers and Shrubs",
                                    MaterialSize = "Various",
                                    MaterialPrice = 30.00m
                                },
                                new Material
                                {
                                    MaterialType = "Mulch",
                                    MaterialQuantity = 10,
                                    MaterialDescription = "Wood Chips",
                                    MaterialSize = "N/A",
                                    MaterialPrice = 20.00m
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
                            BidName = "HVAC System Bid",
                            BidStart = new DateTime(2024, 07, 01),
                            BidEnd = new DateTime(2024, 07, 15),
                            ProjectID = 7,
                            Materials = new List<Material>
                            {
                                new Material
                                {
                                    MaterialType = "HVAC Components",
                                    MaterialQuantity = 20,
                                    MaterialDescription = "Air Conditioning Units and Ducts",
                                    MaterialSize = "Various",
                                    MaterialPrice = 200.00m
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
                        }
                    );             
                    context.SaveChanges();
                }

                if (!context.StaffBids.Any())
                {
                    context.StaffBids.AddRange(                     
                        new StaffBid
                        {
                            StaffID = context.Staffs.FirstOrDefault(s => s.FirstName == "John").StaffID,
                            BidID = context.Bids.FirstOrDefault(b => b.BidName == "Glass Material Bid").BidID
                        },
                        new StaffBid
                        {
                            StaffID = context.Staffs.FirstOrDefault(s => s.FirstName == "Jane").StaffID,
                            BidID = context.Bids.FirstOrDefault(b => b.BidName == "Steel Material Bid").BidID
                        },
                        new StaffBid
                        {
                            StaffID = context.Staffs.FirstOrDefault(s => s.FirstName == "Michael").StaffID,
                            BidID = context.Bids.FirstOrDefault(b => b.BidName == "Concrete Material Bid").BidID
                        },                      
                        new StaffBid
                        {
                            StaffID = context.Staffs.FirstOrDefault(s => s.FirstName == "Emily").StaffID,
                            BidID = context.Bids.FirstOrDefault(b => b.BidName == "Electrical Work Bid").BidID
                        },
                        new StaffBid
                        {
                            StaffID = context.Staffs.FirstOrDefault(s => s.FirstName == "David").StaffID,
                            BidID = context.Bids.FirstOrDefault(b => b.BidName == "Plumbing Material Bid").BidID
                        },
                        new StaffBid
                        {
                            StaffID = context.Staffs.FirstOrDefault(s => s.FirstName == "Alice").StaffID,
                            BidID = context.Bids.FirstOrDefault(b => b.BidName == "Roofing Material Bid").BidID
                        },
                        new StaffBid
                        {
                            StaffID = context.Staffs.FirstOrDefault(s => s.FirstName == "Robert").StaffID,
                            BidID = context.Bids.FirstOrDefault(b => b.BidName == "Painting Bid").BidID
                        },
                        new StaffBid
                        {
                            StaffID = context.Staffs.FirstOrDefault(s => s.FirstName == "Sarah").StaffID,
                            BidID = context.Bids.FirstOrDefault(b => b.BidName == "Flooring Bid").BidID
                        },
                        new StaffBid
                        {
                            StaffID = context.Staffs.FirstOrDefault(s => s.FirstName == "Daniel").StaffID,
                            BidID = context.Bids.FirstOrDefault(b => b.BidName == "Landscaping Bid").BidID
                        },
                        new StaffBid
                        {
                            StaffID = context.Staffs.FirstOrDefault(s => s.FirstName == "Sophia").StaffID,
                            BidID = context.Bids.FirstOrDefault(b => b.BidName == "HVAC System Bid").BidID
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