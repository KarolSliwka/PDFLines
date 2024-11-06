using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PDFLines.Data;
using PDFLines.Models;

namespace PDFLines.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class MaintenancesController : Controller
    {
        private readonly TCZNT5000 _context;
        private readonly INotyfService _toasts;

        public MaintenancesController(TCZNT5000 context, INotyfService toast)
        {
            _context = context;
            _toasts = toast;
        }

        private string CurrentUserName
        {
            get
            {
                return User.Identity.Name.Substring(7);
            }
        }

        // GET: Areas
        public async Task<IActionResult> Index(int? pageNumber)
        {
            int pageSize = 20;
            var maintenances = _context.Maintenance
                    .Include(a => a.Projects)
                    .Include(a => a.Users)
                    .OrderByDescending(a => a.StartDate);

            return View(await PaginatedList<Maintenance>.CreateAsync(maintenances.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Maintenances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Maintenance == null)
            {
                return NotFound();
            }

            var maintenance = await _context.Maintenance
                .Include(m => m.Projects)
                .Include(m => m.Users)
                .FirstOrDefaultAsync(m => m.MaintenanceId == id);
            if (maintenance == null)
            {
                return NotFound();
            }

            return View(maintenance);
        }

        // GET: Maintenances/Create
        public IActionResult Create(int? pageNumber, int DepartmentId)
        {
            ViewBag.pageNumber = pageNumber;
            ViewBag.CreatedBy = _context.Users?
                .FirstOrDefault(u => u.Alias == CurrentUserName)?.UserId;
            ViewData["ProjectId"] = new SelectList(_context.Projects.
                Where(p => p.Name.Contains("Laser")), "ProjectId", "Name");
            return View();
        }

        // POST: Maintenances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? pageNumber, [Bind("MaintenanceId,ProjectId,Description,StartDate,EndDate,CreationDate,UserId")] Maintenance maintenance)
        {
            ViewBag.pageNumber = pageNumber;
            ViewBag.CreatedBy = _context.Users?
                .FirstOrDefault(u => u.Alias == CurrentUserName)?.UserId;
            ViewData["ProjectId"] = new SelectList(_context.Projects.
                Where(p => p.Name.Contains("Laser")), "ProjectId", "Name");

            var duplicate = _context.Maintenance
                .Where(m => m.ProjectId == maintenance.ProjectId &&
                       m.StartDate == maintenance.StartDate &&
                       m.EndDate == maintenance.EndDate).Count();

            if (ModelState.IsValid)
            {
                if (duplicate == 0)
                {
                    maintenance.StartDate = maintenance.StartDate.Add(new TimeSpan(0, 0, 0));
                    maintenance.EndDate = maintenance.EndDate.Add(new TimeSpan(0, 0, 0));
                    _context.Add(maintenance);
                    await _context.SaveChangesAsync();
                    _toasts.Success("Przegląd został dodany");
                    return RedirectToAction("Index", "Maintenances", new { pageNumber = pageNumber });
                }
                else
                {
                    _toasts.Warning("Przeglą już istnieje");
                    return RedirectToAction("Create", "Maintenances", new { pageNumber = pageNumber });
                }
            }
            else
            {
                _toasts.Error("Przegląd nie został dodany");
                return RedirectToAction("Create", "Maintenances", new { pageNumber = pageNumber });
            }
        }

        // GET: Maintenances/Edit/5
        public async Task<IActionResult> Edit(int? id, int? pageNumber)
        {

            ViewBag.pageNumber = pageNumber;
            ViewBag.CreatedBy = _context.Users?
                .FirstOrDefault(u => u.Alias == CurrentUserName)?.UserId;
            ViewData["ProjectId"] = new SelectList(_context.Projects.
                Where(p => p.Name.Contains("Laser")), "ProjectId", "Name");


            if (id == null || _context.Maintenance == null)
            {
                return NotFound();
            }

            var maintenance = await _context.Maintenance.FindAsync(id);
            if (maintenance == null)
            {
                return NotFound();
            }

            return View(maintenance);
        }

        // POST: Maintenances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int? pageNumber, [Bind("MaintenanceId,ProjectId,Description,StartDate,EndDate,CreationDate,UserId")] Maintenance maintenance)
        {
            ViewBag.pageNumber = pageNumber;
            ViewBag.CreatedBy = _context.Users?
                .FirstOrDefault(u => u.Alias == CurrentUserName)?.UserId;
            ViewData["ProjectId"] = new SelectList(_context.Projects.
                Where(p => p.Name.Contains("Laser")), "ProjectId", "Name");

            if (id != maintenance.MaintenanceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(maintenance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaintenanceExists(maintenance.MaintenanceId))
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

            return View(maintenance);
        }

        // GET: Maintenances/Delete/5
        public async Task<IActionResult> Delete(int? id, int? pageNumber)
        {
            ViewBag.pageNumber = pageNumber;

            if (id == null || _context.Maintenance == null)
            {
                return NotFound();
            }

            var maintenance = await _context.Maintenance
                .Include(m => m.Projects)
                .Include(m => m.Users)
                .FirstOrDefaultAsync(m => m.MaintenanceId == id);
            if (maintenance == null)
            {
                return NotFound();
            }

            return View(maintenance);
        }

        // POST: Maintenances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int? pageNumber)
        {
            ViewBag.pageNumber = pageNumber;

            if (_context.Maintenance == null)
            {
                return Problem("Entity set 'TCZNT5000.Maintenance'  is null.");
            }
            var maintenance = await _context.Maintenance.FindAsync(id);
            if (maintenance != null)
            {
                _context.Maintenance.Remove(maintenance);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaintenanceExists(int id)
        {
            return (_context.Maintenance?.Any(e => e.MaintenanceId == id)).GetValueOrDefault();
        }
    }
}
