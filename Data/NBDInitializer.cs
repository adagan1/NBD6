using NBD6.Models;
using NBD6.Data;
using System.Diagnostics;

namespace NBD6.Data
{
    public static class NBDInitializer
    {
        private static readonly Random random = new Random();

        private static readonly List<string> companyNames = new List<string>
        {
            "ABC Corp","XYZ Ltd","123 Company","ABC Corp", "XYZ Ltd", "123 Company", "Global Enterprises", "Dynamic Innovations", "Sunset Builders", "Tech Solutions Inc", "Oceanic Ventures", "Elite Creations", "Empire Builders"
        };

        private static readonly List<string> firstNames = new List<string>
        {
            "John",
            "Jane",
            "Michael",
            "Emily",
            "David",
            "Alice",
            "Robert",
            "Sarah",
            "Daniel",
            "Sophia",
            "Chris",
            "Rachel",
            // Add more first names as needed
        };

        private static readonly List<string> lastNames = new List<string>
        {
            "Smith",
            "Johnson",
            "Brown",
            "Williams",
            "Jones",
            "Davis",
            "Taylor",
            "Martinez",
            "Wilson",
            "Anderson",
            "Garcia",
            "Thomas",
            // Add more last names as needed
        };

        private static string GenerateRandomPhoneNumber()
        {
            // Generate a random 10-digit phone number
            return $"{random.Next(100, 999)}{random.Next(100, 999)}{random.Next(1000, 9999)}";
        }

        private static Address GenerateRandomAddress()
        {
            // Generate a random address
            return new Address
            {
                Country = "Canada",
                Province = "Ontario",
                Postal = $"{random.Next(100, 999)} {random.Next(100, 999)}",
                Street = $"{random.Next(10, 999)} {firstNames[random.Next(firstNames.Count)]} Street"
            };
        }

        public static Client GenerateRandomClient()
        {
            return new Client
            {
                CompanyName = companyNames[random.Next(companyNames.Count)],
                FirstName = firstNames[random.Next(firstNames.Count)],
                MiddleName = $"{(char)random.Next('A', 'Z')}",
                LastName = lastNames[random.Next(lastNames.Count)],
                ClientContact = $"{firstNames[random.Next(firstNames.Count)].ToLower()}{lastNames[random.Next(lastNames.Count)].ToLower()}@example.com",
                ClientPhone = GenerateRandomPhoneNumber(),
                Address = GenerateRandomAddress()
            };
        }

        public static Project GenerateRandomProject(int clientID)
        {
            return new Project
            {
                ProjectName = $"Random Project {random.Next(1, 100)}",
                ProjectStartDate = DateTime.Now.AddDays(random.Next(1, 30)),
                ProjectEndDate = DateTime.Now.AddDays(random.Next(31, 60)),
                ProjectSite = $"Random Site {random.Next(1, 10)}",
                BidAmount = $"{random.Next(1000, 5000)}",
                ClientID = clientID,
                Address = GenerateRandomAddress()
            };
        }

        public static Bid GenerateRandomBid(int projectID)
        {
            return new Bid
            {
                BidName = $"Random Bid {random.Next(1, 100)}",
                BidStart = DateTime.Now.AddDays(random.Next(1, 10)),
                BidEnd = DateTime.Now.AddDays(random.Next(11, 20)),
                ProjectID = projectID,
                Materials = new List<Material>
            {
                new Material
                {
                    MaterialType = "Random Material",
                    MaterialQuantity = random.Next(50, 200),
                    MaterialDescription = $"Random Material Description {random.Next(1, 5)}",
                    MaterialSize = $"{random.Next(5, 20)} cm",
                    MaterialPrice = random.Next(50, 200)
                }
            },
                Labours = new List<Labour>
            {
                new Labour
                {
                    LabourHours = random.Next(20, 100),
                    LabourDescription = $"Random Labour Description {random.Next(1, 5)}",
                    LabourPrice = random.Next(20, 50)
                }
            }
            };
        }
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            NBDContext context = applicationBuilder.ApplicationServices.CreateScope()
                .ServiceProvider.GetRequiredService<NBDContext>();
            try
            {
                // Your existing seeding logic here
                //context.Database.EnsureDeleted();
                //context.Database.EnsureCreated();
                // Generate random clients
                for (int i = 0; i < 10; i++)
                {
                    Client randomClient = GenerateRandomClient();
                    context.Clients.Add(randomClient);
                }
                context.SaveChanges();

                // Retrieve existing client IDs
                var clientIDs = context.Clients.Select(c => c.ClientID).ToList();

                // Generate random projects for each client
                foreach (var clientID in clientIDs)
                {
                    Project randomProject = GenerateRandomProject(clientID);
                    context.Projects.Add(randomProject);
                }
                context.SaveChanges();

                // Retrieve existing project IDs
                var projectIDs = context.Projects.Select(p => p.ProjectID).ToList();

                // Generate random bids for each project
                foreach (var projectID in projectIDs)
                {
                    Bid randomBid = GenerateRandomBid(projectID);
                    context.Bids.Add(randomBid);
                }
                context.SaveChanges();

                // Other seeding logic
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetBaseException().Message);
            }
        }
    }
}
