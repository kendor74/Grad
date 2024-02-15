﻿namespace TestUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();

        }   
        
        public IActionResult Chat()
        {
            return View();
        }

     

        public IActionResult LiveStream()
        {
            return View();
        }

        public IActionResult LiveStreamRoom()
        {
            return View();
        }
        
        
        public IActionResult ExitTheStream()
        {
            return RedirectToAction("LiveStreamRoom");
        }
        
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
