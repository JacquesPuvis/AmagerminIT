using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AmagerminIT.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AmagerminIT.Data
{
    public class AmagerminITContext : IdentityDbContext<User>
    {
        public AmagerminITContext (DbContextOptions<AmagerminITContext> options)
            : base(options)
        {
        }

        public DbSet<AmagerminIT.Models.User> User { get; set; } = default!;
        public DbSet<Achievements> Achievements { get; set; }

    }
}
