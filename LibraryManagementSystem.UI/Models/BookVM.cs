using LibraryManagementSystem.DAL.Models;
using System.ComponentModel;

namespace LibraryManagementSystem.UI.Models
{
    public class BookVM
    {
        public int Id { get; set; }
        [DisplayName("Ad")]
        public string Name { get; set; }
        [DisplayName("Yayin tarixi")]
        public DateTime PublishDate { get; set; }
        [DisplayName("Aciqlama")]
        public string Description { get; set; }
        [DisplayName("Yazar adi")]
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        [DisplayName("Kateqoriya adi")]

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Thumbnail { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Author> Authors { get; set; }
        public IFormFile[] ImageFiles { get; set; }

    }
}
