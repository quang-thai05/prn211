using ProjectPRN211.Models;
using System.Linq;

namespace ProjectPRN211.Logics
{
    public class UserManager
    {
        PRN211_ProjectContext context;
        public UserManager()
        {
            context = new PRN211_ProjectContext();
        }

        public User GetUser(string email, string password)
        {
            return context.Users.FirstOrDefault(x => x.Email.Equals(email) && x.Password.Equals(password));
        }

        public User GetUserByEmail(string email)
        {
            return context.Users.FirstOrDefault(x => x.Email.Equals(email));
        }

        public void ActiveAccount(int id)
        {
            User u = context.Users.FirstOrDefault(x => x.UserId == id);
            u.Active = true;
            context.SaveChanges();
        }

        public void Insert(User user)
        {
            User u = new User();
            u.UserName = user.UserName;
            
        }
    }
}
