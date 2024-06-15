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
            //_studentService = studentService;
            _studentService = studentService;
        }

        [HttpGet("GetAllStudents")]
        public async Task<IActionResult> GetStudents()
        {

            var students = await _studentService.GetAll(q => q.Include(u => u.User));


            return Ok(students);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {

            var students = await _studentService.FindById( id ,q => q.Include(u => u.User));

            if (students == null)
                return BadRequest("Entity Not Found");

            return Ok(students);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(Student st)
        {
            var student = await _studentService.Create(st , "User");

            return Ok(student);
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _studentService.Delete(id);

            return Ok(student);
        }
    }
}
