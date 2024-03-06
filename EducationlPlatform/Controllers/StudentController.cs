namespace EducationlPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly Services<Student> _studentService;
        //private readonly IdentityUserDbContext _context;
        public StudentController(Services<Student> studentService) 
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {

            var students = _studentService.GetAll();


            return Ok(students);
        }

    }
}
