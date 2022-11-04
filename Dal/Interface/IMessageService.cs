using Dal.Models;

namespace Dal.Interface
{
    public interface IMessageService
    {
        IEnumerable<Message> GetAll();
        void Post(string content, string Token);
    }
}