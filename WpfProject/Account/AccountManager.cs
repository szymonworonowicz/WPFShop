using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WpfProject.DAL;
using WpfProject.Helpers;
using WpfProject.Models;

namespace WpfProject.Account
{
    public static class AccountManager
    {
        private static DataContext context;
        static AccountManager()
        {
            context = DataContextAccesor.GetDataContext();
        }

        public static async Task<bool> Register(User user)
        {
            if (await context.Users.AnyAsync(x => x.Name == user.Name) == false) // uzytkownik nie istnieje w pazie
            {
                var hashedpassword = HashPassword(user.Password);

                user.Password = hashedpassword;

                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public static async Task<(AppRole role,User user)> Login(User user)
        {
            User userFromDb = await context.Users.FirstOrDefaultAsync(x => x.Name == user.Name);

            if (userFromDb == null)
            {
                return (AppRole.NotExist,null);
            }

            if (VerifyPassword(user.Password, userFromDb.Password) == true)
            {
                return (userFromDb.Role,userFromDb);
            }
            return (AppRole.BadPassword,null);
        }

        private static string HashPassword(string password)
        {
            var passwordHasher = new MD5CryptoServiceProvider();
            var buffer = passwordHasher.ComputeHash(Encoding.ASCII.GetBytes(password));

            var sb = new StringBuilder();

            foreach (var item in buffer)
            {
                sb.Append(item.ToString());
            }
            return sb.ToString();
        }

        private static bool VerifyPassword(string password, string hashed)
        {
            var hashedpassword = HashPassword(password);
            for (int i = 0; i < hashedpassword.Length || i < hashed.Length;i++)
            {
                if (hashedpassword[i].ToString() != hashed[i].ToString())
                {
                    return false;
                }
            }

            return true;
        }
    }
}
