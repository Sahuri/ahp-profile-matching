using Ahp.Core.Common;
using Ahp.Core.Models;
using DataTablesParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace Ahp.Core.GenericRepositories.Abstractions
{
    public interface IGenericDataRepository<T> : IDisposable
        where T : class
    {
        UserProfile UserProfile { get; set; }
        List<T> GetAll();
        List<T> GetAll(params Expression<Func<T, object>>[] navigationProperties);
        List<T> GetList(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        T GetSingle(string paramValues);
        T GetSingle(params object[] keyValues);
        T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        T GetLates();
        List<dynamic> Browse();
        bool Create(T model);
        bool Update(T model);
        bool Delete(params object[] keyValues);
        bool Delete(T model);
        IQueryable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
        T SelectById(int Id);
        bool IsExist(params object[] keyValues);
        List<Dropdown> Dropdown(string term);
        List<Dropdown> DropdownByKey(string term);
        List<Dropdown> Dropdown(T model, string term);
        List<Dropdown> Dropdown2(T model, string term);
        List<Dropdown> DropdownByKey(T model, string term);
        FormatedList<T> DataTables();
        dynamic DynamicData(string term);
        List<dynamic> DynamicListData();

    }

    public interface IGenericDataRepositoryExtended<T> : IGenericDataRepository<T>
    where T : class
    {
        bool Creates(List<T> models);
        bool InsertOrUpdate(List<T> models);
        bool Updates(List<T> models);
        bool Deletes(List<T> models);
    }
}
