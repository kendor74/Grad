namespace EducationlPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentTutorRoomController : ControllerBase
    {
        //Need work
        private readonly Services<StudentTutorRoom> _context;
        public StudentTutorRoomController(Services<StudentTutorRoom> context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task <IActionResult> GetAllstudentRoom() 
        {
            var list = await _context.GetAll(q => q.Include(r => r.Room).Include(t => t.Tutor).Include(s => s.Student));
            return Ok(list);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetbyTutorId(int id)
        {
            var tutor = await _context.FindById(id);
            return Ok(tutor);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetbyStudnet(int id)
        {
            var student = await _context.FindById(id);
            return Ok(student);
        }



        //adding an extra student into Room
        [HttpPost]
        public async Task<IActionResult> AddStudent(StudentTutorRoom studentRoom)
        {
            
            if (studentRoom != null)
            {
                
                var result = await _context.Create(studentRoom);
                return Ok(result);
            }

            return BadRequest("Invalid Model State");
        }
    }
}
