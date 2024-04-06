using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using NBD6.Data;
using NBD6.Models;
using NBD6.Utilities;
using System.Diagnostics;

namespace NBD6.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly NBDContext _context;

        public ProjectsController(NBDContext context)
        {
            _context = context;
        }

        // GET: Projects
        [Authorize(Roles = "Admin, Management, Designer")]
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchTerm, int? page)
        {
            var projectsQuery = _context.Projects
                .Include(p => p.Client)
                .Include(p => p.Address)
                .AsNoTracking()
                .AsQueryable();

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) || sortOrder != "name_desc" ? "name_desc" : "name_asc";
            ViewBag.StartDateSortParm = sortOrder == "start_date_asc" ? "start_date_desc" : "start_date_asc";
            ViewBag.EndDateSortParm = sortOrder == "end_date_asc" ? "end_date_desc" : "end_date_asc";
            ViewBag.CompanySortParm = sortOrder == "company_asc" ? "company_desc" : "company_asc";
            ViewBag.SiteSortParm = sortOrder == "site_asc" ? "site_desc" : "site_asc";
            ViewBag.StreetSortParm = sortOrder == "street_asc" ? "street_desc" : "street_asc";
            ViewBag.PostalSortParm = sortOrder == "Postal_asc" ? "Postal_desc" : "Postal_asc";

            if (searchTerm != null)
            {
                page = 1;
            }
            else
            {
                searchTerm = currentFilter;
            }

            ViewBag.CurrentFilter = searchTerm;

            if (!String.IsNullOrEmpty(searchTerm))
            {
                var lowerCaseSearchTerm = searchTerm.ToLower();

                projectsQuery = projectsQuery.Where(p =>
                    p.ProjectName.ToLower().Contains(lowerCaseSearchTerm)
                    || p.ProjectStartDate.ToString().ToLower().Contains(lowerCaseSearchTerm)
                    || p.ProjectEndDate.ToString().ToLower().Contains(lowerCaseSearchTerm)
                    || p.Client.FirstName.ToLower().Contains(lowerCaseSearchTerm)
                    || p.ProjectSite.ToLower().Contains(lowerCaseSearchTerm)
                    || p.Address.Street.ToLower().Contains(lowerCaseSearchTerm)
                    || p.Address.Postal.ToLower().Contains(lowerCaseSearchTerm));
            }

            // Apply sorting
            switch (sortOrder)
            {
                case "name_asc":
                    projectsQuery = projectsQuery.OrderBy(p => p.ProjectName);
                    break;
                case "name_desc":
                    projectsQuery = projectsQuery.OrderByDescending(p => p.ProjectName);
                    break;
                case "start_date_asc":
                    projectsQuery = projectsQuery.OrderBy(p => p.ProjectStartDate);
                    break;
                case "start_date_desc":
                    projectsQuery = projectsQuery.OrderByDescending(p => p.ProjectStartDate);
                    break;
                case "end_date_asc":
                    projectsQuery = projectsQuery.OrderBy(p => p.ProjectEndDate);
                    break;
                case "end_date_desc":
                    projectsQuery = projectsQuery.OrderByDescending(p => p.ProjectEndDate);
                    break;
                case "company_asc":
                    projectsQuery = projectsQuery.OrderBy(p => p.Client.FirstName);
                    break;
                case "company_desc":
                    projectsQuery = projectsQuery.OrderByDescending(p => p.Client.FirstName);
                    break;
                case "site_asc":
                    projectsQuery = projectsQuery.OrderBy(p => p.ProjectSite);
                    break;
                case "site_desc":
                    projectsQuery = projectsQuery.OrderByDescending(p => p.ProjectSite);
                    break;
                case "street_asc":
                    projectsQuery = projectsQuery.OrderBy(p => p.Address.Street);
                    break;
                case "street_desc":
                    projectsQuery = projectsQuery.OrderByDescending(p => p.Address.Street);
                    break;
                case "Postal_asc":
                    projectsQuery = projectsQuery.OrderBy(p => p.Address.Postal);
                    break;
                case "Postal_desc":
                    projectsQuery = projectsQuery.OrderByDescending(p => p.Address.Postal);
                    break;
                default:
                    projectsQuery = projectsQuery.OrderBy(p => p.ProjectName);
                    break;
            }

            int pageSize = 10;
            var pagedData = await PaginatedList<Project>.CreateAsync(projectsQuery.AsNoTracking(), page ?? 1, pageSize);

            return View(pagedData);
        }

        // GET: Project/Details/5
        [Authorize(Roles = "Admin, Management, Designer")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(a => a.Address)
                .Include(c => c.Client)
                .Include(b => b.Bids) 
                .FirstOrDefaultAsync(m => m.ProjectID == id);

            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }


        // GET: Projects/Create
        [Authorize(Roles = "Admin, Management, Designer")]
        public IActionResult Create()
        {
            TempData["ProjectUrl"] = HttpContext.Request.Headers["Referer"].ToString();
            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "ClientSummary");

            ViewBag.Country = new SelectList(new[] { "Canada" });

            // List of Canadian provinces as strings
            var provinces = new[]
            {
                "Alberta",
                "British Columbia",
                "Manitoba",
                "New Brunswick",
                "Newfoundland and Labrador",
                "Northwest Territories",
                "Nova Scotia",
                "Nunavut",
                "Ontario",
                "Prince Edward Island",
                "Quebec",
                "Saskatchewan",
                "Yukon"
            };

            ViewBag.Provinces = new SelectList(provinces, "Ontario");

            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Management, Designer")]
        public async Task<IActionResult> Create([Bind("ProjectID,ProjectName,ProjectStartDate,ProjectEndDate,ProjectSite,ClientID,AddressID,Country,Province,Postal,Street,BidAmount")] Project project, Address address)
        {

            var client = await _context.Clients
                .Include(c => c.Address)
                .Include(c => c.Projects)
                .FirstOrDefaultAsync(m => m.ClientID == project.ClientID);

            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();

            // Set the AddressID for the client
            project.AddressID = address.AddressID;
            project.Address = address;
            project.Client = client;
            if (client.AddressID == 0)
            {
                client.AddressID = address.AddressID;
                client.Address = address;
            }

            await _context.SaveChangesAsync();
            var errors = ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .Select(x => new { x.Key, x.Value.Errors })
                .ToArray();

            if (ModelState.IsValid)
            {
                _context.Add(project);
                await _context.SaveChangesAsync();

                TempData["NewProjectID"] = project.ProjectID;
                TempData["NewProjectSummary"] = project.ProjectSummary;
                TempData["AlertMessageProject"] = "Project Successfully Added";

                // Check if TempData contains the project temp data
                if (TempData.ContainsKey("ProjectUrl"))
                {
                    // Redirect back a page
                    return Redirect(TempData["ProjectUrl"].ToString());
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "ClientSummary", project.ClientID);

            Debug.WriteLine(client);

            ViewBag.Country = new SelectList(new[] { "Canada" });

            // List of Canadian provinces as strings
            var provinces = new[]
            {
                "Alberta",
                "British Columbia",
                "Manitoba",
                "New Brunswick",
                "Newfoundland and Labrador",
                "Northwest Territories",
                "Nova Scotia",
                "Nunavut",
                "Ontario",
                "Prince Edward Island",
                "Quebec",
                "Saskatchewan",
                "Yukon"
            };

            ViewBag.Provinces = new SelectList(provinces, "Ontario");

            return View(project);
        }

        // GET: Projects/Edit/5
        [Authorize(Roles = "Admin, Management, Designer")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Address)
                .Include(p => p.Client)
                .FirstOrDefaultAsync(m => m.ProjectID == id);

            if (project == null)
            {
                return NotFound();
            }
            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "ClientSummary", project.ClientID);

            ViewBag.Country = new SelectList(new[] { "Canada" });

            // List of Canadian provinces as strings
            var provinces = new[]
            {
                "Alberta",
                "British Columbia",
                "Manitoba",
                "New Brunswick",
                "Newfoundland and Labrador",
                "Northwest Territories",
                "Nova Scotia",
                "Nunavut",
                "Ontario",
                "Prince Edward Island",
                "Quebec",
                "Saskatchewan",
                "Yukon"
            };

            ViewBag.Provinces = new SelectList(provinces, "Ontario");
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Management, Designer")]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectID,ProjectName,ProjectStartDate,ProjectEndDate,ProjectSite,Client,ClientID,AddressID,Country,Province,Postal,Street,BidAmount")] Project project, Address address)
        {
            if (id != project.ProjectID)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .Include(c => c.Address)
                .Include(c => c.Projects)
                .FirstOrDefaultAsync(m => m.ClientID == project.ClientID);

            var errors = ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .Select(x => new { x.Key, x.Value.Errors })
                .ToArray();

            if (ModelState.IsValid)
            {
                try
                {
                    // Retrieve the existing project including its associated address and client
                    var existingProject = await _context.Projects
                        .Include(p => p.Address)
                        .Include(p => p.Client)
                        .FirstOrDefaultAsync(p => p.ProjectID == id);

                    if (existingProject == null)
                    {
                        return NotFound();
                    }

                    // Update project properties
                    existingProject.ProjectName = project.ProjectName;
                    existingProject.ProjectStartDate = project.ProjectStartDate;
                    existingProject.ProjectEndDate = project.ProjectEndDate;
                    existingProject.ProjectSite = project.ProjectSite;
                    existingProject.BidAmount = project.BidAmount;

                    // Update client properties
                    existingProject.Client = client;


                    // Update address properties
                    existingProject.Address.Country = address.Country;
                    existingProject.Address.Province = address.Province;
                    existingProject.Address.Postal = address.Postal;
                    existingProject.Address.Street = address.Street;

                    TempData["AlertMessageProjectEdit"] = "Project Successfully Edited";

                    _context.Update(existingProject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.ProjectID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            // If model state is not valid
            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "ClientSummary", project.ClientID);

            ViewBag.Country = new SelectList(new[] { "Canada" });

            // List of Canadian provinces as strings
            var provinces = new[]
            {
                "Alberta",
                "British Columbia",
                "Manitoba",
                "New Brunswick",
                "Newfoundland and Labrador",
                "Northwest Territories",
                "Nova Scotia",
                "Nunavut",
                "Ontario",
                "Prince Edward Island",
                "Quebec",
                "Saskatchewan",
                "Yukon"
            };

            ViewBag.Provinces = new SelectList(provinces, "Ontario");

            return View(project);
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.ProjectID == id);
        }
    }
}
