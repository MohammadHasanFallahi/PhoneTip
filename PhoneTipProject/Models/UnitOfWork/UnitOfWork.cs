using PhoneTipProject.Models.Context;
using PhoneTipProject.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PhoneTipProject.Models.DataLayer;

namespace PhoneTipProject.Models.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        private MyContext context = new MyContext();
        public Repository<PagesGroup> pagegroup { get => new Repository<PagesGroup>(context); }
        public Repository<Pages> Pages { get => new Repository<Pages>(context); }
        public Repository<PageComments> PageComments { get => new Repository<PageComments>(context); }

        public void Save()
        {
            context.SaveChanges();
        }
        public void Dispose()
        {
            context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}