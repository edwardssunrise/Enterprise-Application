## Data Access Layer

The "Data Layer" is used for:

1. Providing a database connection (Context)
2. Providing basic CRUD operations on the database (Generic Repository)
3. Providing basic CRUD and query operations on the database (UnitOfWork)

Design patterns in this layer provide generic work.

### 1. Context

The Context class must derive from the DbContext class. The Connection String defined in the Web.config in the Presentation Layer is specified in this scope, and it is informed which database to connect to. Tables in the database must be specified in the *Context class. Also, the OnModelCreating() method of the DbContext class must be overridden:

```csharp
using EnterpriseArchitecture.Core;

namespace EntepriseArchitecture.Data.Context
{
    public partial class EnterpriseArchitectureContext : DbContext
    {
        private readonly EnterpriseArchitectureContext _context;

        /// <summary>
        /// Connection String defined in Web.config in the Presentation Layer is specified in this scope, and it is informed which database to connect to.
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User", "dbo");
            modelBuilder.Entity<Post>().ToTable("Post", "dbo");
            modelBuilder.Entity<Category>().ToTable("Category", "dbo");

            base.OnModelCreating(modelBuilder);
        }
    }
}
```

### 2. Unit Of Work

The Unit Of Work design pattern is used to perform basic CRUD operations on the database. Unit Of Work design pattern allows to perform a batch operation on the database and to undo the operation in case of a possible error. The Unit Of Work design pattern is a design pattern that prevents every action related to the database from being reflected to the database instantly in our software application, and accordingly, it accumulates all actions and ensures that they are carried out as a whole at once over a single connection, thus minimizing database costs.

Transaction is a mechanism that has the capacity to control one or more queries. Therefore, instead of activating a transaction for each operative query, the cost of activating one transaction to cover all our queries will be minimized. The technical name of this solution is Unit Of Work.

Unit Of Work is a design pattern that executes batch database transactions once at a time and thus can report how many records were affected as a result of this batch operation. Unit Of Work, which is generally preferred to be used with the Repository Design Pattern, is also (usually) together with transaction control. It can control all query processes from a single center by using In addition to all these, in the operations performed step by step (chain) by the user, if the process is abandoned without ending the process in detail, all changes made up to that point must be reversed. Here, Unit Of Work eliminates the cost of work and does not physically change the value of any entity in the database without ending the chain.

Experience-based reviews:

1. Since Entity Framework implements the UnitOfWork design pattern within itself, it is recommended not to be used together.
2. This process may not be repeated because Entity Framework Core automatically "disposes" the context object with Dependency Injection.

### 3. Generic Repository

Coding unconsciously while performing database operations in our programs will lead us to a great code repetition. Especially, CRUD (Create-Read-Update-Delete) operations made in an application bring these repetitions to the line. At this point, Repository Design Pattern is a cure-all.

It is necessary to apply the Generic Repository Design Pattern in order not to create a separate DAL (Data Access Layer) for each CRUD operation to be applied on each Entity Class that represents the tables in the database.
