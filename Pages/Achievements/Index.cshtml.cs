using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AmagerminIT.Data;
using AmagerminIT.Models;

namespace AmagerminIT.Pages.Achievements
{
    public class IndexModel : PageModel
    {
        private readonly AmagerminIT.Data.AmagerminITContext _context;

        public IndexModel(AmagerminIT.Data.AmagerminITContext context)
        {
            _context = context;
        }

        public IList<Achievement> Achievements { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Achievements = await _context.Achievements.ToListAsync();
        }
    }
}
