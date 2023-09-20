using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tutorial_proj.Models;

namespace Tutorial_proj.Data
{
    public class DataContext : DbContext
    {
        #region Properties

        public DbSet<Character> Characters => Set<Character>();
        public DbSet<User> Users => Set<User>();

        #endregion

        #region Constructor

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        #endregion
    }
}