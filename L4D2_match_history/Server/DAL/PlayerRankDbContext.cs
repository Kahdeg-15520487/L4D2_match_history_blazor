using L4D2_match_history.Shared;

using Microsoft.EntityFrameworkCore;

namespace L4D2_match_history.Server.DAL
{
    public class PlayerRankDbContext : DbContext
    {
        public DbSet<PlayerRank> PlayerRanks { get; set; }
        public DbSet<PlayerRankView> PlayerRankViews { get; set; }

        public DbSet<PlayerSkillModifier> PlayerSkillModifiers { get; set; }

        public DbSet<DisplayTemplate> displayTemplates { get; set; }

        public DbSet<DisplayColumn> displayColumns { get; set; }

        public PlayerRankDbContext(DbContextOptions<PlayerRankDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<PlayerRank>().ToTable("STATS_PLAYERS");
            modelBuilder.Entity<PlayerRank>().HasKey(pr => pr.steam_id);

            modelBuilder.Entity<PlayerRankView>(pr =>
            {
                pr.HasNoKey();
                pr.ToView("STATS_VW_PLAYER_RANKS");
            });
            modelBuilder.Entity<PlayerSkillModifier>().ToTable("STATS_SKILLS");

            modelBuilder.Entity<PlayerSkillModifier>().HasKey(psm => psm.name);

            modelBuilder.Entity<DisplayTemplate>().ToTable("DISPLAY_TEMPLATE");
            modelBuilder.Entity<DisplayTemplate>().HasKey(dt => dt.Name);

            modelBuilder.Entity<DisplayColumn>().ToTable("DISPLAY_COLUMN");
            modelBuilder.Entity<DisplayColumn>().Property(dc => dc.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<DisplayColumn>().HasKey(dc => dc.Id);

            modelBuilder.Entity<DisplayColumn>()
                        .HasOne(_ => _.Template)
                        .WithMany(_ => _.Columns)
                        .HasForeignKey(_ => _.TemplateName);
        }
    }
}
