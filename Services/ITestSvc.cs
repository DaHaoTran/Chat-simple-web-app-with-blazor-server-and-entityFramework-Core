using ChatSimple.Data;
using Microsoft.AspNetCore.Mvc;

namespace ChatSimple.Services
{
    public interface ITestSvc
    {
        Task<List<Message>> GetMessages();
        Task<bool> Login(string email, string password);
        Task<Message> AddNewMessage(Message message);
        Task<Account> GetAccount(string email);
    }
}
