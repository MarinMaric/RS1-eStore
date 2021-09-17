using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eStore_web.EF_Models
{
    public class AccountImage
    {
      
        [Key]
        public int AccountImageID { get; set; }
        [ForeignKey("Base")]
        public int BaseID { get; set; }
        public AccountBase Base { get; set; }


        [Column(TypeName = "varbinary(max)")]
        public byte[] Image { get; set; }


       
    }
}
