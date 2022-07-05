using ProjectPRN211.Models;
using System;
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
            context.Add(user);
            context.SaveChanges();
        }

        public string GenerateOTP()
        {
            Random random = new Random();
            string otp = random.Next(100000, 999999).ToString();
            return otp;
        }
    }
}
