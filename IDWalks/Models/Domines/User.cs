using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace IDWalks.Models.Domines
{
    public class User
    {
        public Guid id { get; set; }

        public string username { get; set; }

        public string Email { get; set; }

        public string password { get; set; }

        [NotMapped]
        public List<string> Roles { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; } = string.Empty;

        public List<User_Role> UserRoles { get; set; }
    }
}
