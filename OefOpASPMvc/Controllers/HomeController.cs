using Infrastructuur.Files;
using Infrastructuur.Models;
using Microsoft.AspNetCore.Mvc;
using OefOpASPMvc.Models;
using System.Diagnostics;

namespace OefOpASPMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Data _studentsData;
        private static List<BmiEntity> bmis = new List<BmiEntity>();

        public HomeController(ILogger<HomeController> logger, Data studentsData)
        {
            _logger = logger;
            _studentsData = studentsData;
        }

        public IActionResult Index(string searchInput, string filter)
        {
            var students = _studentsData.GetStudentsFromFile().ToList();
            if (!string.IsNullOrEmpty(searchInput)) 
            {
                searchInput = searchInput.ToLower();
                students = students.Where(x => x.FirstName.ToLower().Contains(searchInput) ||
                                          x.LastName.ToLower().Contains(searchInput)).ToList();
            }
            switch (filter)
            {
                case "FirstName":
                    students = students.OrderBy(x => x.FirstName).ToList();
                    break;
                case "LastName":
                    students = students.OrderBy(x => x.LastName).ToList();
                    break;
                case "Points":
                    students = students.OrderByDescending(x => x.Points).ToList();
                    break;
                default:
                    students = students.OrderBy(x => x.Id).ToList();
                    break;
            }
            return View(students);
        }

        public ActionResult Bmi()
        {
            ViewData["bmis"] = bmis;
            return View();
        }
        [HttpPost]
        public ActionResult BmiPost(BmiEntity bmiVm, string weight, string length)
        {
            weight = weight.Replace(",", ".");
            length = length.Replace(",", ".");
            bmiVm.Weight = double.Parse(weight);
            bmiVm.Length = double.Parse(length);
            bmiVm.CalculateBmi();
            bmis.Add(bmiVm);
            return RedirectToAction(nameof(Bmi));
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