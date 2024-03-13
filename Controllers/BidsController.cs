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
    public class BidsController : Controller
    {
        private readonly NBDContext _context;

        public BidsController(NBDContext context)
        {
            _context = context;
        }

        // GET: Bids
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchTerm, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.ProjectNameSortParm = sortOrder == "Project Name" ? "project_name_desc" : "Project Name";
            ViewBag.DateSortParm = sortOrder == "Start Date" ? "start_date_desc" : "Start Date";
            ViewBag.EndDateSortParm = sortOrder == "End Date" ? "end_date_desc" : "End Date";

            if (searchTerm != null)
            {
                page = 1;
            }
            else
            {
                searchTerm = currentFilter;
            }

            ViewBag.CurrentFilter = searchTerm;

            // Include the 'Project' navigation property in your query
            var bidsQuery = _context.Bids
                .Include(b => b.Project) // Ensure your Bid entity has a navigation property 'Project'
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                var lowerCaseSearchTerm = searchTerm.ToLower();

                // Ensure your Bid and Project entities have properties 'BidName' and 'ProjectName'
                bidsQuery = bidsQuery.Where(b => b.BidName.ToLower().Contains(lowerCaseSearchTerm)
                                                 || b.Project.ProjectName.ToLower().Contains(lowerCaseSearchTerm));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    bidsQuery = bidsQuery.OrderByDescending(b => b.BidName);
                    break;
                case "Project Name":
                    bidsQuery = bidsQuery.OrderBy(b => b.Project.ProjectName);
                    break;
                case "project_name_desc":
                    bidsQuery = bidsQuery.OrderByDescending(b => b.Project.ProjectName);
                    break;
                case "Start Date":
                    bidsQuery = bidsQuery.OrderBy(b => b.BidStart);
                    break;
                case "start_date_desc":
                    bidsQuery = bidsQuery.OrderByDescending(b => b.BidStart);
                    break;
                // Added case for sorting by end date
                case "End Date":
                    bidsQuery = bidsQuery.OrderBy(b => b.BidEnd);
                    break;
                case "end_date_desc":
                    bidsQuery = bidsQuery.OrderByDescending(b => b.BidEnd);
                    break;
                default:
                    bidsQuery = bidsQuery.OrderBy(b => b.BidName);
                    break;
            }

            int pageSize = 10;
            var pagedBids = await PaginatedList<Bid>.CreateAsync(bidsQuery.AsNoTracking(), page ?? 1, pageSize);

            return View(pagedBids);
        }



        // GET: Bids/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bids == null)
            {
                return NotFound();
            }

            var bid = await _context.Bids
                .Include(b => b.Project)
                .Include(b => b.Materials)
                .Include(b => b.Labours)
                .Include(b => b.StaffBids)
                    .ThenInclude(sb => sb.Staff) // Include Staff entity
                .FirstOrDefaultAsync(m => m.BidID == id);

            if (bid == null)
            {
                return NotFound();
            }

            return View(bid);
        }

        // GET: Bids/Approval/5
        public async Task<IActionResult> Approval(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bid = await _context.Bids
                .Include(b => b.Project)
                .Include(b => b.Materials)
                .Include(b => b.Labours)
                .Include(b => b.StaffBids)
                    .ThenInclude(sb => sb.Staff) // Include Staff entity
                .FirstOrDefaultAsync(m => m.BidID == id);
            if (bid == null)
            {
                return NotFound();
            }

            return View(bid);
        }

        // POST: Bids/Approval/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approval(int id, string notes, bool clientApproval, bool nbdApproval)
        {
            var bid = await _context.Bids
                .Include(b => b.Project)
                .Include(b => b.Materials)
                .Include(b => b.Labours)
                .Include(b => b.StaffBids)
                    .ThenInclude(sb => sb.Staff) // Include Staff entity
                .FirstOrDefaultAsync(m => m.BidID == id);

            if (bid == null)
            {
                return NotFound();
            }

            bid.Notes = notes;
            bid.ClientApproved = clientApproval;
            bid.NBDApproved = nbdApproval;

            _context.Update(bid);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Bids/Create
        public IActionResult Create()
        {
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ProjectID", "ProjectName");
            //Populate the staff dropdown
            ViewData["StaffID"] = new MultiSelectList(_context.Staffs, "StaffID", "StaffSummary");
            return View();
        }

        // POST: Bids/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BidID,BidName,BidStart,BidEnd,MaterialID,MaterialType,MaterialQuantity,MaterialDescription,MaterialSize,MaterialPrice,LabourID,LabourHours,LabourDescription,LabourPrice,ProjectID")] Bid bid, List<int> StaffIDList, Material material, Labour labour)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bid);
                await _context.SaveChangesAsync();

                // Now bid has an ID assigned by the database

                // Assign the Bid ID to Material and Labour
                material.BidID = bid.BidID;
                labour.BidID = bid.BidID;

                _context.Add(material);
                _context.Add(labour);

                // Associate staff members with the bid
                if (StaffIDList != null)
                {
                    foreach (var staffID in StaffIDList)
                    {
                        _context.Add(new StaffBid { StaffID = staffID, BidID = bid.BidID });
                    }
                }

                TempData["AlertMessageBid"] = "Bid Successfully Added";
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ProjectID", "ProjectName", bid.ProjectID);
            ViewData["StaffID"] = new SelectList(_context.Staffs, "StaffID", "StaffSummary", bid.StaffBids);
            return View(bid);
        }

        // GET: Bids/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bids == null)
            {
                return NotFound();
            }

            var bid = await _context.Bids
                .Include(b => b.Project)
                .Include(b => b.Materials)
                .Include(b => b.Labours)
                .FirstOrDefaultAsync(m => m.BidID == id);
            if (bid == null)
            {
                return NotFound();
            }
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ProjectID", "ProjectName", bid.ProjectID);
            return View(bid);
        }

        // POST: Bids/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BidID,BidName,BidStart,BidEnd,MaterialID,MaterialType,MaterialQuantity,MaterialDescription,MaterialSize,MaterialPrice,LabourID,LabourHours,LabourDescription,LabourPrice,ProjectID")] Bid bid, Material material, Labour labour)
        {
            if (id != bid.BidID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Retrieve existing material and labor records
                    var existingMaterial = await _context.Materials.FirstOrDefaultAsync(m => m.BidID == bid.BidID);
                    var existingLabour = await _context.Labours.FirstOrDefaultAsync(l => l.BidID == bid.BidID);

                    if (existingMaterial != null)
                    {
                        // Update existing material properties
                        existingMaterial.MaterialType = material.MaterialType;
                        existingMaterial.MaterialQuantity = material.MaterialQuantity;
                        existingMaterial.MaterialDescription = material.MaterialDescription;
                        existingMaterial.MaterialSize = material.MaterialSize;
                        existingMaterial.MaterialPrice = material.MaterialPrice;

                        _context.Update(existingMaterial);
                    }

                    if (existingLabour != null)
                    {
                        // Update existing labor properties
                        existingLabour.LabourHours = labour.LabourHours;
                        existingLabour.LabourDescription = labour.LabourDescription;
                        existingLabour.LabourPrice = labour.LabourPrice;

                        _context.Update(existingLabour);
                    }

                    // Update bid properties
                    _context.Update(bid);

                    TempData["AlertMessageBidEdit"] = "Bid Successfully Edited";
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BidExists(bid.BidID))
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
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ProjectID", "ProjectName", bid.ProjectID);
            return View(bid);
        }

        // GET: Bids/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bids == null)
            {
                return NotFound();
            }

            var bid = await _context.Bids
                .Include(b => b.Project)
                .FirstOrDefaultAsync(m => m.BidID == id);
            if (bid == null)
            {
                return NotFound();
            }

            return View(bid);
        }

        // POST: Bids/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bids == null)
            {
                return Problem("Entity set 'NBDContext.Bids'  is null.");
            }
            var bid = await _context.Bids.FindAsync(id);
            if (bid != null)
            {
                _context.Bids.Remove(bid);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BidExists(int id)
        {
            return _context.Bids.Any(e => e.BidID == id);
        }
    }
}