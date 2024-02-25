namespace EduConsummer.Models
{
    public interface IApiRequest<T>
    {
        Task<List<T>> GetAll(string api);
        Task<T> GetById(string api, int id);
        Task<T> GetByEmail(string api, string Email);
        Task<T> Post(string api, T entity);
        Task<T> Put(string api, T entity);
        Task<T> Delete(string api, int id);

        Task<string> Login(string Email , string Password);
        Task<string> SigUp(string Email , string Password);
        Task<string> Logout();
    }
}
