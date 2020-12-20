using Microsoft.EntityFrameworkCore;

namespace Spatial.API.Entities
{
    public class AppDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Field> Fields { get; set; }
        
        public AppDbContext()
        {
            Database.Migrate();
        }

        // for dependency injection (in construction)
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(@"Host=localhost;Database=postgres;Username=postgres;Password=2416",
                                    x => x.UseNetTopologySuite());
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            

            builder.Entity<Person>().ToTable("People");
            builder.Entity<Person>().HasKey(p => p.Id);
            builder.Entity<Person>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Person>().Property(p => p.Name).IsRequired().HasMaxLength(20);

            // one to many relationship creating FK // Hatali yaptım !!
            // builder.Entity<Person>()
            // .HasMany(p => p.Fields)
            // .WithOne(p => p.Person)
            // .HasForeignKey(p => p.Id);

            builder.Entity<Field>().ToTable("Field");
            builder.Entity<Field>().HasKey(f => f.Id);
            builder.Entity<Field>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Field>().Property(p => p.Name).IsRequired().HasMaxLength(20);

            
            // Seeding database
            // builder.Entity<Person>().HasData
            // (
            //     new Person { Id = 1, Name = "Burhan", Job = "Engineer", Gender = "Male", Age = 26 },
            //     new Person { Id = 2, Name = "Mehmet", Job = "Farmer", Gender = "Male", Age = 21 },
            //     new Person { Id = 3, Name = "Ayse", Job = "Baker", Gender = "Female", Age = 29 },
            //     new Person { Id = 4, Name = "Zekeriya", Job = "İssiz", Gender = "Male", Age = 54 }
            // );

            // builder.Entity<Field>().HasData
            // (
            //     new Field { Id = 1, Name = "parsel-1", PersonId = 1, Wkt = "MultiPolygon (((29.4695947 36.35849605, 29.46567075 36.37798056, 29.46567075 36.39061656, 29.47286465 36.40114498, 29.48594447 36.4079877, 29.50229425 36.41009302, 29.51537408 36.40167137, 29.5284539 36.39219591, 29.53041587 36.38377232, 29.520606 36.376" },
            //     new Field { Id = 2, Name = "parsel-2", PersonId = 1, Wkt = "MultiPolygon (((29.48267452 36.2694372, 29.49967829 36.27787317, 29.51799004 36.27734595, 29.52126 36.27681872, 29.5356478 36.2736553, 29.54022574 36.2694372, 29.53433982 36.26627348, 29.51864403 36.26680077, 29.5062182 36.26680077, 29.49379237 36.263109" },
            //     new Field { Id = 3, Name = "parsel-3", PersonId = 1, Wkt = "MultiPolygon (((29.65990611 36.26047299, 29.65271221 36.26627348, 29.65598217 36.2694372, 29.66906199 36.26996448, 29.68148782 36.27312805, 29.69391365 36.27787317, 29.70045356 36.27418254, 29.70306953 36.26785536, 29.70045356 36.26047299, 29.69718361 36" },
            //     new Field { Id = 4, Name = "parsel-4", PersonId = 2, Wkt = "MultiPolygon (((29.79920622 36.33584576, 29.78089447 36.34216743, 29.78481842 36.35270242, 29.79659026 36.36270933, 29.80443815 36.36428925, 29.81424802 36.36218268, 29.82601986 36.35270242, 29.8305978 36.34480131, 29.82798183 36.33900666, 29.82405789 36" },
            //     new Field { Id = 5, Name = "parsel-5", PersonId = 3, Wkt = "MultiPolygon (((29.87245323 36.3932488, 29.87310722 36.41114567, 29.8822631 36.4153561, 29.88880301 36.41325091, 29.89272695 36.41693495, 29.89599691 36.42009256, 29.90122884 36.4195663, 29.9038448 36.41167198, 29.90319081 36.4048296, 29.89992086 36.4027" },
            //     new Field { Id = 6, Name = "parsel-6", PersonId = 3, Wkt = "MultiPolygon (((30.03987496 36.36481589, 30.03987496 36.37218838, 30.05033882 36.38324581, 30.06341864 36.39272236, 30.06145667 36.38324581, 30.05622474 36.37060862, 30.05230079 36.36060272, 30.04968483 36.35480924, 30.04118294 36.35796937, 30.03987496 3" },
            //     new Field { Id = 7, Name = "parsel-7", PersonId = 4, Wkt = "MultiPolygon (((29.9411223 36.24306893, 29.94243028 36.25308992, 29.95616409 36.26100032, 29.96728194 36.26469157, 29.98363172 36.26627348, 29.99474957 36.26890992, 30.00259746 36.2726008, 30.01633128 36.26785536, 30.02417917 36.26574618, 30.03791298 36." }
            // );
        }

    }
}