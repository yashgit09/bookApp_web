using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookTrackingApplication.Data;
using BookTrackingApplication.Models;
using Microsoft.EntityFrameworkCore;


namespace BookTrackingApplication.Pages
{
    public class IndexModel : PageModel
    {
        private readonly BookTrackingApplicationContext _context;

        public List<Book> Books { get; set; }
       
        
        public IndexModel(BookTrackingApplicationContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            //Books = await _context.Books.ToListAsync();
            Books = await _context.Books
                    .Include(b => b.CategoryType)
                        .ThenInclude(c => c.Category)
                    .AsNoTracking()
                    .ToListAsync();

        }

       
    }
}
