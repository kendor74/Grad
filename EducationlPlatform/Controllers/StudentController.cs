namespace EducationlPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IRepository<Student> _studentService;
        public StudentController(IRepository<Student> studentService) 
        {
            _studentService = studentService;
        }



    }
}
