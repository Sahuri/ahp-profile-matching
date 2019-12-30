using Ahp.Core.Common;
using Ahp.Core.GenericRepositories.Abstractions;
using Ahp.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Ahp.Core.Repositories.Abstractions.Administrasi
{
    public interface IAdministrasiUserRepository : IGenericDataRepository<AdministrasiUser>
    {
        bool IsAdministrator {get;}
        AdministrasiUserLogin Login(string userName, string password);
        bool ChangePassword(string oldPassword, string newPassword);
        bool ResetPassword();
        bool UploadAvatar(string avatar);
        IQueryable<AdministrasiUser> DatatablesAuditor();
   
    }
}
