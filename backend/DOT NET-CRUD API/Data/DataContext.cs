using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DOT_NET_CRUD_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DOT_NET_CRUD_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<SuperHero> SuperHeroes { get; set; }
    }
}