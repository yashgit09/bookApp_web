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

namespace BookTrackingApplication.Pages.CategoryTypes
{
    public class EditModel : PageModel
    {
        private readonly BookTrackingApplicationContext _context;
        public SelectList CategorySL { get; set; }

        [BindProperty]
        public CategoryType CategoryType { get; set; }
        public EditModel(BookTrackingApplicationContext context)
        {
            _context = context;
            PopulateDepartmentsDropDownList(_context);
        }
        public void PopulateDepartmentsDropDownList(BookTrackingApplicationContext _context,
           object selectedCategory = null)
        {
            var categoryQuery = from c in _context.Categories
                                orderby c.Name // Sort by name.
                                select c;

            CategorySL = new SelectList(categoryQuery.AsNoTracking(),
                        "Type", "Name", selectedCategory);
        }
        public async Task<IActionResult> OnGetAsync(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
           
            CategoryType = await _context.CategoryTypies.FirstOrDefaultAsync(ct => ct.NameToken == id);

            if (CategoryType == null)
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

            CategoryType updatedCategory =
                _context.CategoryTypies.FirstOrDefault(ct => ct.NameToken == CategoryType.NameToken);
            if (updatedCategory != null)
            {
                updatedCategory.Description = CategoryType.Description;
                updatedCategory.CategoryTypeCode =
                CategoryType.CategoryTypeCode;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!categoryTypeExists(CategoryType.NameToken))
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

        private bool categoryTypeExists(string id)
        {
            return _context.CategoryTypies.Any(e => e.NameToken == id);
        }
        #endregion
    }
}

