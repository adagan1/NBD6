using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin, Management, Designer")]
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchTerm, int? page, string approvalFilter = "All", string timeframeFilter = "AllTime", DateTime? startDate = null, DateTime? endDate = null)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.ProjectNameSortParm = sortOrder == "Project Name" ? "project_name_desc" : "Project Name";
            ViewBag.DateSortParm = sortOrder == "Start Date" ? "start_date_desc" : "Start Date";
            ViewBag.EndDateSortParm = sortOrder == "End Date" ? "end_date_desc" : "End Date";
            ViewBag.CurrentApprovalFilter = approvalFilter;

            if (searchTerm != null)
            {
                page = 1;
            }
            else
            {
                searchTerm = currentFilter;
            }

            ViewBag.CurrentFilter = searchTerm;

            var bidsQuery = _context.Bids
                .Include(b => b.Project)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                var lowerCaseSearchTerm = searchTerm.ToLower();
                bidsQuery = bidsQuery.Where(b => b.BidName.ToLower().Contains(lowerCaseSearchTerm)
                                              || b.Project.ProjectName.ToLower().Contains(lowerCaseSearchTerm));
            }

            // Filtering by approval status
            if (!string.IsNullOrEmpty(approvalFilter) && approvalFilter != "All")
            {
                switch (approvalFilter)
                {
                    case "ClientApproved":
                        bidsQuery = bidsQuery.Where(b => b.ClientApproved == true);
                        break;
                    case "NBDApproved":
                        bidsQuery = bidsQuery.Where(b => b.NBDApproved == true);
                        break;
                    case "Declined":
                        bidsQuery = bidsQuery.Where(b => b.BidDeclined == true);
                        break;
                }
            }

            // Filter by timeframe
            if (timeframeFilter != "AllTime")
            {
                DateTime today = DateTime.Today;
                switch (timeframeFilter)
                {
                    case "ThisWeek":
                        startDate = today.AddDays(-(int)today.DayOfWeek);
                        endDate = startDate.Value.AddDays(6);
                        break;
                    case "ThisMonth":
                        startDate = new DateTime(today.Year, today.Month, 1);
                        endDate = startDate.Value.AddMonths(1).AddDays(-1);
                        break;
                    case "Custom":
                        break;
                }

                bidsQuery = bidsQuery.Where(b => b.BidStart >= startDate && b.BidEnd <= endDate);
            }

            // Retrieve the filtered bids
            var filteredBids = await bidsQuery.ToListAsync();

            decimal totalValue = 0;
            if (approvalFilter == "ClientApproved")
            {
                totalValue = filteredBids.Where(b => b.ClientApproved).Sum(b => b.ProjectAmount);
            }
            else if (approvalFilter == "NBDApproved")
            {
                totalValue = filteredBids.Where(b => b.NBDApproved).Sum(b => b.ProjectAmount);
            }
            else if (approvalFilter == "Declined")
            {
                totalValue = filteredBids.Where(b => b.BidDeclined).Sum(b => b.ProjectAmount);
            }
            else
            {
                totalValue = filteredBids.Sum(b => b.ProjectAmount);
            }

            ViewBag.TotalValue = totalValue;
            switch (sortOrder)
            {
                case "name_desc":
                    filteredBids = filteredBids.OrderByDescending(b => b.BidName).ToList();
                    break;
                case "Project Name":
                    filteredBids = filteredBids.OrderBy(b => b.Project.ProjectName).ToList();
                    break;
                case "project_name_desc":
                    filteredBids = filteredBids.OrderByDescending(b => b.Project.ProjectName).ToList();
                    break;
                case "Start Date":
                    filteredBids = filteredBids.OrderBy(b => b.BidStart).ToList();
                    break;
                case "start_date_desc":
                    filteredBids = filteredBids.OrderByDescending(b => b.BidStart).ToList();
                    break;
                case "End Date":
                    filteredBids = filteredBids.OrderBy(b => b.BidEnd).ToList();
                    break;
                case "end_date_desc":
                    filteredBids = filteredBids.OrderByDescending(b => b.BidEnd).ToList();
                    break;
                default:
                    filteredBids = filteredBids.OrderBy(b => b.BidName).ToList();
                    break;
            }

            int pageSize = 10;
            var pagedBids = await PaginatedList<Bid>.CreateAsync(bidsQuery.AsNoTracking(), page ?? 1, pageSize);

            return View(pagedBids);
        }

        // GET: Bids/Details/5
        [Authorize(Roles = "Admin, Management, Designer")]
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
        [Authorize(Roles = "Admin, Management")]
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
        [Authorize(Roles = "Admin, Management")]
        public async Task<IActionResult> Approval(int id, string notes, bool clientApproval, bool nbdApproval, bool bidDeclined)
        {
            var bid = await _context.Bids
                .Include(b => b.Project)
                .Include(b => b.Materials)
                .Include(b => b.Labours)
                .Include(b => b.StaffBids)
                    .ThenInclude(sb => sb.Staff)
                .FirstOrDefaultAsync(m => m.BidID == id);

            if (bid == null)
            {
                return NotFound();
            }

            bid.Notes = notes;
            bid.ClientApproved = clientApproval;
            bid.NBDApproved = nbdApproval;
            bid.BidDeclined = bidDeclined;

            if (bid.BidDeclined == true)
            {
                TempData["AlertMessageDecline"] = "Bid Successfully Declined";
            }
            else if (bid.ClientApproved == true || bid.NBDApproved == true)
            {
                TempData["AlertMessageApproval"] = "Bid Successfully Approved";
            }
            _context.Update(bid);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Bids/Create
        [Authorize(Roles = "Admin, Management, Designer")]
        public IActionResult Create()
        {
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ProjectID", "ProjectName");
            ViewData["StaffID"] = new MultiSelectList(_context.Staffs, "StaffID", "StaffSummary");

            // Populate ViewBag.MaterialTypes with Material Types
            ViewBag.MaterialTypes = new List<SelectListItem>
            {
                new SelectListItem { Value = "Plants", Text = "Plants" },
                new SelectListItem { Value = "Pottery", Text = "Pottery" },
                new SelectListItem { Value = "Tools", Text = "Tools" },
                new SelectListItem { Value = "Materials", Text = "Materials" }
                
            };

            // Populate ViewBag.LabourDescriptions with Labour Descriptions
            ViewBag.LabourDescriptions = new List<string>
            {
                "Production Worker",
                "Designer",
                "Equipment Operator",
                "Botanist"
            };

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Management, Designer")]
        public async Task<IActionResult> Create([Bind("BidID,BidName,BidStart,BidEnd,ProjectID")] Bid bid, List<Material> materials, List<Labour> labours, List<int> StaffIDList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bid);
                await _context.SaveChangesAsync();

                if (StaffIDList != null)
                {
                    foreach (var staffID in StaffIDList)
                    {
                        _context.Add(new StaffBid { StaffID = staffID, BidID = bid.BidID });
                    }
                }

                // Associate materials with the bid
                if (materials != null && materials.Any())
                {
                    foreach (var material in materials)
                    {
                        material.BidID = bid.BidID;
                        _context.Add(material);
                    }
                }
                Console.WriteLine(materials);

                // Associate labours with the bid
                if (labours != null && labours.Any())
                {
                    foreach (var labour in labours)
                    {
                        labour.BidID = bid.BidID;
                        _context.Add(labour);
                    }
                }
                Console.WriteLine(labours);
                TempData["AlertMessageBid"] = "Bid Successfully Added";
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // If the model state is not valid, repopulate necessary data for the view
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ProjectID", "ProjectName", bid.ProjectID);
            ViewData["StaffID"] = new SelectList(_context.Staffs, "StaffID", "StaffSummary", bid.StaffBids);
            return View(bid);
        }

        // GET: Bids/Edit/5
        [Authorize(Roles = "Admin, Management, Designer")]
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
                .Include(b => b.StaffBids)
                .Include(b => b.Staff)
                .FirstOrDefaultAsync(m => m.BidID == id);
            if (bid == null)
            {
                return NotFound();
            }
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ProjectID", "ProjectName", bid.ProjectID);
            ViewData["StaffID"] = new SelectList(_context.Staffs, "StaffID", "StaffSummary", bid.StaffBids);

            return View(bid);
        }

        // POST: Bids/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Management, Designer")]
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

        private bool BidExists(int id)
        {
            return _context.Bids.Any(e => e.BidID == id);
        }
    }
}