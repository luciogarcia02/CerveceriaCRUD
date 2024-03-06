using CerveceriaCRUD.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace CerveceriaCRUD.Db
{
    public class BeersDb : DbContext
    {   
        public BeersDb(DbContextOptions<BeersDb> options) : base(options) { }

        public DbSet<Beer> Beers { get; set; }
    }
}
