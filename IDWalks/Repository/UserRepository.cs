using IDWalks.Data;
using IDWalks.Models.Domines;
using Microsoft.EntityFrameworkCore;

namespace IDWalks.Repository
{
    public interface IUserRepository
    {
        Task<User> AuthenticateAsync(string username, string password);
    }
    public class UserRepository : IUserRepository

    {
        private readonly IndiaWalkDbContext indiaWalkDbContext;

        public UserRepository(IndiaWalkDbContext indiaWalkDbContext)
        {
            this.indiaWalkDbContext = indiaWalkDbContext;
        }
        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = await indiaWalkDbContext.users.FirstOrDefaultAsync(x => x.username.ToLower()== username.ToLower()  && x.password == password);

            if (user == null)
            {
                return null;
            }

            var userRoles = await indiaWalkDbContext.usersRoles.Where(x => x.Userid == user.id).ToListAsync();


            if(userRoles.Any())
            {
                user.Roles = new List<string>();
                foreach (var item in userRoles)
                {
                    var role = await indiaWalkDbContext.roles.FirstOrDefaultAsync(x => x.Id == item.RoleId);
                    if(item != null)
                    {
                        user.Roles.Add(role.Name);
                    }
                }
            }

            user.password = null;
            return user;
        }
    }
}
