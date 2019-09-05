using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HerokuTest1.Data;
using HerokuTest1.Models;

namespace HerokuTest1.Pages.Data
{
    public class DeleteModel : PageModel
    {
        private readonly HerokuTest1.Models.DataContext _context;

        public DeleteModel(HerokuTest1.Models.DataContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TestItem TestItem { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TestItem = await _context.TestItem.FirstOrDefaultAsync(m => m.Id == id);

            if (TestItem == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TestItem = await _context.TestItem.FindAsync(id);

            if (TestItem != null)
            {
                _context.TestItem.Remove(TestItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
