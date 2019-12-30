using Ahp.Core.Common;
using Ahp.Core.GenericRepositories.Abstractions;
using Ahp.Core.Models;
using Ahp.Core.Repositories.Abstractions;
using Ahp.Core.Repositories.Abstractions.Administrasi;
using Ahp.Core.Security;
using System.Collections.Generic;
using System.Linq;
using DataTablesParser;
using System;
using System.Linq.Expressions;

namespace Ahp.Core.Repositories.Concretes.Administrasi
{
    public class AdministrasiUserRepository : GenericDataRepository<AdministrasiUser, IGenericContext>, IAdministrasiUserRepository
    {
        protected IGenericContext ctx;
        public AdministrasiUserRepository(IGenericContext ctx)
            : base(ctx)
        {
            this.ctx = ctx;
        }

        public bool IsAdministrator
        {
            get
            {
                return p_IsAdministrator();
            }
        }

        private bool p_IsAdministrator()
        {
            //var user = base.User;
            //var rec = base.GetSingle(user);
            //if (rec != null)
            //{
            //    return rec.IsAdministrator.Value;
            //}
            //else
            //{
            //    return false;
            //}

            return true;
        }

        public override bool Create(AdministrasiUser model)
        {
            model.IsAdministrator = model.IsAdministrator.HasValue ? model.IsAdministrator : false;
           
            model.Password = string.IsNullOrEmpty(model.Password) ? Cryptography.DefaultPassword
                : Cryptography.EncryptString(model.Password);

            var result = base.Create(model);

            return result;
        }

        public override bool Update(AdministrasiUser model)
        {
            model.IsAdministrator = model.IsAdministrator.HasValue ? model.IsAdministrator : false;
           
            var rec = base.GetSingle(model.Kode);
            if (rec != null)
            {
                rec.Aktif = model.Aktif;
                rec.Alamat = model.Alamat;
                rec.Avatar = model.Avatar;
                rec.CreatedBy = model.CreatedBy;
                rec.CreatedDate = model.CreatedDate;
                rec.Email = model.Email;
                rec.IsAdministrator = model.IsAdministrator;
                
                rec.Nama = model.Nama;
                rec.Telepon = model.Telepon;
                
                rec.UpdatedBy = this.UserProfile.UserID;
                rec.UpdatedDate = DateTime.Now;
                rec.Password = string.IsNullOrEmpty(model.Password) ? rec.Password : Cryptography.EncryptString(model.Password);
            }

            return base.Update(rec);
        }

        public override List<Dropdown> Dropdown(AdministrasiUser model, string term)
        {
            IEnumerable<AdministrasiUser> records;
            if (string.IsNullOrEmpty(term))
            {
                records = ctx.Set<AdministrasiUser>();
            }
            else
            {
                records = ctx.Set<AdministrasiUser>()
                .Where(x => x.Nama.ToLower().Contains(term.ToLower()));
            }

            var dropdown = records.Select(x => new Dropdown()
            {
                id = x.Kode,
                value = x.Kode,
                text = x.Nama
            }).OrderBy(x => x.text).ToList();

            return dropdown;
        }

        public override List<Dropdown> DropdownByKey(AdministrasiUser model, string term)
        {
            IEnumerable<AdministrasiUser> records;
            if (string.IsNullOrEmpty(term))
            {
                records = ctx.Set<AdministrasiUser>()
                .OrderBy(x => x.Kode);
            }
            else
            {
                records = ctx.Set<AdministrasiUser>()
                .Where(x => x.Kode.ToLower().Contains(term.ToLower()))
                .OrderBy(x => x.Nama);
            }

            var dropdown = records.Select(x => new Dropdown()
            {
                id = x.Kode,
                value = x.Kode,
                text = x.Nama
            }).OrderBy(x => x.text).ToList();

            return dropdown;
        }

        public AdministrasiUserLogin Login(string userName, string password)
        {
            var z = Cryptography.EncryptString(password);

            var recUser = ctx.Set<AdministrasiUser>().Where(x => x.Kode == userName && x.Aktif == true).FirstOrDefault();
           
            var rec = new AdministrasiUserLogin();
            if (recUser != null)
            {
                var recRoles = ctx.Set<AdministrasiRoleUser>().Where(x => x.KodeUser == userName)
                    .Select(x => x.KodeRole);
                var recHakAkses = ctx.Set<AdministrasiHakAksesRole>().Where(x => recRoles.Contains(x.KodeRole))
                    .Select(x => x.KodeHakAkses);


                rec.Kode = recUser.Kode;
                rec.Nama = recUser.Nama ?? "";
                rec.Aktif = recUser.Aktif;
                rec.Alamat = recUser.Alamat ?? "";
                rec.Avatar = recUser.Avatar ?? "";
                rec.Email = recUser.Email ?? "";
                rec.IsAdministrator = recUser.IsAdministrator;
                rec.Telepon = recUser.Telepon ?? "";
                rec.Roles = recUser.IsAdministrator.Value ? "Administrator" : recRoles != null ? string.Join(",", recRoles) : "";
          
            }
            else
            {
                rec = null;
            }

            return rec;
        }

        public override AdministrasiUser GetLates()
        {
            var rec = base.GetLates();
            if (rec != null)
            {
                rec.Password = string.Empty;
            }

            return rec;
        }

        public override List<dynamic> Browse()
        {
            var recs = base.Browse();
            recs.ForEach(x =>
            {
                x.Password = string.Empty;
            });

            return recs;
        }

        public override FormatedList<AdministrasiUser> DataTables()
        {
            var dataTables = base.DataTables();
            dataTables.data.ForEach(x =>
            {
                x.Password = string.Empty;
            });

            return dataTables;
        }

        public override IQueryable<AdministrasiUser> FindBy(Expression<Func<AdministrasiUser, bool>> predicate)
        {
            var recs = base.FindBy(predicate);
            recs.ToList().ForEach(x =>
            {
                x.Password = string.Empty;
            });

            return recs;
        }

        public override List<AdministrasiUser> GetAll()
        {
            var recs = base.GetAll();

            recs.ForEach(x =>
            {
                x.Password = string.Empty;
            });

            return recs;
        }

        public override List<AdministrasiUser> GetAll(params Expression<Func<AdministrasiUser, object>>[] navigationProperties)
        {
            var recs = base.GetAll(navigationProperties);

            recs.ForEach(x =>
            {
                x.Password = string.Empty;
            });

            return recs;
        }

        public override List<AdministrasiUser> GetList(Func<AdministrasiUser, bool> where, params Expression<Func<AdministrasiUser, object>>[] navigationProperties)
        {
            var recs = base.GetList(where, navigationProperties);

            recs.ForEach(x =>
            {
                x.Password = string.Empty;
            });

            return recs;
        }

        public override AdministrasiUser GetSingle(Func<AdministrasiUser, bool> where, params Expression<Func<AdministrasiUser, object>>[] navigationProperties)
        {
            var rec = base.GetSingle(where, navigationProperties);
            if (rec != null)
            {
                rec.Password = string.Empty;
            }

            return rec;
        }

        public override AdministrasiUser GetSingle(params object[] keyValues)
        {
            var rec = base.GetSingle(keyValues);
            if (rec != null)
            {
                rec.Password = string.Empty;
            }

            return rec;
        }

        public override AdministrasiUser GetSingle(string paramValues)
        {
            var rec = base.GetSingle(paramValues);
            if (rec != null)
            {
                rec.Password = string.Empty;
            }

            return rec;
        }

        public bool ChangePassword(string oldPassword, string newPassword)
        {
            var oldPass = Cryptography.EncryptString(oldPassword);
            var rec = base.FindBy(x => x.Kode == this.UserProfile.UserID && x.Password == oldPass).FirstOrDefault();
            if (rec != null)
            {
                rec.Password = Cryptography.EncryptString(newPassword);
                var result = base.Update(rec);

                rec.Password = "";
                return result;
            }
            else
            {
                throw new Exception(BaseConstants.MESSAGE_INVALID_OLD_PASSWORD);
            }
        }

        public bool ResetPassword()
        {
            var rec = base.GetSingle(this.UserProfile.UserID);
            if (rec != null)
            {
                rec.Password = Cryptography.DefaultPassword;

                var result = base.Update(rec);

                rec.Password = "";
                return result;
            }
            else
            {
                throw new Exception(BaseConstants.MESSAGE_INVALID_OLD_PASSWORD);
            }
        }

        public bool UploadAvatar(string avatar)
        {
            var rec = base.GetSingle(this.UserProfile.UserID);
            if (rec != null)
            {
                rec.Avatar = avatar;

                return base.Update(rec);
            }
            else
            {
                throw new Exception(BaseConstants.MESSAGE_INVALID_DATA);
            }
        }

        public IQueryable<AdministrasiUser> DatatablesAuditor()
        {
            var roleAuditor = "Auditor,LeadAuditor".ToLower().Split(',');

            var queryable = (from a in ctx.Set<AdministrasiUser>().ToList()
                             join b in ctx.Set<AdministrasiRoleUser>().ToList()
                                 on a.Kode equals b.KodeUser
                             where roleAuditor.Contains(b.KodeRole.ToLower())
                             select a).Distinct().AsQueryable();

            queryable = queryable.Select(x => new AdministrasiUser()
            {
                Kode = x.Kode,
                Nama = x.Nama
            });

            return queryable;
        }


    }
}
