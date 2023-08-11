using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MVC_AdoptionLab.Models;
using System.Diagnostics;

namespace MVC_AdoptionLab.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private AdoptionDbContext _adoptionDbContext = new AdoptionDbContext();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Animal> result = _adoptionDbContext.Animals.ToList();
            return View(result);
        }

        public IActionResult Results(int id)
        {
            Animal a = _adoptionDbContext.Animals.FirstOrDefault(x => x.Id == id);
            return View(a);
        }

        public IActionResult RemoveAnimal(int id)
        {
            Animal a = _adoptionDbContext.Animals.FirstOrDefault(x => x.Id == id);
            _adoptionDbContext.Animals.Remove(a);
            _adoptionDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult AddAnimal(Animal a)
        {
            _adoptionDbContext.Animals.Add(a);
            _adoptionDbContext.SaveChanges();
            return RedirectToAction("Index");
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