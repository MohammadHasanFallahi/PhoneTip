using PhoneTipProject.Models.DataLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PhoneTipProject.Models.Context
{
    public class MyContext : DbContext
    {
        public DbSet<PagesGroup> PagesGroups { get; set; }
        public DbSet<Pages> Pages { get; set; }
        public DbSet<PageComments> PageComments { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Users> Users { get; set; }

        public MyContext() : base("MyConnection")
        {

        }
    }
}