using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationlPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatSystemController : ControllerBase
    {
        private readonly IdentityUserDbContext _context;
        private readonly UserManager<User> _userManager;  // Inject UserManager

        public ChatSystemController(IdentityUserDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetContactsForTutor(int tutorId)
        {
            var contacts = await _context.Contacts
            .Where(c => c.Tutor.Id == tutorId)
            .Include(c => c.Student)
            .Include(c => c.Tutor)
            .ToListAsync();

            return Ok(contacts);
        }


        



    }
}
