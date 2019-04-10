using DiyetLine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiyetLine
{
    public class UserService
    {
        public KullaniciIsletmeViewModel ValidateUser(string email, string password)
        {
            // Here you can write the code to validate
            // User from database and return accordingly
            // To test we use dummy list here

            var userList = GetUserList();
            var user = userList.FirstOrDefault(x => x.isletme_sahibi.Restorant_Email == email && x.isletme_sahibi.Sifre == password || x.kullanici.Email==email && x.kullanici.Sifre==password);
            return user;
        }
        private diyetlineEntities db = new diyetlineEntities();
        public List<KullaniciIsletmeViewModel> GetUserList()
        {
           List <KullaniciIsletmeViewModel> km = new List<KullaniciIsletmeViewModel>();
            return km;

            // Create the list of user and return           
        }
    }
}
