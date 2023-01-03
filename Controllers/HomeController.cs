using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using JamiesRecipes.Data;
using JamiesRecipes.Models;
using Microsoft.Net.Http.Headers;

namespace JamiesRecipes.Controllers
{
    public class HomeController : Controller
    {
        private readonly JamiesRecipesContext _context;

        public HomeController(JamiesRecipesContext context)
        {
            _context = context;
        }

        // GET: Home
        public async Task<IActionResult> Index(string searchString)
        {
            if (_context.Recipe == null)
            {
                return Problem("Entity set 'JamiesRecipesContext.Recipe'  is null.");
            }

            var recipes = from r in _context.Recipe select r;

            if (!String.IsNullOrEmpty(searchString))
            {
                recipes = recipes.Where(s => s.Title!.ToLower().Contains(searchString.ToLower()));
            }

            return View(await recipes.ToListAsync());
        }

        // GET: Home/Privacy
        public IActionResult Privacy()
        {
            return View();
        }

        // GET: Home/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Recipe == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipe
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // GET: Home/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Serves,Ingredients,Method,Dietary,ImageUrl")] Recipe recipe, [FromForm]IFormFile uploadImage)
        {
            if (ModelState.IsValid)
            {
                // Save uploaded image
                if(uploadImage != null && uploadImage.Length > 0)
                    {
                        var fileName = Path.GetFileName(uploadImage.FileName);
                        await SaveUploadedImage(uploadImage);
                        recipe.ImageUrl = fileName;
                    }

                _context.Add(recipe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = recipe.Id });
            }
            return View(recipe);
        }

        // GET: Home/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Recipe == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipe.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }
            return View(recipe);
        }

        // POST: Home/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Serves,Ingredients,Method,Dietary,ImageUrl")] Recipe recipe, [FromForm]IFormFile? uploadImage)
        {
            if (id != recipe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(uploadImage != null && uploadImage.Length > 0)
                    {
                        var fileName = Path.GetFileName(uploadImage.FileName);
                        await SaveUploadedImage(uploadImage);
                        recipe.ImageUrl = fileName;
                    }
                    
                    _context.Update(recipe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeExists(recipe.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id = recipe.Id });
            }
            return View(recipe);
        }

        // GET: Home/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Recipe == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipe
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Recipe == null)
            {
                return Problem("Entity set 'JamiesRecipesContext.Recipe'  is null.");
            }
            var recipe = await _context.Recipe.FindAsync(id);
            if (recipe != null)
            {
                _context.Recipe.Remove(recipe);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecipeExists(int id)
        {
          return (_context.Recipe?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private async Task<IActionResult> SaveUploadedImage(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {

                var fileName = Path.GetFileName(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Uploads", fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    await file.CopyToAsync(stream);
                }
            }

            return Ok();
        }
    }
}
