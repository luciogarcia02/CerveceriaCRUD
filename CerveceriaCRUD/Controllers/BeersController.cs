using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CerveceriaCRUD.Db;
using CerveceriaCRUD.Models;
using System.Text.Json;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace CerveceriaCRUD.Controllers
{
    public class BeersController : Controller
    {
        private readonly BeersDb _context;
        private string _APIurl;
        public BeersController(BeersDb context)
        {
            _context = context;
            
        }

        // GET: Beers
        public async Task<IActionResult> Index()
        {
              return _context.Beers != null ? 
                          View(await _context.Beers.ToListAsync()) :
                          Problem("Entity set 'BeersDb.Beers'  is null.");
        }
     

        public async Task<IActionResult> Shop()
        {
            string _APIurl = "https://65e7c1b453d564627a8f36b2.mockapi.io/api/beers";
            using (HttpClient httpClient = new HttpClient())
            {
                var beerList = new ShopVM();
                try
                {
                    var response = await httpClient.GetAsync(_APIurl);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        var responseData = JsonConvert.DeserializeObject<List<Beer>>(jsonString);
                        beerList.beers = responseData;
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                }

                return View(beerList);
            }
        }

        // GET: Beers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Beers == null)
            {
                return NotFound();
            }

            var beer = await _context.Beers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (beer == null)
            {
                return NotFound();
            }

            return View(beer);
        }

        // GET: Beers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Beers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Beer beer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(beer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(beer);
        }

        // GET: Beers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Beers == null)
            {
                return NotFound();
            }

            var beer = await _context.Beers.FindAsync(id);
            if (beer == null)
            {
                return NotFound();
            }
            return View(beer);
        }

        // POST: Beers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Beer beer)
        {
            if (id != beer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(beer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BeerExists(beer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(beer);
        }

        // GET: Beers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Beers == null)
            {
                return NotFound();
            }

            var beer = await _context.Beers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (beer == null)
            {
                return NotFound();
            }

            return View(beer);
        }

        // POST: Beers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Beers == null)
            {
                return Problem("Entity set 'BeersDb.Beers'  is null.");
            }
            var beer = await _context.Beers.FindAsync(id);
            if (beer != null)
            {
                _context.Beers.Remove(beer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BeerExists(int id)
        {
          return (_context.Beers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
