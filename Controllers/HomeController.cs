using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;
using SignalRDemo3ytEFC.Data;
using SignalRDemo3ytEFC.Models;
using SignalRDemo3ytEFC.Reports;
using System.Diagnostics;
using SignalRDemo3ytEFC.Repositories;
using SelectPdf;
using Microsoft.EntityFrameworkCore;

namespace SignalRDemo3ytEFC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _oHostEnvironment;
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment oHostEnvironment, ApplicationDbContext context)
        {
            _logger = logger;
            _oHostEnvironment = oHostEnvironment;
            _context = context;
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

            List<UpdateProductViewModel> products = new List<UpdateProductViewModel>();
            UpdateProductViewModel product = new UpdateProductViewModel();
            for (int i=1; i <=6; i++)
            {
                
            }

            StudentReport rpt = new StudentReport(_oHostEnvironment);
            return File(rpt.Report(students), "application/pdf");
        }
    }
}