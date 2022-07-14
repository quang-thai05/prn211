using ProjectPRN211.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public User GetUserById(int id)
        {
            return context.Users.FirstOrDefault(x => x.UserId == id);
        }

        public User GetUserByEmail(string email)
        {
            return context.Users.FirstOrDefault(x => x.Email.Equals(email));
        }

        public List<User> GetUsersByRole(int role)
        {
            return context.Users.Where(x => x.RoleId == role).ToList();
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

        public void Update(User user, int id)
        {
            User u = context.Users.FirstOrDefault(x => x.UserId == id);
            u.UserName = user.UserName;
            u.Address = user.Address;
            u.Phone = user.Phone;
            u.DateOfBirth = user.DateOfBirth;
            context.SaveChanges();
        }

        public string GenerateOTP()
        {
            Random random = new Random();
            string otp = random.Next(100000, 999999).ToString();
            return otp;
        }

        public void ChangePassword(string pass, int id)
        {
            User u = context.Users.FirstOrDefault(x => x.UserId == id);
            u.Password = pass;
            context.SaveChanges();
        }

        public string GenerateRandomPass()
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            for (int i = 0; i < 8; i++)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        public void UpdateToDoctor(int id)
        {
            User u = context.Users.FirstOrDefault(x => x.UserId == id);
            u.RoleId = 2;
            context.SaveChanges();
        }

        public void UpdateToPatient(int id)
        {
            User u = context.Users.FirstOrDefault(x => x.UserId == id);
            u.RoleId = 3;
            context.SaveChanges();
        }

        public void ChangeHos(int user, int hospital)
        {
            User u = context.Users.FirstOrDefault(x => x.UserId == user);
            u.HospitalId = hospital;
            context.SaveChanges();
        }
    }
}
