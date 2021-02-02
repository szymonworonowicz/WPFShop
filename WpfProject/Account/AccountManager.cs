using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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

        public static bool RegisteUser(User user)
        {
            if (context.Users.Any(x => x.Name == user.Name) == false) // uzytkownik nie istnieje w pazie
            {
                var hashedpassword = HashPassword(user.Password);

                user.Password = hashedpassword;

                context.Adreses.Add(user.UserData.Adres);
                context.SaveChanges();

                user.UserData.AdresId = user.UserData.Adres.AdresId;
                context.UserData.Add(user.UserData);
                context.SaveChanges();

                user.UserDataId = user.UserData.Id;
                context.Users.Add(user);

                context.SaveChanges();

                return true;
            }

            return false;
        }

        public static bool RegisterAdmin(User user)
        {
            if (context.Users.Any(x => x.Name == user.Name) == false) // uzytkownik nie istnieje w pazie
            {
                var hashedpassword = HashPassword(user.Password);

                user.Password = hashedpassword;

                context.Users.Add(user);

                context.SaveChanges();

                return true;
            }

            return false;
        }

        public static (AppRole role, User user) Login(User user)
        {
            User userFromDb = context.Users
                .Include(x => x.UserData)
                .ThenInclude(x => x.Adres)
                .FirstOrDefault(x => x.Name == user.Name);

            if (userFromDb == null)
            {
                return (AppRole.NotExist, null);
            }

            if (VerifyPassword(user.Password, userFromDb.Password) == true)
            {
                return (userFromDb.Role, userFromDb);
            }
            return (AppRole.BadPassword, null);
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
            for (int i = 0; i < hashedpassword.Length || i < hashed.Length; i++)
            {
                if (hashedpassword[i].ToString() != hashed[i].ToString())
                {
                    return false;
                }
            }

            return true;
        }

        public static bool ChangePassword(string oldPassword, string newPassword)
        {
            var user = LoginService.user;
            if (VerifyPassword(oldPassword, user.Password))
            {
                user.Password = HashPassword(newPassword);
                try
                {
                    context.Users.Update(user);
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {

                    return false;
                }
                return true;
            }
            return false;
        }
    }
}
