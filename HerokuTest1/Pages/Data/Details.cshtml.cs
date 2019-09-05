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
    public class DetailsModel : PageModel
    {
        private readonly HerokuTest1.Models.DataContext _context;

        public DetailsModel(HerokuTest1.Models.DataContext context)
        {
            _context = context;
        }

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
    }
}
