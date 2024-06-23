using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace EducationlPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IdentityUserDbContext _context;
        private readonly UserManager<User> _userManager;  // Inject UserManager

        public ChatController(IdentityUserDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetContactsForTutor(int tutorId)
        {
            var contacts = await _context.Contacts
            .Where(c =>  c.Tutor.Id == tutorId)
            .Include(c => c.Student)
			.Include(c => c.Tutor)
			.ToListAsync();

            return Ok(contacts);
        }


        [HttpPost]
        public async Task<IActionResult> AddContact(int tutorId, int studentId)
        {
            Contact contact = new Contact
            {
                TutorId = tutorId,
                StudentId = studentId
            };

            var result = await _context.Contacts.AddAsync(contact);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> SaveMessage(int studentId, int tutorId, string message)
        {
            var contact = await _context.Contacts
				.Where(c => c.TutorId == tutorId && c.StudentId == studentId)
                .FirstOrDefaultAsync();
			Message m = new Message
            {
                Text = message,
                ContactId = contact.Id,
                Timestamp = DateTime.Now
            };

            var result = await _context.Messages.AddAsync(m);
            await _context.SaveChangesAsync();
            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetMessages(int studentId, int tutorId)
        {
			var contact = await _context.Contacts
			   .Where(c => c.TutorId == tutorId && c.StudentId == studentId)
			   .FirstOrDefaultAsync();
            var messages = await _context.Messages
                .Where(m => m.ContactId == contact.Id).ToListAsync();

            return Ok(messages);
        }


    }
}
