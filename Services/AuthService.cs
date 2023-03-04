using Carpool.Database;
using Carpool.Models;
using Carpool.Services.Contracts;

namespace Carpool.Services
{
    public class AuthService : IAuthService
    {
        DatabaseContext CarpoolDb;
        public AuthService(DatabaseContext CarpoolDb)
        {
            this.CarpoolDb = CarpoolDb;
        }

        public bool IsSignedUp(string username, string password)
        {
            try
            {
                var users = CarpoolDb.Users;
                foreach (var user in users)
                {
                    if (user.UserName == username)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public int CheckPassword(string username, string password)
        {
            try
            {
                var users = CarpoolDb.Users;
                foreach(var user in users)
                {
                    if(user.UserName == username)
                    {
                        if (user.Password == password)
                        {
                            return user.Id;
                        }
                    }

                }
                return -1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<bool> SignUp(SignUpCredential signUp)
        {
            try
            {
                if (signUp.Password == signUp.ConfirmPassword)
                {
                    var newUser = new User
                    {
                        UserName = signUp.UserName,
                        Password = signUp.Password,
                    };

                    CarpoolDb.Users.Add(newUser);
                    CarpoolDb.SaveChanges();
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                return false;
            }
            
        }
    }
}
