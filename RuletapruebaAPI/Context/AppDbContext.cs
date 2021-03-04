using Microsoft.EntityFrameworkCore;
using RuletapruebaAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RuletapruebaAPI.Context
{
    public class AppDbContext:DbContext
    {
      

        public AppDbContext (DbContextOptions<AppDbContext>options):base(options)
        {
        }
        public DbSet<Ruleta> Ruleta { get; set; }
        public DbSet<Historial> Historial { get; set; }

    }
}
