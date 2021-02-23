using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Core.DataAccess
{
   public interface IRepository<T> 
    {
        IQueryable<T> ListQueryable();
        List<T> List();


         int Insert(T obj);

         int Save();




         int Update(T obj);



         int Delete(T obj);



         List<T> List(Expression<Func<T, bool>> where);

        //public IQueryable<T> List1(Expression<Func<T, bool>> where) istediğimiz sorguyu döndğrmek için
        //{

        //    return _objectSet.Where(where);
        //}

         T Find(Expression<Func<T, bool>> where);
       
    }
}
