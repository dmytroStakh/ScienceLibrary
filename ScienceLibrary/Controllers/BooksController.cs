using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScienceLibrary.Dal;
using ScienceLibrary.Data;
using ScienceLibrary.Models;
using ScienceLibrary.Services;

namespace ScienceLibrary.Controllers
{
    public class BooksController : Controller
    {
        private UnitOfWork unitOfWork;
        public BooksController(LibraryContext _context)
        {
            unitOfWork = new UnitOfWork(_context);
        }

        // GET: Books
        public ActionResult Index()
        {
            var libraryContext = unitOfWork.BookCopyRepository._context.Books.Include(b => b.BookCopies);
            return View(libraryContext);
        }

        // GET: Books/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = unitOfWork.BookRepository.GetById(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Title,Author")] Book book)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.BookRepository.Insert(book);
                unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = unitOfWork.BookRepository.GetById(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Title,Author")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.BookRepository.Update(book);
                    unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (unitOfWork.BookRepository.GetById(book.Id) == null)
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
            return View(book);
        }

        // GET: Books/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = unitOfWork.BookRepository.GetById(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int bookId)
        {
            unitOfWork.BookRepository.Delete(bookId);

            unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult GetInAlfabetOrder() 
        {
            return View(unitOfWork.BookRepository.GetBooksInAlfabetOrder());
        }

        public IActionResult ExportEmployeeData()
        {
            //code to get employee list
            var employeeData = unitOfWork.BookRepository.GetAll();
            var fileDownloadName = "books.csv";
            return new BooksCsvResult(employeeData, fileDownloadName);
        }
    }
}
