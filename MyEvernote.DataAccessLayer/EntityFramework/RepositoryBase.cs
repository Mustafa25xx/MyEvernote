using MyEvernote.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.DataAccessLayer.EntityFramework
{
  public  class RepositoryBase
    {
        private static DatabaseContext _db;
        private static object _llokSync=new object();
        protected RepositoryBase() // bu classsın newlenmemesi ve sadece 1 kere oluşması için singleton pattern oluşturuyorz.
        {

        }
        public static DatabaseContext CreateContext ()
        {
            if (_db == null)
            {
                lock (_llokSync) // multithread uygulamalar için de sadece bir kere db context oluşturucak
                {
                    if (_db == null)
                    {
                        _db = new DatabaseContext();
                    }
                }

            }
            return _db;
        }
    }
}
