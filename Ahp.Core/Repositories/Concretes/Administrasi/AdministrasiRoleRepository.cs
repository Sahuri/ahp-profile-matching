using Ahp.Core.Common;
using Ahp.Core.GenericRepositories.Abstractions;
using Ahp.Core.Models;
using Ahp.Core.Repositories.Abstractions;
using Ahp.Core.Repositories.Abstractions.Administrasi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Ahp.Core.Repositories.Concretes.Administrasi
{
    public class AdministrasiRoleRepository : GenericDataRepository<AdministrasiRole, IGenericContext>, IAdministrasiRoleRepository
    {
        protected IGenericContext ctx;
        public AdministrasiRoleRepository(IGenericContext ctx)
            : base(ctx)
        {
            this.ctx = ctx;
        }

        #region IAdministrasiRoleRepository Members
        #region Created and Deleted Role Details
        public bool CreateWithDetail(AdministrasiRole header, List<AdministrasiRoleDtl> detail)
        {
            using (var trans = ctx.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    base.Create(header);
                    using (var repoDtl = new AdministrasiRoleDtlRepository(ctx))
                    {
                        foreach (var dtl in detail)
                        {
                            dtl.KodeWilayahKerja = "CE01";
                            repoDtl.Create(dtl);
                        }
                    }
                    trans.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception(ex.Message, ex.InnerException);
                }
            }
        }
        public bool UpdateWithDetail(AdministrasiRole header, List<AdministrasiRoleDtl> detail)
        {
            using (var trans = ctx.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    base.Update(header);
                    using (var repoDtl = new AdministrasiRoleDtlRepository(ctx))
                    {
                        var redcDetail = repoDtl.GetAll().Where(x => x.KodeRole == header.Kode).ToList();
                        repoDtl.Deletes(redcDetail);

                        //repoDtl.User = this.User;
                        foreach (var dtl in detail)
                        {
                            repoDtl.Create(dtl);
                        }
                    }
                    trans.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception(ex.Message, ex.InnerException);
                }
            }
        }
        public bool DeleteWithDetail(string kode)
        {
            using (var trans = ctx.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    var model = base.GetSingle(kode);
                    base.Delete(model);
                    using (var repoDtl = new AdministrasiRoleDtlRepository(ctx))
                    {
                        repoDtl.UserProfile = this.UserProfile;

                        var detail = repoDtl.GetAll().Where(x => x.KodeRole == kode).ToList();
                        repoDtl.Deletes(detail);
                    }
                    trans.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception(ex.Message, ex.InnerException);
                }
            }
        }
        #endregion

        #region Created and Deleted Role Users
        public bool CreateWithDetailUser(AdministrasiRole header, List<AdministrasiRoleUser> detail)
        {
            using (var trans = ctx.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    using (var repoDtl = new AdministrasiRoleUserRepository(ctx))
                    {
                        repoDtl.UserProfile = this.UserProfile;

                        var redcDetail = repoDtl.GetAll().Where(x => x.KodeRole == header.Kode).ToList();
                        repoDtl.Deletes(redcDetail);

                        //repoDtl.User = this.User;
                        foreach (var dtl in detail)
                        {
                            repoDtl.Create(dtl);
                        }
                    }
                    trans.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception(ex.Message, ex.InnerException);
                }
            }
        }
        #endregion

        public override List<Dropdown> Dropdown(string term)
        {
            IEnumerable<AdministrasiRole> records;
            if (string.IsNullOrEmpty(term))
            {
                records = ctx.Set<AdministrasiRole>();
            }
            else
            {
                records = ctx.Set<AdministrasiRole>()
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
        public override List<Dropdown> DropdownByKey(string term)
        {
            IEnumerable<AdministrasiRole> records;
            if (string.IsNullOrEmpty(term))
            {
                records = ctx.Set<AdministrasiRole>()
                .OrderBy(x => x.Kode);
            }
            else
            {
                records = ctx.Set<AdministrasiRole>()
                .Where(x => x.Kode.ToLower().Contains(term.ToLower()))
                .OrderBy(x => x.Kode);
            }

            var dropdown = records.Select(x => new Dropdown()
            {
                id = x.Kode,
                value = x.Kode,
                text = x.Nama
            }).OrderBy(x => x.text).ToList();

            return dropdown;
        }

        #endregion
    }
}
