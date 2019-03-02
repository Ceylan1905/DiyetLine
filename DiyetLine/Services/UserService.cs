using DiyetLine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiyetLine
{
    public class UserService
    {
        public Table_Kullanıcılar ValidateUser(string email, string password)
        {
            // Here you can write the code to validate
            // User from database and return accordingly
            // To test we use dummy list here

            var userList = GetUserList();
            var user = userList.FirstOrDefault(x => x.Email == email && x.Sifre == password);
            return user;
        }
        private diyetlineEntities db = new diyetlineEntities();
        public List<Table_Kullanıcılar> GetUserList()
        {
            return db.Table_Kullanıcılar.ToList();

            // Create the list of user and return           
        }
    }
}
