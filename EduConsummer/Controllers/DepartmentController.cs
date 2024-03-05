using Microsoft.AspNetCore.Mvc;

namespace EduConsummer.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IApiRequest<Department> _request;

        public DepartmentController(IApiRequest<Department> request)
        {
            _request = request;
        }


        
        public async Task<IActionResult> Departments()
        {
            var list = await _request.GetAll("api/Department/GetDepartments");    
            return View(list);
        }



        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateDepartment(Department department)
        {
            var result = await _request.Post("api/Department/CreateDepartment", department);
            if(result)
                return RedirectToAction("Departments" , "Department");
            else
                return RedirectToAction("Create");
        }
    }
}
