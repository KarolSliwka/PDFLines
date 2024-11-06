using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDFLines.Data;
using PDFLines.Models;
using System.Diagnostics;

namespace PDFLines.Controllers
{
    [Authorize(Policy = "AllUsers")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TCZNT5000 _context;

        public HomeController(ILogger<HomeController> logger, TCZNT5000 context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var SMTCount = _context.Projects?
                .Where(p => p.Type == 1 && p.Active == true)
                .Count();

            var NonSMTCount = _context.Projects?
                .Where(p => p.Type == 2 && p.Active == true)
                .Count();

            var BackendCount = _context.Projects?
                .Where(p => p.Type == 3 && p.Active == true)
                .Count();

            var FabCount = _context.Projects?
                .Where(p => p.Type == 4 && p.Active == true)
                .Count();

            var IntCount = _context.Projects?
                .Where(p => p.Type == 5 && p.Active == true)
                .Count();

            var WelCount = _context.Projects?
                .Where(p => p.Type == 6 && p.Active == true)
                .Count();

            var StaCount = _context.Projects?
                .Where(p => p.Type == 7 && p.Active == true)
                .Count();

            var PntCount = _context.Projects?
                .Where(p => p.Type == 8 && p.Active == true)
                .Count();

            ViewData["SMTCount"] = SMTCount == null ? 0 : SMTCount;
            ViewData["NonSMTCount"] = NonSMTCount == null ? 0 : NonSMTCount;
            ViewData["Backend"] = BackendCount == null ? 0 : BackendCount;
            ViewData["FabricationCount"] = FabCount == null ? 0 : FabCount;
            ViewData["IntegrationCount"] = IntCount == null ? 0 : IntCount;
            ViewData["WeldingCount"] = WelCount == null ? 0 : WelCount;
            ViewData["StampingCount"] = StaCount == null ? 0 : StaCount;
            ViewData["PaintshopCount"] = PntCount == null ? 0 : PntCount;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}