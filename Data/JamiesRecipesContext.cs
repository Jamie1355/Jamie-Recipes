using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using JamiesRecipes.Models;

namespace JamiesRecipes.Data
{
    public class JamiesRecipesContext : DbContext
    {
        public JamiesRecipesContext (DbContextOptions<JamiesRecipesContext> options)
            : base(options)
        {
        }

        public DbSet<JamiesRecipes.Models.Recipe> Recipe { get; set; } = default!;
    }
}
