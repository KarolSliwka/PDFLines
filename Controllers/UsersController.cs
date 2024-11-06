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
    public class UsersController : Controller
    {
        private readonly TCZNT5000 _context;
        //private readonly IToastNotification _toasts;
        private readonly INotyfService _toasts;
        private readonly object status;

        public UsersController(TCZNT5000 context, INotyfService toast) //IToastNotification toast)
        {
            _context = context;
            _toasts = toast;
            status = new[]
            {
                new { Value = "noaccess", Name = "Brak Dostępu" },
                new { Value = "user", Name = "Operator" },
                new { Value = "visor", Name = "Supervisor" },
                new { Value = "admin", Name = "Administrator" },
                new { Value = "super", Name = "Super Użytkownik" }
            }.ToList();
        }

        private List<SelectListItem> GetAccessLevels()
        {
            return new List<SelectListItem> {
                new SelectListItem { Value = "noaccess", Text = "Brak Dostępu" },
                new SelectListItem { Value = "user", Text = "Operator" },
                new SelectListItem { Value = "visor", Text = "Supervisor" },
                new SelectListItem { Value = "admin", Text = "Administrator" },
                new SelectListItem { Value = "super", Text = "Super Użytkownik" }
            };
        }

        // GET: Users
        public async Task<IActionResult> Index(string? message, int? pageNumber)
        {
            int pageSize = 20;
            var accessLevels = GetAccessLevels();

            var usersQuery = _context.Users.Select(user => new User
            {
                UserId = user.UserId,
                Alias = user.Alias,
                NameSurname = user.NameSurname,
                AccessLevels = accessLevels, // Use the local variable in the query
                AccessLevel = user.AccessLevel
            });

            return View(await PaginatedList<User>.CreateAsync(usersQuery.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            var model = new User
            {
                AccessLevels = GetAccessLevels()
            };

            return View(model);
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Alias,NameSurname,Email,AccessLevel")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,Alias,NameSurname,Email,AccessLevel")] User user)
        {
            var duplicate = _context.Users?
                .Where(o => o.Alias.ToLower() == user.Alias.ToLower() &&
                    o.NameSurname.ToLower() == user.NameSurname.ToLower())
                .ToList()
                .Count();

            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (duplicate == 0)
                {
                    try
                    {
                        _context.Update(user);
                        await _context.SaveChangesAsync();
                        _toasts.Success("Zmiany zostały zapisane");
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!UserExists(user.UserId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction("Index", "Users", new { });
                }
                else
                {
                    _toasts.Warning("Użytkownik już istnieje");
                    return RedirectToAction("Edit", "Users", new { });
                }
            }
            else
            {
                _toasts.Error("Wystąpił błąd! Zmiany nie zostały zapisane");
                return RedirectToAction("Edit", "Users", new { });
            }
        }


        // GET: Users/ChangeAccessLevel/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeAccessLevel(int? userId, string userAccess)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'TCZNT5000.Uesrs' is null.");
            }
            if (userId == null || string.IsNullOrEmpty(userAccess))
            {
                return BadRequest("User ID or Access Level is invalid.");
            }

            try
            {
                var user = await _context.Users.FindAsync(userId);
                if (user != null)
                {
                    // Validate userAccess against allowed values (e.g., "Admin", "User", etc.)
                    // Example: if (!allowedAccessLevels.Contains(userAccess)) { return BadRequest("Invalid access level."); }

                    user.AccessLevel = userAccess;
                    await _context.SaveChangesAsync();
                    string message = $"Dostęp użytkownika {user.NameSurname} został zmieniony na '{userAccess}'";
                    _toasts.Success(message);
                    return Json(new { success = true });
                }
                else
                {
                    _toasts.Error("Wystąpił błąd! Dostęp użytkownika nie został zmieniony");
                    return View();
                }
            }
            catch (Exception ex)
            {
                _toasts.Error($"Wystąpił problem {ex.Message}");
                return View();
            }
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'TCZNT5000.Uesrs' is null.");
            }
            else
            {
                var user = await _context.Users.FindAsync(id);
                if (user != null)
                {
                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();
                    _toasts.Success("Użytkownik został usunięty");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _toasts.Error("Wystąpił błąd! Użytkownik nie został usunięty");
                    return RedirectToAction("Delete", new { id = id });
                }
            }
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}