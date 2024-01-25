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
                //context.Database.EnsureDeleted();
                //context.Database.EnsureCreated();

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
                    context.SaveChanges();
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
                            AddressID = 1
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
                            AddressID = 1

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
