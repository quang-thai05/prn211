﻿using ProjectPRN211.Models;
using System;
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
    }
}
