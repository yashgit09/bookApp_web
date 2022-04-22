using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookTrackingApplication.Data;
using BookTrackingApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookTrackingApplication.Pages.CategoryTypes
{
    public class CreateModel : PageModel
    {
        private readonly BookTrackingApplicationContext _context;
        public SelectList CategorySL { get; set; }

        [BindProperty]
        public CategoryType CategoryType { get; set; }
        public CreateModel(BookTrackingApplicationContext context)
        {
            _context = context;
            PopulateDepartmentsDropDownList(_context);
        }
        public void PopulateDepartmentsDropDownList(BookTrackingApplicationContext _context,
           object selectedCategory = null)
        {
            var categoryQuery = _context.Categories.ToList();

            CategorySL = new SelectList(categoryQuery,
                        "Type", "Name", selectedCategory);
           
        }
        public IActionResult OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            _context.CategoryTypies.Add(CategoryType);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
