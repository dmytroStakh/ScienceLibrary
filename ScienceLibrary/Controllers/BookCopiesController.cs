using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScienceLibrary.Dal;
using ScienceLibrary.Data;
using ScienceLibrary.Models;

namespace ScienceLibrary.Controllers
{
    public class BookCopiesController : Controller
    {
        private UnitOfWork unitOfWork;
        public BookCopiesController(LibraryContext _context)
        {
            unitOfWork = new UnitOfWork(_context);
        }

        // GET: BookCopies
        public IActionResult Index()
        {
            var libraryContext = unitOfWork.BookCopyRepository._context.BookCopies.Include(b => b.Book);
            return View(libraryContext.ToList());
        }

        // GET: BookCopies/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookCopy = unitOfWork.BookCopyRepository._context.BookCopies
                .Include(b => b.Book)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookCopy == null)
            {
                return NotFound();
            }

            return View(bookCopy);
        }

        // GET: BookCopies/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(unitOfWork.BookCopyRepository._context.Books, "Id", "Id");
            return View();
        }

        // POST: BookCopies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,IsReserved,IsArchived,BookId")] BookCopy bookCopy)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.BookCopyRepository.Insert(bookCopy);
                unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(unitOfWork.BookCopyRepository._context.Books, "Id", "Id", bookCopy.BookId);
            return View(bookCopy);
        }

        // GET: BookCopies/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var bookCopy = unitOfWork.BookCopyRepository.GetById(id);
            if (bookCopy == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(unitOfWork.BookCopyRepository._context.Books, "Id", "Id", bookCopy.BookId);
            return View(bookCopy);
        }

        // POST: BookCopies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,IsReserved,IsArchived,BookId")] BookCopy bookCopy)
        {
            if (id != bookCopy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.BookCopyRepository.Update(bookCopy);
                    unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookCopyExists(bookCopy.Id))
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
            ViewData["BookId"] = new SelectList(unitOfWork.BookCopyRepository._context.Books, "Title", "Title", bookCopy.BookId);
            return View(bookCopy);
        }

        // GET: BookCopies/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookCopy = unitOfWork.BookCopyRepository._context.BookCopies
                .Include(b => b.Book)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookCopy == null)
            {
                return NotFound();
            }

            return View(bookCopy);
        }

        // POST: BookCopies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            unitOfWork.BookCopyRepository.Delete(id);

            unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool BookCopyExists(int id)
        {
            return unitOfWork.BookCopyRepository.GetById(id) != null;
        }
    }
}
