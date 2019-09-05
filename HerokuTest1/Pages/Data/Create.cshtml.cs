using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HerokuTest1.Data;
using HerokuTest1.Models;

namespace HerokuTest1.Pages.Data
{
    public class CreateModel : PageModel
    {
        private readonly HerokuTest1.Models.DataContext _context;

        public CreateModel(HerokuTest1.Models.DataContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public TestItem TestItem { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.TestItem.Add(TestItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}