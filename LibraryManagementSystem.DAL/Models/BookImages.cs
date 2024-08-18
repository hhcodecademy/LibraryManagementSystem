using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.DAL.Models
{
    public class BookImages : BaseEntity
    {
        public string Name { get; set; }
        public string Directory { get; set; }
    }
}
