using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NBD6.Data;
using NBD6.Models;
using NBD6.Utilities;

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
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchTerm, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) || sortOrder != "name_desc" ? "name_desc" : "name_asc";
            ViewBag.StartDateSortParm = sortOrder == "start_date_asc" ? "start_date_desc" : "start_date_asc";
            ViewBag.EndDateSortParm = sortOrder == "end_date_asc" ? "end_date_desc" : "end_date_asc";
            ViewBag.BidAmountSortParm = sortOrder == "bid_amount_asc" ? "bid_amount_desc" : "bid_amount_asc";
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

            var projectsQuery = _context.Projects
                .Include(p => p.Client)
                .Include(p => p.Address)
                .AsQueryable();

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

            // Apply dynamic sorting
            // Note: Sorting for related entities should be handled differently if needed

            switch (sortOrder)
            {
                case "name_desc":
                    projectsQuery = projectsQuery.OrderByDescending(p => p.ProjectName);
                    break;
                // Add other cases here
                default:
                    projectsQuery = projectsQuery.OrderBy(p => p.ProjectName);
                    break;
            }

            int pageSize = 10; // Set your page size
            var pagedData = await PaginatedList<Project>.CreateAsync(projectsQuery.AsNoTracking(), page ?? 1, pageSize);

            return View(pagedData);
        }


        // GET: Projects/Create
        public IActionResult Create()
        {
            TempData.Clear();
            TempData["ProjectKey"] = "Access";
            TempData["ProjectUrl"] = HttpContext.Request.Headers["Referer"].ToString();
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
