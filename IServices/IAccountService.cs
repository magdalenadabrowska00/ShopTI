using ShopTI.Models;

namespace ShopTI.IServices
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUser newUser);
        string SignInUser(Login userSignIn);
    }
}
