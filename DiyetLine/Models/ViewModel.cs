using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DiyetLine.Models
{
    public class ViewModel
    {
        public Table_IsletmeSahibi isletmesahibi { get; set; }
        public int IlId { get; set; }
        public int IlceId { get; set; }
    }
}