using Infrastructuur.Files;
using Microsoft.AspNetCore.Mvc;
using OefOpASPMvc.Models;
using System.Diagnostics;

namespace OefOpASPMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Data _studentsData;


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