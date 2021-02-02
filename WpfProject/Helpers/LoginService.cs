using System;
using System.Collections.Generic;
using System.Text;
using WpfProject.Models;

namespace WpfProject.Helpers
{
    public static class LoginService
    {
        public static AppRole Role { get; set; } = AppRole.None;
        public static User user;
        public static void Login((AppRole role,User user) data)
        {
            Role = data.role;
            user = data.user;
        }

        public static void Logout()
        {
            Role = AppRole.None;
            user = null;
        }
    }
}
