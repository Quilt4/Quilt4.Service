using System.Linq;

namespace Quilt4.Service.Repository.SqlRepository
{
    internal static class RepositoryExtensions
    {
        public static int GetUserIdByName(this Quilt4DataContext context, string userName)
        {
            var aspNetUsers = context.AspNetUsers.Single(x => x.UserName == userName);
            var user = context.Users.SingleOrDefault(x => x.ExternalUserId == aspNetUsers.Id);
            if (user == null)
            {
                user = new User { ExternalUserId = aspNetUsers.Id };
                context.Users.InsertOnSubmit(user);
                context.SubmitChanges();
            }

            return user.UserId;
        }
    }
}