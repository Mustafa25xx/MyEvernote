using MyEvernote.Common;
using MyEvernote.Core.DataAccess;
using MyEvernote.DataAccessLayer;
using MyEvernote.DataAccessLayer.EntityFramework;
using MyEvernote.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.BusinessLayer
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DbSet<T> _objectSet;
        //private DatabaseContext db = new DatabaseContext();
        private DatabaseContext db;
        public Repository()
        {
            db = RepositoryBase.CreateContext();
            _objectSet = db.Set<T>();
        }

        public List<T> List()

        {

            return _objectSet.ToList();

        }
        public int Insert(T obj)
        {
            _objectSet.Add(obj);

            if (obj is MyEntityBase)
            {

                MyEntityBase o = obj as MyEntityBase;
                o.CreatedOn = DateTime.Now;
                o.ModifiedOn = DateTime.Now;

                o.ModifiedUserName = App.Common.GetUsername();

            }

            return Save();



        }
        public int Save()
        {

            return db.SaveChanges();
        }


        public int Update(T obj)
        {
            if (obj is MyEntityBase)
            {

                MyEntityBase o = obj as MyEntityBase;

                o.ModifiedOn = DateTime.Now;

                o.ModifiedUserName = App.Common.GetUsername(); ;
            }

            return Save();
        }

        public int Delete(T obj)
        {


            _objectSet.Remove(obj);
            return Save();
        }

        public List<T> List(Expression<Func<T, bool>> where)
        {

            return _objectSet.Where(where).ToList();
        }
        //public IQueryable<T> List1(Expression<Func<T, bool>> where) istediğimiz sorguyu döndğrmek için
        //{

        //    return _objectSet.Where(where);
        //}

        public T Find(Expression<Func<T, bool>> where)
        {

            return _objectSet.FirstOrDefault(where);
        }

        public IQueryable<T> ListQueryable()
        {
            return _objectSet.AsQueryable<T>();
        }
    }
}
