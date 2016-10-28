using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication1.EF
{
    public class Dal: DbContext
    {
        public DbSet<wChain> Chains { get; set; }

        public Dal() : base("DefaultConnection")
        {

        }
    }
}