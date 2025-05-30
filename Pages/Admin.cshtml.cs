using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AmagerminIT.Data;
using AmagerminIT.Models;
using Microsoft.EntityFrameworkCore;

namespace AmagerminIT.Pages
{
    public class AdminModel : PageModel
    {
        private readonly AmagerminITContext _context;

        public AdminModel(AmagerminITContext context)
        {
            _context = context;
        }

        public List<Achievement> Achievements { get; set; } = new List<Achievement>();

        public async Task OnGetAsync()
        {
            // Load achievements, e.g. the newest 5 achievements sorted by date descending
            Achievements = await _context.Achievements
                .Take(5)
                .ToListAsync();
        }
    }
}