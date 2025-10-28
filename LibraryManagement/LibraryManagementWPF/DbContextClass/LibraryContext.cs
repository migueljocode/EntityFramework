namespace LibraryTestConsole.DbContextClass
{
    internal class LibraryContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Review> Reviews { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server = (localdb)\\MSSQLLocalDB; database = Library; Integrated Security = true");
        }

        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
        }

        public LibraryContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Loan>().HasOne(l => l.User).
                                        WithMany(u => u.Loans).

                                        HasForeignKey(l => l.UserId);

            modelBuilder.Entity<Loan>().HasOne(l => l.Book).
                                        WithMany(b => b.Loans).
                                        HasForeignKey(b => b.BookId);

            modelBuilder.Entity<Review>().HasOne(r => r.User).
                                          WithMany(u => u.Reviews).
                                          HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Review>().HasOne(r => r.Book).
                                          WithMany(b => b.Reviews).
                                          HasForeignKey(r => r.BookId);
        }
    }
}
