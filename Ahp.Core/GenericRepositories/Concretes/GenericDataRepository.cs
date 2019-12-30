using Ahp.Core.Common;
using Ahp.Core.Models;
using Ahp.Core.Repositories.Abstractions;
using DataTablesParser;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Validation;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Ahp.Core.GenericRepositories.Abstractions
{
    public abstract class GenericDataRepository<T, TContext> : IGenericDataRepository<T>
        where T : class
        where TContext : IGenericContext
    {
        private UserProfile _UserProfile;
        protected TContext ctx;
        protected readonly IDbSet<T> dbset;

        protected GenericDataRepository(TContext ctx)
        {
            this.ctx = ctx;
        }

        private ReadOnlyMetadataCollection<EdmProperty> p_GetPrimaryKeyInfo()
        {
            var metadata = ctx.ObjectContext.MetadataWorkspace;
            var objectItemCollection = ((ObjectItemCollection)metadata.GetItemCollection(DataSpace.OSpace));

            var entityMetadata = metadata
                    .GetItems<EntityType>(DataSpace.OSpace)
                    .Single(e => objectItemCollection.GetClrType(e) == typeof(T));

            return entityMetadata.KeyProperties;
        }

        public virtual List<T> GetAll()
        {
            List<T> list;
            var dbQuery = ctx.Set<T>();


            list = dbQuery.ToList();

            return list;
        }

        public virtual List<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            IQueryable<T> dbQuery = ctx.Set<T>();

            //Apply eager loading
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<T, object>(navigationProperty);

            list = dbQuery
                .AsNoTracking()
                .ToList<T>();

            return list;
        }

        public virtual List<T> GetList(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            IQueryable<T> dbQuery = ctx.Set<T>();

            //Apply eager loading
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<T, object>(navigationProperty);

            list = dbQuery
                .AsNoTracking()
                .Where(where).ToList();

            return list;
        }

        public virtual T GetSingle(string paramValues)
        {
            var keyNames = ctx.Set<T>().GetType().GetProperties().FirstOrDefault(p => p.CustomAttributes.Any(attr => attr.AttributeType == typeof(KeyAttribute)));

            var key = p_GetPrimaryKeyInfo();
            var length = key.Count();
            var param = paramValues.Split(',');
            object[] keyValues = new object[length];

            for (var i = 0; i < length; i++)
            {
                keyValues[i] = Convert.ChangeType(param[i], (TypeCode)Enum.Parse(typeof(TypeCode), key[i].TypeName));
            }

            var rec = ctx.Set<T>().Find(keyValues);

            return rec;
        }

        public virtual T GetSingle(params object[] keyValues)
        {
            var rec = ctx.Set<T>().Find(keyValues);

            return rec;
        }

        public T SelectById(int Id)
        {
            return dbset.Find(Id);
        }

        public virtual T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            T item = null;
            IQueryable<T> dbQuery = ctx.Set<T>();

            //Apply eager loading
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<T, object>(navigationProperty);

            item = dbQuery
                .AsNoTracking() //Don't track any changes for the selected item
                .FirstOrDefault(where); //Apply where clause

            return item;
        }

        public virtual bool Create(T model)
        {
            try
            {
                var CreatedDate = DateTime.Now;
                if (model.GetType().GetProperty("CreatedBy") != null)
                {
                    var userID = model.GetType().GetProperty("CreatedBy").GetValue(model, null);
                    if (userID == null)
                    {
                        model.GetType().GetProperty("CreatedBy").SetValue(model, this._UserProfile.UserID, null);
                    }
                }
                if (model.GetType().GetProperty("CreateBy") != null)
                {
                    var userID = model.GetType().GetProperty("CreateBy").GetValue(model, null);
                    if (userID == null)
                    {
                        model.GetType().GetProperty("CreateBy").SetValue(model, this._UserProfile.UserID, null);
                    }
                }
                if (model.GetType().GetProperty("UpdatedBy") != null)
                {
                    var value  = model.GetType().GetProperty("UpdatedBy").GetValue(model, null);
                    if (value == null)
                    {
                        model.GetType().GetProperty("UpdatedBy").SetValue(model, null, null);
                    }
                }
                if (model.GetType().GetProperty("UpdateBy") != null)
                {
                    var value = model.GetType().GetProperty("UpdateBy").GetValue(model, null);
                    if (value == null)
                    {
                        model.GetType().GetProperty("UpdateBy").SetValue(model, null, null);
                    }
                }

                if (model.GetType().GetProperty("CreatedDate") != null)
                {
                    model.GetType().GetProperty("CreatedDate").SetValue(model, CreatedDate, null);
                }
                if (model.GetType().GetProperty("CreateDate") != null)
                {
                    model.GetType().GetProperty("CreateDate").SetValue(model, CreatedDate, null);
                }
                if (model.GetType().GetProperty("UpdatedDate") != null)
                {
                    model.GetType().GetProperty("UpdatedDate").SetValue(model, CreatedDate, null);
                }
                if (model.GetType().GetProperty("UpdateDate") != null)
                {
                    model.GetType().GetProperty("UpdateDate").SetValue(model, CreatedDate, null);
                }

                
                ctx.Entry(model).State = System.Data.Entity.EntityState.Added;
                return ctx.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                var fullErrorMessage = string.Join("; ", errorMessages);
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        public virtual bool Update(T model)
        {
            try
            {
                var update_at = DateTime.Now;

                if (model.GetType().GetProperty("UpdatedBy") != null)
                {
                    model.GetType().GetProperty("UpdatedBy").SetValue(model, this._UserProfile.UserID, null);
                }
                if (model.GetType().GetProperty("UpdateBy") != null)
                {
                    model.GetType().GetProperty("UpdateBy").SetValue(model, this._UserProfile.UserID, null);
                }

                if (model.GetType().GetProperty("UpdatedDate") != null)
                {
                    model.GetType().GetProperty("UpdatedDate").SetValue(model, update_at, null);
                }
                if (model.GetType().GetProperty("UpdateDate") != null)
                {
                    model.GetType().GetProperty("UpdateDate").SetValue(model, update_at, null);
                }

                if (model.GetType().GetProperty("AreaID") != null)
                {
                    var value = model.GetType().GetProperty("AreaID").GetValue(model, null);
                    if (value == null)
                    {
                        model.GetType().GetProperty("AreaID").SetValue(model, this._UserProfile.KodePlant, null);
                    }
                }

                ctx.Entry(model).State = EntityState.Modified;
                return ctx.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                var fullErrorMessage = string.Join("; ", errorMessages);
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }


        public virtual bool Delete(params object[] keyValues)
        {
            try
            {
                var rec = ctx.Set<T>().Find(keyValues);
                ctx.Entry(rec).State = System.Data.Entity.EntityState.Deleted;
                return ctx.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                var fullErrorMessage = string.Join("; ", errorMessages);
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        public virtual bool Delete(T model)
        {
            try
            {
                ctx.Entry(model).State = System.Data.Entity.EntityState.Deleted;
                return ctx.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                var fullErrorMessage = string.Join("; ", errorMessages);
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        public virtual bool IsExist(params object[] keyValues)
        {
            return ctx.Set<T>().Find(keyValues) != null ? true : false;
        }

        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public virtual T GetLates()
        {
            return ctx.Set<T>().ToList().LastOrDefault();
        }

        public virtual List<dynamic> Browse()
        {
            var list = ctx.Set<T>().ToList<dynamic>();

            return list;
        }

        public virtual IQueryable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {

            IQueryable<T> query = ctx.Set<T>().Where(predicate);
            return query;
        }

        public virtual List<Dropdown> Dropdown(string term)
        {
            var text = "";

            if (typeof(T).GetType().GetProperty("Deskripsi") != null)
            {
                text = "Deskripsi";
            }

            if (typeof(T).GetType().GetProperty("Nama") != null)
            {
                text = "Nama";
            }

            var clause = string.IsNullOrEmpty(term) ? "" : string.Format("where {0} like '{%1%}'", text, term);

            var tableName = typeof(T).Name;
            var sql = string.Format("select Kode as id, Kode + '{0}' + {1}  as text, Kode as [value] from {2} {3}", " - ", text, tableName, clause);
            var list = ctx.Database.SqlQuery<Dropdown>(sql).ToList<Dropdown>();

            return list;
        }

        public virtual List<Dropdown> DropdownByKey(string term)
        {
            var text = "";

            if (typeof(T).GetType().GetProperty("Deskripsi") != null)
            {
                text = "Deskripsi";
            }

            if (typeof(T).GetType().GetProperty("Nama") != null)
            {
                text = "Nama";
            }

            var clause = string.IsNullOrEmpty(term) ? "" : string.Format("where Kode like '{%0%}'", term);

            var tableName = typeof(T).Name;
            var sql = string.Format("select Kode as id, Kode + '{0}' + {1}  as text, Kode as [value] from {2} {3}", " - ", text, tableName, clause);
            var list = ctx.Database.SqlQuery<Dropdown>(sql).ToList<Dropdown>();

            return list;
        }

        public virtual List<Dropdown> Dropdown2(T model, string term)
        {
            var text = "";

            if (model.GetType().GetProperty("Deskripsi") != null)
            {
                text = "Deskripsi";
            }

            if (model.GetType().GetProperty("Nama") != null)
            {
                text = "Nama";
            }

            var clause = string.IsNullOrEmpty(term) ? "" : string.Format("where {0} like '{%1%}'", text, term);

            var tableName = model.GetType().Name;
            //var sql = string.Format("select Kode as id, Kode + '{0}' +  {1}  as text, Kode as [value] from {2} {3}", "-", text, tableName, clause);
            var sql = string.Format("select Kode as id, {0}  as text, Kode as [value] from {1} {2}", text, tableName, clause);
            var list = ctx.Database.SqlQuery<Dropdown>(sql).ToList<Dropdown>();

            return list;
        }

        public virtual List<Dropdown> Dropdown(T model, string term)
        {
            var text = "";

            if (model.GetType().GetProperty("Deskripsi") != null)
            {
                text = "Deskripsi";
            }

            if (model.GetType().GetProperty("Nama") != null)
            {
                text = "Nama";
            }

            var clause = string.IsNullOrEmpty(term) ? "" : string.Format("where {0} like '{%1%}'", text, term);

            var tableName = model.GetType().Name;
            var sql = string.Format("select Kode as id, Kode + '{0}' +  {1}  as text, Kode as [value] from {2} {3}", "-", text, tableName, clause);
            var list = ctx.Database.SqlQuery<Dropdown>(sql).ToList<Dropdown>();

            return list;
        }

        public virtual List<Dropdown> DropdownByKey(T model, string term)
        {
            var text = "";

            if (model.GetType().GetProperty("Deskripsi") != null)
            {
                text = "Deskripsi";
            }

            if (model.GetType().GetProperty("Nama") != null)
            {
                text = "Nama";
            }

            var clause = string.IsNullOrEmpty(term) ? "" : string.Format("where Kode like '{%0%}'", term);

            var tableName = model.GetType().Name;
            //var sql = string.Format("select Kode as id, Kode + '{0}' + {1}  as text, Kode as [value] from {2} {3}", "-", text, tableName, clause);
            var sql = string.Format("select Kode as id, {0}  as text, Kode as [value] from {1} {2}", text, tableName, clause);
            var list = ctx.Database.SqlQuery<Dropdown>(sql).ToList<Dropdown>();

            return list;
        }


        public virtual FormatedList<T> DataTables()
        {
            IQueryable<T> queryable;
            var dbQuery = ctx.Set<T>();
            queryable = dbQuery.AsQueryable();

            var request = HttpContext.Current.Request;
            var wrapper = new HttpRequestWrapper(request);
            var parser = new DataTablesParser<T>(wrapper, queryable);
            var datatable = parser.Parse();

            return datatable;
        }

        public virtual dynamic DynamicData(string term)
        {
            dynamic data = new ExpandoObject();

            return data;
        }

        public virtual List<dynamic> DynamicListData()
        {
            dynamic data = new ExpandoObject();
            List<dynamic> listData = new List<dynamic>();
            listData.Add(data);

            return data;
        }



        #region IGenericDataRepository<T> Members
        public UserProfile UserProfile { get => this._UserProfile; set => this._UserProfile = value; }

        #endregion

    }

    public abstract class GenericDataRepositoryExtended<T, TContext> : GenericDataRepository<T, TContext>, IGenericDataRepositoryExtended<T>
    where T : class
    where TContext : IGenericContext
    {
        protected IGenericContext ctx;

        public GenericDataRepositoryExtended(TContext _ctx)
            : base(_ctx)
        {
            this.ctx = _ctx;
        }

        public virtual bool Creates(List<T> models)
        {
            try
            {
                ctx.Set<T>().AddRange(models);
                return ctx.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                var fullErrorMessage = string.Join("; ", errorMessages);
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        public virtual bool InsertOrUpdate(List<T> models)
        {
            try
            {
                foreach (var model in models)
                {
                    ctx.Entry(model).State = base.IsExist((string)model.GetType().GetProperty("Kode").GetValue(this, null)) ?
                                                       EntityState.Added :
                                                       EntityState.Modified;
                }

                return ctx.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                var fullErrorMessage = string.Join("; ", errorMessages);
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        public virtual bool Updates(List<T> models)
        {
            try
            {
                ctx.Set<T>().RemoveRange(models);
                ctx.Set<T>().AddRange(models);

                return ctx.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                var fullErrorMessage = string.Join("; ", errorMessages);
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }

        }

        public virtual bool Deletes(List<T> models)
        {
            try
            {
                ctx.Set<T>().RemoveRange(models);
                return ctx.SaveChanges() > 0;
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                var fullErrorMessage = string.Join("; ", errorMessages);
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }
    }


}
