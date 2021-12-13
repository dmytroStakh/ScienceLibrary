using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScienceLibrary.Data;
using ScienceLibrary.Models;

namespace ScienceLibrary.Controllers
{
    public class BookReservesController : Controller
    {
        private readonly LibraryContext _context;

        public BookReservesController(LibraryContext context)
        {
            _context = context;
        }

        // GET: BookReserves
        public async Task<IActionResult> Index()
        {
            var libraryContext = _context.BookReserves.Include(b => b.BookCopy);
            return View(await libraryContext.ToListAsync());
        }

        // GET: BookReserves/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookReserve = await _context.BookReserves
                .Include(b => b.BookCopy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookReserve == null)
            {
                return NotFound();
            }

            return View(bookReserve);
        }

        // GET: BookReserves/Create
        public IActionResult Create()
        {
            ViewData["BookCopyId"] = new SelectList(_context.BookCopies, "Id", "Id");
            return View();
        }

        // POST: BookReserves/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CreatedDate,BookCopyId")] BookReserve bookReserve)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookReserve);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookCopyId"] = new SelectList(_context.BookCopies, "Id", "Id", bookReserve.BookCopyId);
            return View(bookReserve);
        }

        // GET: BookReserves/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookReserve = await _context.BookReserves.FindAsync(id);
            if (bookReserve == null)
            {
                return NotFound();
            }
            ViewData["BookCopyId"] = new SelectList(_context.BookCopies, "Id", "Id", bookReserve.BookCopyId);
            return View(bookReserve);
        }

        // POST: BookReserves/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CreatedDate,BookCopyId")] BookReserve bookReserve)
        {
            if (id != bookReserve.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookReserve);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookReserveExists(bookReserve.Id))
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
            ViewData["BookCopyId"] = new SelectList(_context.BookCopies, "Id", "Id", bookReserve.BookCopyId);
            return View(bookReserve);
        }

        // GET: BookReserves/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookReserve = await _context.BookReserves
                .Include(b => b.BookCopy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookReserve == null)
            {
                return NotFound();
            }

            return View(bookReserve);
        }

        // POST: BookReserves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookReserve = await _context.BookReserves.FindAsync(id);
            _context.BookReserves.Remove(bookReserve);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookReserveExists(int id)
        {
            return _context.BookReserves.Any(e => e.Id == id);
        }
    }
}
