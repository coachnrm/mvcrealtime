using Microsoft.AspNetCore.Mvc;
using SignalRDemo3ytEFC.Data;
using SignalRDemo3ytEFC.Models;

namespace SignalRDemo3ytEFC.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

    }
}