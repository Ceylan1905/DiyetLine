using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DiyetLine.Models
{
    public class ViewModel
    {

        public int IlId { get; set; }
        public int IlceId { get; set; }
  
        public Table_IsletmeSahibi isletmesahibi { get; set; }
      
    }
    
}