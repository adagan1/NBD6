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
                if (!context.Clients.Any())
                {
                    context.Clients.AddRange(
                        new Client
                        {
                            ClientFirstName = "Lamar",
                            ClientLastName = "Jackson",
                            ClientAddress = "101 Freak St St Catharines, ON LB1 2A3",
                            ClientContact = "lamarjackson@gmail.com",
                            ClientPhone = "2222223212"
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
                            BidAmount = 21
                        },
                        new Project
                        {
                            ProjectName = "Tech Hub Renovation",
                            ProjectStartDate = new DateTime(2023, 02, 22),
                            ProjectEndDate = new DateTime(2023, 03, 12),
                            ProjectSite = "City Tech Center",
                            BidAmount = 35
                        },
                        new Project
                        {
                            ProjectName = "Green Energy Park",
                            ProjectStartDate = new DateTime(2023, 06, 18),
                            ProjectEndDate = new DateTime(2023, 09, 02),
                            ProjectSite = "Green Valley",
                            BidAmount = 50
                        },
                        new Project
                        {
                            ProjectName = "Urban Redevelopment",
                            ProjectStartDate = new DateTime(2023, 04, 15),
                            ProjectEndDate = new DateTime(2023, 07, 29),
                            ProjectSite = "Downtown District",
                            BidAmount = 42
                        },
                        new Project
                        {
                            ProjectName = "Smart City Infrastructure",
                            ProjectStartDate = new DateTime(2023, 02, 30),
                            ProjectEndDate = new DateTime(2023, 05, 17),
                            ProjectSite = "SmartCity Center",
                            BidAmount = 60
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
