﻿using img_api.Models;
using Microsoft.EntityFrameworkCore;

namespace img_api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Person> Person { get; set; }
    }
}
