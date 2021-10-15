using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTweetbookApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTweetbookApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Post> Posts {get; set;}
    }
}