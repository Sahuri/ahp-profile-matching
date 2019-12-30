using Ahp.Core.Models;
using Ahp.Core.Repositories.Abstractions;
using System;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace Ahp.Core.Reposirories.Concretes
{
    public partial class GenericContext :AhpEntities, IGenericContext
    {
        public GenericContext() : base()
        {
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = true;
        }

        public bool SaveStatus()
        {
            try
            {
                SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int SaveChangeUpdate(object objSource, object objDestination)
        {
            foreach (PropertyInfo propInfo in objSource.GetType().GetProperties())
            {
                string propName = propInfo.Name;
                string objName = (from n in objDestination.GetType().GetProperties() where n.Name.Equals(propName, StringComparison.CurrentCultureIgnoreCase) select n.Name).FirstOrDefault();
                if (!string.IsNullOrEmpty(objName))
                {
                    objDestination.GetType().GetProperty(objName).SetValue(objDestination, propInfo.GetValue(objSource, null), null);
                }
            }

            return this.SaveChanges();
        }
    }
}
