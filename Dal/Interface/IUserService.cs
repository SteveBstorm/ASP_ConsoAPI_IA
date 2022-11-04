using Dal.Models;

namespace Dal.Interface
{
    public interface IUserService
    {
        User Login(string idenfifiant, string password);
        void Register(NewUserModel user);
        User GetById(int id);
    }
}