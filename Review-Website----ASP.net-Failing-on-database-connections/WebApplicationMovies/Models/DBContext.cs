using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplicationMovies.Models
{
    public class DBContext : DbContext
    {

        public DbSet<Film> Films { get; set; }

        public DbSet<Entry> Entries { get; set; }

        public DbSet<Acting> Actings { get; set; }

        public DbSet<Review> Reviews { get; set; }
    }
}