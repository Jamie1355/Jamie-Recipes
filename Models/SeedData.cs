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
                    Title = "Mac and Cheese",
                    Description = "A dirty Mac & Cheese - the perfect alternative to a greasy takeaway!",
                    Serves = 2,
                    Ingredients = "6 handfuls Macaroni Pasta\nSome Broccoli\nA knob of butter\nA heaped tablespoon of flour\nLots of Milk\nLots of Cheese",
                    Method = "1. Boil pasta and broccoli in a pan.\n2. Melt butter in second pan.\n3. Add flour to melted butter and mix.\n4. Slowly add milk to butter and flour mixture until smooth-ish, then add cheese.\n5. Drain pasta/broccoli and add to baking tray.\n6. Mix sauce into baking tray with pasta.\n7. Put in oven until looking a tad crispy.\n8. Voila!",
                    Dietary = "V",
                    ImageUrl="MacAndCheese.jpg"
                }
            );
            context.SaveChanges();
        }
    }
}