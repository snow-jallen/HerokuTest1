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
    public class IndexModel : PageModel
    {
        private readonly HerokuTest1.Models.DataContext _context;

        public IndexModel(HerokuTest1.Models.DataContext context)
        {
            _context = context;
        }

        public IList<TestItem> TestItem { get;set; }

        public async Task OnGetAsync()
        {
            TestItem = await _context.TestItem.ToListAsync();
        }
    }
}
