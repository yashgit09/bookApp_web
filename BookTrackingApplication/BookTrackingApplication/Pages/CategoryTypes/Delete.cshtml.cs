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

namespace BookTrackingApplication.Pages.CategoryTypes
{
    public class DeleteModel : PageModel
    {
        private readonly BookTrackingApplicationContext _context;
        [BindProperty]
        public CategoryType CategoryTypes { get; set; }
        public DeleteModel(BookTrackingApplicationContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CategoryTypes = await _context.CategoryTypies
                .Include(ct => ct.Category)
                    .FirstOrDefaultAsync(ct => ct.NameToken == id);

            if (CategoryTypes == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CategoryTypes = await _context.CategoryTypies.FindAsync(id);

            if (CategoryTypes != null)
            {
                _context.CategoryTypies.Remove(CategoryTypes);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
