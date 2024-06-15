namespace EducationlPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly Services<Room> _Rooms;
        public RoomController(Services<Room> Room)
        {
            _Rooms = Room;
        }

        [HttpGet("GetAllRooms")]
        public async Task<IActionResult> GetRooms()
        {
            var result = await _Rooms.GetAll();
           
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddRoom(int duration, int tutorRate)
        {
            Room room = new Room
            {
                Duration = duration,
                Cost = duration * tutorRate,
            };
            var result = await _Rooms.Create(room);
            return Ok(result);
        }

        
    }
}
