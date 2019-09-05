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

        public void OnPostConvertConnectionString(string database_url)
        {
            //Incoming format:
            //      postgres://oaajnotluddcod:9fc08ee2cca7c32650899a443c23c54358d3e5f9c36192a7955f52641476b015@ec2-54-227-251-33.compute-1.amazonaws.com:5432/d28hpoae3rero6

            ParseDatabaseUrl(database_url);
        }

        private void ParseDatabaseUrl(string database_url)
        {
            HerokuDatabaseUrl = database_url;

            if (!database_url.StartsWith("postgres://"))
            {
                DotNetConnectionString = PsqlConnectionCommand = null;
                return;
            }

            var words = database_url.Split(':', '@', '/');
            var user = words[3];
            var password = words[4];
            var host = words[5];
            var port = words[6];
            var database = words[7];

            DotNetConnectionString = $"Server={host}; Port={port}; Database={database}; User ID={user}; Password={password}; SSL Mode=Require; Trust Server Certificate=True;";
            PsqlConnectionCommand = $"docker run -e PGPASSWORD={password} --rm -it postgres psql -h {host} -U {user} {database} --set=sslmode=require";
        }

        public string HerokuDatabaseUrl { get; set; }
        public string DotNetConnectionString { get; set; }
        public string PsqlConnectionCommand { get; set; }
    }
}
