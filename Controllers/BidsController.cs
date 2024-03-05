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
    public class BidsController : Controller
    {
        private readonly NBDContext _context;

        public BidsController(NBDContext context)
        {
            _context = context;
        }

        // GET: Bids
        public async Task<IActionResult> Index()
        {
            var nBDContext = _context.Bids.Include(b => b.project);
            return View(await nBDContext.ToListAsync());
        }

        // GET: Bids/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bids == null)
            {
                return NotFound();
            }

            var bid = await _context.Bids
                .Include(b => b.project)
                .FirstOrDefaultAsync(m => m.BidID == id);
            if (bid == null)
            {
                return NotFound();
            }

            return View(bid);
        }

        // GET: Bids/Create
        public IActionResult Create()
        {
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ProjectID", "ProjectName");
            return View();
        }

        // POST: Bids/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BidID,BidName,BidStart,BidEnd,ProjectID")] Bid bid)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bid);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ProjectID", "ProjectName", bid.ProjectID);
            return View(bid);
        }

        // GET: Bids/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bids == null)
            {
                return NotFound();
            }

            var bid = await _context.Bids.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("BidID,BidName,BidStart,BidEnd,ProjectID")] Bid bid)
        {
            if (id != bid.BidID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bid);
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
                .Include(b => b.project)
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
