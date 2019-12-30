using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahp.Core.Models
{
    public class ParamAdministrasiHakAks
    {
        public AdministrasiHakAks HakAkses { get; set; }
        public List<AdministrasiHakAksesRole> Roles { get; set; }
        public List<AdministrasiHakAksesMenu> Menus { get; set; }
        public List<AdministrasiHakAksesTombol> Tombols { get; set; }
    }
}
