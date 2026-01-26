namespace AskExperts.Infrastructure.Context
{
    public class DBContext : DbContext
    {

        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }
        public DbSet<Expert> Experts { get; set; }
		public DbSet<Experience> Experiences { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<ExpertQuestion> ExpertQuestions { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Lk_RequestStage> Lk_RequestStages { get; set; }
        public bool AllMigrationsApplied()
        {
            var applied = this.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = this.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
        public void Migrate()
        {
            this.Database.Migrate();
        }
    }
}