using LibraryManagementSystem.DAL.Models;
using LibraryManagementSystem.DAL.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Drawing2D;

namespace LibraryManagementSystem.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly IGenericRepository<Category> _categoryRepository;

        public CategoriesController(IGenericRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }


        public async Task<IActionResult> Index()
        {
            var brands = await _categoryRepository.GetAllAsync();
            return View(brands.ToList());
        }


        public async Task<IActionResult> Create()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category model)
        {
            await _categoryRepository.CreateAsync(model);
            TempData["Success"] = "Category succesfully added";
            return RedirectToAction("Index");
        }
    }
}
