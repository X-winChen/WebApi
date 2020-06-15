using API.Models.Home;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.DataAccess.Comm
{
    public class APIDBContext:DbContext
    {

        public APIDBContext()
        {

        }
        public APIDBContext(DbContextOptions<APIDBContext> options)
            : base(options)
        {

        }

        public DbSet<Menu> Menu { get; set; }

    }
}
