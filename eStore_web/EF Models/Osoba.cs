using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eStore_web.EF_Models
{
    public class AccountBase
    {
        [Key]
        public int AccountBaseID { get; set; }
        
        [ForeignKey("LoginInfo")]
        public int? LoginInfoID { get; set; }
        public LoginInfo LoginInfo { get; set; }

        [ForeignKey("EmailAddress")]
        public int? EmailAddressID { get; set; }
        public EmailAddress EmailAddress { get; set; }
        [ForeignKey("OsobaImage")]
        public int? ImageID { get; set; }
        public AccountImage Image { get; set; }

        [ForeignKey("Drzava")]
        public int DrzavaID { get; set; }
        public Drzava Drzava { get; set; }
    }
}
