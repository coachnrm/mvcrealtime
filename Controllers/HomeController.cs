using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;
using SignalRDemo3ytEFC.Models;
using SignalRDemo3ytEFC.Reports;
using System.Diagnostics;

namespace SignalRDemo3ytEFC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _oHostEnvironment;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment oHostEnvironment)
        {
            _logger = logger;
            _oHostEnvironment = oHostEnvironment;
        }

        public IActionResult Index()
        {
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

        public IActionResult PrintStudent(int param)
        {
            List<Student> students = new List<Student>();
            for (int i = 1; i <= 9; i++)
            {
                students.Add(new Student()
                {
                    StudentId = i,
                    Name = "Stu" + i,
                    Roll = "100" + i
                });
            }

            List<Product> products = new List<Product>();
            for (int i =1; i <=9; i++)
            {
                products.Add(new Product()
                {
                    Id = i,
                    Name = "x",
                    Category = "x",
                    Price = 400
                });
            }

            StudentReport rpt = new StudentReport(_oHostEnvironment);
            return File(rpt.Report(students, products), "application/pdf");
        }

    }
}