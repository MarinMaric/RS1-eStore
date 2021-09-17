using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eStore_web.EF_Models
{
    public class Drzava
    {
        [Key]
        public int DrzavaID { get; set; }
        public string Kratica { get; set; }
        [Required,MaxLength(50)]
        public string Naziv { get; set; }
        public string Iso { get; set; }
        public string Iso3 { get; set; }
        public string NumCode { get; set; }
        public string PhoneCode { get; set; }

    }
}
