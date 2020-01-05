using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahp.Core.Models
{
    public class UserProfile
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string KodePlant { get; set; }
        public string NamaPlant { get; set; }
        
        public string ProfileID { get; set; }
        public bool IsAdministrator { get; set; }
    }

    public class AdministrasiUserLogin
    {
        public string Kode { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string Telepon { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public bool? IsAdministrator { get; set; }
        public bool Aktif { get; set; }
        public string Roles { get; set; }
        public string HakAkses { get; set; }
        public string KodePlant { get; set; }
       
        public bool IsLdapAccount { get; set; }
    }
}
