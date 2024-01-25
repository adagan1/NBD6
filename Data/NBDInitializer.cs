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
                //Delete and recreate3 the Database with every restart
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                if (!context.Addresses.Any())
                {
                    context.Addresses.AddRange(
                        new Address
                        {
                            Country = "Canada",
                            ProvinceState = "St.Catharines",
                            AreaCode = "L2S 3A1",
                            Street = "Glenridge Avenue"
                        },
                        new Address
                        {
                            Country = "America",
                            ProvinceState = "Chicago",
                            AreaCode = "67823",
                            Street = "Green Valley Avenue"
                        },
                        new Address
                        {
                            Country = "America",
                            ProvinceState = "New York",
                            AreaCode = "33542",
                            Street = "Chocolate Street"
                        },
                        new Address
                        {
                            Country = "America",
                            ProvinceState = "Florida",
                            AreaCode = "21986",
                            Street = "Tech City Way"
                        },
                        new Address
                        {
                            Country = "America",
                            ProvinceState = "Ohio",
                            AreaCode = "22389",
                            Street = "Environmental Street"
                        }
                        );
                }

                if (!context.Clients.Any())
                {
                    context.Clients.AddRange(
                        new Client
                        {
                            ClientFirstName = "Lamar",
                            ClientLastName = "Jackson",
                            ClientAddress = "101 Freak St St.Catharines, ON LB1 2A3",
                            ClientContact = "lamarjackson@gmail.com",
                            ClientPhone = "2222223212",
                        },
                        new Client
                        {
                            ClientFirstName = "Serena",
                            ClientLastName = "Williams",
                            ClientAddress = "15 Champion Lane Palm Beach Gardens, FL 33418",
                            ClientContact = "serenawilliams@gmail.com",
                            ClientPhone = "5555556543"
                        },
                        new Client
                        {
                            ClientFirstName = "Tom",
                            ClientLastName = "Brady",
                            ClientAddress = "12 Goat Blvd Tampa, FL 33601",
                            ClientContact = "tombrady@gmail.com",
                            ClientPhone = "6666667654"
                        },
                        new Client
                        {
                            ClientFirstName = "Michael",
                            ClientLastName = "Jordan",
                            ClientAddress = "23 Legend Ave Chicago, IL 60601",
                            ClientContact = "mjordan@gmail.com",
                            ClientPhone = "3333334321"
                        },
                        new Client
                        {
                            ClientFirstName = "LeBron",
                            ClientLastName = "James",
                            ClientAddress = "6 King's Court Los Angeles, CA 90001",
                            ClientContact = "lebronjames@gmail.com",
                            ClientPhone = "4444445432"
                        });
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
                            ClientID = context.Clients.FirstOrDefault(c => c.ClientFirstName == "Lamar" && c.ClientLastName == "Jackson").ClientID,
                            AddressID = context.Addresses.FirstOrDefault(a => a.Street == "Glenridge Avenue" && a.Country == "Canada").AddressID


                        },
                        new Project
                        {
                            ProjectName = "Tech Hub Renovation",
                            ProjectStartDate = new DateTime(2023, 02, 22),
                            ProjectEndDate = new DateTime(2023, 03, 12),
                            ProjectSite = "Brock University",
                            BidAmount = 35,
                            ClientID = context.Clients.FirstOrDefault(c => c.ClientFirstName == "Lamar" && c.ClientLastName == "Lamar").ClientID,
                            AddressID = context.Addresses.FirstOrDefault(a => a.Street == "Glenridge Avenue" && a.Country == "Canada").AddressID
                        },
                        new Project
                        {
                            ProjectName = "Green Energy Park",
                            ProjectStartDate = new DateTime(2023, 06, 18),
                            ProjectEndDate = new DateTime(2023, 09, 02),
                            ProjectSite = "Green Valley",
                            BidAmount = 50,
                            ClientID = context.Clients.FirstOrDefault(c => c.ClientFirstName == "Serena" && c.ClientLastName == "Williams").ClientID,
                            AddressID = context.Addresses.FirstOrDefault(a => a.Street == "Green Vally Avenue" && a.Country == "America").AddressID

                        },
                        new Project
                        {
                            ProjectName = "Urban Redevelopment",
                            ProjectStartDate = new DateTime(2023, 04, 15),
                            ProjectEndDate = new DateTime(2023, 07, 29),
                            ProjectSite = "Green Valley",
                            BidAmount = 42,
                            ClientID = context.Clients.FirstOrDefault(c => c.ClientFirstName == "Serena" && c.ClientLastName == "Serena").ClientID,
                            AddressID = context.Addresses.FirstOrDefault(a => a.Street == "Green Vally Avenue" && a.Country == "America").AddressID

                        },
                        new Project
                        {
                            ProjectName = "Smart City Infrastructure",
                            ProjectStartDate = new DateTime(2023, 02, 30),
                            ProjectEndDate = new DateTime(2023, 05, 17),
                            ProjectSite = "Riverside Park",
                            BidAmount = 60,
                            ClientID = context.Clients.FirstOrDefault(c => c.ClientFirstName == "Tom" && c.ClientLastName == "Brady").ClientID,
                            AddressID = context.Addresses.FirstOrDefault(a => a.Street == "Chocolate Street" && a.Country == "America").AddressID

                        },
                        new Project
                        {
                            ProjectName = "Riverside Park Development",
                            ProjectStartDate = new DateTime(2023, 10, 01),
                            ProjectEndDate = new DateTime(2023, 12, 15),
                            ProjectSite = "Riverside Park",
                            BidAmount = 25,
                            ClientID = context.Clients.FirstOrDefault(c => c.ClientFirstName == "Tom" && c.ClientLastName == "Brady").ClientID,
                            AddressID = context.Addresses.FirstOrDefault(a => a.Street == "Chocolate Street" && a.Country == "America").AddressID

                        },
                        new Project
                        {
                            ProjectName = "Tech Center Expansion",
                            ProjectStartDate = new DateTime(2023, 11, 20),
                            ProjectEndDate = new DateTime(2024, 01, 10),
                            ProjectSite = "Tech City Park",
                            BidAmount = 28,
                            ClientID = context.Clients.FirstOrDefault(c => c.ClientFirstName == "Michael" && c.ClientLastName == "Jordan").ClientID,
                            AddressID = context.Addresses.FirstOrDefault(a => a.Street == "Tech City Way" && a.Country == "America").AddressID

                        },
                        new Project
                        {
                            ProjectName = "City Park Revitalization",
                            ProjectStartDate = new DateTime(2023, 08, 05),
                            ProjectEndDate = new DateTime(2023, 10, 25),
                            ProjectSite = "Tech City Park",
                            BidAmount = 40,
                            ClientID = context.Clients.FirstOrDefault(c => c.ClientFirstName == "Michael" && c.ClientLastName == "Jordan").ClientID,
                            AddressID = context.Addresses.FirstOrDefault(a => a.Street == "Tech City Way" && a.Country == "America").AddressID

                        },
                        new Project
                        {
                            ProjectName = "Downtown Plaza Renovation",
                            ProjectStartDate = new DateTime(2023, 09, 15),
                            ProjectEndDate = new DateTime(2023, 11, 30),
                            ProjectSite = "Environmental Hub",
                            BidAmount = 38,
                            ClientID = context.Clients.FirstOrDefault(c => c.ClientFirstName == "LeBron" && c.ClientLastName == "James").ClientID,
                            AddressID = context.Addresses.FirstOrDefault(a => a.Street == "Environmental Street" && a.Country == "America").AddressID

                        },

                        new Project
                        {
                            ProjectName = "LeBron's Green Initiative",
                            ProjectStartDate = new DateTime(2023, 08, 20),
                            ProjectEndDate = new DateTime(2023, 10, 10),
                            ProjectSite = "Environmental Hub",
                            BidAmount = 48,
                            ClientID = context.Clients.FirstOrDefault(c => c.ClientFirstName == "LeBron" && c.ClientLastName == "James").ClientID,
                            AddressID = context.Addresses.FirstOrDefault(a => a.Street == "Environmental Street" && a.Country == "America").AddressID

                        });
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
