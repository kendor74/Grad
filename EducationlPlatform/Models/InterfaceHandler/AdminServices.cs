namespace EducationlPlatform.Models.InterfaceHandler
{
    public class AdminServices : IService<Admin>, IAdmin
    {
        public Task<Admin> Add(Admin entity)
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

        public Task<Admin> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Admin>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Admin>? GetById(int entityId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExist(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Admin> Update(Admin entity, int id)
        {
            throw new NotImplementedException();
        }
    }
}
