using EducationlPlatform.Models.Interfaces.UserInterfaces;

namespace EducationlPlatform.Models.InterfaceHandler.UsersHandler
{
    public class StudentServices : IService<Student>, IStudent
    {
        public Task<Student> Add(Student entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> Count()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int entityId)
        {
            throw new NotImplementedException();
        }

        public Task<Student> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Student>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Student>? GetById(int entityId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExist(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Student> Update(Student entity, int id)
        {
            throw new NotImplementedException();
        }
    }
}
