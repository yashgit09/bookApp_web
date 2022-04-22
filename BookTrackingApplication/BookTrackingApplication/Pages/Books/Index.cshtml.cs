using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookTrackingApplication.Data;
using Microsoft.EntityFrameworkCore;
using local = BookTrackingApplication.Models;
using schema = Schema.NET;

namespace BookTrackingApplication.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly BookTrackingApplicationContext _context;

        public IndexModel(BookTrackingApplicationContext context)
        {
            _context = context;
        }
        public IList<local.Book> Books { get; set; }
        [BindProperty]
        public local.Book Book { get; set; }
        public ICollection<schema.Thing> JSONLD
        {
            get
            {
                List<schema.Thing> lst = new List<schema.Thing>() { };
                foreach (var thing in GetBooksList())
                {
                    lst.Add(GetJson(thing));
                }

                return lst;
            }
        }
        public async Task OnGetAsync()
        {
           Books = await _context.Books
                    .Include(b => b.CategoryType)
                        .ThenInclude(c => c.Category)
                    .AsNoTracking()
                    .ToListAsync();
        }
        public schema.Thing GetJson(local.Book Book)
        {
            schema.Book book = new schema.Book();
            book.Isbn = Book.ISBN;
            book.Author = new schema.Person() { Name = Book.Author };
            book.Name = Book.Title;
            book.IsBasedOn = new List<object>() {
                new schema.Product()
                {
                    Name = Book.CategoryType.NameToken,
                    Description = Book.CategoryType.Description,
                    Category = new schema.Thing()
                    {
                        AlternateName =  Book.CategoryType.Category.Name,
                        Name = Book.CategoryType.Category.Name
                    }


                }
            };

            return book;
        }
        public List<local.Book> GetBooksList()
        {
            List<local.Book> BooksList = _context.Books
                   .Include(b => b.CategoryType)
                       .ThenInclude(c => c.Category)
                   .ToList();
            return BooksList;
        }
    }
}

