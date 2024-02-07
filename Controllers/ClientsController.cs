using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NBD6.Data;
using NBD6.Models;
using NBD6.Utilities;

namespace NBD6.Controllers
{
    public class ClientsController : Controller
    {
        private readonly NBDContext _context;

        public ClientsController(NBDContext context)
        {
            _context = context;
        }


        // GET: Clients
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchTerm, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CompanyNameSortParm = sortOrder == "companyname_asc" ? "companyname_desc" : "companyname_asc";
            ViewBag.ClientNameSortParm = sortOrder == "clientname_asc" ? "clientname_desc" : "clientname_asc";
            ViewBag.ContactSortParm = sortOrder == "contact_asc" ? "contact_desc" : "contact_asc";
            ViewBag.PhoneSortParm = sortOrder == "phone_asc" ? "phone_desc" : "phone_asc";
            ViewBag.CountrySortParm = sortOrder == "country_asc" ? "country_desc" : "country_asc";

            if (searchTerm != null)
            {
                page = 1;
            }
            else
            {
                searchTerm = currentFilter;
            }

            ViewBag.CurrentFilter = searchTerm;

            var clientsQuery = _context.Clients.Include(c => c.Address).AsQueryable();

            if (!String.IsNullOrEmpty(searchTerm))
            {
                var lowerCaseSearchTerm = searchTerm.ToLower();

                clientsQuery = clientsQuery.Where(c =>
                    c.CompanyName.ToLower().Contains(lowerCaseSearchTerm)
                    || c.ClientName.ToLower().Contains(lowerCaseSearchTerm)
                    || c.ClientContact.ToLower().Contains(lowerCaseSearchTerm)
                    || c.ClientPhone.ToLower().Contains(lowerCaseSearchTerm)
                    || c.Address.Country.ToLower().Contains(lowerCaseSearchTerm)
                );
            }

            // Apply sorting based on sortOrder
            switch (sortOrder)
            {
                case "companyname_asc":
                    clientsQuery = clientsQuery.OrderBy(c => c.CompanyName);
                    break;
                case "companyname_desc":
                    clientsQuery = clientsQuery.OrderByDescending(c => c.CompanyName);
                    break;
                case "clientname_asc":
                    clientsQuery = clientsQuery.OrderBy(c => c.ClientName);
                    break;
                case "clientname_desc":
                    clientsQuery = clientsQuery.OrderByDescending(c => c.ClientName);
                    break;
                case "contact_asc":
                    clientsQuery = clientsQuery.OrderBy(c => c.ClientContact);
                    break;
                case "contact_desc":
                    clientsQuery = clientsQuery.OrderByDescending(c => c.ClientContact);
                    break;
                case "phone_asc":
                    clientsQuery = clientsQuery.OrderBy(c => c.ClientPhone);
                    break;
                case "phone_desc":
                    clientsQuery = clientsQuery.OrderByDescending(c => c.ClientPhone);
                    break;
                case "country_asc":
                    clientsQuery = clientsQuery.OrderBy(c => c.Address.Country);
                    break;
                case "country_desc":
                    clientsQuery = clientsQuery.OrderByDescending(c => c.Address.Country);
                    break;
                default:
                    clientsQuery = clientsQuery.OrderBy(c => c.ClientName);
                    break;
            }

            int pageSize = 10;
            var pagedData = await PaginatedList<Client>.CreateAsync(clientsQuery.AsNoTracking(), page ?? 1, pageSize);

            return View(pagedData);
        }


        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .Include(c => c.Address)
                .FirstOrDefaultAsync(m => m.ClientID == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            TempData["ClientUrl"] = HttpContext.Request.Headers["Referer"].ToString();
            ViewData["AddressID"] = new SelectList(_context.Addresses, "AddressID", "AddressSummary");
            return View();

        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClientID,CompanyName,ClientName,ClientContact,ClientPhone,AddressID")] Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Add(client);
                await _context.SaveChangesAsync();

                TempData["NewClientID"] = client.ClientID;
                TempData["NewClientSummary"] = client.ClientSummary;

                if (TempData.ContainsKey("AddressUrl"))
                {
                    if (TempData.ContainsKey("ProjectKey"))
                    {
                        TempData.Remove("ProjectKey");
                        return RedirectToAction("Create", "Projects");
                    }
                    else
                    {
                        // Redirect back to the ClientUrl
                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                {
                    // Redirect back to the ClientUrl
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["AddressID"] = new SelectList(_context.Addresses, "AddressID", "AddressSummary", client.AddressID);
            return View(client);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .Include(c => c.Address) // Include the related Address data
                .FirstOrDefaultAsync(m => m.ClientID == id);

            if (client == null)
            {
                return NotFound();
            }
            ViewData["AddressID"] = new SelectList(_context.Addresses, "AddressID", "AddressSummary", client.AddressID);
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClientID,CompanyName,ClientName,ClientContact,ClientPhone,AddressID")] Client client)
        {
            if (id != client.ClientID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.ClientID))
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
            ViewData["AddressID"] = new SelectList(_context.Addresses, "AddressID", "AddressSummary", client.AddressID);
            return View(client);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .Include(c => c.Address)
                .FirstOrDefaultAsync(m => m.ClientID == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Clients == null)
            {
                return Problem("Entity set 'NBDContext.Clients'  is null.");
            }
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.ClientID == id);
        }
    }
}
