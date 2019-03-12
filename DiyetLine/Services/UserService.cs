using DiyetLine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiyetLine
{
    public class UserService
    {
        public Table_Kullanicilar ValidateUser(string email, string password)
        {
            // Here you can write the code to validate
            // User from database and return accordingly
            // To test we use dummy list here

            var userList = GetUserList();
            var user = userList.FirstOrDefault(x => x.Email == email && x.Sifre == password);
            return user;
        }
        private DiyetlineEntities db = new DiyetlineEntities();
        public List<Table_Kullanicilar> GetUserList()
        {
            return db.Table_Kullanicilar.ToList();

            // Create the list of user and return           
        }
    }
}
