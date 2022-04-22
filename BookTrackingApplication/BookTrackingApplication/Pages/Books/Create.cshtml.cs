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
    public class CreateModel : PageModel
    {
        private readonly BookTrackingApplicationContext _context;

        public CreateModel(BookTrackingApplicationContext context)
        {
            _context = context;
            PopulateDepartmentsDropDownList(_context);
        }
        public SelectList CategoryTypiesSL { get; set; }

        public void PopulateDepartmentsDropDownList(BookTrackingApplicationContext _context,
            object selectedCategoryTypies = null)
        {
            var categoryTypiesQuery = from ct  in _context.CategoryTypies
                                   orderby ct.Description // Sort by name.
                                   select ct;

            CategoryTypiesSL = new SelectList(categoryTypiesQuery.AsNoTracking(),
                        "NameToken", "Description", selectedCategoryTypies);
        }

        [BindProperty]
        public Book Book { get; set; }
        public  IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
           

            _context.Books.Add(Book);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
