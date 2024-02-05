using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NBD6.Data;
using NBD6.Models;

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
        public async Task<IActionResult> Index(string sortOrder, string searchTerm)
        {
            // Set up sorting parameters
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) || sortOrder != "name_desc" ? "name_desc" : "name_asc";
            ViewBag.StartDateSortParm = sortOrder == "start_date_asc" ? "start_date_desc" : "start_date_asc";
            ViewBag.EndDateSortParm = sortOrder == "end_date_asc" ? "end_date_desc" : "end_date_asc";
            ViewBag.BidAmountSortParm = sortOrder == "bid_amount_asc" ? "bid_amount_desc" : "bid_amount_asc"; // Corrected this line
            ViewBag.CompanySortParm = sortOrder == "company_asc" ? "company_desc" : "company_asc";
            ViewBag.SiteSortParm = sortOrder == "site_asc" ? "site_desc" : "site_asc";
            ViewBag.StreetSortParm = sortOrder == "street_asc" ? "street_desc" : "street_asc";
            ViewBag.PostalSortParm = sortOrder == "Postal_asc" ? "Postal_desc" : "Postal_asc";

            // Retrieve projects with their associated client and address
            var projectsQuery = _context.Projects
                .Include(p => p.Client)
                .Include(p => p.Address)
                .AsQueryable();

            // Apply search filter if searchTerm is provided
            if (!String.IsNullOrEmpty(searchTerm))
            {
                var lowerCaseSearchTerm = searchTerm.ToLower();

                projectsQuery = projectsQuery.Where(p =>
                    p.ProjectName.ToLower().Contains(lowerCaseSearchTerm)
                    || p.ProjectStartDate.ToString().ToLower().Contains(lowerCaseSearchTerm)
                    || p.ProjectEndDate.ToString().ToLower().Contains(lowerCaseSearchTerm)
                    || p.Client.ClientName.ToLower().Contains(lowerCaseSearchTerm)
                    || p.ProjectSite.ToLower().Contains(lowerCaseSearchTerm)
                    || p.Address.Street.ToLower().Contains(lowerCaseSearchTerm)
                    || p.Address.Postal.ToLower().Contains(lowerCaseSearchTerm));
            }

            // Execute the query and materialize the data
            var projects = await projectsQuery.ToListAsync();

            // Apply sorting in memory for bid amount
            switch (sortOrder)
            {
                case "name_desc":
                    projects = projects.OrderByDescending(p => p.ProjectName).ToList();
                    break;
                case "start_date_asc":
                    projects = projects.OrderBy(p => p.ProjectStartDate).ToList();
                    break;
                case "start_date_desc":
                    projects = projects.OrderByDescending(p => p.ProjectStartDate).ToList();
                    break;
                case "end_date_asc":
                    projects = projects.OrderBy(p => p.ProjectEndDate).ToList();
                    break;
                case "end_date_desc":
                    projects = projects.OrderByDescending(p => p.ProjectEndDate).ToList();
                    break;
                case "bid_amount_asc":
                    projects = projects.OrderBy(p => p.BidAmount).ToList(); // Corrected this line
                    break;
                case "bid_amount_desc":
                    projects = projects.OrderByDescending(p => p.BidAmount).ToList(); // Corrected this line
                    break;
                case "company_asc":
                    projects = projects.OrderBy(p => p.Client.CompanyName).ToList();
                    break;
                case "company_desc":
                    projects = projects.OrderByDescending(p => p.Client.CompanyName).ToList();
                    break;
                case "site_asc":
                    projects = projects.OrderBy(p => p.ProjectSite).ToList();
                    break;
                case "site_desc":
                    projects = projects.OrderByDescending(p => p.ProjectSite).ToList();
                    break;
                case "street_asc":
                    projects = projects.OrderBy(p => p.Address.Street).ToList();
                    break;
                case "street_desc":
                    projects = projects.OrderByDescending(p => p.Address.Street).ToList();
                    break;
                case "Postal_asc":
                    projects = projects.OrderBy(p => p.Address.Postal).ToList();
                    break;
                case "Postal_desc":
                    projects = projects.OrderByDescending(p => p.Address.Postal).ToList();
                    break;
                default:
                    projects = projects.OrderBy(p => p.ProjectName).ToList();
                    break;
            }

            // Return the view with the sorted and filtered projects
            return View(projects);
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(int? id)
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

            return View(project);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            ViewData["AddressID"] = new SelectList(_context.Addresses, "AddressID", "AddressSummary");
            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "ClientSummary");
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectID,ProjectName,ProjectStartDate,ProjectEndDate,ProjectSite,BidAmount,ClientID,AddressID")] Project project)
        {
            if (ModelState.IsValid)
            {
                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressID"] = new SelectList(_context.Addresses, "AddressID", "AddressSummary", project.AddressID);
            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "ClientSummary", project.ClientID);
            return View(project);
        }

        // GET: Projects/Edit/5
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
            ViewData["AddressID"] = new SelectList(_context.Addresses, "AddressID", "AddressSummary", project.AddressID);
            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "ClientSummary", project.ClientID);
            return View(project); // Pass the project to the view
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectID,ProjectName,ProjectStartDate,ProjectEndDate,ProjectSite,BidAmount,ClientID,AddressID")] Project project)
        {
            if (id != project.ProjectID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
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
            ViewData["AddressID"] = new SelectList(_context.Addresses, "AddressID", "AddressSummary", project.AddressID);
            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "ClientSummary", project.ClientID);
            return View(project);
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Projects == null)
            {
                return Problem("Entity set 'NBDContext.Projects'  is null.");
            }
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {
          return _context.Projects.Any(e => e.ProjectID == id);
        }
    }
}
