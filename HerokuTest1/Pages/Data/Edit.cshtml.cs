using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HerokuTest1.Data;
using HerokuTest1.Models;

namespace HerokuTest1.Pages.Data
{
    public class EditModel : PageModel
    {
        private readonly HerokuTest1.Models.DataContext _context;

        public EditModel(HerokuTest1.Models.DataContext context)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(TestItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TestItemExists(TestItem.Id))
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

        private bool TestItemExists(int id)
        {
            return _context.TestItem.Any(e => e.Id == id);
        }
    }
}
