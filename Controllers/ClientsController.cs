using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PDFLines.Data;
using PDFLines.Models;

namespace PDFLines.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class ClientsController : Controller
    {
        private readonly TCZNT5000 _context;
        //private readonly IToastNotification _toasts;
        private readonly INotyfService _toasts;

        public ClientsController(TCZNT5000 context, INotyfService toast) //IToastNotification toast)
        {
            _context = context;
            _toasts = toast;
        }

        // GET: Clients
        public async Task<IActionResult> Index()
        {
            return _context.Clients != null ?
                        View(await _context.Clients.ToListAsync()) :
                        Problem("Entity set 'TCZNT5000.Clients'  is null.");
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClientId,Name,Active")] Client client)
        {
            var duplicate = _context.Clients?
                .Where(o => o.Name.ToLower() == client.Name.ToLower())
                .ToList()
                .Count();

            if (ModelState.IsValid)
            {
                if (duplicate == 0)
                {
                    _context.Add(client);
                    await _context.SaveChangesAsync();
                    _toasts.Success("Klient został dodany");
                    return RedirectToAction("Index", "Clients", new { });
                }
                else
                {
                    _toasts.Warning("Klient już istnieje");
                    return RedirectToAction("Create", "Clients", new { });
                }
            }
            else
            {
                _toasts.Error("Klient nie został dodany");
                return RedirectToAction("Create", "Clients", new { });
            }
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClientId,Name,Active")] Client client)
        {
            var duplicate = _context.Clients?
                .Where(o => o.Name.ToLower() == client.Name.ToLower() && o.Active == client.Active)
                .ToList()
                .Count();

            if (id != client.ClientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (duplicate == 0)
                {
                    try
                    {
                        _context.Update(client);
                        await _context.SaveChangesAsync();
                        _toasts.Success("Zmiany zostały zapisane");
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ClientExists(client.ClientId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction("Index", "Clients", new { });
                }
                else
                {
                    _toasts.Warning("Klient już istnieje");
                    return RedirectToAction("Edit", "Clients", new { });
                }
            }
            else
            {
                _toasts.Error("Wystąpił błąd! Zmiany nie zostały zapisane");
                return RedirectToAction("Edit", "Clients", new { });
            }
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Clients == null)
            {
                return Problem("Entity set 'TCZNT5000.Clients' is null.");
            }

            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                _toasts.Error("Wystąpił błąd! Klient nie został usunięty");
                return RedirectToAction("Delete", new { id = id });
            }

            // Check if the client has associated projects
            var hasProjects = _context.Projects.Any(p => p.ClientId == id);
            if (hasProjects)
            {
                // Redirect to a custom error page
                return RedirectToAction("DeleteError", new { id = id });
            }

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            _toasts.Success("Klient został usunięty");
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteError(int id)
        {
            var client = _context.Clients.Find(id);
            if (client == null)
            {
                return NotFound();
            }

            ViewBag.ClientId = id; // Pass the client ID to the view
            ViewBag.ClientName = client.Name; // Assuming the client has a Name property
            return View();
        }


        private bool ClientExists(int id)
        {
            return (_context.Clients?.Any(e => e.ClientId == id)).GetValueOrDefault();
        }
    }
}
