using Microsoft.AspNetCore.Mvc;
using ScienceLibrary.Dal;
using ScienceLibrary.Data;
using System.Dynamic;

namespace ScienceLibrary.Controllers
{
    public class BookManagmentController : Controller
    {
        private UnitOfWork unitOfWork;
        public BookManagmentController(LibraryContext _context)
        {
            unitOfWork = new UnitOfWork(_context);
        }
        public IActionResult Index()
        {
            ViewBag.Message = "Welcome to my demo!";
            dynamic mymodel = new ExpandoObject();
            mymodel.BooksInAlfabetOrder = unitOfWork.BookRepository.GetBooksInAlfabetOrder();
            mymodel.AllBooks = unitOfWork.BookRepository.GetAllBooks();
            return View(mymodel);
        }
    }
}
