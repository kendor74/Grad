namespace EducationlPlatform.Models.InterfaceHandler.ServicesHandler
{
    public class TransactionServices : IService<Transaction>
    {
        public Task<Transaction> Add(Transaction entity)
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

        public Task<Transaction> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Transaction>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Transaction>? GetById(int entityId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExist(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Transaction> Update(Transaction entity, int id)
        {
            throw new NotImplementedException();
        }
    }
}
