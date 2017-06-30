using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Librarie.Models;
using Microsoft.AspNetCore.Mvc;
using Librarie.Models.LibraryViewModels;
using Librarie.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Librarie.Controllers
{
    [Authorize]
    public class LibraryController : Controller
    {
        private ILibraryService _libraryService;

        public LibraryController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
          
        }

        public IActionResult Index()
        {
            var books = _libraryService.getBooks(); //luam cartile din DB prin intermediul unui LibraryService

            var transactions = _libraryService.GetAllTransactions();

            var model = new LibraryViewModel(books);

            model.informations.NumberOfBooks = model.books.Count;
            model.informations.Date = DateTime.Now;

            return View(model);
        }
        public IActionResult Borrow(int id)
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            bool isSuccessful = _libraryService.Borrow(userId, id);
            var book = _libraryService.getBooks().Single(item => item.id ==id);//itereaza prin toate item-urile si returneaza pe singurul care are acel id
            var borrowViewModel = new BorrowViewModel
            {
                IsSuccessful = isSuccessful,
                BookName = book.title
            };
            return View(borrowViewModel);
            
        }
    }
}