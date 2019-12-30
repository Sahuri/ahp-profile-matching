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
    public class AdministrasiHakAksesRepository : GenericDataRepository<AdministrasiHakAks, IGenericContext>, IAdministrasiHakAksesRepository
    {
        protected IGenericContext ctx;
        public AdministrasiHakAksesRepository(IGenericContext ctx)
            : base(ctx)
        {
            this.ctx = ctx;
        }

        #region IAdministrasiHakAksesRepository Members
        #region Created and Deleted HakAkses Details
        public bool CreateWithDetail(ParamAdministrasiHakAks data)
        {
            using (var trans = ctx.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    data.HakAkses.Kode = Guid.NewGuid().ToString().Substring(0, 30);
                    base.Create(data.HakAkses);
                    p_DeleteInsertDetail(data);
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
        public bool UpdateWithDetail(ParamAdministrasiHakAks data)
        {
            using (var trans = ctx.Database.BeginTransaction(IsolationLevel.ReadCommitted))
            {
                try
                {
                    base.Update(data.HakAkses);
                    p_DeleteInsertDetail(data);
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
                    using (var repoHakAksesRole = new AdministrasiHakAksesRoleRepository(ctx))
                    {
                        var detail = repoHakAksesRole.GetAll().Where(x => x.KodeHakAkses == kode).ToList();
                        repoHakAksesRole.Deletes(detail);
                    }
                    using (var repoHakAksesMenu = new AdministrasiHakAksesMenuRepository(ctx))
                    {
                        var detail = repoHakAksesMenu.GetAll().Where(x => x.KodeHakAkses == kode).ToList();
                        repoHakAksesMenu.Deletes(detail);
                    }
                    using (var repoHakAksesTombol = new AdministrasiHakAksesTombolRepository(ctx))
                    {
                        var detail = repoHakAksesTombol.GetAll().Where(x => x.KodeHakAkses == kode).ToList();
                        repoHakAksesTombol.Deletes(detail);
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
            IEnumerable<AdministrasiHakAks> records;
            if (string.IsNullOrEmpty(term))
            {
                records = ctx.Set<AdministrasiHakAks>();
            }
            else
            {
                records = ctx.Set<AdministrasiHakAks>()
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
            IEnumerable<AdministrasiHakAks> records;
            if (string.IsNullOrEmpty(term))
            {
                records = ctx.Set<AdministrasiHakAks>()
                .OrderBy(x => x.Kode);
            }
            else
            {
                records = ctx.Set<AdministrasiHakAks>()
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

        #region Private
        private void p_DeleteInsertDetail(ParamAdministrasiHakAks data)
        {
            var kode = data.HakAkses.Kode;

            using (var repoHakAksesRole = new AdministrasiHakAksesRoleRepository(ctx))
            {
                repoHakAksesRole.UserProfile = this.UserProfile;

                var records = repoHakAksesRole.GetAll().Where(x => x.KodeHakAkses == kode).ToList();
                repoHakAksesRole.Deletes(records);

                foreach (var mdl in data.Roles)
                {
                    mdl.KodeHakAkses = kode;
                    repoHakAksesRole.Create(mdl);
                }
            }

            using (var repoHakAksesMenu = new AdministrasiHakAksesMenuRepository(ctx))
            {
                repoHakAksesMenu.UserProfile = this.UserProfile;

                var records = repoHakAksesMenu.GetAll().Where(x => x.KodeHakAkses == kode).ToList();
                repoHakAksesMenu.Deletes(records);

                foreach (var mdl in data.Menus)
                {
                    mdl.KodeHakAkses = kode;
                    repoHakAksesMenu.Create(mdl);
                }
            }

            using (var repoHakAksesTombol = new AdministrasiHakAksesTombolRepository(ctx))
            {
                repoHakAksesTombol.UserProfile = this.UserProfile;

                var records = repoHakAksesTombol.GetAll().Where(x => x.KodeHakAkses == kode).ToList();
                repoHakAksesTombol.Deletes(records);

                foreach (var mdl in data.Tombols)
                {
                    mdl.KodeHakAkses = kode;
                    repoHakAksesTombol.Create(mdl);
                }
            }
        }
        #endregion
    }
}