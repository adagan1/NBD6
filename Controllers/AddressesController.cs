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
using PagedList;

namespace NBD6.Controllers
{
    public class AddressesController : Controller
    {
        private readonly NBDContext _context;

        public AddressesController(NBDContext context)
        {
            _context = context;
        }

        // GET: Addresses
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CountrySortParm = String.IsNullOrEmpty(sortOrder) ? "country_desc" : "";
            ViewBag.ProvinceSortParm = sortOrder == "Province" ? "province_desc" : "Province";
            ViewBag.PostalSortParm = sortOrder == "Postal" ? "postal_desc" : "Postal";
            ViewBag.StreetSortParm = sortOrder == "Street" ? "street_desc" : "Street";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var addresses = from a in _context.Addresses
                            select a;

            if (!String.IsNullOrEmpty(searchString))
            {
                addresses = addresses.Where(a => a.Country.ToLower().Contains(searchString.ToLower())
                                           || a.Province.ToLower().Contains(searchString.ToLower())
                                           || a.Postal.ToLower().Contains(searchString.ToLower())
                                           || a.Street.ToLower().Contains(searchString.ToLower()));
            }

            switch (sortOrder)
            {
                case "country_desc":
                    addresses = addresses.OrderByDescending(a => a.Country);
                    break;
                case "Province":
                    addresses = addresses.OrderBy(a => a.Province);
                    break;
                case "province_desc":
                    addresses = addresses.OrderByDescending(a => a.Province);
                    break;
                case "Postal":
                    addresses = addresses.OrderBy(a => a.Postal);
                    break;
                case "postal_desc":
                    addresses = addresses.OrderByDescending(a => a.Postal);
                    break;
                case "Street":
                    addresses = addresses.OrderBy(a => a.Street);
                    break;
                case "street_desc":
                    addresses = addresses.OrderByDescending(a => a.Street);
                    break;
                default:
                    addresses = addresses.OrderBy(a => a.Country);
                    break;
            }

            int pageSize = 10;
            return View(await PaginatedList<Address>.CreateAsync(addresses.AsNoTracking(), page ?? 1, pageSize));
        }


        // GET: Addresses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Addresses == null)
            {
                return NotFound();
            }

            var address = await _context.Addresses
                .FirstOrDefaultAsync(m => m.AddressID == id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // GET: Addresses/Create
        public IActionResult Create()
        {
            TempData["AddressUrl"] = HttpContext.Request.Headers["Referer"].ToString();
            return View();
        }

        // POST: Addresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AddressID,Country,Province,Postal,Street")] Address address)
        {
            if (ModelState.IsValid)
            {
                _context.Add(address);
                await _context.SaveChangesAsync();

                TempData["NewAddressID"] = address.AddressID; // Add address ID to TempData
                TempData["NewAddressSummary"] = address.AddressSummary; // Add address summary to TempData

                // Check if TempData contains the ReferrerUrl
                if (TempData.ContainsKey("AddressUrl"))
                {
                    // Redirect back to the ReferrerUrl
                    return Redirect(TempData["AddressUrl"].ToString());
                }
                else
                {
                    TempData.Clear();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(address);
        }

        // GET: Addresses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Addresses == null)
            {
                return NotFound();
            }

            var address = await _context.Addresses.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }
            return View(address);
        }

        // POST: Addresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AddressID,Country,Province,Postal,Street")] Address address)
        {
            if (id != address.AddressID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(address);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddressExists(address.AddressID))
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
            return View(address);
        }

        // GET: Addresses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Addresses == null)
            {
                return NotFound();
            }

            var address = await _context.Addresses
                .FirstOrDefaultAsync(m => m.AddressID == id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Addresses == null)
            {
                return Problem("Entity set 'NBDContext.Addresses'  is null.");
            }
            var address = await _context.Addresses.FindAsync(id);
            if (address != null)
            {
                _context.Addresses.Remove(address);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddressExists(int id)
        {
          return _context.Addresses.Any(e => e.AddressID == id);
        }
    }
}
