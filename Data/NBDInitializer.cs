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
                //Delete and recreate the Database with every restart
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                if (!context.Addresses.Any())
                {

                    context.Addresses.AddRange(
                        new Address
                        {
                            AddressID = 1,
                            Country = "Canada",
                            Province = "Ontario",
                            Postal = "K1A 0B1",
                            Street = "Highland Park"
                        },
                        new Address
                        {
                            AddressID = 2,
                            Country = "Canada",
                            Province = "Ontario",
                            Postal = "M5H 2N2",
                            Street = "Peachtree Street"
                        },
                        new Address
                        {
                            AddressID = 3,
                            Country = "Canada",
                            Province = "Manitoba",
                            Postal = "R3C 1V1",
                            Street = "Beverly Drive"
                        },
                        new Address
                        {
                            AddressID = 4,
                            Country = "Canada",
                            Province = "British Columbia",
                            Postal = "V6C 2J8",
                            Street = "Congress Avenue"
                        },
                        new Address
                        {
                            AddressID = 5,
                            Country = "Canada",
                            Province = "Quebec",
                            Postal = "H2Y 2E7",
                            Street = "Liberty Bell Road"
                        },
                        new Address
                        {
                            AddressID = 6,
                            Country = "Canada",
                            Province = "Alberta",
                            Postal = "T5K 2J6",
                            Street = "Glenridge Avenue"
                        },
                        new Address
                        {
                            AddressID = 7,
                            Country = "Canada",
                            Province = "Nova Scotia",
                            Postal = "B3J 1R2",
                            Street = "Green Valley Avenue"
                        },
                        new Address
                        {
                            AddressID = 8,
                            Country = "Canada",
                            Province = "Newfoundland and Labrador",
                            Postal = "A1A 1A1",
                            Street = "Chocolate Street"
                        },
                        new Address
                        {
                            AddressID = 9,
                            Country = "Canada",
                            Province = "Saskatchewan",
                            Postal = "S4P 3Y2",
                            Street = "Tech City Way"
                        },
                        new Address
                        {
                            AddressID = 10,
                            Country = "Canada",
                            Province = "Prince Edward Island",
                            Postal = "C1A 4P3",
                            Street = "Environmental Street"
                        },
                        new Address
                        {
                            AddressID = 11,
                            Country = "Canada",
                            Province = "Quebec",
                            Postal = "G1K 8M8",
                            Street = "Sunny Road"
                        },
                        new Address
                        {
                            AddressID = 12,
                            Country = "Canada",
                            Province = "British Columbia",
                            Postal = "V6Z 1A1",
                            Street = "Dusty Way"
                        },
                        new Address
                        {
                            AddressID = 13,
                            Country = "Canada",
                            Province = "Manitoba",
                            Postal = "R2C 0A1",
                            Street = "Casino Boulevard"
                        },
                        new Address
                        {
                            AddressID = 14,
                            Country = "Canada",
                            Province = "Alberta",
                            Postal = "T1X 0L3",
                            Street = "Rainy Street"
                        },
                        new Address
                        {
                            AddressID = 15,
                            Country = "Canada",
                            Province = "Ontario",
                            Postal = "K0J 1B0",
                            Street = "Forest Drive"
                        }
                    );
                    context.SaveChanges();
                }

                if (!context.Clients.Any())
                {
                    context.Clients.AddRange(
                        new Client
                        {
                            CompanyName = "Ravens HQ",
                            FirstName = "Lamar",
                            MiddleName = "S",
                            LastName = "Jackson",
                            ClientContact = "lamarjackson@gmail.com",
                            ClientPhone = "2222223212",
                            AddressID = 1
                        },
                        new Client
                        {
                            CompanyName = "Adidas",
                            FirstName = "Serena",
                            MiddleName = "G",
                            LastName = "Williams",
                            ClientContact = "serenawilliams@gmail.com",
                            ClientPhone = "5555556543",
                            AddressID = 2
                        },
                        new Client
                        {
                            CompanyName = "Tampa Bucks",
                            FirstName = "Tom",
                            MiddleName = "W",
                            LastName = "Brady",
                            ClientContact = "tombrady@gmail.com",
                            ClientPhone = "6666667654",
                            AddressID = 3
                        },
                        new Client
                        {
                            CompanyName = "Jordans",
                            FirstName = "Michael",
                            MiddleName = "H",
                            LastName = "Jordan",
                            ClientContact = "mjordan@gmail.com",
                            ClientPhone = "3333334321",
                            AddressID = 4
                        },
                        new Client
                        {
                            CompanyName = "LA Lakers",
                            FirstName = "LeBron",
                            MiddleName = "N",
                            LastName = "James",
                            ClientContact = "lebronjames@gmail.com",
                            ClientPhone = "4444445432",
                            AddressID = 5
                        }
                    );
                    context.SaveChanges();
                }
                if (!context.Projects.Any())
                {
                    context.Projects.AddRange(
                        new Project
                        {
                            ProjectName = "BU Glass Garden",
                            ProjectStartDate = new DateTime(2023, 01, 01),
                            ProjectEndDate = new DateTime(2023, 03, 22),
                            ProjectSite = "Brock University",
                            BidAmount = 21,
                            ClientID = 1,
                            AddressID = 6
                        },
                        new Project
                        {
                            ProjectName = "Tech Hub Renovation",
                            ProjectStartDate = new DateTime(2023, 02, 22),
                            ProjectEndDate = new DateTime(2023, 03, 12),
                            ProjectSite = "Brock University",
                            BidAmount = 35,
                            ClientID = 1,
                            AddressID = 7
                        },
                        new Project
                        {
                            ProjectName = "Green Energy Park",
                            ProjectStartDate = new DateTime(2023, 06, 18),
                            ProjectEndDate = new DateTime(2023, 09, 02),
                            ProjectSite = "Green Valley",
                            BidAmount = 50,
                            ClientID = 2,
                            AddressID = 8
                        },
                        new Project
                        {
                            ProjectName = "Urban Redevelopment",
                            ProjectStartDate = new DateTime(2023, 04, 15),
                            ProjectEndDate = new DateTime(2023, 07, 29),
                            ProjectSite = "Green Valley",
                            BidAmount = 42,
                            ClientID = 2,
                            AddressID = 9
                        },
                        new Project
                        {
                            ProjectName = "Smart City",
                            ProjectStartDate = new DateTime(2023, 02, 19),
                            ProjectEndDate = new DateTime(2023, 05, 17),
                            ProjectSite = "Riverside Park",
                            BidAmount = 60,
                            ClientID = 2,
                            AddressID = 10
                        },
                        new Project
                        {
                            ProjectName = "Riverside Park Development",
                            ProjectStartDate = new DateTime(2023, 10, 01),
                            ProjectEndDate = new DateTime(2023, 12, 15),
                            ProjectSite = "Riverside Park",
                            BidAmount = 25,
                            ClientID = 3,
                            AddressID = 11
                        },
                        new Project
                        {
                            ProjectName = "Tech Center Expansion",
                            ProjectStartDate = new DateTime(2023, 11, 20),
                            ProjectEndDate = new DateTime(2024, 01, 10),
                            ProjectSite = "Tech City Park",
                            BidAmount = 28,
                            ClientID = 4,
                            AddressID = 12
                        },
                        new Project
                        {
                            ProjectName = "City Park Revitalization",
                            ProjectStartDate = new DateTime(2023, 08, 05),
                            ProjectEndDate = new DateTime(2023, 10, 25),
                            ProjectSite = "Tech City Park",
                            BidAmount = 40,
                            ClientID = 4,
                            AddressID = 13
                        },
                        new Project
                        {
                            ProjectName = "Downtown Plaza Renovation",
                            ProjectStartDate = new DateTime(2023, 09, 15),
                            ProjectEndDate = new DateTime(2023, 11, 30),
                            ProjectSite = "Environmental Hub",
                            BidAmount = 38,
                            ClientID = 5,
                            AddressID = 14
                        },
                        new Project
                        {
                            ProjectName = "LeBron's Green Initiative",
                            ProjectStartDate = new DateTime(2023, 08, 20),
                            ProjectEndDate = new DateTime(2023, 10, 10),
                            ProjectSite = "Environmental Hub",
                            BidAmount = 48,
                            ClientID = 5,
                            AddressID = 15
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
