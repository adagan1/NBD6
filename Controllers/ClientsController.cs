﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin, Management, Designer, Sales")]
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchTerm, int? page)
        {
            var clientsQuery = _context.Clients
                .Include(c => c.Address)
                .Include(c => c.Projects)
                .AsNoTracking()
                .AsQueryable();


            ViewBag.CurrentSort = sortOrder;
            ViewBag.CompanyNameSortParm = sortOrder == "companyname_asc" ? "companyname_desc" : "companyname_asc";
            ViewBag.FirstNameSortParm = sortOrder == "firstname_asc" ? "firstname_desc" : "firstname_asc";
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

            if (!String.IsNullOrEmpty(searchTerm))
            {
                var lowerCaseSearchTerm = searchTerm.ToLower();

                clientsQuery = clientsQuery.Where(c =>
                    c.CompanyName.ToLower().Contains(lowerCaseSearchTerm)
                    || c.FirstName.ToLower().Contains(lowerCaseSearchTerm)
                    || c.ClientContact.ToLower().Contains(lowerCaseSearchTerm)
                    || c.ClientPhone.ToLower().Contains(lowerCaseSearchTerm)
                    || c.Address.Country.ToLower().Contains(lowerCaseSearchTerm)
                );
            }

            // Apply sorting
            switch (sortOrder)
            {
                case "companyname_asc":
                    clientsQuery = clientsQuery.OrderBy(c => c.CompanyName);
                    break;
                case "companyname_desc":
                    clientsQuery = clientsQuery.OrderByDescending(c => c.CompanyName);
                    break;
                case "firstname_asc":
                    clientsQuery = clientsQuery.OrderBy(c => c.FirstName);
                    break;
                case "firstname_desc":
                    clientsQuery = clientsQuery.OrderByDescending(c => c.FirstName);
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
                    clientsQuery = clientsQuery.OrderBy(c => c.FirstName);
                    break;
            }

            int pageSize = 10;
            var pagedData = await PaginatedList<Client>.CreateAsync(clientsQuery.AsNoTracking(), page ?? 1, pageSize);

            return View(pagedData);
        }


        // GET: Clients/Details/5
        [Authorize(Roles = "Admin, Management, Sales")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .Include(c => c.Address)
                .Include(c => c.Projects)
                .FirstOrDefaultAsync(m => m.ClientID == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        [Authorize(Roles = "Admin, Management, Sales")]
        public IActionResult Create()
        {
            TempData["ClientUrl"] = HttpContext.Request.Headers["Referer"].ToString();
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

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Management, Sales")]
        public async Task<IActionResult> Create([Bind("CompanyName,FirstName,MiddleName,LastName,ClientContact,ClientPhone,Address")] Client client)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Addresses.Add(client.Address);
                    await _context.SaveChangesAsync();

                    // Set the AddressID for the client
                    client.AddressID = client.Address.AddressID;

                    _context.Clients.Add(client);
                    await _context.SaveChangesAsync();

                    TempData["NewClientID"] = client.ClientID;
                    TempData["NewClientSummary"] = client.ClientSummary;
                    TempData["AlertMessageClient"] = "Client Successfully Added";

                    // Check if TempData contains client temp
                    if (TempData.ContainsKey("ClientUrl"))
                    {
                        // Redirect back a page
                        return Redirect(TempData["ClientUrl"].ToString());
                    }
                    else
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error occurred while creating the client.");
                return View(client);
            }

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

            return View(client);
        }

        // GET: Clients/Edit/5
        [Authorize(Roles = "Admin, Management, Sales")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .Include(c => c.Address)
                .Include(c => c.Projects)
                .FirstOrDefaultAsync(m => m.ClientID == id);

            if (client == null)
            {
                return NotFound();
            }

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

            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Management, Sales")]
        public async Task<IActionResult> Edit(int id, [Bind("ClientID,CompanyName,FirstName,MiddleName,LastName,ClientContact,ClientPhone,AddressID,Country,Province,Postal,Street")] Client client, Address address)
        {
            if (id != client.ClientID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Retrieve the existing client including its associated address
                    var existingClient = await _context.Clients
                        .Include(c => c.Address)
                        .FirstOrDefaultAsync(c => c.ClientID == id);

                    if (existingClient == null)
                    {
                        return NotFound();
                    }

                    // Update client properties
                    existingClient.CompanyName = client.CompanyName;
                    existingClient.FirstName = client.FirstName;
                    existingClient.MiddleName = client.MiddleName;
                    existingClient.LastName = client.LastName;
                    existingClient.ClientContact = client.ClientContact;
                    existingClient.ClientPhone = client.ClientPhone;

                    // Update address properties
                    existingClient.Address.Country = address.Country;
                    existingClient.Address.Province = address.Province;
                    existingClient.Address.City = address.City;
                    existingClient.Address.Postal = address.Postal;
                    existingClient.Address.Street = address.Street;

                    TempData["AlertMessageClientEdit"] = "Client Successfully Edited";
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

            return View(client);
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.ClientID == id);
        }
    }

}
