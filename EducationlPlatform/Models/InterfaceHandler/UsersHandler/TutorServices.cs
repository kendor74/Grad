using EducationlPlatform.Models.Interfaces.UserInterfaces;

namespace EducationlPlatform.Models.InterfaceHandler.UsersHandler
{
    public class TutorServices : IService<Tutor>, ITutor
    {
        public Task<Tutor> Add(Tutor entity)
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

        public Task<Tutor> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Tutor>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Tutor>? GetById(int entityId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExist(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Tutor> Update(Tutor entity, int id)
        {
            throw new NotImplementedException();
        }
    }
}
