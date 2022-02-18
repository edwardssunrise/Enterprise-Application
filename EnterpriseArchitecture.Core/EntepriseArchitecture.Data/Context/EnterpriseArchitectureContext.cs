using EnterpriseArchitecture.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntepriseArchitecture.Data.Context
{
    /// <summary>
    /// *Context class must inherit from DbContext class and override OnModelCreating() method.
    /// </summary>
    public partial class EnterpriseArchitectureContext : DbContext
    {
        private readonly EnterpriseArchitectureContext _context;

        /// <summary>
        /// The Connection String defined in the Web.config in the Presentation Layer is specified
        /// in this scope, and it is informed which database to connect to.
        /// </summary>
        public EnterpriseArchitectureContext() : base("name=EnterpriseArchitectureEntities")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        /// <summary>
        /// Tables in the database must be specified in *Context class.
        /// </summary>
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<Category> Category { get; set; }

        /// <summary>
        /// All CRUD operations can be performed on the table schema of type "dbo".
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User", "dbo");
            modelBuilder.Entity<Post>().ToTable("Post", "dbo");
            modelBuilder.Entity<Category>().ToTable("Category", "dbo");
            base.OnModelCreating(modelBuilder);
        }
    }
}