using LibraryManagementSystem.DAL.Models;
using LibraryManagementSystem.DAL.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.UI.Controllers
{
    public class BooksController : Controller
    {
        private readonly IGenericRepository<Book> _repository;

        public BooksController(IGenericRepository<Book> repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> AddToFavorites(int bookId)
        {
            var book = await _repository.GetById(bookId);

            var favoritesSession = HttpContext.Session.GetString("Favorites");

            List<string> currentFavorites = new List<string>();

            if (favoritesSession != null)
            {
                foreach (string item in favoritesSession.Split(","))
                {
                    currentFavorites.Add(item);
                }
            }

            if (!currentFavorites.Contains(bookId.ToString()))
            {
                currentFavorites.Add(bookId.ToString());
            }

            HttpContext.Session.SetString("Favorites", string.Join(",", currentFavorites));

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Index() => View();
    }
}
