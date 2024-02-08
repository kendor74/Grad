namespace EducationlPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentRoomController : ControllerBase
    {
        private readonly IRepository<StudentRoom> _context;
        public StudentRoomController(IRepository<StudentRoom> context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task <IActionResult> GetAllstudentRoom() 
        {
            var list = await _context.GetAll();
            return Ok(list);
        }
        /*
        [HttpGet("{id}")]
        public async Task<IActionResult> GetbyTutorsId(int id)
        {
            var tutor = await _context.studentRoom.FindAsync(id);
            return Ok(tutor);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetbyStudnetId(int id)
        {
            var student = await _context.studentRoom.FindAsync(id);
            return Ok(student);
        }
        */


        [HttpPost]
        public async Task<IActionResult> AddStudentToTutor(StudentRoom studentRoom)
        {
            var result = await _context.Create(studentRoom);

            if (ModelState.IsValid)
                return Ok(result);
            else
                return BadRequest("There is a problem While Adding!!");
            
        }


        [HttpPut]
        public async Task<IActionResult> UpdaEducationlPlatformudentTutor(StudentRoom studentRoom , int id)
        {
            
            if (studentRoom != null)
            {
                
                var result = await _context.Update(id, studentRoom);
                return Ok(result);
            }

            return BadRequest("Invalid Model State");
        }
    }
}
