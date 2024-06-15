namespace EducationlPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentTutorRoomController : ControllerBase
    {
        //Need work
        private readonly IdentityUserDbContext _context;

        public StudentTutorRoomController(IdentityUserDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task <IActionResult> GetAllstudentRoom() 
        {

            var list = await _context.StudentTutorRoom.Include(str => str.Student)
                     .ThenInclude(s => s.User)
                     .Include(str => str.Tutor)
                     .ThenInclude(t => t.User)
                     .Include(str => str.Room)
                     .ToListAsync();
            
            return Ok(list);
        }

        [HttpPost("AddParticepents")]
        public async Task<IActionResult> Create(List<StudentTutorRoomDto> particepents)
        {
            List<string> result = new List<string>();
           
            foreach (var item in particepents)
            {
                StudentTutorRoom str = new StudentTutorRoom
                {
                    RoomId = item.RoomId,
                    TutorId = item.TutorId,
                    StudentId = item.StudentId
                };
                var temp = await _context.AddAsync(str);
                result.Add(temp.ToString());
                await _context.SaveChangesAsync();
            }

            return Ok(result);
        }

        [HttpPost("AddMoreStudents")]
        public async Task<IActionResult> AddMoreStudent(List<int> Ids, int roomId)
        {
            List<StudentTutorRoom> particepents = await _context.StudentTutorRoom
                .Where(r => r.RoomId == roomId).ToListAsync();

            foreach (int item in Ids)
            {
                StudentTutorRoom str = new StudentTutorRoom
                {
                    TutorId = particepents[0].TutorId,
                    StudentId = item,
                    RoomId = roomId
                };
                particepents.Add(str);
                await _context.AddAsync(str);
                await _context.SaveChangesAsync();
            }

            return Ok(particepents);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRoom(int roomId)
        {
            var particepents = await _context.StudentTutorRoom
                 .Where(r => r.RoomId == roomId).ToListAsync();
            
            foreach(var item in particepents)
            {
                 _context.Remove(item);
                await _context.SaveChangesAsync();
            }
            return Ok(particepents + "\n Deleted successfully");
        }
    }
}
