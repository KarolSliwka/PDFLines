using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PDFLines.Data;
using PDFLines.Models;

namespace PDFLines.Controllers
{
    [Authorize(Policy = "AllUsers")]
    public class ProjectsController : Controller
    {
        private readonly TCZNT5000 _context;
        //private readonly IToastNotification _toasts;
        private readonly INotyfService _toasts;

        public ProjectsController(TCZNT5000 context, INotyfService toast) //IToastNotification toast)
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

        // GET: Projects
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Index(int? pageNumber)
        {
            ViewData["pageNumber"] = pageNumber;

            var projects = _context.Projects
                .Include(p => p.Clients).Include(p => p.Users)
                .OrderBy(p => p.Clients.Name)
                .ThenBy(p => p.Type)
                .ThenBy(p => p.Order);

            int pageSize = 20;

            return View(await PaginatedList<Project>.CreateAsync(projects.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: SMT Clients for projects
        public async Task<IActionResult> SMTClients()
        {
            var projects = await _context.Projects
                .Where(p => p.Active == true && p.Type == 1)
                .Distinct()
                .ToListAsync();

            var clients = await _context.Clients
                .Where(c => c.Active == true && projects.Select(o => o.ClientId).Contains(c.ClientId))
                .Select(c => new { c.ClientId, c.Name })
                .Distinct()
                .ToListAsync();

            var clientProjectCounts = clients.Select(client => new
            {
                Client = client,
                ProjectCount = projects.Where(p => p.ClientId == client.ClientId).Count()
            }).ToList();

            return View(clientProjectCounts);
        }

        // GET: SMT project only
        public async Task<IActionResult> SMT(int id)
        {
            ViewData["ClientName"] = _context.Clients?
                .FirstOrDefault(p => p.ClientId == id)?.Name;

            // types: 1 - SMT, 2 - NonSMT, 3 - Backend, 4 - Fabrication, 5 - Integration, 6 - Welding, 7 - Stamping, 8 - Paintshop
            return _context.Projects?
                .Where(p => p.Type == 1 && p.Active == true && p.ClientId == id) != null ?
                    View(await _context.Projects
                    .Where(p => p.Type == 1 && p.Active == true && p.ClientId == id)
                    .OrderBy(p => p.Order)
                    .ToListAsync()) :
                    Problem("Entity set 'TCZNT5000.Project'  is null.");
        }

        // GET: NonSMT project only
        public async Task<IActionResult> NonSMT()
        {
            // types: 1 - SMT, 2 - NonSMT, 3 - Backend, 4 - Fabrication, 5 - Integration, 6 - Welding, 7 - Stamping, 8 - Paintshop
            return _context.Projects?
                .Where(p => p.Type == 2 && p.Active == true) != null ?
                    View(await _context.Projects
                    .Where(p => p.Type == 2 && p.Active == true)
                    .OrderBy(p => p.Order)
                    .ToListAsync()) :
                    Problem("Entity set 'TCZNT5000.Project'  is null.");
        }

        // GET: Backend clients for projects
        public async Task<IActionResult> BackendClients()
        {
            var projects = await _context.Projects
                .Where(p => p.Active == true && p.Type == 3)
                .Distinct()
                .ToListAsync();

            var clients = await _context.Clients
                .Where(c => c.Active == true && projects.Select(o => o.ClientId).Contains(c.ClientId))
                .Select(c => new { c.ClientId, c.Name })
                .Distinct()
                .ToListAsync();

            var clientProjectCounts = clients.Select(client => new
            {
                Client = client,
                ProjectCount = projects.Where(p => p.ClientId == client.ClientId).Count()
            }).ToList();

            return View(clientProjectCounts);
        }

        // GET: Backend project only
        public async Task<IActionResult> Backend(int id)
        {
            ViewData["ClientName"] = _context.Clients?
                .FirstOrDefault(c => c.ClientId == id)?.Name;

            // types: 1 - SMT, 2 - NonSMT, 3 - Backend, 4 - Fabrication, 5 - Integration, 6 - Welding, 7 - Stamping, 8 - Paintshop
            return _context.Projects?
                .Where(p => p.Type == 3 && p.Active == true && p.ClientId == id) != null ?
                    View(await _context.Projects
                    .Where(p => p.Type == 3 && p.Active == true && p.ClientId == id)
                    .OrderBy(p => p.Order)
                    .ToListAsync()) :
                    Problem("Entity set 'TCZNT5000.Project'  is null.");
        }

        // GET: Fabrication clients for projects
        public async Task<IActionResult> FabricationClients()
        {
            var projects = await _context.Projects
                .Where(p => p.Active == true && p.Type == 4)
                .Distinct()
                .ToListAsync();

            var clients = await _context.Clients
                .Where(c => c.Active == true && projects.Select(o => o.ClientId).Contains(c.ClientId))
                .Select(c => new { c.ClientId, c.Name })
                .Distinct()
                .ToListAsync();

            var clientProjectCounts = clients.Select(client => new
            {
                Client = client,
                ProjectCount = projects.Where(p => p.ClientId == client.ClientId).Count()
            }).ToList();

            return View(clientProjectCounts);
        }

        // GET: Fabrication project only
        public async Task<IActionResult> Fabrication(int id)
        {
            ViewData["ClientName"] = _context.Clients?
                .FirstOrDefault(c => c.ClientId == id)?.Name;

            // types: 1 - SMT, 2 - NonSMT, 3 - Backend, 4 - Fabrication, 5 - Integration, 6 - Welding, 7 - Stamping, 8 - Paintshop
            return _context.Projects?
                .Where(p => p.Type == 4 && p.Active == true && p.ClientId == id) != null ?
                    View(await _context.Projects
                    .Where(p => p.Type == 4 && p.Active == true && p.ClientId == id)
                    .OrderBy(p => p.Order)
                    .ToListAsync()) :
                    Problem("Entity set 'TCZNT5000.Project'  is null.");
        }

        // GET: Integration clients for projects
        public async Task<IActionResult> IntegrationClients()
        {
            var projects = await _context.Projects
                .Where(p => p.Active == true && p.Type == 5)
                .Distinct()
                .ToListAsync();

            var clients = await _context.Clients
                .Where(c => c.Active == true && projects.Select(o => o.ClientId).Contains(c.ClientId))
                .Select(c => new { c.ClientId, c.Name })
                .Distinct()
                .ToListAsync();

            var clientProjectCounts = clients.Select(client => new
            {
                Client = client,
                ProjectCount = projects.Where(p => p.ClientId == client.ClientId).Count()
            }).ToList();

            return View(clientProjectCounts);
        }

        // GET: Integration project only
        public async Task<IActionResult> Integration(int id)
        {
            ViewData["ClientName"] = _context.Clients?
                .FirstOrDefault(c => c.ClientId == id)?.Name;

            // types: 1 - SMT, 2 - NonSMT, 3 - Backend, 4 - Fabrication, 5 - Integration, 6 - Welding, 7 - Stamping, 8 - Paintshop
            return _context.Projects?
                .Where(p => p.Type == 5 && p.Active == true && p.ClientId == id) != null ?
                    View(await _context.Projects
                    .Where(p => p.Type == 5 && p.Active == true && p.ClientId == id)
                    .OrderBy(p => p.Order)
                    .ToListAsync()) :
                    Problem("Entity set 'TCZNT5000.Project'  is null.");
        }

        // GET: Welding clients for projects
        public async Task<IActionResult> WeldingClients()
        {
            var projects = await _context.Projects
                .Where(p => p.Active == true && p.Type == 6)
                .Distinct()
                .ToListAsync();

            var clients = await _context.Clients
                .Where(c => c.Active == true && projects.Select(o => o.ClientId).Contains(c.ClientId))
                .Select(c => new { c.ClientId, c.Name })
                .Distinct()
                .ToListAsync();

            var clientProjectCounts = clients.Select(client => new
            {
                Client = client,
                ProjectCount = projects.Where(p => p.ClientId == client.ClientId).Count()
            }).ToList();

            return View(clientProjectCounts);
        }

        // GET: Welding project only
        public async Task<IActionResult> Welding(int id)
        {
            ViewData["ClientName"] = _context.Clients?
                .FirstOrDefault(c => c.ClientId == id)?.Name;

            // types: 1 - SMT, 2 - NonSMT, 3 - Backend, 4 - Fabrication, 5 - Integration, 6 - Welding, 7 - Stamping, 8 - Paintshop
            return _context.Projects?
                .Where(p => p.Type == 6 && p.Active == true && p.ClientId == id) != null ?
                    View(await _context.Projects
                    .Where(p => p.Type == 6 && p.Active == true && p.ClientId == id)
                    .OrderBy(p => p.Order)
                    .ToListAsync()) :
                    Problem("Entity set 'TCZNT5000.Project'  is null.");
        }

        // GET: Stamping clients for projects
        public async Task<IActionResult> StampingClients()
        {
            var projects = await _context.Projects
                .Where(p => p.Active == true && p.Type == 7)
                .Distinct()
                .ToListAsync();

            var clients = await _context.Clients
                .Where(c => c.Active == true && projects.Select(o => o.ClientId).Contains(c.ClientId))
                .Select(c => new { c.ClientId, c.Name })
                .Distinct()
                .ToListAsync();

            var clientProjectCounts = clients.Select(client => new
            {
                Client = client,
                ProjectCount = projects.Where(p => p.ClientId == client.ClientId).Count()
            }).ToList();

            return View(clientProjectCounts);
        }

        // GET: Stamping project only
        public async Task<IActionResult> Stamping(int id)
        {
            ViewData["ClientName"] = _context.Clients?
                .FirstOrDefault(c => c.ClientId == id)?.Name;

            // types: 1 - SMT, 2 - NonSMT, 3 - Backend, 4 - Fabrication, 5 - Integration, 6 - Welding, 7 - Stamping, 8 - Paintshop
            return _context.Projects?
                .Where(p => p.Type == 7 && p.Active == true && p.ClientId == id) != null ?
                    View(await _context.Projects
                    .Where(p => p.Type == 7 && p.Active == true && p.ClientId == id)
                    .OrderBy(p => p.Order)
                    .ToListAsync()) :
                    Problem("Entity set 'TCZNT5000.Project'  is null.");
        }

        // GET: Paintshop clients for projects
        public async Task<IActionResult> PaintshopClients()
        {
            var projects = await _context.Projects
                .Where(p => p.Active == true && p.Type == 8)
                .Distinct()
                .ToListAsync();

            var clients = await _context.Clients
                .Where(c => c.Active == true && projects.Select(o => o.ClientId).Contains(c.ClientId))
                .Select(c => new { c.ClientId, c.Name })
                .Distinct()
                .ToListAsync();

            var clientProjectCounts = clients.Select(client => new
            {
                Client = client,
                ProjectCount = projects.Where(p => p.ClientId == client.ClientId).Count()
            }).ToList();

            return View(clientProjectCounts);
        }

        // GET: Paintshop project only
        public async Task<IActionResult> Paintshop(int id)
        {
            ViewData["ClientName"] = _context.Clients?
                .FirstOrDefault(c => c.ClientId == id)?.Name;

            // types: 1 - SMT, 2 - NonSMT, 3 - Backend, 4 - Fabrication, 5 - Integration, 6 - Welding, 7 - Stamping, 8 - Paintshop
            return _context.Projects?
                .Where(p => p.Type == 8 && p.Active == true && p.ClientId == id) != null ?
                    View(await _context.Projects
                    .Where(p => p.Type == 8 && p.Active == true && p.ClientId == id)
                    .OrderBy(p => p.Order)
                    .ToListAsync()) :
                    Problem("Entity set 'TCZNT5000.Project'  is null.");
        }

        public IActionResult CheckFileExists(int id)
        {
            var project = _context.Projects?.FirstOrDefault(p => p.ProjectId == id);
            string projectType = "";
            switch (project.Type)
            {
                case 1:
                    projectType = "SMT";
                    break;
                case 2:
                    projectType = "NonSMT";
                    break;
                case 3:
                    projectType = "Backend";
                    break;
                case 4:
                    projectType = "Fabrication";
                    break;
                case 5:
                    projectType = "Integration";
                    break;
                case 6:
                    projectType = "Welding";
                    break;
                case 7:
                    projectType = "Stamping";
                    break;
                case 8:
                    projectType = "Paintshop";
                    break;
            }

            if (project == null)
            {
                return NotFound();
            }

            try
            {
                string[] catalogs = Directory.GetDirectories(project.FileLocation, "*", SearchOption.AllDirectories);

                // Initialize a variable to keep track of the newest file
                FileInfo newestFile = null;

                foreach (var catalog in catalogs)
                {
                    // Get list of all files in directory that match the project file name pattern
                    string[] searchFiles = Directory.GetFiles(catalog, project.FileName + "*");

                    foreach (var file in searchFiles)
                    {
                        var fileInfo = new FileInfo(file);

                        // Update the newestFile if this file is newer
                        if (newestFile == null || fileInfo.CreationTime > newestFile.CreationTime)
                        {
                            newestFile = fileInfo;
                        }
                    }
                }

                if (newestFile == null)
                {
                    // No files found, handle accordingly
                    _toasts.Error("Nie znaleziono pliku PDF");
                    return RedirectToAction(projectType, "Projects", new { id = project.ClientId });
                }
                else
                {
                    return RedirectToAction("DisplayPDF", new { fileUrl = newestFile, id = id, newestFile = newestFile.CreationTime });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
                return StatusCode(500, "Issue with directory path / folders");
            }
        }

        public IActionResult DisplayPDF(string fileUrl, int id, DateTime newestFile)
        {
            var project = _context.Projects?.FirstOrDefault(p => p.ProjectId == id);
            string projectType = "";
            switch (project.Type)
            {
                case 1:
                    projectType = "SMT";
                    break;
                case 2:
                    projectType = "NonSMT";
                    break;
                case 3:
                    projectType = "Backend";
                    break;
                case 4:
                    projectType = "Fabrication";
                    break;
                case 5:
                    projectType = "Integration";
                    break;
                case 6:
                    projectType = "Welding";
                    break;
                case 7:
                    projectType = "Stamping";
                    break;
                case 8:
                    projectType = "Paintshop";
                    break;
            }
            ViewBag.ProjectTypeHeader = projectType;
            ViewBag.FileUrl = fileUrl;
            ViewBag.FileCreationDate = newestFile.ToString("yyyy-MM-dd HH:mm:ss");
            ViewBag.ProjectName = project?.Name;
            ViewBag.ProjectType = project?.Type;
            ViewBag.ClientId = project?.ClientId;
            ViewBag.ClientName = _context.Clients.FirstOrDefault(c => c.ClientId == project.ClientId).Name;
            ViewBag.Maintenances = _context.Maintenance
                .Include(p => p.Projects)
                .Where(m => m.ProjectId == id && m.StartDate >= DateTime.Now.AddDays(-1))
                .OrderBy(m => m.StartDate)
                .ToList();
            return View();
        }

        [HttpGet]
        public IActionResult GetPDF(string fileUrl)
        {
            if (string.IsNullOrEmpty(fileUrl) || !IsValidFilePath(fileUrl))
            {
                return BadRequest("Invalid file path.");
            }

            var baseDirectory = Path.Combine(Directory.GetCurrentDirectory(), fileUrl);
            var fullPath = Path.Combine(baseDirectory, fileUrl);

            // Normalize the path to prevent directory traversal
            var normalizedPath = Path.GetFullPath(fullPath);

            // Ensure the normalized path starts with the base directory
            if (!normalizedPath.StartsWith(baseDirectory, StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Invalid file path.");
            }

            // Check if the file exists
            if (!System.IO.File.Exists(normalizedPath))
            {
                return NotFound("File not found.");
            }

            // Read the file and return it
            var fileBytes = System.IO.File.ReadAllBytes(normalizedPath);
            return File(fileBytes, "application/pdf");
        }

        private bool IsValidFilePath(string filePath)
        {
            return !filePath.Contains("..") && !filePath.Contains(":") && !filePath.Contains("/");
        }

        public IActionResult BackToProject(int ProjectId)
        {
            var project = _context.Projects?.FirstOrDefault(p => p.ProjectId == ProjectId);
            string projectType = "";
            switch (project.Type)
            {
                case 1:
                    projectType = "SMT";
                    break;
                case 2:
                    projectType = "NonSMT";
                    break;
                case 3:
                    projectType = "Backend";
                    break;
                case 4:
                    projectType = "Fabrication";
                    break;
                case 5:
                    projectType = "Integration";
                    break;
                case 6:
                    projectType = "Welding";
                    break;
                case 7:
                    projectType = "Stamping";
                    break;
                case 8:
                    projectType = "Paintshop";
                    break;
            }
            return RedirectToAction(projectType, "Projects", new { id = project.ClientId });
        }

        // GET: Projects/Details/5
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Clients)
                .Include(p => p.Users)
                .FirstOrDefaultAsync(m => m.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Projects/Create
        [Authorize(Policy = "AdminOnly")]
        public IActionResult Create(int? pageNumber)
        {
            ViewData["pageNumber"] = pageNumber;
            ViewData["Clients"] = new SelectList(_context.Clients?
                .Where(c => c.Active == true), "ClientId", "Name");
            ViewData["UserId"] = _context.Users?
                .FirstOrDefault(o => o.Alias == CurrentUserName)?.UserId;
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Create(int? pageNumber, [Bind("ProjectId,ClientId,Name,Type,FileName,FileLocation,Active,Order,UserId")] Project project)
        {
            ViewData["Clients"] = new SelectList(_context.Clients?
                .Where(c => c.Active == true), "ClientId", "Name");
            ViewData["UserId"] = _context.Users?
                .FirstOrDefault(o => o.Alias == CurrentUserName)?.UserId;

            // update project order based on selected type
            var maxorder = _context.Projects?
                .Where(p => p.Type == project.Type)?
                .OrderByDescending(p => p.Order)?
                .FirstOrDefault()?.Order;

            if (maxorder == null)
            {
                project.Order = 1;
            }
            else
            {
                project.Order = maxorder.Value + 1;
            }

            var duplicate = _context.Projects
                .Where(p => p.ClientId == project.ClientId &&
                       p.Name.ToLower() == project.Name.ToLower() &&
                       p.Type == project.Type)
                .ToList()
                .Count();

            if (ModelState.IsValid)
            {
                if (duplicate == 0)
                {
                    _context.Add(project);
                    await _context.SaveChangesAsync();
                    _toasts.Success("Projekt został dodany");
                    return RedirectToAction("Index", "Projects", new { pageNumber = pageNumber });
                }
                else
                {
                    _toasts.Warning("Projekt już istnieje");
                    return RedirectToAction("Create", "Projects", new { pageNumber = pageNumber });
                }
            }
            else
            {
                _toasts.Error("Projekt nie został dodany");
                return RedirectToAction("Create", "Projects", new { pageNumber = pageNumber });
            }
        }

        // GET: Projects/Edit/5
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Edit(int? id, int? pageNumber)
        {
            ViewData["pageNumber"] = pageNumber;

            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            ViewData["Clients"] = new SelectList(_context.Clients?
                .Where(c => c.Active == true), "ClientId", "Name");
            ViewData["UserId"] = _context.Users?
                .FirstOrDefault(o => o.Alias == CurrentUserName)?.UserId;

            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Edit(int id, int? pageNumber, [Bind("ProjectId,ClientId,Name,Type,FileName,FileLocation,Active,Order,UserId")] Project project)
        {
            ViewData["Clients"] = new SelectList(_context.Clients?
                .Where(c => c.Active == true), "ClientId", "Name");
            ViewData["UserId"] = _context.Users?
                .FirstOrDefault(o => o.Alias == CurrentUserName)?.UserId;

            var duplicate = _context.Projects
                .Where(p => p.ClientId == project.ClientId &&
                       p.Name.ToLower() == project.Name.ToLower() &&
                       p.FileLocation.ToLower() == project.FileLocation.ToLower() &&
                       p.Type == project.Type)
                .ToList()
                .Count();

            if (id != project.ProjectId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (duplicate == 0)
                {
                    try
                    {
                        _context.Update(project);
                        await _context.SaveChangesAsync();
                        _toasts.Success("Zmiany zostały zapisane");
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ProjectExists(project.ProjectId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction("Index", "Projects", new { pageNumber = pageNumber });
                }
                else
                {
                    _toasts.Warning("Projekt już istnieje");
                    return RedirectToAction("Edit", "Projects", new { pageNumber = pageNumber });
                }
            }
            else
            {
                _toasts.Error("Wystąpił błąd! Zmiany nie zostały zapisane");
                return RedirectToAction("Edit", "Projects", new { pageNumber = pageNumber });
            }
        }

        // GET: Projects/Delete/5
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Delete(int? id, int? pageNumber)
        {
            ViewData["pageNumber"] = pageNumber;

            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Clients)
                .Include(p => p.Users)
                .FirstOrDefaultAsync(m => m.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteConfirmed(int id, int? pageNumber)
        {
            if (_context.Projects == null)
            {
                return Problem("Entity set 'TCZNT5000.Projects'  is null.");
            }
            else
            {
                var project = await _context.Projects.FindAsync(id);
                if (project != null)
                {
                    _context.Projects.Remove(project);
                    await _context.SaveChangesAsync();
                    _toasts.Success("Projekt został usunięty");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _toasts.Error("Wystąpił błąd! Projekt nie został usunięty");
                    return RedirectToAction("Delete", new { id = id, pageNumber = pageNumber });
                }
            }
        }

        // GET: Clients for order
        [Authorize(Policy = "AdminOnly")]
        public IActionResult Order()
        {
            ViewData["Clients"] = new SelectList(_context.Clients?
                .Where(o => o.Active == true), "ClientId", "Name");
            return View();
        }

        // GET: GetProjects
        [HttpGet]
        [Authorize(Policy = "AdminOnly")]
        public JsonResult GetProjects(int? id)
        {
            if (id != null)
            {
                try
                {
                    var lastOrder = _context?.Projects?.Where(o => o.ClientId == id && o.Active == true);
                    if (lastOrder?.Count() > 0)
                    {
                        return Json(lastOrder);
                    }
                    else
                    {
                        return Json(new { status = "not_found", message = "Brak kroków dla wybranego projektu" });
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception (ex) as needed
                    return Json(new { status = "error", message = "An error occurred while processing your request." });
                }
            }
            else
            {
                return Json(new { status = "select_project", message = "Brak kroków dla wybranego projektu" });
            }
        }


        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> UpdateOrder(int[] stepsIds, int? clientId)
        {
            if (clientId != null)
            {
                var steps = await _context.Projects
                    .Where(s => s.ClientId == clientId && stepsIds.Contains(s.ProjectId))
                    .ToListAsync();

                if (steps.Any())
                {
                    for (int i = 0; i < stepsIds.Length; i++)
                    {
                        var step = steps.FirstOrDefault(s => s.ProjectId == stepsIds[i]);
                        if (step != null)
                        {
                            step.Order = i + 1;
                        }
                    }
                    await _context.SaveChangesAsync();
                    _toasts.Success("Kolejność kroków została zapisana");
                }
                else
                {
                    _toasts.Error("Kolejność kroków nie została zmieniona");
                }
            }
            else
            {
                _toasts.Error("Kolejność kroków nie została zapisana");
            }
            return Ok();
        }

        [Authorize(Policy = "AdminOnly")]
        private bool ProjectExists(int id)
        {
            return (_context.Projects?.Any(e => e.ProjectId == id)).GetValueOrDefault();
        }
    }
}
