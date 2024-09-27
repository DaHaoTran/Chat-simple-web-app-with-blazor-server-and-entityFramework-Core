using ChatSimple.Context;
using ChatSimple.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatSimple.Services
{
    public class TestSvc : ITestSvc
    {
        private readonly TestContext _context;
        public TestSvc(TestContext context)
        {
            _context = context;
        }

        public async Task<Message> AddNewMessage(Message message)
        {
            message.time = DateTime.Now;
            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
            return message;
        }

        public async Task<List<Message>> GetMessages()
        {
            return await _context.Messages.ToListAsync(); 
        }

        public async Task<Account> GetAccount(string email)
        {
            return await _context.Accounts.Where(x => x.email == email).SingleOrDefaultAsync();
        }

        public async Task<bool> Login(string email, string password)
        {
            try
            {
                var byemail = await _context.Accounts.Where(x => x.email == email).SingleOrDefaultAsync();
                if (byemail != default)
                {
                    var bypass = await _context.Accounts.Where(x => x.password == password).SingleOrDefaultAsync();
                    if (bypass != default)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
