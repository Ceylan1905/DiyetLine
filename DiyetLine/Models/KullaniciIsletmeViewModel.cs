using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiyetLine.Models
{
    public class KullaniciIsletmeViewModel
    {
        public Table_Kullanicilar kullanici { get; set; }
        public  Table_IsletmeSahibi isletme_sahibi { get; set; }
     
    }
}