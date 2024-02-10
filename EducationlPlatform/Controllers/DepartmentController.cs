using Microsoft.AspNetCore.Authorization;

namespace EducationlPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IRepository<Department> _repository;

        public DepartmentController(IRepository<Department> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDepartments()
        {
            var list = await _repository.GetAll();

            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var list = await _repository.FindById(id);

            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment(Department Department)
        {
            await _repository.Create(Department);

            return Ok(await _repository.GetAll());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Department Department)
        {
            var result = await _repository.Update(id, Department);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.Delete(id);
            return Ok(await _repository.GetAll());
        }

        [HttpGet("Search")]
        public async Task<IActionResult> SearchByLetter(string Letter)
        {
            var list = _repository.Search(l => l.DepartmentName.StartsWith(Letter));

            return Ok((list.IsNullOrEmpty()) ? "No Data Found": list);
        }
    }
}
