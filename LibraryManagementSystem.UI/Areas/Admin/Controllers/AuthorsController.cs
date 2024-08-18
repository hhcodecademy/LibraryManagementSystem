using LibraryManagementSystem.DAL.Models;
using LibraryManagementSystem.DAL.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace LibraryManagementSystem.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorsController : Controller
    {
        private readonly IGenericRepository<Author> _authorRepository;
        private readonly IToastNotification _toastNotification;
        public AuthorsController(IGenericRepository<Author> authorRepository, IToastNotification toastNotification)
        {
            _authorRepository = authorRepository;
            _toastNotification = toastNotification;
        }


        public async Task<IActionResult> Index()
        {
            var brands = await _authorRepository.GetAllAsync();
            return View(brands.ToList());
        }


        public async Task<IActionResult> Create()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Author model)
        {
            await _authorRepository.CreateAsync(model);
            //TempData["Success"] = "Author succesfully added";
            _toastNotification.AddSuccessToastMessage("Author succesfully added with library");
            return RedirectToAction("Index");
        }
    }
}
