using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookTrackingApplication.Data;
using BookTrackingApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookTrackingApplication.Pages.Books
{
    public class EditModel : PageModel
    {
        private readonly BookTrackingApplicationContext _context;

        public EditModel(BookTrackingApplicationContext context)
        {
            _context = context;
            PopulateDepartmentsDropDownList(_context);
        }
        public SelectList CategoryTypiesSL { get; set; }

        public void PopulateDepartmentsDropDownList(BookTrackingApplicationContext _context,
            object selectedCategoryTypies = null)
        {
            var categoryTypiesQuery = from ct in _context.CategoryTypies
                                      orderby ct.Description // Sort by name.
                                      select ct;

            CategoryTypiesSL = new SelectList(categoryTypiesQuery.AsNoTracking(),
                        "NameToken", "Description", selectedCategoryTypies);
        }

        public IList<Book> Books { get; set; }
        public IList<Category> Categories { get; set; }
        public IList<CategoryType> CategoryTypies { get; set; }

        [BindProperty]
        public Book Book { get; set; }
        public async Task<IActionResult> OnGetAsync(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Book = await _context.Books.FirstOrDefaultAsync(b => b.ISBN == id);

            if (Book == null)
            {
                return NotFound();
            }
            return Page();
        }

        #region snippet
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Book updatedBook = _context.Books.SingleOrDefault(b => b.ISBN == Book.ISBN);
            if (updatedBook != null)
            {
                updatedBook.Author = Book.Author;
                updatedBook.CategoryTypeNameToken = Book.CategoryTypeNameToken;
                updatedBook.Title = Book.Title;

            }
            Book = updatedBook;
            try
            {
                await _context.SaveChangesAsync();
            }
           
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(updatedBook.ISBN))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            
            return RedirectToPage("./Index");
        }

        private bool BookExists(string id)
        {
            return _context.Books.Any(e => e.ISBN == id); 
        }
        #endregion
    }
}