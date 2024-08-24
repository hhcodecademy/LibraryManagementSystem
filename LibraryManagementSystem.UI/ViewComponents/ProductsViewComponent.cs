using LibraryManagementSystem.DAL.Models;
using LibraryManagementSystem.DAL.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.UI.ViewComponents
{
    public class ProductsViewComponent : ViewComponent
    {
        private readonly IGenericRepository<Book> _bookRepository;
        public ProductsViewComponent(IGenericRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            var booksList = books.ToList();
            var favoritesSession = HttpContext.Session.GetString("Favorites");
            List<int> currentFavorites = new List<int>();

            if (favoritesSession != null)
            {
                foreach (string item in favoritesSession.Split(","))
                    currentFavorites.Add(int.Parse(item));
            }

            var viewModel = new List<Book>();

            foreach (int favoriteId in currentFavorites)
            {
                var favoritebook = await _bookRepository.GetById(favoriteId);
                viewModel.Add(favoritebook);
                booksList.Remove(favoritebook);
            }

            return View(booksList);
        }
    }
}
