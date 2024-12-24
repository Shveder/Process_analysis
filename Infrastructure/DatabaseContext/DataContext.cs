namespace Infrastructure.DatabaseContext;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Company> Companies { get; set; }
    
    public DbSet<LoginHistory> LoginHistories { get; set; }

    public DbSet<Notification> Notifications { get; set; }
    
    public DbSet<Process> Processes { get; set; }
    
    public DbSet<RecentPassword> RecentPasswords { get; set; }
    
    public DbSet<Subscription> Subscriptions { get; set; }
    
    public DbSet<User> Users { get; set; }
    
    public DbSet<Comment> Comments { get; set; }
    
    public DbSet<Indicator> Indicators { get; set; }
    
    public DbSet<Record> Records { get; set; }
    
    public DbSet<Access> Accesses{ get; set; }
    
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=YourDatabase;Username=YourUsername;Password=YourPassword");

            return new DataContext(optionsBuilder.Options);
        }
    }
}