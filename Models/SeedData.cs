using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using JamiesRecipes.Data;
using System;
using System.Linq;

namespace JamiesRecipes.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new JamiesRecipesContext(
            serviceProvider.GetRequiredService<
            DbContextOptions<JamiesRecipesContext>>()))
        {
            // Look for any recipes
            if (context.Recipe.Any())
            {
                return; // DB has been seeded
            }
            context.Recipe.AddRange(
                new Recipe
                {
                    Title = "Mac & Cheese",
                    Description = "A dirty Mac & Cheese - the perfect alternative to a greasy takeaway!",
                    Serves = 2,
                    Ingredients = "6 handfuls Macoroni Pasta, Some Broccoli, A knob of butter, A heaped tablespoon of flour, Lots of Milk, Lots of Cheese",
                    Method = "1. Boil pasta and broccoli in a pan. 2. Melt butter in second pan. 3. Add flour to melted butter and mix. 4. Slowly add milk to butter and flour mixture until smooth-ish, then add cheese. 5. Drain pasta/broccoli and add to baking tray. 6. Mix sauce into baking tray with pasta. 7. Put in oven until looking a tad crispy. 8. Voila!",
                    Dietary = "V"
                }
            );
            context.SaveChanges();
        }
    }
}