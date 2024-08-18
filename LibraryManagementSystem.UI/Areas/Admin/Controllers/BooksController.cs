using LibraryManagementSystem.DAL.Models;
using LibraryManagementSystem.DAL.Repository.Interfaces;
using LibraryManagementSystem.UI.Models;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace LibraryManagementSystem.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BooksController : Controller
    {
        private readonly IGenericRepository<Book> _bookRepository;
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IGenericRepository<Author> _authorRepository;
        private readonly IToastNotification _toastNotification;

        private readonly IWebHostEnvironment _webHostEnvironment;
        public BooksController(IGenericRepository<Book> bookRepository, IGenericRepository<Category> categoryRepository, IGenericRepository<Author> authorRepository, IToastNotification toastNotification, IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
            _authorRepository = authorRepository;
            _toastNotification = toastNotification;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            List<BookVM> bookVMs = new List<BookVM>();
            var books = await _bookRepository.GetAllAsync();
            var models = books.ToList();
            foreach (var model in models)
            {
                var author = await _authorRepository.GetById(model.AuthorId);
                var category = await _categoryRepository.GetById(model.CategoryId);
                bookVMs.Add(new BookVM
                {
                    Id = model.Id,
                    Name = model.Name,
                    PublishDate = model.PublishDate,
                    Description = model.Description,
                    AuthorId = author.Id,
                    AuthorName = author.Name,
                    CategoryId = category.Id,
                    CategoryName = category.Name,
                   Thumbnail = model.Thumbnail 
                });
            }
            return View(bookVMs);
        }
        public async Task<IActionResult> Create()
        {
            BookVM bookVM = new BookVM();
            bookVM.Categories = await _categoryRepository.GetAllAsync();
            bookVM.Authors = await _authorRepository.GetAllAsync();



            return View(bookVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(BookVM model)
        {
            Book book = new Book();
            book.Name = model.Name;
            book.AuthorId = model.AuthorId;
            book.CategoryId = model.CategoryId;
            book.Description = model.Description;
            book.PublishDate = model.PublishDate;

            List<BookImages> bookImages = new();

            foreach (var imageFile in model.ImageFiles)
            {
                string wwwRoot = _webHostEnvironment.WebRootPath;
                string folderName = "uploads";
                string fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";

                string path = Path.Combine(wwwRoot, folderName, fileName);

                using FileStream fileStream = new FileStream(path, FileMode.Create);

                await imageFile.CopyToAsync(fileStream);

                bookImages.Add(new BookImages
                {
                    Directory = folderName,
                    Name = fileName,
                });
            }

            book.BookImages = bookImages;
            book.Thumbnail = Path.Combine(bookImages[0].Directory, bookImages[0].Name);

            await _bookRepository.CreateAsync(book);
            _toastNotification.AddSuccessToastMessage("Book added successfully");
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookRepository.GetById(id);
            return View(book);
        }
        [HttpPost]

        public async Task<IActionResult> Delete(BookVM model)
        {

            return RedirectToAction("Index");
        }
    }
}
