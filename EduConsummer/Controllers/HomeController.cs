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
                    new Tutor { Id = 1 ,  Name = "Mohamed Abdulaziz",  Descrp  = "Mohamed is an experienced math tutor with a passion for teaching algebra, calculus, and geometry.",
                        Field = "Arabic", ImageSrc = "Male-tutor-1440x960.jpeg" , Email = "mohamed.abdulaziz@gmail.com" ,Rate = 100
                        ,Phone = "01002631470"},
                     new Tutor { Id = 6 ,  Name = "Mohamed Abdulaziz",  Descrp  = "Mohamed is an experienced math tutor with a passion for teaching algebra, calculus, and geometry.",
                        Field = "Arabic", ImageSrc = "Male-tutor-1440x960.jpeg" , Email = "mohamed.abdulaziz@gmail.com" ,Rate = 10
                        ,Phone = "01002631470"},
                      new Tutor { Id = 7 ,  Name = "Mohamed Abdulaziz",  Descrp  = "Mohamed is an experienced math tutor with a passion for teaching algebra, calculus, and geometry.",
                        Field = "Arabic", ImageSrc = "Male-tutor-1440x960.jpeg" , Email = "mohamed.abdulaziz@gmail.com" ,Rate = 10
                        ,Phone = "01002631470"},
                    new Tutor { Id = 2,  Name = "Mohamed Abdulaziz",  Descrp  = "Mohamed is an experienced math tutor with a passion for teaching algebra, calculus, and geometry.",
                        Field = "Mathematics", ImageSrc = "Male-tutor-1440x960.jpeg" , Email = "mohamed.abdulaziz@gmail.com" ,Rate = 25
                        ,Phone = "01002631470"},
                    new Tutor { Id = 3 ,  Name = "Mohamed Abdulaziz",  Descrp  = "Mohamed is an experienced math tutor with a passion for teaching algebra, calculus, and geometry.",
                        Field = "Mathematics", ImageSrc = "Male-tutor-1440x960.jpeg" , Email = "mohamed.abdulaziz@gmail.com" ,Rate = 25
                        ,Phone = "01002631470"},
                    new Tutor { Id = 4 ,  Name = "Mohamed Abdulaziz",  Descrp  = "Mohamed is an experienced math tutor with a passion for teaching algebra, calculus, and geometry.",
                        Field = "Mathematics", ImageSrc = "Male-tutor-1440x960.jpeg" , Email = "mohamed.abdulaziz@gmail.com" ,Rate = 25
                        ,Phone = "01002631470"},
                    new Tutor { Id = 8 ,  Name = "Mohamed Abdulaziz",  Descrp  = "Mohamed is an experienced math tutor with a passion for teaching algebra, calculus, and geometry.",
                        Field = "Mathematics", ImageSrc = "Male-tutor-1440x960.jpeg" , Email = "mohamed.abdulaziz@gmail.com" ,Rate = 100
                        ,Phone = "01002631470"},
                    new Tutor { Id = 5 ,  Name = "Mohamed El-Hoseny",  Descrp  = "Mohamed specializes in physics tutoring, covering topics such as mechanics, thermodynamics, and electromagnetism.",
                        Field = "Physics", ImageSrc = "201710_students_Johannsen_Shane_Tutoring_02.jpg", Rate = 20,
                        Email = "mohamed.abdulaziz@gmail.com" , Phone = "01002631470"},
                    // Add more items as needed
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
