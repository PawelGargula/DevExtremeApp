using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

namespace MvcMovie.Data
{
    public class MvcMovieContext : DbContext
    {
        public MvcMovieContext (DbContextOptions<MvcMovieContext> options)
            : base(options)
        {
        }

        public DbSet<MvcMovie.Models.Movie> Movie { get; set; } = default!;
        public DbSet<MvcMovie.Models.Additional> Additional { get; set; } = default!;
        public DbSet<MvcMovie.Models.Dictionary> Dictionary { get; set; } = default!;
        public DbSet<MvcMovie.Models.Person> Person { get; set; } = default!;
        public DbSet<MvcMovie.Models.DictionaryDefinition> DictionaryDefinition { get; set; } = default!;
        public DbSet<MvcMovie.Models.Organization> Organization { get; set; } = default!;
        public DbSet<MvcMovie.Models.ToolsHistory> ToolsHistory { get; set; } = default!;
    }
}
