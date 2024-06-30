using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TestUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        List<Tutor> _list;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _list = new List<Tutor>
            {
                    new Tutor { Id = 1 ,  Name = "Harvey Specter",  Descrp  = "An experienced C# tutor with a passion for teaching ASP .Net FrameWork, MVC Core and Web API.",
                        Field = "C#", ImageSrc = "http://emilcarlsson.se/assets/harveyspecter.png" , Email = "HarveySpecter@gmail.com" ,Rate = 100
                        ,Phone = "01002631470"},
                    new Tutor { Id = 2 ,  Name = "Jonathan Sidwell",  Descrp  = "An experienced English tutor with a passion for teaching English literature and linguistics",
                        Field = "English", ImageSrc = "http://emilcarlsson.se/assets/jonathansidwell.png" , Email = "JonathanSidwell@gmail.com" ,Rate = 10
                        ,Phone = "01234568100"},
                    new Tutor { Id = 3 ,  Name = "Charles Forstman",  Descrp  = "An experienced Python and Image Processing tutor with a passion for teaching image processing and enhancment.",
                        Field = "Python", ImageSrc = "http://emilcarlsson.se/assets/charlesforstman.png" , Email = "CharlesForstman@gmail.com" ,Rate = 10
                        ,Phone = "01122338810"},
                    new Tutor { Id = 4,  Name = "Katrina Bennett",  Descrp  = "An experienced math tutor with a passion for teaching algebra, calculus, and geometry.",
                        Field = "Mathematics", ImageSrc = "http://emilcarlsson.se/assets/katrinabennett.png" , Email = "KatrinaBennett@gmail.com" ,Rate = 25
                        ,Phone = "01506806510"},
                    new Tutor { Id = 5 ,  Name = "Daniel Hardman",  Descrp  = "An experienced math tutor with a passion for teaching ASP .Net FrameWork, MVC Core and Web API.",
                        Field = ".Net", ImageSrc = "http://emilcarlsson.se/assets/danielhardman.png" , Email = "DanielHardman@gmail.com" ,Rate = 25
                        ,Phone = "01111122365"},
                    new Tutor { Id = 6 ,  Name = "Harold Gunderson",  Descrp  = "An experienced math tutor with a passion for teaching Geography and Countries.",
                        Field = "Geography", ImageSrc = "http://emilcarlsson.se/assets/haroldgunderson.png" , Email = "HaroldGunderson@gmail.com" ,Rate = 25
                        ,Phone = "01002631470"},
                    new Tutor { Id = 7 ,  Name = "Jessica Pearson",  Descrp  = "An experienced math tutor with a passion for teaching USA History and Euorpe History.",
                        Field = "History", ImageSrc = "http://emilcarlsson.se/assets/jessicapearson.png" , Email = "JessicaPearson@gmail.com" ,Rate = 100
                        ,Phone = "01506806510"},
            };
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
            //ViewBag.UserId = userId;
            //ViewBag.RoomId = roomId;
            ViewBag.InitialTime = 3600; // 1 hour for example
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


        public IActionResult SearchField(string searchBar)
        {
            var list = _list;
            list = list.Where(l => l.Field.Equals(searchBar, StringComparison.OrdinalIgnoreCase)).ToList();
            ViewBag.search = searchBar;
            
            
            return View("Tutor",list);
        }
        

        public IActionResult Tutor(Tutor tutor)
        {

            var list = _list;
            if (tutor != null)
            {
                var filteredTutors = _list.AsQueryable();
                if (!string.IsNullOrEmpty(tutor.Field))
                {
                    filteredTutors = filteredTutors.Where(t => t.Field.Equals(tutor.Field, StringComparison.OrdinalIgnoreCase));
                    ViewBag.field = tutor.Field;
                }
                if (tutor.Rate != 0)
                {
                    filteredTutors = filteredTutors.Where(t => t.Rate == tutor.Rate);
                    ViewBag.rate = tutor.Rate;
                }

                var filteredList = filteredTutors.ToList();
                list = filteredList;

                // Check if there are more than one tutor in any field
                var fieldCounts = filteredList.GroupBy(t => t.Field)
                                              .ToDictionary(g => g.Key, g => g.Count());

                bool multipleTutorsInAnyField = fieldCounts.Any(kv => kv.Value > 1);

                // Set ViewBag.search based on the number of tutors in fields
                ViewBag.search = multipleTutorsInAnyField ? "" : filteredList.FirstOrDefault()?.Field;
            }

            // Return the view with the filtered list
            return View(list);
        }

        
        public IActionResult TutorPageIndividual(int id)
        {
            var t = _list.FirstOrDefault(x => x.Id == id);
            return View(t);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }

    
}
