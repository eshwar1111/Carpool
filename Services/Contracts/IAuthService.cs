using Carpool.Models;

namespace Carpool.Services.Contracts
{
    public interface IAuthService
    {
        public int CheckPassword(string username, string password);
        public bool IsSignedUp(string username, string password);
        public Task<bool> SignUp(SignUpCredential signUp);
    }
}