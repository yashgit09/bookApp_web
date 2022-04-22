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

namespace BookTrackingApplication.Pages.CategoryTypes
{
    public class IndexModel : PageModel
    {
        private readonly BookTrackingApplicationContext _context;
        [BindProperty]
        public CategoryType CategoryTypes { get; set; }
        public IList<CategoryType> CategoryTypies { get; set; }
        public IndexModel(BookTrackingApplicationContext context)
        {
            _context = context;
        }
        public async Task OnGetAsync()
        {
            CategoryTypies =  await _context.CategoryTypies
                    .Include(ct => ct.Category)
                     .AsNoTracking()
                   .ToListAsync(); ;
        }
    }
}