using LibraryManagementSystem.DAL.Models;
using LibraryManagementSystem.DAL.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.UI.ViewComponents
{
    public class FavoriteBooksViewComponent : ViewComponent
    {
        private readonly IGenericRepository<Book> _repository;

        public FavoriteBooksViewComponent(IGenericRepository<Book> repository)
        {
            _repository = repository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var favoritesSession = HttpContext.Session.GetString("Favorites");

            List<int> currentFavorites = new List<int>();

            if (favoritesSession != null)
            {
                foreach (string item in favoritesSession.Split(","))
                {
                    currentFavorites.Add(int.Parse(item));
                }
            }

            var viewModel = new List<Book>();

            foreach (int favoriteId in currentFavorites)
            {
                viewModel.Add(await _repository.GetById(favoriteId));
            }

            return View(viewModel);
        }
    }
}
