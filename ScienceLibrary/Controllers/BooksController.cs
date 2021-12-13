﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScienceLibrary.Dal;
using ScienceLibrary.Data;
using ScienceLibrary.Models;

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
            return View(unitOfWork.BookRepository.GetAllBooks());
        }

        // GET: Books/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = unitOfWork.BookRepository.GetSBookByID(id);
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
                unitOfWork.BookRepository.AddNewBook(book);
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

            var book = unitOfWork.BookRepository.GetSBookByID(id);
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
                    unitOfWork.BookRepository.UpdateBook(book);
                    unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (unitOfWork.BookRepository.GetSBookByID(book.Id) == null)
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

            var book = unitOfWork.BookRepository.GetSBookByID(id);
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
            unitOfWork.BookRepository.DeleteBook(bookId);

            unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult GetInAlfabetOrder() 
        {
            return View(unitOfWork.BookRepository.GetBooksInAlfabetOrder());
        }
    }
}