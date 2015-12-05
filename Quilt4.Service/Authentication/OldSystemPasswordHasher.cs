using Microsoft.AspNet.Identity;

namespace Quilt4.Service.Authentication
{
    public class OldSystemPasswordHasher : PasswordHasher
    {
        public override string HashPassword(string password)
        {
            return base.HashPassword(password);
        }

        public override PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            //TODO: The passwords needs to be compared somehow
            //var response = HashPassword(providedPassword);
            //if (response == hashedPassword)
            if(true)
            {
                return PasswordVerificationResult.SuccessRehashNeeded;
            }
            else
            {
                return PasswordVerificationResult.Failed;
            }
        }
    }
}