using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScienceLibrary.Dal;
using ScienceLibrary.Data;
using ScienceLibrary.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ScienceLibrary.Controllers
{
    public class BookReservesController : Controller
    {
        private UnitOfWork unitOfWork;
        public BookReservesController(LibraryContext _context)
        {
            unitOfWork = new UnitOfWork(_context);
        }

        // GET: BookReserves
        public IActionResult Index()
        {
            var libraryContext = unitOfWork.BookReserveRepository._context.BookReserves.Include(b => b.BookCopy.Book);
            return View(libraryContext.ToList());
        }

        // GET: BookReserves/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookReserve = unitOfWork.BookReserveRepository._context.BookReserves
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
            ViewData["BookCopyId"] = new SelectList(unitOfWork.BookReserveRepository._context.BookCopies, "Id", "Id");
            return View();
        }

        // POST: BookReserves/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,CreatedDate,BookCopyId")] BookReserve bookReserve)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.BookReserveRepository.Insert(bookReserve);
                unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookCopyId"] = new SelectList(unitOfWork.BookReserveRepository._context.BookCopies, "Id", "Id", bookReserve.BookCopyId);
            return View(bookReserve);
        }

        // GET: BookReserves/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookReserve = unitOfWork.BookReserveRepository.GetById(id);
            if (bookReserve == null)
            {
                return NotFound();
            }
            ViewData["BookCopyId"] = new SelectList(unitOfWork.BookReserveRepository._context.BookCopies, "Id", "Id", bookReserve.BookCopyId);
            return View(bookReserve);
        }

        // POST: BookReserves/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,CreatedDate,BookCopyId")] BookReserve bookReserve)
        {
            if (id != bookReserve.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.BookReserveRepository.Update(bookReserve);
                    unitOfWork.Save();
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
            ViewData["BookCopyId"] = new SelectList(unitOfWork.BookReserveRepository._context.BookCopies, "Id", "Id", bookReserve.BookCopyId);
            return View(bookReserve);
        }

        // GET: BookReserves/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookReserve = unitOfWork.BookReserveRepository._context.BookReserves
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
        public IActionResult DeleteConfirmed(int id)
        {
            unitOfWork.BookReserveRepository.Delete(id);
            unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool BookReserveExists(int id)
        {
            return unitOfWork.BookReserveRepository._context.BookReserves.Any(e => e.Id == id);
        }
    }
}
